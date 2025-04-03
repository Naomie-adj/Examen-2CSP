using Microsoft.EntityFrameworkCore;
using BookApi.Models;

namespace BookApi.Data;

public class BookDb : DbContext
{
    public BookDb(DbContextOptions<BookDb> options) : base(options) { }

    public DbSet<Book> Books { get; set; } = null!;

    // TODO: Ajouter le DbSet pour les livres
}