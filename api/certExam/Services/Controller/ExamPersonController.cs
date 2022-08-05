using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

using certExam.Services.Models;
using certExam.Entities;
using certExam.Infrastructure;
using certExam.Implements;

namespace certExam.Controllers
{
    public class ExamPersonController : ApiController
    {
        public ExamPersonService ExamPersonService { get; set; }
        public string actIp { get; set; }

        public ExamPersonController()
        {
            this.ExamPersonService = new ExamPersonService();
            this.actIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// 取得場次所有考生資料檔(限制: 有應用題的場次)
        /// </summary>
        /// <param name="outId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAppErpexamPersons(int outId)
        {
            var controllerName = this.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = this.ActionContext.ActionDescriptor.ActionName;
            string[] operLogDate = new string[]{ actIp, controllerName, actionName };
            return this.Ok(ExamPersonService.GetAppErpexamPersons(outId, operLogDate));
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateErpexamPerson(ErpexamPerson erpexamPerson)
        {
            return this.Ok(ExamPersonService.UpdateErpexamPerson(erpexamPerson));
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddErpexamPerson(ErpexamPerson erpexamPerson)
        {
            return this.Ok(ExamPersonService.AddErpexamPerson(erpexamPerson));
        }

        [HttpPost]
        public async Task<IHttpActionResult> RemoveErpexamPerson(ErpexamPerson erpexamPerson)
        {
            return this.Ok(ExamPersonService.RemoveErpexamPerson(erpexamPerson));
        }

        
    }
}