using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connect to Postgres Database.
var connectionString = builder.Configuration.GetConnectionString("DefaulConnection");

// Add services to the container.
builder.Services.AddDbContext<DinnerPokerDb>(options => options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/", () => "Dinner Poker API - ASP.NET Minimal - Npgsql Entity Framework");

// Get Cards
app.MapGet("/cards/", async (DinnerPokerDb db) => await db.Cards.ToListAsync());

// Get decks
app.MapGet("/decks/", async (DinnerPokerDb db) => await db.Decks.ToListAsync());

class Card
{
    public int Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public int DeckId { get; set; } = default!;
    public int SuitId { get; set; } = default!;
    public bool IsVegetarian { get; set; } = default!;
}
class Deck
{
    public int Id { get; set; } = default!;
    public string Title { get; set; } = default!;
}
class Suit
{
    public int Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

}

class DinnerPokerDb : DbContext
{
    public DinnerPokerDb(DbContextOptions<DinnerPokerDb> options) : base(options){}
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<Deck> Decks => Set<Deck>();
    public DbSet<Suit> Suits => Set<Suit>();
}
