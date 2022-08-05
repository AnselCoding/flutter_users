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
using Models;
using System.Net.Http.Headers;

using certExam.Entities;
using certExam.Infrastructure;
using certExam.Implements;
using Newtonsoft.Json.Linq;
using certExam.Services.Models;

namespace certExam.Controllers
{
    public class ExamController : ApiController
    {
        public ExamService ExamService { get; set; }
        public string actIp { get; set; }

        public ExamController()
        {
            this.ExamService = new ExamService();
            this.actIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// ���o�������
        /// </summary>
        /// <param name="outId">�����N�X</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetErpexamView(int outId)
        {
            var controllerName = this.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = this.ActionContext.ActionDescriptor.ActionName;
            string[] operLogDate = new string[] { actIp, controllerName, actionName };
            return this.Ok(ExamService.GetErpexamView(outId, operLogDate));
        }

        /// <summary>
        /// ���o�����D��(�p�����D�������A�w�惡������X�D��)
        /// </summary>
        /// <param name="outId">�����N�X</param>
        /// <param name="statusId2">�D������</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetExamQues(int outId, string statusId2)
        {
            var controllerName = this.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = this.ActionContext.ActionDescriptor.ActionName;
            string[] operLogDate = new string[] { actIp, controllerName, actionName };
            return this.Ok(ExamService.GetExamQues(outId, statusId2, operLogDate));
        }

        /// <summary>
        /// �]�w�C�D�����D�t����
        /// </summary>
        /// <param name="appSingleScores"></param>
        /// <param name="operLogData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SetAppSingleScore(IEnumerable<AppSingleScore> appSingleScores)
        {
            var controllerName = this.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = this.ActionContext.ActionDescriptor.ActionName;
            string[] operLogDate = new string[] { actIp, controllerName, actionName };
            return this.Ok(ExamService.SetAppSingleScore(appSingleScores, operLogDate));
        }

        /// <summary>
        /// �����ҥͧ@�����G
        /// </summary>
        /// <param name="outId">�����N�X</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetPersonRecords(int outId)
        {
            var controllerName = this.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = this.ActionContext.ActionDescriptor.ActionName;
            string[] operLogDate = new string[] { actIp, controllerName, actionName };
            return this.Ok(ExamService.GetPersonRecords(outId, operLogDate));
        }

        /// <summary>
        /// �����U������ƶ��X�d��
        /// </summary>
        /// <param name="outId">�����N�X</param>
        /// <param name="qKey">�d�߶���</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetListFromOutId(int outId, string qKey)
        {
            var controllerName = this.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = this.ActionContext.ActionDescriptor.ActionName;
            string[] operLogDate = new string[] { actIp, controllerName, actionName };
            return this.Ok(ExamService.GetListFromOutId(outId, qKey, operLogDate));
        }
    }
}