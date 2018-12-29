
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MySQLTools
{
    /// <summary>
    /// 输出打印的回调委托
    /// </summary>
    /// <param name="obj"></param>
    public delegate void DebugCallback(object obj);
    public class SqlManager
    {
        /// <summary>
        /// 正常输出
        /// </summary>
        public DebugCallback DEBUG;

        /// <summary>
        /// 错误输出
        /// </summary>
        public DebugCallback ERROR;

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        MySQLUtil Sql = new MySQLUtil();

        /// <summary>
        /// 单例对象
        /// </summary>
        private static SqlManager instance;
        public static SqlManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SqlManager();
                return instance;
            }
        }

        /// <summary>
        /// 开启数据库
        /// </summary>
        /// <param name="host">数据库连接</param>
        /// <param name="database">数据库名称</param>
        /// <param name="uid">数据库连接账号</param>
        /// <param name="psd">数据库密码</param>
        public void Start(string host,string database,string uid,string psd)
        {
            //"127.0.0.1", 3306, "niuzaigame", "root", "newpasswd"
            Sql.InitData(host ,3306,database ,uid ,psd);
            Sql.Connect();
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void Close()
        {
            Sql.Close();
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool AddTable(DictionaryPara param)
        {
            //检查传入的待插入的值中是否有空值
            foreach (string key in param .Keys)
            {
                if(string .IsNullOrEmpty (param[key]))
                {
                    if (DEBUG != null)
                        DEBUG(param.tablename + "添加数据失败，" + key + "不能为空数据");
                    return false;
                }
            }
            //转换传入的数据为数组类型
            List<string> tempValue = new List<string>(param.Values);
            string[] values = tempValue.ToArray();
            //执行插入数据
            int res = Sql.InsertData(param.tablename, values);
            //如果插入的结果影响的行数不为0
            if (res >0)
            {
                return true;//返回插入成功
            }
            return false;//返回插入失败
        }

        /// <summary>
        /// 根据字段名插入一行数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool AddTableAtItems(DictionaryPara param)
        {
            List<string> tempKey = new List<string>();
            List<string> tempValue = new List<string>();
            //检查传入的待插入的值中是否有空值
            foreach (string key in param.Keys)
            {
                if (!string.IsNullOrEmpty(param[key]))
                {
                    tempKey.Add(key);
                    tempValue.Add (param[key]);
                }
            }
            //转换传入的数据为数组类型
            string[] items = tempKey.ToArray();
            string[] values = tempValue.ToArray();
            //执行插入数据
            int res = Sql.InsertData(param .tablename ,values,items);
            //如果插入的结果影响的行数不为0
            if (res > 0)
            {
                return true;//返回插入成功
            }
            return false;//返回插入失败

        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool UpdateInItems(DictionaryPara param)
        {
            if (param.Count < 2) return false;
            List<string> keys = new List<string>();
            List<string> values = new List<string>();
            foreach (string key in param .Keys)
            {
                if(!string .IsNullOrEmpty (param[key]))
                {
                    keys.Add(key);
                    values.Add(param[key]);
                }
            }
            //搜寻规则：排位位于0的键和值作为搜寻字段和值
            //string whereKey = param.Keys.ToList()[0];
            //string whereValue = param[whereKey];
            string whereKey = keys[0];
            string whereValue = values[0];
            //删除位于第0位的
            keys.RemoveAt(0);
            values.RemoveAt(0);
            int res = Sql.UpdateData(param.tablename, keys.ToArray(), values.ToArray(), whereKey, whereValue);
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改语句
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Delete(DictionaryPara param)
        {
            if (param.Count < 2) return false;
            List<string> keys = new List<string>();
            List<string> values = new List<string>();
            foreach (string key in param.Keys)
            {
                if (!string.IsNullOrEmpty(param[key]))
                {
                    keys.Add(key);
                    values.Add(param[key]);
                }
            }
            string whereKey = keys[0];
            string whereValue = values[0];
            int res = Sql.DeleteData(param.tablename, whereKey, whereValue);
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询一行数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool GetTable(ref DictionaryPara param)
        {
            if (param .Count< 2) return false;
            //查询一行的数据，待查询的语句不需要包含值
            List<string> keys = new List<string>(param.Keys);
            //但是要保证要搜寻的首位键值对必须有值
            if (string.IsNullOrEmpty(param[keys[0]]))
                return false;
            string whereKey = keys[0];
            string whereValue = param[keys[0]];
            keys.RemoveAt(0);
            //执行查询单行数据
            DataSet ds = Sql.SelectData(param.tablename, keys.ToArray(), whereKey, whereValue);
            //如果查询成功并且得到查询数据
            if (ds!=null && ds .Tables .Count !=0)
            {
                DataTable table = ds.Tables[0];
                //Rows用来存储查询结果数据
                //Colume用来存储查询的字段
                if (table.Rows.Count == 0)
                    return false;
                for (int i = 0; i < keys .Count; i++)
                {
                    //如果查询的数据不为空，将查询的数据赋值给相应的参数
                    if(table .Rows [0][keys [i]]!=null && table .Rows [0][keys [i]].ToString() != "")
                    {
                        param[keys[i]] = table.Rows[0][keys[i]].ToString();
                    }
                }
                return true;
            }
            //查询失败
            return false;
            }

        /// <summary>
        /// 获取某列最后的值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool GetEndLineValue(ref DictionaryPara param)
        {
            if (param.Count == 0) return false;
            List<string> keysList = new List<string>(param.Keys);
            string where = keysList[0];
            DataSet ds = Sql.SelectMaxRow(param.tablename,where);
            if(ds!=null && ds.Tables .Count > 0)
            {
                DataTable table = ds.Tables[0];
                if (table.Rows.Count == 0)
                    return false;
                if (table.Rows[0]["max("+where+")"] != null && table.Rows[0]["max(" + where + ")"].ToString() != "")
                {
                    param[where] = table.Rows[0]["max(" + where + ")"].ToString();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取某列全部的值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List <DictionaryPara > GetColume(DictionaryPara param)
        {
            if (param.Count == 0) return null;
            List<string> items = new List<string>(param.Keys);
            DataSet ds = Sql.SelectColumn(param.tablename, items.ToArray());
            if(ds!=null && ds.Tables .Count > 0)
            {
                DataTable table = ds.Tables[0];
                if (table.Rows.Count == 0)
                    return null;
                List<DictionaryPara> dp = new List<MySQLTools.DictionaryPara>();
                //Rows用来存储查询结果数据
                foreach (DataRow row in table .Rows)
                {
                    DictionaryPara p = new MySQLTools.DictionaryPara(param.tablename);
                    //Colume用来存储查询的字段
                    foreach (DataColumn col in table .Columns)
                    {
                        p.Add(col.ColumnName, row[col.ColumnName].ToString());
                    }
                    dp.Add(p);
                }
                return dp;
            }
            return null;
        }
    }
}
