/***
modelBuilder
    .Entity<Post>()
    .HasData(new Post { PostId = 1, Title = "First" });

modelBuilder
    .Entity<Tag>()
    .HasData(new Tag { TagId = "ef" });

modelBuilder
    .Entity<Post>()
    .HasMany(p => p.Tags)
    .WithMany(p => p.Posts)
    .UsingEntity(j => j.HasData(new { PostsPostId = 1, TagsTagId = "ef" }));

Indirect many-to-many relationships
You can also represent a many-to-many relationship by just adding the join entity type and mapping two separate one-to-many relationships.
**/

using System;
using System.Linq;
using System.Collections.Generic;  
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DotNetCoreAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Book
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Display(Name = "Number")]
        [Key] 
        public int Key { get; set; }
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

     public DateTime? PublishDate { get; set; }

    [NotMapped]
    public ICollection<BookCategory> BookCategories { get; set; }
}  
public class Category
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Display(Name = "Number")]
        [Key] 
        public int Key { get; set; } 
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public ICollection<BookCategory> BookCategories { get; set; }
}  
public class BookCategory
{
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Display(Name = "Number")]
        [Key] 
        public int Key { get; set; }
     //    [Required]
    public int BookId { get; set; }
    [NotMapped]
    public Book Book { get; set; }
    // [Required]
    public int CategoryId { get; set; }
    [NotMapped]
    public Category Category { get; set; }
}

public class BookCategoryContext : DbContext
{
          private BookCategoryContext _prodContext;
        public BookCategoryContext(DbContextOptions<BookCategoryContext> options)
            : base(options)
        {  
            // Configuration.ProxyCreationEnabled = true;
             //Configuration.LazyLoadingEnabled = true;   
           // _prodContext.Currency.Attach(_prod);
           // _prodContext.Currency.Attach(_pInit[0]);  //MyDBContext.MyEntity.Add(mynewObject)
        }
/* 
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; } */
    public DbSet<BookCategory> BookCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookCategory>()
            .HasKey(bc => new { bc.BookId, bc.CategoryId });  
        modelBuilder.Entity<BookCategory>()
            .HasOne(bc => bc.Book)
            .WithMany(b => b.BookCategories)
            .HasForeignKey(bc => bc.BookId);  
        modelBuilder.Entity<BookCategory>()
            .HasOne(bc => bc.Category)
            .WithMany(c => c.BookCategories)
            .HasForeignKey(bc => bc.CategoryId);
    }
/* 
       public async Task<Book>  Add(Book Books){
            var result = await _prodContext.Books.AddAsync(Books);
            await _prodContext.SaveChangesAsync();
            return result.Entity;
        } 

        public async Task<Category>  Add(Category Category){
            var result = await _prodContext.Categories.AddAsync(Category);
            await _prodContext.SaveChangesAsync();
            return result.Entity;
        } */

}




/* 
public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostTag>()
            .HasKey(t => new { t.PostId, t.TagId });

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagId);
    }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public List<PostTag> PostTags { get; set; }
}

public class Tag
{
    public string TagId { get; set; }

    public List<PostTag> PostTags { get; set; }
}

public class PostTag
{
    public DateTime PublicationDate { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }

    public string TagId { get; set; }
    public Tag Tag { get; set; }
} */