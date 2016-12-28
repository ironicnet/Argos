using Microsoft.Owin;
using ArgosWeb;
using System;
using System.Collections.Generic;
using Ninject;

[assembly: OwinStartup(typeof(Web))]
namespace ArgosWeb
{
    using System;
    using System.Web.Http;
    using Microsoft.Owin;
    using Microsoft.Owin.Extensions;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;
    using Ninject.Web.Common;
    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    public class Web : System.IDisposable
    {
        private ArgosCore.Guardian guardian;

        public Web(ArgosCore.Guardian guardian)
        {
            this.guardian = guardian;
        }

        public string Address { get; set; }
        protected System.IDisposable WebApp { get; set; }

        public void Dispose()
        {
            if (WebApp != null)
            {
                WebApp.Dispose();
            }
        }

        public void Start()
        {
            WebApp = Microsoft.Owin.Hosting.WebApp.Start(url: Address, startup: BuildApp);
        }
        public void Stop()
        {
            if (WebApp != null)
                WebApp.Dispose();
            WebApp = null;
        }
        protected void BuildApp(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var kernel = ConfigureNinject(app, httpConfiguration);
            GlobalConfiguration.Configuration.DependencyResolver = new LocalNinjectDependencyResolver(kernel);

            // Make ./public the default root of the static files in our Web Application.
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./wwwroot"),
                EnableDirectoryBrowsing = false,
            });
            app.UseStageMarker(PipelineStage.MapHandler);
        }
        public IKernel ConfigureNinject(IAppBuilder app, HttpConfiguration config)
        {
            var kernel = CreateKernel();
            app.UseNinjectMiddleware(() => kernel)
               .UseNinjectWebApi(config);

            return kernel;
        }

        public IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(System.Reflection.Assembly.GetExecutingAssembly());
            kernel.Load(typeof(ArgosWeb.Web).Assembly);
            kernel.Bind<ArgosCore.IGuardian>().ToConstant(guardian);
            return kernel;
        }
    }
}

public class LocalNinjectDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
{
    private readonly Ninject.IKernel _kernel;

    public LocalNinjectDependencyResolver(Ninject.IKernel kernel)
    {
        _kernel = kernel;
    }
    public System.Web.Http.Dependencies.IDependencyScope BeginScope()
    {
        return this;
    }

    public object GetService(Type serviceType)
    {
        return _kernel.TryGet(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        try
        {
            return _kernel.GetAll(serviceType);
        }
        catch (Exception)
        {
            return new List<object>();
        }
    }

    public void Dispose()
    {
        // When BeginScope returns 'this', the Dispose method must be a no-op.
    }
}