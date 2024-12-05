using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_api_c_sharp.DTOs.Comment
{
    public class UpdateCommentRequest
    {
        public string Title { get; set;} = string.Empty;
        public string Content { get; set;} = string.Empty;
    }
}