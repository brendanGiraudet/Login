using System.Reflection;
using Login.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("Login");
var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

// configure identity server with in-memory stores, keys, clients and scopes
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    // this adds the config data from DB (clients, resources)
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = builder =>
            builder.UseSqlite(connectionString,
                sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    // this adds the operational data from DB (codes, tokens, consents)
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = builder =>
            builder.UseSqlite(connectionString,
                sql => sql.MigrationsAssembly(migrationsAssembly));

        // this enables automatic token cleanup. this is optional.
        options.EnableTokenCleanup = true;
        options.TokenCleanupInterval = 30;
    });

var app = builder.Build();

// Initialize database : apply migrations
app.InitialiazeDatabase();

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

app.UseIdentityServer();

app.Run();
