using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
var physicalFileProvider = new PhysicalFileProvider(
    Path.Combine(builder.Environment.ContentRootPath, "Scripts/app"));
var requestPath = "";

app.UseFileServer(new FileServerOptions  
{  
    FileProvider = physicalFileProvider,  
    RequestPath = requestPath,  
    EnableDefaultFiles = true  
}); 
// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = physicalFileProvider,
//     RequestPath = requestPath,
//     
// });
// app.UseDirectoryBrowser(new DirectoryBrowserOptions
// {
//     FileProvider = physicalFileProvider,
//     RequestPath = requestPath
// });

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
