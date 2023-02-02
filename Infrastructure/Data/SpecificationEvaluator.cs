using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null) 
            {
                query = query.Where(spec.Criteria);
            }

            // Aggregate method explained: The first parameter "query" is essentially the "seed" when you look
            // at the Microsoft documenation. The 2nd parameter is a function. The parameter "current" within
            // the function is the value of "query". "include" param is the first element in the "Includes" list.
            // Takes the query and uses method .Include(include) --> which is the include expression in the list. 
            // Overall, applies the function to each element in the "Includes" list and adds it to the "current"
            // query expression with the query expression starting out as the "query" param.  
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;

        }
        
    }
}