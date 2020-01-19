using System;
using System.Text;
using AspNetCoreRateLimit;
using AutoMapper;
using Cinema.Api.Hubs;
using Cinema.Api.Models;
using Cinema.Models.Data;
using Cinema.Models.Models;
using Cinema.Models.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Cinema.Api
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private static ModelBuilder _modelBuilder { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //0. HTTPS en JSON options
            services.AddMvc(options =>
            {
            if (!_env.IsProduction())
            {
                options.SslPort = 44343;
                //in productie wordt een real SSL toegepast.
                //default poort voor SSL 443xx (cfr. 80xx voor HTTP)
                //gebruik van development poort 44343 (Chrome test)
            }
                //Filters zijn middleware die door alle controllers verwerkt worden.
                //HTTPS (of SSL) opleggen gebeurt met een Filter.
                //Het SSL filter veroorzaakt een redirect naar het HTTPS adres( met poortnummer 443..)
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddIdentity<CinemaUser, IdentityRole>()
                .AddEntityFrameworkStores<DBContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(options => options.EnableEndpointRouting = false)
                  .AddNewtonsoftJson(options => {
                      options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                     //circulaire referenties verhinderen
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                  });

            //1.Container voor DbContext en SQLrepos
            services.AddDbContext<DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Connectionstring")), ServiceLifetime.Transient);
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped(typeof(IMovieRepo), typeof(MovieRepo));
            services.AddScoped(typeof(IUserRepo), typeof(UserRepo));
            services.AddScoped(typeof(IMovieRoomRepo), typeof(MovieRoomRepo));

            //2. Services toevoegen

            //2.2 Cors
            services.AddCors(cfg =>
            {
                cfg.AddPolicy("MAGI", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    //.AllowAnyOrigin();
                    .WithOrigins("https://localhost:44346/", "https://fullstackcinemaapiservice.azurewebsites.net/" , "http://localhost:8080", "http://localhost:5000", "http://localhost/127.0.0.1", "localhost/127.0.0.1");
                });
            });

            //2.3 Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinema_Api", Version = "v1" });
            });


            //2.4 Automapper volgens configuratie
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MovieProfile());

            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            //2.5 Rate limiting
            //opzetten van MemoryCache om rates te bewaren
            services.AddMemoryCache();

            //één of meerdere RateLimitRules definiëren in appSettings.json
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //Singletons voor stokeren vd waarden
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddTransient<Seeder>();

            //2.6 Versioning
            services.AddApiVersioning(
             options =>
             {
                 options.ReportApiVersions = true;
                 options.AssumeDefaultVersionWhenUnspecified = true;
                 options.DefaultApiVersion = new ApiVersion(1, 0);
                 // options.Conventions.Controller<RecipesController>().HasApiVersion(1, 0);
                 //options.ApiVersionReader = new HeaderApiVersionReader("ver");
             });

            //2.7 Add Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = Configuration["Tokens:Issuer"],
                         ValidAudience = Configuration["Tokens:Audience"],

                         IssuerSigningKey = new SymmetricSecurityKey
                     (Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                     };
                     options.SaveToken = false;
                 });

            //2.8 realtime
            services.AddSignalR();

            //2.9 Logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.RollingFile( "Serilogs/DBFirst-{Date}.txt")
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DBContext context,
            UserManager<CinemaUser> userManager, RoleManager<IdentityRole> roleManager, ILoggerFactory logger
           , Seeder seeder)
        {

            Log.Logger.Warning("Serilog wordt opgestart");

            //2.5 Rate limiting (bovenaan)
            app.UseIpRateLimiting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MAGI");

            app.UseHttpsRedirection();
          
            //2.3 Swagger
            app.UseSwagger(); //enable swagger
            app.UseSwaggerUI(c =>
            {
                //c.RoutePrefix = "swagger"; //path naar de UI pagina: /swagger/index.html
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema_Api v1");
            });

            //realtime toevoegen met websockets
            app.UseWebSockets(new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(180),
                ReceiveBufferSize = 4 * 1024
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<RepoHub>("/repohub");
            });

            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 44343));

            //10 routes mvc

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            ModelBuilderExtensions._configuration = Configuration;
            ModelBuilderExtensions._context = context;
            ModelBuilderExtensions._contentRootPath = env.ContentRootPath.Replace("Cinema_Api", "DBContext.Models");

            seeder.SeedRole(roleManager).Wait();
            seeder.SeedUser(userManager).Wait();
        }
    }
}
