using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLTools
{
    /// <summary>
    /// mysql语句和c#语句的中间语言
    /// </summary>
    public class DictionaryPara:Dictionary<string ,string >
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string tablename = "";

        /// <summary>
        /// 第一个键值对是否必须有值
        /// </summary>
        private bool isMustFirstValue = true;

        /// <summary>
        /// 声明一个构造函数，执行被继承的Dictionary构造函数
        /// </summary>
        /// <param name="param"></param>
        public DictionaryPara(Dictionary <string ,string > param):base (param) { }

        public DictionaryPara(string tablename,bool isMustFirstValue=true) {
            this.tablename = tablename;
            this.isMustFirstValue = isMustFirstValue;
        }

        /// <summary>
        /// c#和字典对应的语句
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key,object value)
        {
            string strValue;
            if (value == null)
                strValue = null;
            else if (value is string)
                strValue = (string)value;
            //int int?(空的int)
            else if (value is Nullable<int>)
                strValue = (value as Nullable<int>).Value.ToString();
            //long long?
            else if (value is Nullable<long>)
                strValue = (value as Nullable<long>).Value.ToString();
            //double double?
            else if (value is Nullable<double>)
                strValue = (value as Nullable<double>).Value.ToString();
            //bool bool?  ToLower转化为小写
            else if (value is Nullable<bool>)
                strValue = (value as Nullable<bool>).Value.ToString().ToLower();
            else
                strValue = value.ToString();
            Add(key, strValue);
        }

        /// <summary>
        /// 字典和MySQL对应的语句
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public new void Add(string key,string value)
        {
            if(string .IsNullOrEmpty (key))
            {
                if(SqlManager .Instance .DEBUG !=null)
                   SqlManager .Instance .DEBUG("字典主键不能为空");
                return;
            }
            //if(string .IsNullOrEmpty (value ) && isMustFirstValue && Count == 0)
            //{
            //    DebugUtil.Instance.LogToTime("当期第一个键值对必须有值");
            //    return;
            //}
            base.Add(key, value);
        }
    }
}
