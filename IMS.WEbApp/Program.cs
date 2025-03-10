using IMS.UseCases.PluginInterfaces;
using IMS.Plugins.InMemory;
using IMS.UseCases.Activities;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.Inventories;
using IMS.UseCases.Products;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.Products.Interfaces;
using IMS.WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Check if running in production (published) and set the correct WebRootPath
if (!builder.Environment.IsDevelopment())
{
    // Ensures the app serves static files from the correct location when published
    builder.WebHost.UseWebRoot(Path.Combine(AppContext.BaseDirectory, "wwwroot"));
}

// Explicitly configure Kestrel to listen on all network interfaces for Folder publish
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(51000); // HTTP
    serverOptions.ListenAnyIP(51001, listenOptions => listenOptions.UseHttps()); // HTTPS
});

#region Add services to the container
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// Register InventoryRepository as a singleton service (one instance for the entire app lifetime)
builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();
builder.Services.AddSingleton<IProductTransactionRepository, ProductTransactionRepository>();


#region Register transient services (one instance per request)
// Inventory Use Cases
builder.Services.AddTransient<IViewInventoriesByName, ViewInventoriesByName>();
builder.Services.AddTransient<IViewInventoryByIdUseCase, ViewProducsByIdUseCase>();
builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
builder.Services.AddTransient<IDeleteInventoryUseCase, DeleteInventoryUseCase>();
// Product Use Cases
builder.Services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
builder.Services.AddTransient<IDeleteProductByIdUseCase, DeleteProductsByIdUseCase>();
builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
builder.Services.AddTransient<IViewProductByIdUseCase, ViewProductByIdUseCase>();
// Activity Use Cases
builder.Services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();
builder.Services.AddTransient<IProduceProductUseCase, ProduceProductUseCase>();
builder.Services.AddTransient<ISellProductUseCase, SellProductUseCase>();
#endregion
#endregion Add services to the container

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


#region Middleware
// Ensure Static Files (CSS, Bootstrap, JS) Load Correctly in both Debug and Published mode
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
#endregion

app.Run();
