using App.Domain.AppServices.YoutubeVideos.AppServices;
using App.Domain.Core.YoutubeVideos.AppServices;
using App.Domain.Core.YoutubeVideos.Data.Repositories;
using App.Domain.Core.YoutubeVideos.Services;
using App.Domain.Services.YoutubeVideos.Services;
using App.Infra.Data.Repos.ApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<HttpRequestService>();
builder.Services.AddScoped<IYoutubeVideoAppService, YoutubeVideoAppService>();
builder.Services.AddScoped<IYoutubeVideoService, YoutubeVideoService>();

builder.Services.AddScoped<IYoutubeVideoDetailsAPI, YoutubeVideoDetailsAPI>();
builder.Services.AddScoped<IYoutubeVideoTranscriptAPI, YoutubeVideoTranscriptAPI>();
builder.Services.AddScoped<ITranscriptTranslateAPI, TranscriptTranslateAPI>();


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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
