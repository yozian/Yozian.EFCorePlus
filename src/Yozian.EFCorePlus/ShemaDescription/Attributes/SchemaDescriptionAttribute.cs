using System;
using System.Collections.Generic;
using System.Text;

namespace Yozian.EFCorePlus.ShemaDescription.Attributes
{
    public class SchemaDescriptionAttribute : Attribute, IDescriptionAttribute
    {
        public string Description { get; private set; }

        public SchemaDescriptionAttribute(string description)
        {
            this.Description = description;
        }
    }
}
