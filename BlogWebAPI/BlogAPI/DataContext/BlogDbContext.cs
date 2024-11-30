using BlogWebAPI.BlogAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogWebAPI.BlogAPI.DataContext;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    public DbSet<BlogPost> BlogPosts { get; set; }
}

