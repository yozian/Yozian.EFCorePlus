using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yozian.EFCorePlus.Extensions
{
    public static class PropertyBuilderExtension
    {

        public static PropertyBuilder HasEnumTransform<T>(this PropertyBuilder @this)
        {
            var converter = new ValueConverter<T, string>(
               v => v.ToString(),
               v => (T)Enum.Parse(typeof(T), v)
            );

            @this.HasConversion(converter);

            return @this;
        }

    }
}
