using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Yozian.EFCorePlus.ShemaDescription;
using Yozian.Extension;

namespace Yozian.EFCorePlus.Extensions
{
    public static class ModelBuilderExtension
    {

        /// <summary>
        /// only set without defined properties
        /// effect on inherite of baseType 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static ModelBuilder SetDefaultStringMaxLengthWithBaseType<T>(this ModelBuilder @this, int maxLength)
        {
            @this.Model
               .GetEntityTypesByBaseType(typeof(T))
               .Select(e => (IMutableEntityType)e)
               .SelectMany(t => t.GetProperties())
               .Where(p => p.ClrType == typeof(string) && p.GetMaxLength() == null)
               .ForEach(p => p.SetMaxLength(maxLength));

            return @this;
        }

    }
}
