
using Contoso.FoodDelivery.Api;

var builder = WebApplication.CreateBuilder(args);

// Setup services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = builder.Environment.ApplicationName, Version = "v1" });
});

builder.Services.AddRestaurants(builder.Configuration);


var app = builder.Build();

await app.SetupRestaurantsAsync();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapSwagger();
    app.UseSwaggerUI();

}

app.UseRestaurants();


app.Run();
