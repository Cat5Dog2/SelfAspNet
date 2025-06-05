using System;
using Microsoft.EntityFrameworkCore;

namespace SelfAspNet.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }
    public DbSet<Book> Books { get; set; }
    public DbSet<Meta> Metas { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new TimestampInterceptor());
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Meta>(e =>
            {
                e.HasData(
                    new Meta
                    {
                        Id = 1,
                        Controller = "Home",
                        Action = "Privacy",
                        Name = "keywords",
                        Content = "メタ情報"
                    },
                    new Meta
                    {
                        Id = 2,
                        Controller = "Home",
                        Action = "Privacy",
                        Name = "description",
                        Content = "ページごとに異なる<meta>要素を生成"
                    },
                    new Meta
                    {
                        Id = 3,
                        Controller = "Tag",
                        Action = "Index",
                        Name = "description",
                        Content = "メタ情報取得の別解"
                    }
                );
            });
    }
}
