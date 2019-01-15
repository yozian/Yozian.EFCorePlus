# Features

* Update entity without fetch and can specify columns to be updated.
* Provide schema description & extension method to generate update scheam desciription sql scripts


# Partial field update example


```csharp

    var book = new Book()
            {
                Id = 1 // it should exits Id = 1 in the database
            };

    book.Name = "The Phoenix Project";
    book.Author = "fake-yozian";

    dbContext.Update(
        book,
        x => x.Name
        );

    // when the SaveChanges called, only the [Name] column will be updated.
    dbContext.SaveChanges();

```


# Generate Schema description, Extension method of IModel

```csharp

        // it's recommanded doing after migration or data seed.

        var schemaDescriptionScripts = dbContext
            .Model
            .GenerateInsertOrUpdateDescriptionScripts(SqlType.SqlServer);
        
        // if the command won't exceed the maximun query line limits, you could join the scritps and execute once.
        schemaDescriptionScripts.ForEach(script =>
        {
            // excute command to database
            dbContext.Database.ExecuteCommand(script);

        });


```


### feel free for pull request

