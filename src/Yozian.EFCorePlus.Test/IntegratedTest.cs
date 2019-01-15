using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Yozian.EFCorePlus.Constants;
using Yozian.EFCorePlus.Extensions;
using Yozian.EFCorePlus.ShemaDescription;
using Yozian.EFCorePlus.Test.Data;
using Yozian.EFCorePlus.Test.Data.Entities;
using Yozian.Extension;

namespace Tests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var dbContext = new SouthSeaDbContext();

            var books = Enumerable.Range(1, 10)
                .Select(x => new Book()
                {
                    Name = x.ToString(),
                    Author = x.ToString("d3")
                });


            dbContext.AddRange(books);

            dbContext.SaveChanges();
        }

        [Test]
        public void Test_PartialColumnUpdate()
        {
            var dbContext = new SouthSeaDbContext();
            var book = dbContext.Books.Find(1);

            var originalName = book.Name;
            var targetName = "newName";

            book.Name = targetName;
            book.Author = "yozian";

            dbContext.Update(
                book,
                x => x.Name
                );

            dbContext.SaveChanges();

            var updatedBook = dbContext
                .Books
                .Where(x => x.Id == 1)
                .AsNoTracking()
                .First();


            Assert.AreEqual(targetName, updatedBook.Name);
            Assert.AreEqual(book.Author, updatedBook.Author);
        }

        [Test()]
        public void Test_ShouldGenerateSqlSchemaDescriptionScripts()
        {
            // inmemory db dosen't support to perform the scripts
            var dbContext = new SouthSeaDbContext();

            var schemaDescriptionScripts = dbContext
                .Model
                .GenerateInsertOrUpdateDescriptionScripts(SqlType.SqlServer);


            schemaDescriptionScripts.ForEach(script =>
            {
                // excute command to database


            });


            Assert.AreEqual(1, schemaDescriptionScripts.Count());

        }
    }
}