using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Yozian.EFCorePlus.Constants;
using Yozian.EFCorePlus.ShemaDescription.Attributes;
using Yozian.EFCorePlus.ShemaDescription.Generators;
using Yozian.EFCorePlus.ShemaDescription.Models;
using Yozian.Extension;

namespace Yozian.EFCorePlus.ShemaDescription
{
    public static class IModelExtension
    {

        public static IEnumerable<IEntityType> GetEntityTypesByBaseType(
            this IModel @this,
            Type baseType
            )
        {

            return @this
               .GetEntityTypes()
               .Where(t => baseType.IsAssignableFrom(t.ClrType));
        }


        public static IEnumerable<TableSchema> GetTableSchemas(
            this IModel @this,
            Func<IEntityType, bool> entityFilter = null
            )
        {

            var schemaList = @this.GetEntityTypes()
                .AsQueryable()
                .WhereWhen(null != entityFilter, e => entityFilter(e))
                .AsEnumerable()
                .Select(entity =>
                {
                    // Get Table Name
                    var tableName = entity.Relational().TableName;

                    // table description
                    var tableDescriptAttr = getDescriptionAttr(entity.ClrType);

                    //var tableDescriptAttr = Attribute.GetCustomAttribute(entity.ClrType, typeof(SchemaDescription)) as SchemaDescription;
                    var tableDescription = tableDescriptAttr?.Description ?? "";

                    var columnList = entity.GetProperties()
                      .Select(property =>
                      {
                          var columnName = property.Relational().ColumnName;

                          // Get Description
                          var columnDescriptionAttr = getDescriptionAttr(property.PropertyInfo);

                          var description = columnDescriptionAttr?.Description ?? string.Empty;

                          return new ColumnSchema
                          {
                              Name = columnName,
                              Description = description
                          };

                      })
                      .ToList();

                    return new TableSchema()
                    {
                        Name = tableName,
                        Description = tableDescription,
                        Columns = columnList
                    };

                })
                .ToList();

            return schemaList;
        }


        /// <summary>
        /// only works for MSSQL 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<string> GenerateInsertOrUpdateDescriptionScripts(
                this IModel @this,
                SqlType sqlType,
                Func<IEntityType, bool> entityFilter = null
            )
        {

            var schemaList = GetTableSchemas(@this, entityFilter);

            // executing 
            IDescriptionGenerator descriptionGenerator = null;

            switch (sqlType)
            {
                case SqlType.SqlServer:
                    descriptionGenerator = new SqlDescriptionGenerator();
                    break;
                default:
                    throw new NotImplementedException($"${sqlType.ToString()} is NOT supported yet!");
            }

            return schemaList
                 .Select(descriptionGenerator.GenerateInserOrUpdateScript)
                 .AsEnumerable();
        }


        private static IDescriptionAttribute getDescriptionAttr(Type type)
        {
            return Attribute.GetCustomAttributes(type)
                     .Where(attr => typeof(IDescriptionAttribute).IsAssignableFrom(attr.GetType()))
                     .FirstOrDefault() as IDescriptionAttribute;
        }


        private static IDescriptionAttribute getDescriptionAttr(MemberInfo mi)
        {
            return Attribute.GetCustomAttributes(mi)
                      .Where(attr => typeof(IDescriptionAttribute).IsAssignableFrom(attr.GetType()))
                      .FirstOrDefault() as IDescriptionAttribute;
        }

    }
}
