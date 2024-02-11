using centrala.Models;
using centrala.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHostedService<DataService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run("http://0.0.0.0:7878");

public static class GlobalValues
{
    public static int dataServiceTimeSpanSeconds = 120;
    public static string[] dataIDs { get; } = { "timestamp", "pbattery1", "house_consumption", "ppv1", "ppv2", "ppv", "battery_soc" };
    public static Sensor[] latestSensors = new Sensor[145];
}
