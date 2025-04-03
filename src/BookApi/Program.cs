using BookApi.Data;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookDb>(opt => opt.UseInMemoryDatabase("BookList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/", () => "Hello this a bookshop!");

app.MapGet("/books", async (BookDb db) => {
    await db.Books.ToListAsync();
});

app.MapPost("/books", async (Book book, BookDb db) => {
    db.Book.Add(book);
    await db.SaveChangesAsync();
    return Results.Created($"/books/{book.Id}", book)
});

app.MapPut("/books/{id}", async (int id, BookDb db) => {
    var existing = db.Book.Find(id);
    if (existing is null) return Results.NotFound();

    var updatedbook = existing with {IsDone = updatedbook.IsDone };
    db.Entry(existing).CurrentValues.SetValues(updatedbook);
    db.SveChanges();
    return Results.NoContent();

});

app.Run();
