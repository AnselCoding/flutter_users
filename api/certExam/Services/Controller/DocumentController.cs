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
        /// ���o�ҷӸ��
        /// </summary>
        /// <param name="certId">�ҷӥN�X</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCertificate(string certId)
        {
            return this.Ok(DocumentService.GetCertificate(certId));
        }

        /// <summary>
        /// ���o���A��
        /// </summary>
        /// <param name="statusType">���A����</param>
        /// <param name="statusId">���A�N�X</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStatusId(string statusType, string statusId)
        {
            return this.Ok(DocumentService.GetStatusId(statusType, statusId));
        }

    }
}