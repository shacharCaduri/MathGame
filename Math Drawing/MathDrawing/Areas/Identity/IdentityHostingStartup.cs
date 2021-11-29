using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MathDrawing.Areas.Identity.IdentityHostingStartup))]
namespace MathDrawing.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}