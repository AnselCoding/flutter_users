using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace certExam.Common
{

    public class Sql : IDisposable
    {
        #region Sql Body
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private SqlTransaction sqlTransaction;
        private bool IsTransaction;
        private StringCollection _SQLCommand;
        private string sqlCommandText;
        private Params _Params;

        //public Sql(IConfiguration configuration)
        //{
        //    this.Configuration = configuration;

        //    var connectName = this.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

        //    this.connect(connectName);
        //}

        public Sql(string connectName)
        {
            this.connect(connectName);
        }

        private void connect(string connectName)
        {
            IsTransaction = false;
            _SQLCommand = new StringCollection();
            sqlConnection = new SqlConnection();

            sqlConnection.ConnectionString = connectName;
            sqlCommand = new SqlCommand();
            sqlCommand.CommandTimeout = 60;
            sqlCommand.CommandText = string.Empty;
            sqlCommandText = string.Empty;
            sqlCommand.Connection = sqlConnection;
            sqlDataAdapter = new SqlDataAdapter();

            _Params = new Params(sqlCommand.Parameters);
        }
        public string ExpectionMessage
        {
            get;
            private set;
        }

        public bool Status
        {
            get
            {
                return true;
            }
        }
        public StringCollection Command
        {
            get
            {
                return _SQLCommand;
            }
        }
        public String CommandText
        {
            get
            {
                return sqlCommandText;
            }
            set
            {
                this.ExpectionMessage = "";
                _SQLCommand.Clear();
                _SQLCommand.Add(value);
            }

        }
        /// <summary>
        /// 開始交易
        /// </summary>
        public void BeginTransaction()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            try
            {
                sqlTransaction = sqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                sqlCommand.Transaction = sqlTransaction;
                IsTransaction = true;
            }
            catch (SqlException oex)
            {
                sqlConnection.Close();
                IsTransaction = false;
                throw oex;
            }
        }


        /// <summary>
        /// 取消交易
        /// </summary>
        public void Rollback()
        {
            try
            {
                sqlTransaction.Rollback();
            }
            catch (SqlException oex)
            {
                throw oex;
            }
            finally
            {
                sqlConnection.Close();
                IsTransaction = false;
            }

        }
        /// <summary>
        /// 完成交易
        /// </summary>
        public void Commit()
        {
            try
            {
                sqlTransaction.Commit();
            }
            catch (SqlException oex)
            {
                throw oex;
            }
            finally
            {
                sqlConnection.Close();
                IsTransaction = false;
            }
        }
        /// <summary>
        /// 執行查詢
        /// </summary>
        public DataTable ExecuteQuery()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            try
            {
                DataTable dtDataTable = new DataTable("ExecuteQuery1");

                sqlCommandText = string.Empty;
                for (int i = 0; i < _SQLCommand.Count; i++)
                {
                    sqlCommandText += _SQLCommand[i];
                }
                sqlCommand.CommandText = sqlCommandText;
                sqlDataAdapter.SelectCommand = sqlCommand;

                sqlDataAdapter.Fill(dtDataTable);

                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                return dtDataTable;
            }
            catch (Exception ex)
            {
                sqlDataAdapter.SelectCommand.Parameters.Clear();

                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                var e = ex.Message;
                return new DataTable("ExceptionQuery1");
            }
        }
        /// <summary>
        /// 執行查詢
        /// </summary>
        /// <param name="StartRecord"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public DataTable ExecuteQuery(int StartRecord, int RecordCount)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            try
            {
                DataTable dtDataTable = new DataTable("ExecuteQuery2");


                sqlCommandText = string.Empty;
                for (int i = 0; i < _SQLCommand.Count; i++)
                {
                    sqlCommandText += _SQLCommand[i];
                }
                sqlCommand.CommandText = sqlCommandText;
                sqlDataAdapter.SelectCommand = sqlCommand;

                try
                {
                    sqlDataAdapter.Fill(StartRecord, RecordCount, dtDataTable);
                }
                catch
                {
                    sqlDataAdapter.Fill(StartRecord, RecordCount, dtDataTable);
                }
                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                return dtDataTable;
            }
            catch (Exception)
            {
                sqlDataAdapter.SelectCommand.Parameters.Clear();

                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                return new DataTable("ExceptionQuery2");
            }
        }
        /// <summary>
        /// 執行查詢-Dirty Read
        /// </summary>
        public DataTable ExecuteDirtyQuery()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            try
            {
                DataTable dtDataTable = new DataTable("ExecuteDirtyQuery1");

                sqlCommandText = string.Empty;
                sqlCommandText = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ";
                for (int i = 0; i < _SQLCommand.Count; i++)
                {
                    sqlCommandText += _SQLCommand[i];
                }
                sqlCommand.CommandText = sqlCommandText;
                sqlDataAdapter.SelectCommand = sqlCommand;

                sqlDataAdapter.Fill(dtDataTable);

                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                return dtDataTable;
            }
            catch (Exception)
            {
                sqlDataAdapter.SelectCommand.Parameters.Clear();

                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                return new DataTable("ExceptionDirtyQuery1");
            }
        }
        /// <summary>
        /// 執行查詢-Dirty Read
        /// </summary>
        public DataTable ExecuteDirtyQuery(int StartRecord, int RecordCount)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            try
            {
                DataTable dtDataTable = new DataTable("ExecuteDirtyQuery2");

                sqlCommandText = string.Empty;
                sqlCommandText = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ";
                for (int i = 0; i < _SQLCommand.Count; i++)
                {
                    sqlCommandText += _SQLCommand[i];
                }
                sqlCommand.CommandText = sqlCommandText;
                sqlDataAdapter.SelectCommand = sqlCommand;


                try
                {
                    sqlDataAdapter.Fill(StartRecord, RecordCount, dtDataTable);
                }
                catch
                {
                    sqlDataAdapter.Fill(StartRecord, RecordCount, dtDataTable);
                }
                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                return dtDataTable;
            }
            catch (Exception)
            {
                sqlDataAdapter.SelectCommand.Parameters.Clear();

                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                return new DataTable("ExceptionDirtyQuery2");
            }
        }

        /// <summary>
        /// 執行交易
        /// </summary>
        /// <param name="SqlCommand"></param>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            try
            {
                sqlCommandText = String.Empty;
                for (int i = 0; i < _SQLCommand.Count; i++)
                {
                    sqlCommandText += _SQLCommand[i];
                }
                sqlCommand.CommandText = sqlCommandText;
                int iCount = sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                return iCount;
            }
            catch (Exception ex)
            {
                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                this.ExpectionMessage = ex.Message;
                return -999;
            }
        }

        /// <summary>
        /// 執行交易
        /// SCOPE_IDENTITY 會傳回只在目前範圍內插入的值
        /// </summary>
        /// <param name="SqlCommand"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(out int IdentityNo)
        {
            IdentityNo = 0; // 預設為0 
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            try
            {
                sqlCommandText = String.Empty;
                for (int i = 0; i < _SQLCommand.Count; i++)
                {
                    sqlCommandText += _SQLCommand[i];
                }

                // 增加取得流水號的SQL語法
                sqlCommand.CommandText = string.Format("{0};{1}", sqlCommandText, "SELECT @IdentityNo=SCOPE_IDENTITY()");
                sqlCommand.Parameters.Add(new SqlParameter("IdentityNo", SqlDbType.Int));
                sqlCommand.Parameters["IdentityNo"].Direction = ParameterDirection.Output;

                int iCount = sqlCommand.ExecuteNonQuery();

                // 取得新增後的流水號
                IdentityNo = Convert.ToInt32(sqlCommand.Parameters["IdentityNo"].Value);


                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                return iCount;
            }
            catch (Exception ex)
            {
                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
                this.ExpectionMessage = ex.Message;
                return -999;
            }
        }
        /// <summary>
        /// @@IDENTITY 值，因為此值會用於複寫觸發程序和預存程序中。
        /// IDENT_CURRENT 不受範圍和工作階段的限制。
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int GetIdentityNo(string TableName)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            try
            {
                DataTable dtDataTable = new DataTable();

                if (IsTransaction == true)
                {
                    sqlCommandText = "SELECT @@IDENTITY AS [@@IDENTITY]";
                }
                else
                {
                    sqlCommandText = String.Format("SELECT IDENT_CURRENT ('{0}') AS IDENT_CURRENT", TableName);
                }

                sqlCommand.CommandText = sqlCommandText;
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    sqlDataAdapter.Fill(dtDataTable);
                    return int.Parse(dtDataTable.Rows[0][0].ToString());
                }
                catch
                {
                    return 0;
                }
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                if (IsTransaction == false)
                {
                    sqlConnection.Close();
                }
            }

        }

        /// <summary>
        /// 建立欄位及給值　obj.FieldByName("欄位").String="Val";
        /// </summary>
        /// <param name="sFieldName"></param>
        public Params ParamByName(string sParamName)
        {
            _Params.ParamName = "@" + sParamName;
            return _Params;
        }

        #endregion
        #region Sql Params
        public class Params
        {
            private string _ParamName;
            private string _AsString;
            private int _AsInteger;
            private Double _AsDouble;
            private Decimal _AsDecimal;
            private DateTime _AsDateTime;
            private string _AsText;
            private bool _AsBool;
            private DBNull _AsDBNull;
            private SqlParameterCollection _Parameters;

            public Params(SqlParameterCollection Parameters)
            {
                _Parameters = Parameters;
            }
            /// <summary>
            /// 取得欄位名稱
            /// </summary>
            public string ParamName
            {
                get
                {
                    return _ParamName;
                }
                set
                {
                    _ParamName = value;
                }
            }

            #region AsValues
            /// <summary>
            /// 給值(String)
            /// </summary>
            public string AsString
            {
                set
                {
                    _AsString = value;
                    if (_AsString.Length == 0)
                    {
                        _Parameters.Add(_ParamName, SqlDbType.Char, 1).Value = _AsString;
                    }
                    else if (_AsString.Length <= 20)
                    {
                        if (DbConvert.IsUniCode(_AsString) == true)  // UNICODE 需要用NChar
                        {
                            _Parameters.Add(_ParamName, SqlDbType.NChar, _AsString.Length).Value = _AsString;
                        }
                        else
                        {
                            _Parameters.Add(_ParamName, SqlDbType.Char, _AsString.Length).Value = _AsString;
                        }
                    }
                    else
                    {
                        if (DbConvert.IsUniCode(_AsString) == true)  // UNICODE 需要用NVarChar
                        {
                            _Parameters.Add(_ParamName, SqlDbType.NVarChar, _AsString.Length).Value = _AsString;
                        }
                        else
                        {
                            _Parameters.Add(_ParamName, SqlDbType.VarChar, _AsString.Length).Value = _AsString;
                        }
                    }
                }
                get
                {
                    return _AsString;
                }
            }
            /// <summary>
            /// 給值(Integer)
            /// </summary>
            public int AsInteger
            {
                set
                {
                    _AsInteger = value;
                    _Parameters.Add(_ParamName, SqlDbType.Int, 4).Value = _AsInteger;
                }
                get
                {
                    return _AsInteger;
                }
            }

            /// <summary>
            /// 給值(Double) 
            /// </summary>
            public Double AsDouble
            {
                set
                {
                    _AsDouble = value;
                    _Parameters.Add(_ParamName, SqlDbType.Float, 8).Value = _AsDouble;
                }
                get
                {
                    return _AsDouble;
                }
            }
            /// <summary>
            /// 給值(Double) 
            /// </summary>
            public Decimal AsDecimal
            {
                set
                {
                    _AsDecimal = value;
                    _Parameters.Add(_ParamName, SqlDbType.Decimal).Value = _AsDecimal;
                }
                get
                {
                    return _AsDecimal;
                }
            }
            /// <summary>
            /// 給值(DateTime)
            /// </summary>
            public DateTime AsDateTime
            {
                set
                {
                    if (value.Equals(new DateTime(0001, 01, 01, 0, 0, 0)) == true)
                    {
                        _AsDateTime = new DateTime(2999, 12, 31, 0, 0, 0);
                    }
                    else
                    {
                        _AsDateTime = value;
                    }
                    _Parameters.Add(_ParamName, SqlDbType.DateTime, 4).Value = _AsDateTime;
                }
                get
                {
                    return _AsDateTime;
                }
            }
            /// <summary>
            /// 給值(Text)
            /// </summary>
            public string AsText
            {
                set
                {
                    _AsText = value;
                    if (_AsText.Length == 0)
                    {
                        _Parameters.Add(_ParamName, SqlDbType.Text, 1).Value = _AsText;
                    }
                    else
                    {
                        _Parameters.Add(_ParamName, SqlDbType.Text, _AsText.Length).Value = _AsText;
                    }
                }
                get
                {
                    return _AsText;
                }
            }
            /// <summary>
            /// 給值(Bit)
            /// </summary>
            public bool AsBool
            {
                set
                {
                    _AsBool = value;
                    _Parameters.Add(_ParamName, SqlDbType.Bit, 1).Value = _AsBool;
                }
                get
                {
                    return _AsBool;
                }
            }
            /// <summary>
            /// 給值(DBNull)
            /// </summary>
            public DBNull AsDBNull
            {
                set
                {
                    _AsDBNull = value;
                    _Parameters.Add(_ParamName, SqlDbType.Variant).Value = _AsDBNull;
                }
                get
                {
                    return _AsDBNull;
                }
            }
            #endregion

            #region SetValues
            /// <summary>
            /// 給值(String)
            /// </summary>
            public void SetString(object Value)
            {
                SetString(Value, string.Empty);
            }
            /// <summary>
            /// 給值(String)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetString(object Value, object Default)
            {
                string _AsString = DbConvert.ToString(Value, Default);

                if (_AsString.Length == 0)
                {
                    _Parameters.Add(_ParamName, SqlDbType.Char, 1).Value = _AsString;
                }
                else if (_AsString.Length <= 20)
                {
                    if (DbConvert.IsUniCode(_AsString) == true)  // UNICODE 需要用NChar
                    {
                        _Parameters.Add(_ParamName, SqlDbType.NChar, _AsString.Length).Value = _AsString;
                    }
                    else
                    {
                        _Parameters.Add(_ParamName, SqlDbType.Char, _AsString.Length).Value = _AsString;
                    }
                }
                else
                {
                    if (DbConvert.IsUniCode(_AsString) == true)  // UNICODE 需要用NVarChar
                    {
                        _Parameters.Add(_ParamName, SqlDbType.NVarChar, _AsString.Length).Value = _AsString;
                    }
                    else
                    {
                        _Parameters.Add(_ParamName, SqlDbType.VarChar, _AsString.Length).Value = _AsString;
                    }
                }
            }

            /// <summary>
            /// 給值(Text)
            /// </summary>
            public void SetText(object Value)
            {
                SetText(Value, string.Empty);
            }
            /// <summary>
            /// 給值(Text)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetText(object Value, object Default)
            {
                _AsText = DbConvert.ToString(Value, Default);

                if (_AsText.Length == 0)
                {
                    _Parameters.Add(_ParamName, SqlDbType.Text, 1).Value = _AsText;
                }
                else
                {
                    _Parameters.Add(_ParamName, SqlDbType.Text, _AsText.Length).Value = _AsText;
                }
            }
            /// <summary>
            /// 給值(Xml)
            /// </summary>
            public void SetXml(object Value)
            {
                SetXml(Value, string.Empty);
            }
            /// <summary>
            /// 給值(Xml)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetXml(object Value, object Default)
            {
                _Parameters.Add(_ParamName, SqlDbType.Xml).Value = DbConvert.ToString(Value, Default);
            }

            /// <summary>
            /// 給值(TinyInt)
            /// </summary>
            public void SetTinyInt(object Value)
            {
                SetTinyInt(Value, 0);
            }
            /// <summary>
            /// 給值(TinyInt)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetTinyInt(object Value, object Default)
            {
                _Parameters.Add(_ParamName, SqlDbType.TinyInt, 1).Value = DbConvert.ToTinyInt(Value, Default);
            }
            /// <summary>
            /// 給值(Smallint)
            /// </summary>
            public void SetInt16(object Value)
            {
                SetInt16(Value, 0);
            }
            /// <summary>
            /// 給值(Smallint)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetInt16(object Value, object Default)
            {
                _Parameters.Add(_ParamName, SqlDbType.SmallInt, 2).Value = DbConvert.ToInt16(Value, Default);
            }

            /// <summary>
            /// 給值(Int)
            /// </summary>
            public void SetInt32(object Value)
            {
                SetInt32(Value, 0);
            }
            /// <summary>
            /// 給值(Int)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetInt32(object Value, object Default)
            {
                _AsInteger = DbConvert.ToInt32(Value, Default);
                _Parameters.Add(_ParamName, SqlDbType.Int, 4).Value = _AsInteger;
            }

            /// <summary>
            /// 給值(BigInt)
            /// </summary>
            public void SetInt64(object Value)
            {
                SetInt64(Value, 0);
            }
            /// <summary>
            /// 給值((BigInt)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetInt64(object Value, object Default)
            {
                _Parameters.Add(_ParamName, SqlDbType.BigInt, 8).Value = DbConvert.ToInt64(Value, Default);
            }

            /// <summary>
            /// 給值(Double) 
            /// </summary>
            public void SetDouble(object Value)
            {
                SetDouble(Value, 0);
            }
            /// <summary>
            /// 給值(Double)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetDouble(object Value, object Default)
            {
                _AsDouble = DbConvert.ToDouble(Value, Default);
                _Parameters.Add(_ParamName, SqlDbType.Float, 8).Value = _AsDouble;
            }

            /// <summary>
            /// 給值(Decimal) 
            /// </summary>
            public void SetDecimal(object Value)
            {
                SetDecimal(Value, 0);
            }
            /// <summary>
            /// 給值(Decimal)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetDecimal(object Value, object Default)
            {
                _AsDecimal = DbConvert.ToDecimal(Value, Default);
                _Parameters.Add(_ParamName, SqlDbType.Decimal).Value = _AsDecimal;
            }
            /// <summary>
            /// 給值(Date)
            /// </summary>
            public void SetDate(object Value)
            {
                SetDate(Value, new DateTime(2999, 12, 31));
            }
            /// <summary>
            /// 給值(Date)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetDate(object Value, object Default)
            {
                _Parameters.Add(_ParamName, SqlDbType.Date, 3).Value = DbConvert.ToDateTime(Value, Default);
            }


            /// <summary>
            /// 給值(DateTime)
            /// </summary>
            public void SetDateTime(object Value)
            {
                SetDateTime(Value, new DateTime(2999, 12, 31, 0, 0, 0));
            }
            /// <summary>
            /// 給值(DateTime)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetDateTime(object Value, object Default)
            {
                _AsDateTime = DbConvert.ToDateTime(Value, Default);
                _Parameters.Add(_ParamName, SqlDbType.DateTime, 4).Value = _AsDateTime;

            }
            /// <summary>
            /// 給值(當日時分秒 00:00:00
            /// </summary>
            /// <param name="Value"></param>
            public void SetDateMinTime(object Value)
            {
                DateTime dtDefault = new DateTime(2999, 12, 31, 0, 0, 0);
                SetDateMinTime(Value, dtDefault);
            }
            /// <summary>
            /// 給值(當日時分秒 00:00:00)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetDateMinTime(object Value, object Default)
            {
                DateTime dt1 = Convert.ToDateTime(Value);
                DateTime dt2 = Convert.ToDateTime(Default);

                DateTime dtValue = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
                DateTime dtDefault = new DateTime(dt2.Year, dt2.Month, dt2.Day, 0, 0, 0);

                SetDateTime(dtValue, dtDefault);
            }
            /// <summary>
            /// 給值(當日時分秒 23:59:59)
            /// </summary>
            /// <param name="Value"></param>
            public void SetDateMaxTime(object Value)
            {
                DateTime dtDefault = new DateTime(2999, 12, 31, 23, 59, 59);
                SetDateMaxTime(Value, dtDefault);
            }
            /// <summary>
            /// 給值(當日時分秒 23:59:59)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetDateMaxTime(object Value, object Default)
            {
                DateTime dt1 = Convert.ToDateTime(Value);
                DateTime dt2 = Convert.ToDateTime(Default);

                DateTime dtValue = new DateTime(dt1.Year, dt1.Month, dt1.Day, 23, 59, 59);
                DateTime dtDefault = new DateTime(dt2.Year, dt2.Month, dt2.Day, 23, 59, 59);

                SetDateTime(dtValue, dtDefault);
            }

            /// <summary>
            /// 給值(Bool)
            /// </summary>
            public void SetBool(object Value)
            {
                SetBool(Value, false);
            }
            /// <summary>
            /// 給值(Bool)或預設值
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="Default"></param>
            public void SetBool(object Value, object Default)
            {
                _AsBool = DbConvert.ToBoolean(Value, Default);
                _Parameters.Add(_ParamName, SqlDbType.Bit, 1).Value = _AsBool;
            }
            /// <summary>
            /// 給值(DBNull)
            /// </summary>
            public void SetDBNull(object Value)
            {
                _Parameters.AddWithValue(_ParamName, DBNull.Value);
            }

            /// <summary>
            /// 給值(VarBinary) 請寫入正確的檔案資料流，避免取出後的轉型發生例外
            /// </summary>
            public void SetVarBinary(byte[] Value)
            {
                SetVarBinary(Value, new byte[1]);
            }

            /// <summary>
            /// 給值(VarBinary)或預設值，請寫入正確的檔案資料流，避免取出後的轉型發生例外
            /// </summary>
            public void SetVarBinary(byte[] Value, byte[] Default)
            {
                if (Value.Length == 1) //初始不可為null，至少給1個長度
                {
                    _Parameters.Add(_ParamName, SqlDbType.VarBinary, 1).Value = Default;
                }
                else
                {
                    _Parameters.Add(_ParamName, SqlDbType.VarBinary, Value.Length).Value = Value;
                }
            }

            #endregion
        }
        #endregion

        public void Dispose()
        {
            sqlConnection.Close();
        }
    }

}