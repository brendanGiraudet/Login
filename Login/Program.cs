using Login.Extensions;
using Login.Services;
using Login.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add options
builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGrid"));

// Add service
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ILogger, Logger>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication();

// Add Identity server configuration, context etc
builder.Services.AddIdentityServerConfiguration(builder.Configuration);

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
app.UseAuthentication();

app.MapRazorPages();

app.UseIdentityServer();

app.Run();