using Azure.Data.Tables;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Tilføj TableServiceClient til DI-containeren med brug af URI og Managed Identity
string tableStorageUri = "https://ibasbikeproduction123.table.core.windows.net"; // Udskift med din storage account URI
builder.Services.AddSingleton(new TableServiceClient(new Uri(tableStorageUri), new DefaultAzureCredential()));

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
