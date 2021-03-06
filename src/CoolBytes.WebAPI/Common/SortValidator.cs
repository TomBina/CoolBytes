﻿using CoolBytes.Core.Abstractions;
using CoolBytes.Core.Utils;
using CoolBytes.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoolBytes.WebAPI.Common
{
    public class SortValidator<T> where T : class, IEntity
    {
        private readonly AppDbContext _context;

        public SortValidator(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Validate(List<int> newSortOrder, Expression<Func<T, bool>> whereExpression = null)
        {
            var newSortOrderCount = newSortOrder.Count;
            var allUnique = newSortOrderCount == newSortOrder.Distinct().Count();

            if (!allUnique)
            {
                return Result.ErrorResult(Resources.Common.SortValidator.IdsMustBeUniqueError);
            }

            var ids = whereExpression == null 
                ? (await _context.Set<T>().Select(c => c.Id).ToListAsync()).ToHashSet() 
                : (await _context.Set<T>().Where(whereExpression).Select(c => c.Id).ToListAsync()).ToHashSet();

            if (newSortOrder.Count != ids.Count)
            {
                return Result.ErrorResult(Resources.Common.SortValidator.SortOrderLengthError);
            }

            var allExist = newSortOrder.All(i => ids.Contains(i));

            if (!allExist)
            {
                return Result.ErrorResult(Resources.Common.SortValidator.AllIdsMustExistError);
            }

            return Result.SuccessResult();
        }
    }
}