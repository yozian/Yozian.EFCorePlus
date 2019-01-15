using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Yozian.EFCorePlus.Constants;
using Yozian.Extension;

namespace Yozian.EFCorePlus.Extensions
{
    public static class DbContextExtension
    {
        /// <summary>
        /// update entity with specified properties
        /// Make sure that the entity are not from query but new model from yourself
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="@this"></param>
        /// <param name="entity"></param>
        /// <param name="updateProperties"></param>
        public static EntityEntry<TEntity> Update<TEntity>(
            this DbContext @this,
            TEntity entity,
            params Expression<Func<TEntity, object>>[] updateProperties)
            where TEntity : class, new()
        {
            return updateEntityByProperties(@this, entity, updateProperties);
        }


        private static EntityEntry<TEntity> updateEntityByProperties<TEntity>(
           this DbContext @this,
           TEntity entity,
           IEnumerable<Expression<Func<TEntity, object>>> updateProperties)
           where TEntity : class, new()
        {
            var entry = @this.Attach(entity);

            if (updateProperties == null)
            {
                return entry;
            }

            // reset status
            entry.State = EntityState.Unchanged;

            // mark the properties status
            updateProperties.ForEach(expression =>
            {
                var member = expression.GetMemberName();
                entry.Property(member).IsModified = true;
            });

            return entry;
        }

    }



}
