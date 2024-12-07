using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_api_c_sharp.DTOs.Comment;
using learn_api_c_sharp.Extensions;
using learn_api_c_sharp.Interfaces;
using learn_api_c_sharp.Mappers;
using learn_api_c_sharp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace learn_api_c_sharp.Controllers
{
    [Route("api/comment")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(
            ICommentRepository commentRepository, IStockRepository stockRepository,UserManager<AppUser> userManager
            )
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool IsExist = await _stockRepository.IsExist(stockId);
            if (!IsExist) return BadRequest("Stock does not exist");

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            
            var commentModel = request.ToCommentFromCreate(stockId);
            commentModel.AppUserId = appUser.Id;
            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id}, commentModel.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var comment = await _commentRepository.UpdateAsync(id, request.ToCommentFromUpdate());
            if (comment == null) return NotFound("Comment not found");
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteByIdAsync(id);
            if (comment == null) return NotFound("comment not found");
            return NoContent();
        }
    }
}