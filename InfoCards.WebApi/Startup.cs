using AutoMapper;
using InfoCards.Api.BusinessLogic.Abstract;
using InfoCards.Api.BusinessLogic.RequestHandlers;
using InfoCards.Api.BusinessLogic.Services;
using InfoCards.Api.Contract.Request;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace InfoCards.WebApi {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
      services.AddControllers();

      services.AddMediatR(typeof(CreateInfoCardRequest), typeof(CreateInfoCardRequestHandler));

      services.AddTransient<IInfoCardsService, InfoCardsService>();

      var mapperConfig = new MapperConfiguration(mc => {
        mc.AddProfile(new MappingProfile());
      });

      IMapper mapper = mapperConfig.CreateMapper();
      services.AddSingleton(mapper);

      services.AddSwaggerGen(c => {
        c.CustomSchemaIds(type => type.ToString());
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Autostrong.WebApi", Version = "v1" });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autostrong.WebApi v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
