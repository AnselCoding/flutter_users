using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using NSwag.AspNet.Owin;
using Owin;


[assembly: OwinStartup(typeof(certExam.Startup))]

namespace certExam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 如需如何設定應用程式的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigureAuth(app);
            app.MapSignalR();

            var config = new HttpConfiguration();
            //Swagger UI
            app.UseSwaggerUi3(typeof(Startup).Assembly, settings =>
            {
                //針對RPC-Style WebAPI，指定路由包含Action名稱
                settings.GeneratorSettings.DefaultUrlTemplate =
                    "api/{controller}/{action}/{id?}";
                //可加入客製化調整邏輯
                settings.PostProcess = document =>
                {
                    document.Info.Title = "WebAPI for certExam";
                };
            });
            app.UseWebApi(config);
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
        }
        
    }
}