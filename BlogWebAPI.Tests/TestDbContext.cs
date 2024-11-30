using BlogWebAPI.BlogAPI.DataContext;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI.Tests;

public static class TestDbContext
{
    public static BlogDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new BlogDbContext(options);
    }
}
