using Azure.Data.Tables;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Get Table Storage URI from appsettings.json
string tableStorageUri = builder.Configuration["Storage:TableEndpoint"];

if (!string.IsNullOrEmpty(tableStorageUri))
{
    builder.Services.AddSingleton(new TableServiceClient(new Uri(tableStorageUri), new DefaultAzureCredential()));
}
else
{
    throw new InvalidOperationException("Table Storage URI is not configured. Please set the Storage:TableEndpoint in appsettings.json.");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
