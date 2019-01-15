using System;
using System.Collections.Generic;
using System.Text;
using Yozian.EFCorePlus.ShemaDescription.Models;
using Yozian.Extension;

namespace Yozian.EFCorePlus.ShemaDescription.Generators
{
    internal class SqlDescriptionGenerator : IDescriptionGenerator
    {
        public string GenerateInserOrUpdateScript(TableSchema tableSchema)
        {

            var sb = new StringBuilder();

            var arguments = $@"
                            -- Table ${tableSchema.Name}
                            @name=N'MS_Description', @value=N'{tableSchema.Description}' ,
                            @level0type=N'SCHEMA',@level0name=N'dbo',
                            @level1type=N'TABLE',@level1name=N'{tableSchema.Name}'";

            var cmd = $@"IF NOT EXISTS(SELECT * FROM ::fn_listextendedproperty (NULL, N'schema', N'dbo', N'table', '{tableSchema.Name}', NULL, NULL))
                                BEGIN
                                    exec sp_addextendedproperty  {arguments.Replace("N'Schema'", "N'User'")};
                                END
                            ELSE
                                BEGIN
                                    exec sp_updateextendedproperty  {arguments};
                                END";


            sb.AppendLine(cmd);

            tableSchema.Columns
                .ForEach(column =>
                {

                    string colArguments =
                          $@"@name=N'MS_Description', @value=N'{column.Description}' ,
                                    @level0type=N'SCHEMA',@level0name=N'dbo',
                                    @level1type=N'TABLE',@level1name=N'{tableSchema.Name}',
                                    @level2type=N'COLUMN',@level2name=N'{column.Name}';";

                    var colCmd = $@"IF NOT EXISTS(SELECT * FROM ::fn_listextendedproperty (NULL, 'schema', 'dbo', 'table', '{tableSchema.Name}', 'column', '{column.Name}')) 
                                        BEGIN 
                                            exec sp_addextendedproperty {colArguments};
                                        END 
                                    ELSE 
                                        BEGIN 
                                            exec sp_updateextendedproperty {colArguments};
                                        END";


                    sb.AppendLine(colCmd);
                });

            return sb.ToString();
        }
    }
}
