using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProElection.Persistence;
using ProElection.Repositories;
using ProElection.Repositories.Interfaces;
using ProElection.Services;
using ProElection.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<ProElectionDbContext>();

builder.Services
    .AddScoped<IElectionCodeRepository, ElectionCodeRepository>()
    .AddScoped<IElectionRepository, ElectionRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IVoteRepository, VoteRepository>();

builder.Services
    .AddScoped<IElectionService, ElectionService>()
    .AddScoped<IUserService, UserService>();


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();