using System;
using System.Collections.Generic;
using System.Text;
using Yozian.EFCorePlus.ShemaDescription.Models;

namespace Yozian.EFCorePlus.ShemaDescription.Generators
{
    internal interface IDescriptionGenerator
    {
        string GenerateInserOrUpdateScript(TableSchema tableSchema);
    }
}
