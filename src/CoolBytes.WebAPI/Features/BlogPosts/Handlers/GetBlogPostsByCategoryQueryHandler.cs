﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoolBytes.Core.Models;
using CoolBytes.Data;
using CoolBytes.WebAPI.Features.BlogPosts.CQ;
using CoolBytes.WebAPI.Features.BlogPosts.ViewModels;
using CoolBytes.WebAPI.Services.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolBytes.WebAPI.Features.BlogPosts.Handlers
{
    public class GetBlogPostsByCategoryQueryHandler : IRequestHandler<GetBlogPostsByCategoryQuery, IEnumerable<BlogPostSummaryViewModel>>
    {
        private readonly AppDbContext _context;
        private readonly ICacheService _cacheService;

        public GetBlogPostsByCategoryQueryHandler(AppDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<BlogPostSummaryViewModel>> Handle(GetBlogPostsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _cacheService.GetOrAddAsync(() => BlogPostsFactoryAsync(request.CategoryId));

            return Mapper.Map<IEnumerable<BlogPostSummaryViewModel>>(blogs);
        }

        private async Task<IEnumerable<BlogPost>> BlogPostsFactoryAsync(int categoryId) 
            => await _context.BlogPosts.Where(b => b.CategoryId == categoryId).ToListAsync();
    }
}