using System.Text.Json.Serialization;
using ClienteTACOSWeb.Negocio;
using Grpc.Core;
using IMG;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
                .AddJsonOptions(x =>
                     x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve); 


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IMenuMgt, MenuMgr>();
builder.Services.AddSingleton<IConsultanteMgt, ConsultanteMgr>();
builder.Services.AddSingleton<IConsultanteMgt, ConsultanteMgr>();
builder.Services.AddSingleton<Sesion>();
builder.Services.AddHttpClient("tacos", client => 
{
    client.BaseAddress = new Uri("http://localhost:5174/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});



builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseSession();

app.MapRazorPages();

app.Run();


Channel channel =
                new Channel("localhost", 7252,
                            ChannelCredentials.Insecure);
await channel.ConnectAsync().ContinueWith((task) =>
{

    if (task.Status == TaskStatus.RanToCompletion)

        Console.WriteLine("Cliente conectado satisfactoriamente");

});

channel.ShutdownAsync().Wait();

Console.ReadKey();
