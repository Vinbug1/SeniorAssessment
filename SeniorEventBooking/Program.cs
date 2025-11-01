using SeniorEventBooking.Services; // 👈 make sure namespace matches your project

var builder = WebApplication.CreateBuilder(args);

// ✅ Register your custom services BEFORE building Umbraco
builder.Services.AddSingleton<MemberbaseService>();

// ✅ Build Umbraco
builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

var app = builder.Build();

// ✅ Boot Umbraco
await app.BootUmbracoAsync();

// ✅ Configure the Umbraco request pipeline
app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();

        // 👇 Optional: Map your own custom controller routes (like /booking)
        // u.EndpointRouteBuilder.MapControllerRoute(
        //     name: "booking",
        //     pattern: "booking/{action=Index}/{id?}",
        //     defaults: new { controller = "Booking" });
    });

await app.RunAsync();






// WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// builder.CreateUmbracoBuilder()
//     .AddBackOffice()
//     .AddWebsite()
//     .AddDeliveryApi()
//     .AddComposers()
//     .Build();

// WebApplication app = builder.Build();

// await app.BootUmbracoAsync();


// app.UseUmbraco()
//     .WithMiddleware(u =>
//     {
//         u.UseBackOffice();
//         u.UseWebsite();
//     })
//     .WithEndpoints(u =>
//     {
//         u.UseInstallerEndpoints();
//         u.UseBackOfficeEndpoints();
//         u.UseWebsiteEndpoints();
//     });

// await app.RunAsync();
