using Microsoft.EntityFrameworkCore;
using MyStore.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<MyDatabase>();
builder.Services.AddDbContext<AppDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultValue")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
