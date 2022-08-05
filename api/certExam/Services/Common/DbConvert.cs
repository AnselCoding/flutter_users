
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace certExam.Common
{
    public class DbConvert
    {
        /// <summary>
        /// 將Value轉換為string型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static string ToString(object Value, object Default)
        {
            try
            {
                if (Value == DBNull.Value || Value == null)
                {
                    return Default.ToString().Trim();
                }
                else
                {
                    return Value.ToString().Trim();
                }
            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 將Value轉換為datetime型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static DateTime ToDateTime(object Value, object Default)
        {
            try
            {
                if (Value == null)
                {
                    return Convert.ToDateTime(Default);
                }
                else if (Value == DBNull.Value)
                {
                    return Convert.ToDateTime(Default);
                }
                else if (Value.Equals(new DateTime(0001, 01, 01)) == true)
                {
                    return Convert.ToDateTime(Default);
                }
                else if (Convert.ToDateTime(Value) < new DateTime(1753, 01, 01, 12, 00, 00))
                {
                    return Convert.ToDateTime(Default);
                }
                else
                {
                    return Convert.ToDateTime(Value);
                }
            }
            catch
            {
                return new DateTime(2999, 12, 31, 0, 0, 0);
            }
        }
        /// <summary>
        /// 將Value轉換為TinyInt型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static Byte ToTinyInt(object Value, object Default)
        {
            try
            {
                if (Value == null)
                {
                    return Convert.ToByte(Default);
                }
                else if (Value == DBNull.Value)
                {
                    return Convert.ToByte(Default);
                }
                else if (Value.ToString().Trim().Length == 0)
                {
                    return Convert.ToByte(Default);
                }
                else
                {
                    return Convert.ToByte(Value);
                }
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 將Value轉換為Int16型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static Int16 ToInt16(object Value, object Default)
        {
            try
            {
                if (Value == null)
                {
                    return Convert.ToInt16(Default);
                }
                else if (Value == DBNull.Value)
                {
                    return Convert.ToInt16(Default);
                }
                else if (Value.ToString().Trim().Length == 0)
                {
                    return Convert.ToInt16(Default);
                }
                else
                {
                    return Convert.ToInt16(Value);
                }
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 將Value轉換為Int32型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static int ToInt32(object Value, object Default)
        {
            try
            {
                if (Value == null)
                {
                    return Convert.ToInt32(Default);
                }
                else if (Value == DBNull.Value)
                {
                    return Convert.ToInt32(Default);
                }
                else if (Value.ToString().Trim().Length == 0)
                {
                    return Convert.ToInt32(Default);
                }
                else
                {
                    return Convert.ToInt32(Value);
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 將Value轉換為Int64型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static Int64 ToInt64(object Value, object Default)
        {
            try
            {
                if (Value == null)
                {
                    return Convert.ToInt64(Default);
                }
                else if (Value == DBNull.Value)
                {
                    return Convert.ToInt64(Default);
                }
                else if (Value.ToString().Trim().Length == 0)
                {
                    return Convert.ToInt64(Default);
                }
                else
                {
                    return Convert.ToInt64(Value);
                }
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// 將Value轉換為double型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static double ToDouble(object Value, object Default)
        {
            try
            {
                if (Value == null)
                {
                    return Convert.ToDouble(Default);
                }
                else if (Value == DBNull.Value)
                {
                    return Convert.ToDouble(Default);
                }
                else if (Value.ToString().Trim().Length == 0)
                {
                    return Convert.ToDouble(Default);
                }
                else
                {
                    return Convert.ToDouble(Value);
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 將Value轉換為float型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static float ToFloat(object Value, object Default)
        {
            try
            {
                if (Value == null)
                {
                    return Convert.ToSingle(Default);
                }
                else if (Value == DBNull.Value)
                {
                    return Convert.ToSingle(Default);
                }
                else if (Value.ToString().Trim().Length == 0)
                {
                    return Convert.ToSingle(Default);
                }
                else
                {
                    return Convert.ToSingle(Value);
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 將Value轉換為decimal型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static decimal ToDecimal(object Value, object Default)
        {
            try
            {
                if (Value == null)
                {
                    return Convert.ToDecimal(Default);
                }
                else if (Value == DBNull.Value)
                {
                    return Convert.ToDecimal(Default);
                }
                else if (Value.ToString().Trim().Length == 0)
                {
                    return Convert.ToDecimal(Default);
                }
                else
                {
                    return Convert.ToDecimal(Value);
                }
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 將Value轉換為boolean型態值,Value如為Null或DBNull設定為預設值
        /// </summary>
        public static bool ToBoolean(object Value, object Default)
        {
            try
            {
                if (Value == null)
                {
                    return Convert.ToBoolean(Default);
                }
                else if (Value == DBNull.Value)
                {
                    return Convert.ToBoolean(Default);
                }
                else if (Value.ToString().Trim().Length == 0)
                {
                    return Convert.ToBoolean(Default);
                }
                else
                {
                    return Convert.ToBoolean(Value);
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// IsUniCode(string Value)
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsUniCode(string Value)
        {
            for (int i = 0; i < Value.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(Value.Substring(i, 1))) > Convert.ToInt32(Convert.ToChar(128)))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// ConvertTo<T>(IList<T> list)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> ItemList)
        {
            DataTable dtTable = CreateTable<T>();
            Type EntityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(EntityType);

            foreach (T CurrentItem in ItemList)
            {
                DataRow row = dtTable.NewRow();

                foreach (PropertyDescriptor Prop in properties)
                {
                    row[Prop.Name] = Prop.GetValue(CurrentItem);
                }
                dtTable.Rows.Add(row);
            }
            return dtTable;
        }
        /// <summary>
        /// ConvertTo<T>(IList<DataRow> rows)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<T> ToCollection<T>(IList<DataRow> RowList)
        {
            IList<T> ItemList = null;

            if (RowList == null)
            {
                return ItemList;
            }

            ItemList = new List<T>();
            foreach (DataRow Row in RowList)
            {
                T CurrentItem = CreateItem<T>(Row);
                ItemList.Add(CurrentItem);
            }
            return ItemList;
        }
        /// <summary>
        ///  ConvertTo<T>(DataTable table)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> ToCollection<T>(DataTable Table)
        {
            if (Table == null)
            {
                return null;
            }

            List<DataRow> RowList = new List<DataRow>();
            foreach (DataRow Row in Table.Rows)
            {
                RowList.Add(Row);
            }
            return ToCollection<T>(RowList);
        }

        /// <summary>
        /// CreateItem<T>(DataRow row)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateItem<T>(DataRow Row)
        {
            T ObjectTable = default(T);
            if (Row == null)
            {
                return ObjectTable;
            }

            ObjectTable = Activator.CreateInstance<T>();
            foreach (DataColumn Col in Row.Table.Columns)
            {
                PropertyInfo Prop = ObjectTable.GetType().GetProperty(Col.ColumnName);
                try
                {
                    object RowValue = Row[Col.ColumnName];
                    if (RowValue == DBNull.Value)
                    {
                        RowValue = null;
                    }
                    Prop.SetValue(ObjectTable, RowValue, null);
                }
                catch
                {
                    throw;  // You can log something here  
                }
            }

            return ObjectTable;
        }
        /// <summary>
        /// CreateTable<T>()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataTable CreateTable<T>()
        {
            Type EntityType = typeof(T);
            DataTable dtTable = new DataTable(EntityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(EntityType);

            foreach (PropertyDescriptor Prop in properties)
            {
                dtTable.Columns.Add(Prop.Name, Prop.PropertyType);
            }
            return dtTable;
        }
    }

}