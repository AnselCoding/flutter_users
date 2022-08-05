using System;
using System.Collections.Generic;
using System.Linq;
using certExam.Extensions;
using certExam.Entities;
using certExam.Infrastructure;
using certExam.Common;
using certExam.Services.Models;
using ZhClassV2;

namespace certExam.Implements
{
    public class ExamPersonService : DefaultService
    {
        public string[] operLogData { get; set; }
        public ExamPersonService() {}

        

        /// <summary>
        /// 取得場次所有考生資料檔(限制: 有應用題的場次)
        /// </summary>
        /// <param name="outId"></param>
        /// <returns></returns>
        public ResultModel GetAppErpexamPersons(int outId, string[] operLogData)
        {
            #region 判斷是否可以動作
            //UserClassV1.UserData userData = UserClassV1.UserHelper.GetUserData();
            //if (userData == null)
            //{
            //    return new ResultModel(ResultCode.NoUser);
            //}
            #endregion

            var eCode = 1;
            // 確認場次有應用題
            using (var db = new Sql(this.Connection))
            {
                string cmd = "";
                cmd = @"SELECT t1.statusName
                              ,t.* 
                          FROM CA_erpexamPersons t
                          JOIN S00_statusId t1 ON t1.statusType = t.statusType AND t1.statusId = t.statusId
                         WHERE outId = @outId";
                db.Command.Clear();
                db.Command.Add(cmd);

                db.ParamByName("outId").SetInt32(outId);

                var dt = db.ExecuteQuery();
                var erpexamPersons = dt.ToDynamicList();

                // OperLog 
                //string errStr = OperLog("TCB222", (int)LogActType.DataQuery, "CA_erpexamPersons", eCode, cmd, operLogData);
                //if (errStr.Length > 0)
                //{
                //    eCode = 0;
                //    return new ResultModel(eCode, errStr);
                //}

                return new ResultModel(erpexamPersons);
            }
            
        }

        public ResultModel UpdateErpexamPerson(ErpexamPerson erpexamPerson)
        {
            var eCode = 0;

            using (var db = new Sql(this.Connection))
            {
                db.BeginTransaction();

                string cmd = @"UPDATE CA_erpexamPersons
                                  SET personCName = @personCName
                                WHERE outId = @outId 
                                  AND pId = @pId ";
                db.Command.Clear();
                db.Command.Add(cmd);

                db.ParamByName("outId").SetInt32(erpexamPerson.outId);
                db.ParamByName("pId").SetString(erpexamPerson.pId);
                db.ParamByName("personCName").SetString(erpexamPerson.personCName);

                eCode = db.ExecuteNonQuery();

                if (eCode <= 0)
                {
                    db.Rollback();
                    return new ResultModel(eCode, "異動失敗");
                }
                db.Commit();

            }
            return new ResultModel(eCode, "異動失敗");
        }

        public ResultModel AddErpexamPerson(ErpexamPerson erpexamPerson)
        {
            var eCode = 0;

            using (var db = new Sql(this.Connection))
            {
                db.BeginTransaction();

                // only for testing 在此新建的考生皆為缺考
                string cmd = @"INSERT INTO CA_erpexamPersons
                                        ( 
                                            [outId], [pId], [personCName], [statusId]
                                        )
                                        VALUES
                                        ( 
                                            @outId, @pId, @personCName, 40
                                        ) ";
                db.Command.Clear();
                db.Command.Add(cmd);

                db.ParamByName("outId").SetInt32(erpexamPerson.outId);
                db.ParamByName("pId").SetString(erpexamPerson.pId);
                db.ParamByName("personCName").SetString(erpexamPerson.personCName);

                eCode = db.ExecuteNonQuery();

                if (eCode <= 0)
                {
                    db.Rollback();
                    return new ResultModel(eCode, "異動失敗");
                }
                db.Commit();

            }
            return new ResultModel(eCode, "異動失敗");
        }

        public ResultModel RemoveErpexamPerson(ErpexamPerson erpexamPerson)
        {
            var eCode = 0;

            using (var db = new Sql(this.Connection))
            {
                db.BeginTransaction();
                
                string cmd = @"DELETE FROM CA_erpexamPersons
                                WHERE outId = @outId 
                                  AND pId = @pId ";
                db.Command.Clear();
                db.Command.Add(cmd);

                db.ParamByName("outId").SetInt32(erpexamPerson.outId);
                db.ParamByName("pId").SetString(erpexamPerson.pId);

                eCode = db.ExecuteNonQuery();

                if (eCode <= 0)
                {
                    db.Rollback();
                    return new ResultModel(eCode, "異動失敗");
                }
                db.Commit();

            }
            return new ResultModel(eCode, "異動失敗");
        }

        
    }
}