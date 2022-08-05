using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
//using Microsoft.Owin.Security.Facebook;
//using System.Threading.Tasks;
using Owin;

namespace certExam
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // 如需如何設定應用程式的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkID=316888


            // 讓應用程式使用 Cookie 儲存已登入使用者的資訊
            // 並使用 Cookie 暫時儲存使用者利用協力廠商登入提供者登入的相關資訊；
            // 在 Cookie 中設定簽章
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString("/Account/Login"),
                //LoginPath = new PathString("/Hone/Index"),
                LoginPath = new PathString("/swagger/index.html?url=/swagger/v1/swagger.json#/"),

            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //啟用利用協力廠商登入提供者登入
            app.UseFacebookAuthentication(
                appId: "189433698446832",
                appSecret: "b312879f84335abe83fdabdf034f1be7");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "642835136412-q0f4g84aqa6vc3udfnkohu4msp1oj01l.apps.googleusercontent.com",
                ClientSecret = "Qw74qNJiuWIMKH-d56Fjp_I4"
            });
        }



    }
}