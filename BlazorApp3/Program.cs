using BlazorApp3.Components;
using Microsoft.EntityFrameworkCore; 
using BlazorApp3.Data;
using BlazorApp3.Services;


var builder = WebApplication.CreateBuilder(args);
//Services
builder.Services.AddScoped<UserService>();// Rename later, user services
builder.Services.AddScoped<UserSession>(); // User session service, available across all pages
builder.Services.AddScoped<TopicService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<TestResultService>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//Database setup
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));
//Builds the web app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();