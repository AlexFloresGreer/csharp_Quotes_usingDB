using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using DbConnection;

namespace Quotes
{
    public class Program
    {
          public static void Create()
          {
              //nothing calls this... we'll let it hang here until we use it.
          }

        public static void Main(string[] args)
        {  

            IWebHost host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
            host.Run();                 
        }
    }
}
