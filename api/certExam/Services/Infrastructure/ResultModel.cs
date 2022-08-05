using System;
using System.Collections.Generic;

namespace certExam.Infrastructure
{
    public enum ResultCode
    {
        NoUser = 0,
        Success = 10,
        NotFound = 20,
        Failed = 90,
        Exception = 99
    }
    public class ResultModel
    {
        public ResultCode ResultCode { get; private set; }

        public string Message { get; private set; }

        public IEnumerable<dynamic> Data { get; private set; }

        public System.Net.Http.HttpResponseMessage Response { get; private set; }

        public ResultModel(System.Net.Http.HttpResponseMessage response)
        {
            this.Response = response;
        }
        public ResultModel(IEnumerable<dynamic> data)
        {
            this.set(data);
        }

        public ResultModel(ResultCode resultCode, string msg)
        {
            this.ResultCode = resultCode;
            this.Data = new List<dynamic>();
            this.Message = msg;
        }

        public ResultModel(ResultCode resultCode)
        {
            if (resultCode == ResultCode.NoUser)
            {
                this.noUser();
            }
            else
            {
                this.ResultCode = resultCode;
            }
        }

        public ResultModel(int eCode, string msg)
        {
            this.ResultCode = eCode <= 0 ? ResultCode.Failed : ResultCode.Success;
            this.Data = new List<dynamic>();
            this.Message = eCode <= 0 ? msg : "";
            if (eCode == -999)
            {
                this.ResultCode = ResultCode.Exception;
                //this.Message = "SQL Exception";
            }
        }

        public ResultModel(int eCode, IEnumerable<dynamic> data, string msg)
        {
            this.ResultCode = eCode <= 0 ? ResultCode.Failed : ResultCode.Success;
            this.set(data);
            this.Message = eCode <= 0 ? msg : "";
            if (eCode == -999)
            {
                this.ResultCode = ResultCode.Exception;
                //this.Message = "SQL Exception";
            }
        }

        private void set(IEnumerable<dynamic> data)
        {
            if (data == null)
            {
                this.failed(data);
            }
            else
            {
                this.ResultCode = ResultCode.Success;
                this.Data = data;
                this.Message = "";
            }
        }
        private void failed(IEnumerable<dynamic> data)
        {
            this.ResultCode = ResultCode.NotFound;
            this.Message = "查無資料";
            this.Data = new List<dynamic>();
        }
        private void noUser()
        {
            this.ResultCode = ResultCode.NoUser;
            this.Message = "請先登入系統 方可操作資料";
        }

    }
}