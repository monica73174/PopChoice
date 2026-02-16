using Microsoft.EntityFrameworkCore;
using PopChoice.Data;
using PopChoice.Services;
using PopChoice.Services.Ai;
using PopChoice.Services.Recommendations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IAiEmbeddingService, OpenAiEmbeddingService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=popchoice.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var embeddingService = scope.ServiceProvider.GetRequiredService<IAiEmbeddingService>();

    await DbInitializer.SeedEmbeddingsAsync(context, embeddingService);
}

app.Run();
