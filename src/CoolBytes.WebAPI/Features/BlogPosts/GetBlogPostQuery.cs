﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MediatR;

namespace CoolBytes.WebAPI.Features.BlogPosts
{
    public class GetBlogPostQuery : IRequest<BlogPostViewModel>
    {
        [Required]
        public int Id { get; set; }
    }
}