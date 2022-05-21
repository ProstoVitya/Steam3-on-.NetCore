using Microsoft.EntityFrameworkCore;
using Steam3.Services;
using Steam3.Services.Interfaces;
using Steam3.Services.MockRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    //Find how to get connection string from name
    options.UseSqlServer("server = (localdb)\\MSSqlLocalDB; database = Steam3DB; Integrated Security = true");
});
builder.Services.AddSingleton<IGameRepository, MockGameRepository>();
builder.Services.AddSingleton<IClientRepository, MockClientRepository>();
builder.Services.AddSingleton<IAvalibleGameRepository, MockAvalibleGameRepository>();
builder.Services.AddSingleton<IAdminRepository, MockAdminRepository>();
builder.Services.AddSingleton<ICreditCardRepository, MockCreditCardRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();