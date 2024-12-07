using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_api_c_sharp.Data;
using learn_api_c_sharp.Interfaces;
using learn_api_c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace learn_api_c_sharp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return null;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var editedComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (editedComment == null) return null;
            editedComment.Title = comment.Title;
            editedComment.Content = comment.Content;
            await _context.SaveChangesAsync();
            return editedComment;
        }
    }
}