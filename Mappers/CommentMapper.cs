using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_api_c_sharp.DTOs.Comment;
using learn_api_c_sharp.Models;

namespace learn_api_c_sharp.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                StockId = comment.StockId,
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentRequest request, int stockId)
        {
            return new Comment
            {
                Title = request.Title,
                Content = request.Content,
                StockId = stockId,
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequest request)
        {
            return new Comment
            {
                Title = request.Title,
                Content = request.Content
            };
        }
    }
}