using Test.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Регистрация HttpClient для Wildberries API
builder.Services.AddHttpClient<IWildberriesService, WildberriesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Redirect root to Orders page since Index was removed
app.MapGet("/", () => Results.Redirect("/Orders"));

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
