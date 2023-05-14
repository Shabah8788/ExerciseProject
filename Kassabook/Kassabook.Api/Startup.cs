using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kassabook.BL.Contract.Account;
using Kassabook.BL.Contract.Transcation;
using Kassabook.BL.Repositories.Account;
using Kassabook.BL.Repositories.Transcation;
using Kassabook.DL;
using Kassabook.Service.Contracts.Account;
using Kassabook.Service.Contracts.Transaction;
using Kassabook.Service.Services.Account;
using Kassabook.Service.Services.Transaction;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Kassabook.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            services.AddDbContext<KassabookDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("Kassabook.DL")));

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITranscationRepository, TranscationRepository>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransactionService, TransactionService>();

            services.AddControllers();
            services.AddSwaggerGen();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error");
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<KassabookDbContext>();

                dataContext.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            //app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

