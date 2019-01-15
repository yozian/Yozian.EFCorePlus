using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yozian.EFCorePlus.ShemaDescription.Attributes;

namespace Yozian.EFCorePlus.Test.Data.Entities
{
    public class Book
    {

        [Key]
        public int Id { get; set; }

        [SchemaDescription("BookName")]
        public string Name { get; set; }


        [SchemaDescription("BookAuthor")]
        public string Author { get; set; }
    }
}
