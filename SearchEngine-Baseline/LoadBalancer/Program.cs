using LoadBalancer.Controllers;
using LoadBalancer.Interfaces;
using LoadBalancer.Strategies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ILoadBalancerStrategy, RoundRobinStrategy>();
builder.Services.AddSingleton<ILoadBalancer, RoundRobinLoadBalancer>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "LoadBalancerAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoadBalancerAPI v1");
    c.RoutePrefix = string.Empty;
});

app.UseCors(config => config.AllowAnyOrigin());

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Load Balancer Middleware
app.Use((Func<HttpContext, Func<Task>, Task>)(async (context, next) =>
{
    // Get the active load balancer strategy from the container
    var loadBalancer = context.RequestServices.GetRequiredService<ILoadBalancer>();

    // Get the next service in the load balancer using the active strategy
    var targetService = loadBalancer.NextService();

    // Create a new request message with the original request's details
    var targetRequest = new HttpRequestMessage(new HttpMethod(context.Request.Method), targetService + context.Request.Path + context.Request.QueryString);

    // Forward headers from the original request to the target request
    foreach (var header in context.Request.Headers)
    {
        targetRequest.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
    }

    // Forward the request to the target service
    using var responseMessage = await context
        .RequestServices
        .GetRequiredService<IHttpClientFactory>()
        .CreateClient()
        .SendAsync(targetRequest, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);

    // Copy the target response's headers to the original response
    foreach (var header in responseMessage.Headers)
    {
        context.Response.Headers[header.Key] = header.Value.ToArray();
    }

    // Set the original response's status code to match the target response's status code
    context.Response.StatusCode = (int)responseMessage.StatusCode;

    // Copy the target response's content to the original response
    await responseMessage.Content.CopyToAsync(context.Response.Body);
}));

app.Run();
