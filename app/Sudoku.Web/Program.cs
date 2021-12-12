using Sudoku.Common;
using Sudoku.ExceptionHandling;
using Sudoku.Game;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IGame, GameBoard>();
builder.Services.AddScoped<ISolver>(i => { return new Solver(i.GetRequiredService<IGame>()); });
builder.Services.AddScoped<IGenerator>(i => { return new Generator(i.GetRequiredService<IGame>()); });
builder.Services.AddScoped<IGameService>(i => { return new GameService(i.GetRequiredService<IGame>(), i.GetRequiredService<IGenerator>(), i.GetRequiredService<ISolver>()); });

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseExceptionMiddleware();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sudoku}/{action=Game}/{id?}");

app.Run();

public partial class Program { }