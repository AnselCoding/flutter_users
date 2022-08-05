using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ZhClassV2;

namespace certExam
{
    public class MvcApplication : System.Web.HttpApplication
    {


        protected void Application_Start()
        {
            SetConfig();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void SetConfig()
        {
            //string strSql;
            //string strSql = "select funcId,flagOperLogEnable from S00_menuWebTree where LEN(funcId)=6 ";
            string tmpStr;
            int tmpLoca = 0;
            try
            {

                #region 設定 StrConnection1 連線參數

                ZhConfigV2.GlobalSystemVar.StrConnection1 = System.Configuration.ConfigurationManager.ConnectionStrings["StrConnection1"].ConnectionString;

                tmpStr = ZhConfigV2.GlobalSystemVar.StrConnection1;
                tmpLoca = tmpStr.IndexOf("=");
                //ZhWebPxTms.GlobalSystemVar.DbIp_A0 = "資料庫位址 : " + tmpStr.Substring(tmpLoca + 1, (tmpStr.IndexOf(";") - tmpLoca - 1));

                ZhConfigV2.GlobalSystemVar.DbIp1 = tmpStr.Substring(tmpLoca + 1, (tmpStr.IndexOf(";") - tmpLoca - 1));
                tmpLoca = tmpStr.IndexOf("=", tmpLoca + 1);

                ZhConfigV2.GlobalSystemVar.DbName1 = tmpStr.Substring(tmpLoca + 1, (tmpStr.IndexOf(";", tmpLoca) - tmpLoca - 1));

                #endregion

                ZhConfigV2.GlobalSystemVar.runMode = System.Configuration.ConfigurationManager.AppSettings["runMode"];
                //ZhConfigV2.GlobalSystemVar.Version = System.Configuration.ConfigurationManager.AppSettings["Version"];

                ZhAssemblyInfo asmInfo = new ZhAssemblyInfo(System.Reflection.Assembly.GetExecutingAssembly());
                ZhConfigV2.GlobalSystemVar.Version = asmInfo.AssemblyVersion;
                ZhConfigV2.GlobalSystemVar.ProductTitle = asmInfo.AssemblyProduct;

                string strSql = "select menuId,isOperLog,isOperLogEnable from S00_menus where statusId='10' ";
                ZhConfigV2.GlobalSystemVar.tbl_OperLogFlag = SqlTool.GetDataTable(ZhConfigV2.GlobalSystemVar.StrConnection1, strSql, "tbl_OperLogFlag");
                ZhConfigV2.GlobalSystemVar.tbl_OperLogFlag.PrimaryKey = new System.Data.DataColumn[] { ZhConfigV2.GlobalSystemVar.tbl_OperLogFlag.Columns[0] };

                ZhWebClass.WebApiHelper.APIuserId = "zhtech";
                ZhWebClass.WebApiHelper.APIpassword = "24369238";

            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }

        }

    }

}
