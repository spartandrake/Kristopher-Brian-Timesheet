using System;
using K2BrianMikTimeClockService.Areas.Identity.Data;
using K2BrianMikTimeClockService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(K2BrianMikTimeClockService.Areas.Identity.IdentityHostingStartup))]
namespace K2BrianMikTimeClockService.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<K2BrianMikTimeClockServiceContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("K2BrianMikTimeClockServiceContextConnection")));

                services.AddDefaultIdentity<K2BrianMikTimeClockServiceUser>()
                    .AddEntityFrameworkStores<K2BrianMikTimeClockServiceContext>();
            });
        }
    }
}