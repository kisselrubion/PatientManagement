using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PatientManagementApi.Middlewares;
using PatientManagementApi.Services.AccountServices;
using PatientManagementApi.Services.CourseHistoryService;
using PatientManagementApi.Services.CourseServices;
using PatientManagementApi.Services.DoctorServices;
using PatientManagementApi.Services.MeasurementServices;
using PatientManagementApi.Services.MedicineServices;
using PatientManagementApi.Services.NurseServices;
using PatientManagementApi.Services.PatientServices;
using PatientManagementApi.Services.ProcedureServices;
using PatientManagementApi.Services.TreatmentServices;
using PatientManagementApi.Services.UserServices;
using PatientManagementBackend.Model;

namespace PatientManagementApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			//For IIS deployment
			services.Configure<IISServerOptions>(options =>
			{
				options.AutomaticAuthentication = false;
			});
			//For IIS deployment

			services.Configure<IISOptions>(options =>
			{
				options.ForwardClientCertificate = false;
			});


			//Handles loop referencing
			services.AddMvc()
				.AddJsonOptions(
					options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
				);

			//TODO: Always add services here
			// set the dbContext lifetime to transient
			services.AddDbContext<PMDbContext>(ServiceLifetime.Transient); 
			services.AddTransient<UserService>();
			services.AddTransient<AccountService>();
			services.AddTransient<CourseService>();
			services.AddTransient<CourseHistoryService>();
			services.AddTransient<DoctorService>();
			services.AddTransient<NurseService>();
			services.AddTransient<PatientService>();
			services.AddTransient<MeasurementService>();
			services.AddTransient<MedicineService>();
			services.AddTransient<TreatmentService>();
			services.AddTransient<ProcedureService>();

			//Auth
			services.AddAuthentication(IISServerDefaults.AuthenticationScheme);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseExceptionBlockerMiddleware();
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
