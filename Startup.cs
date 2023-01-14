using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Application
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            services.AddDbContext<DatabaseContext>(opts =>
      opts.UseInMemoryDatabase("database"));
            services.AddSingleton<DatabaseContext>();
        }
    }
}

