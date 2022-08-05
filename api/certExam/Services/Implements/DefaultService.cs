using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using certExam.Common;
using certExam.Entities;
using System.Configuration;
using certExam.Extensions;
using System.Data;
using certExam.Infrastructure;

namespace certExam.Implements
{
    public class DefaultService : IDisposable
    {
        public string Connection {
            get { return ConfigurationManager.ConnectionStrings["StrConnection1"].ConnectionString; } }

        public DefaultService()
        {
            
        }

        /// <summary>
        /// 操作紀錄
        /// </summary>
        /// <param name="menuId">功能Id</param>
        /// <param name="actType">動作類型</param>
        /// <param name="tableName">資料表名稱</param>
        /// <param name="eCode">結果代碼</param>
        /// <param name="strSql">執行字串]</param>
        /// <returns></returns>
        public string OperLog(string menuId, int actType, string tableName, int eCode, string strSql, string[] operLogData)
        {
            string errStr = "";
            UserClassV1.UserData userData = UserClassV1.UserHelper.GetUserData();
            DataTable tbl_OperLog = userData.Get_tbl_operLogV1();
            DataRow operDr = tbl_OperLog.NewRow();
            operDr["actSerial"] = userData.actSerial;
            operDr["logDate"] = DateTime.Now;
            operDr["actIp"] = operLogData[0];// Request.ServerVariables["REMOTE_ADDR"];
            operDr["sysUserId"] = userData.sysUserId;
            operDr["menuId"] = menuId;

            operDr["actType"] = actType;
            operDr["controllerName"] = operLogData[1]; //this.ControllerContext.RouteData.Values["controller"].ToString();
            operDr["actionName"] = operLogData[2]; //this.ControllerContext.RouteData.Values["action"].ToString();
            operDr["tableName"] = tableName;
            operDr["resultCode"] = eCode > 0 ? (int)ResultCode.Success : (int)ResultCode.Failed;

            operDr["errMsg"] = errStr;
            //operDr["tblPrimaryKeysAndValues"] = tblPrimaryKeysAndValues;
            operDr["strSql"] = strSql;
            //operDr["memo"] = DBNull.Value;


            tbl_OperLog.Rows.Add(operDr);
            errStr += UserClassV1.Log.SaveOperLog(tbl_OperLog);
            return errStr;
        }

        protected int SetSQL(string sqlCmd)
        {
            int eCode = 0;

            using (Sql db = new Sql(this.Connection))
            {
                db.BeginTransaction();
                db.Command.Clear();
                db.Command.Add(sqlCmd);

                eCode = db.ExecuteNonQuery();
                if (eCode > 0)
                {
                    db.Commit();
                }
                else
                {
                    db.Rollback();
                }
            }
            return eCode;
        }


        public IEnumerable<T> GetAll<T>() where T : new()
        {
            using (Sql q = new Sql(this.Connection))
            {
                //  Get Table Name
                var table = this.getTables<T>(new T());
                if (table == null)
                {
                    return null;
                }
                string cmdText = string.Format(@"SELECT * FROM {0}.{1}", table.Schema, table.Name);

                q.Command.Clear();
                q.Command.Add(cmdText);

                var dt = q.ExecuteQuery();

                if (dt == null || dt.Rows.Count <= 0)
                {
                    return new List<T>();
                }
                return DataTableExtension.ToList<T>(dt).ToList();
            }
        }
        public IEnumerable<T> Get<T>(string condition) where T : new()
        {
            using (Sql q = new Sql(this.Connection))
            {
                //  Get Table Name
                var table = this.getTables<T>(new T());
                if (table == null)
                {
                    return null;
                }
                string cmdText = string.Format(@"SELECT * FROM {0}.{1} WHERE {2}", table.Schema, table.Name, condition);

                q.Command.Clear();
                q.Command.Add(cmdText);

                var dt = q.ExecuteQuery();

                if (dt == null || dt.Rows.Count <= 0)
                {
                    return new List<T>();
                }
                return DataTableExtension.ToList<T>(dt).ToList();
            }
        }
        public int Add<T>(T entity) where T : class
        {
            int id = 0;

            var columns = getTableColumns<T>(entity);
            if (columns == null)
            {
                return 0;
            }
            using (var q = new Sql(this.Connection))
            {
                if (columns.Count() > 0)
                {
                    id = setCommand<T>(q, columns, entity);
                }
            }
            return id;
        }

        public int Add<T>(Sql db, T entity) where T : class
        {
            int id = 0;

            var columns = getTableColumns<T>(entity);
            if (columns == null)
            {
                return 0;
            }
            if (columns.Count() > 0)
            {
                id = setCommand<T>(db, columns, entity);
            }
            return id;
        }
        protected IEnumerable<TableColumn> getTableColumns<T>(T entity)
        {
            using (var q = new Sql(this.Connection))
            {
                string name = entity.GetType().Name;
                var table = this.getTables<T>(entity);
                if (table == null)
                {
                    return new List<TableColumn>();
                }
                string cmd = @"SELECT A.TABLE_CATALOG AS Catalog, 
                                      A.TABLE_SCHEMA AS SchemaName, 
                                      A.TABLE_NAME AS Name,
                                      B.COLUMN_NAME AS ColumnName,
                                      B.DATA_TYPE AS DataType
                                FROM INFORMATION_SCHEMA.TABLES AS A
                                JOIN INFORMATION_SCHEMA.COLUMNS AS B ON A.TABLE_SCHEMA = B.TABLE_SCHEMA AND A.TABLE_NAME = B.TABLE_NAME
                               WHERE A.TABLE_TYPE = 'BASE TABLE' 
                                 AND A.TABLE_NAME = @TABLE_NAME 
                                 AND A.TABLE_SCHEMA = @TABLE_SCHEMA";
                q.Command.Clear();
                q.Command.Add(cmd);

                q.ParamByName("TABLE_NAME").SetString(table.Name);
                q.ParamByName("TABLE_SCHEMA").SetString(table.Schema);

                var dt = q.ExecuteQuery();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return null;
                }
                return DataTableExtension.ToList<TableColumn>(dt).ToList();
            }
        }

        private TableAttribute getTables<T>(T entity)
        {
            return (TableAttribute)Attribute.GetCustomAttribute(entity.GetType(), typeof(TableAttribute));
        }

        private int setCommand<T>(Sql q, IEnumerable<TableColumn> columns, T entity)
        {
            int eCode = 0;
            int fCount = 0;
            int cCount = 0;
            int id = 0;

            string insertCommand = @"INSERT INTO {0}.{1} ({2}) VALUES ({3});";

            string fields = this.generateColumns<T>(columns, entity, out fCount);
            string conditions = this.generateConditions<T>(columns, entity, out cCount);

            if (fCount != cCount)
            {
                return eCode;
            }
            //  Get Table Name
            var table = this.getTables<T>(entity);
            if (table == null)
            {
                return eCode;
            }

            string cmd = string.Format(insertCommand, table.Schema, table.Name, fields, conditions);

            q.Command.Clear();
            q.Command.Add(cmd);
            foreach (var column in columns)
            {
                var property = typeof(T).GetProperty(column.ColumnName);

                var keys = property.GetCustomAttributes(true).Where(x => (KeyAttribute)x != null).ToList();
                if (keys.Count() > 0)
                {
                    continue;
                }
                if (property == null)
                {
                    continue;
                }

                object value = property.GetValue(entity, null);
                if (value == null)
                {
                    continue;
                }

                switch (Type.GetTypeCode(property.PropertyType))
                {
                    case TypeCode.Byte:
                        q.ParamByName(column.ColumnName).SetInt16(value);
                        continue;
                    case TypeCode.Char:
                        q.ParamByName(column.ColumnName).SetString(value);
                        continue;
                    case TypeCode.DateTime:
                        q.ParamByName(column.ColumnName).SetDateTime(value);
                        continue;
                    case TypeCode.Decimal:
                        q.ParamByName(column.ColumnName).SetDecimal(value);
                        continue;
                    case TypeCode.Double:
                        q.ParamByName(column.ColumnName).SetDouble(value);
                        continue;
                    case TypeCode.Int16:
                        q.ParamByName(column.ColumnName).SetInt16(value);
                        continue;
                    case TypeCode.Int32:
                        q.ParamByName(column.ColumnName).SetInt32(value);
                        continue;
                    case TypeCode.Int64:
                        q.ParamByName(column.ColumnName).SetInt64(value);
                        continue;
                    case TypeCode.SByte:
                        q.ParamByName(column.ColumnName).SetInt16(value);
                        continue;
                    case TypeCode.Single:
                        q.ParamByName(column.ColumnName).SetInt16(value);
                        continue;
                    case TypeCode.String:
                        q.ParamByName(column.ColumnName).SetString(value);
                        continue;
                    case TypeCode.DBNull:
                        q.ParamByName(column.ColumnName).SetDBNull(value);
                        continue;
                    case TypeCode.Boolean:
                        q.ParamByName(column.ColumnName).SetBool(value);
                        continue;
                    case TypeCode.Object:
                        //判斷 Nullable 
                        if (property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            if (Nullable.GetUnderlyingType(property.PropertyType) == typeof(decimal))
                            {
                                q.ParamByName(column.ColumnName).SetDecimal(value);
                            }
                            else if (Nullable.GetUnderlyingType(property.PropertyType) == typeof(DateTime))
                            {
                                q.ParamByName(column.ColumnName).SetDateTime(value);
                            }
                        }
                        continue;
                    default:
                        eCode++;
                        continue;
                }
            }
            if (eCode == 0)
            {
                eCode = q.ExecuteNonQuery(out id);
            }
            return id;
        }

        /// <summary>
        /// 產生 columns
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <param name="entity"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private string generateColumns<T>(IEnumerable<TableColumn> columns, T entity, out int count)
        {
            string field = string.Empty;
            count = 0;
            foreach (var column in columns)
            {
                var prop = typeof(T).GetProperty(column.ColumnName);
                var keys = prop.GetCustomAttributes(true).Where(x => (KeyAttribute)x != null).ToList();
                if (keys.Count() > 0)
                {
                    continue;
                }

                var value = prop.GetValue(entity, null);
                if (value == null)
                {
                    continue;
                }
                count++;
                if (string.IsNullOrEmpty(field) == true)
                {
                    field = string.Format("{0}", column.ColumnName);
                }
                else
                {
                    field = string.Format(@"{0}, {1}", field, column.ColumnName);
                }
            }
            return field;
        }
        /// <summary>
        /// 產生 Where 條件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <param name="entity"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private string generateConditions<T>(IEnumerable<TableColumn> columns, T entity, out int count)
        {
            count = 0;
            string param = string.Empty;
            foreach (var column in columns)
            {
                var prop = typeof(T).GetProperty(column.ColumnName);
                var keys = prop.GetCustomAttributes(true).Where(x => (KeyAttribute)x != null).ToList();
                if (keys.Count() > 0)
                {
                    continue;
                }
                var value = prop.GetValue(entity, null);
                if (value == null)
                {
                    continue;
                }
                count++;
                if (string.IsNullOrEmpty(param) == true)
                {
                    param = string.Format("@{0}", column.ColumnName);
                }
                else
                {
                    param = string.Format(@"{0}, @{1}", param, column.ColumnName);
                }
            }
            return param;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}