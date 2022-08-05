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

namespace certExam.Controllers
{
    public class DocumentController : ApiController
    {
        public DocumentService DocumentService { get; set; }


        public DocumentController()
        {
            this.DocumentService = new DocumentService();
        }

        /// <summary>
        /// 取得證照資料
        /// </summary>
        /// <param name="certId">證照代碼</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCertificate(string certId)
        {
            return this.Ok(DocumentService.GetCertificate(certId));
        }

        /// <summary>
        /// 取得狀態檔
        /// </summary>
        /// <param name="statusType">狀態類型</param>
        /// <param name="statusId">狀態代碼</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStatusId(string statusType, string statusId)
        {
            return this.Ok(DocumentService.GetStatusId(statusType, statusId));
        }

    }
}