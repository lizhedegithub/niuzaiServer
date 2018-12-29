using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace MySQLTools
{
    /// <summary>
    /// 数据库工具类
    /// internal 内部修饰符，在工程内公开，在工程外私有
    /// </summary>
    internal class MySQLUtil
    {
        /// <summary>
        /// 数据库连接对象
        /// 操作界面是永久连接，代码是临时连接
        /// </summary>
        MySqlConnection SqlConnection;
        /// <summary>
        /// 连接名/地址
        /// localhost     本机地址
        /// 192.168.x.xx  局域网地址
        /// 120.xx.xx.xx  公网地址
        /// </summary>
        string host = "localhost";
        /// <summary>
        /// 端口号
        /// </summary>
        int port = 3306;
        /// <summary>
        /// 数据库名字
        /// </summary>
        string database = "niuzai";
        /// <summary>
        /// 用户名
        /// </summary>
        string username = "root";
        /// <summary>
        /// 连接密码
        /// </summary>
        string password = "newpasswd";

        public MySQLUtil() { }

        /// <summary>
        /// 从外部传入数据
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="database"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void InitData(string host, int port, string database, string username, string password)
        {
            this.host = host;
            this.port = port;
            this.database = database;
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        public void Connect()
        {
            try
            {
                Close();
                //server=127.0.0.1;port=3306;database=niuzai;uid=root;psd=newpasswd;
                string connectionString = "server=" + host + ";port=" + port + ";database=" + database + ";uid=" + username + ";pwd=" + password + ";";
                SqlConnection = new MySqlConnection(connectionString);
                SqlConnection.Open();
                if (SqlManager.Instance.DEBUG != null)
                    SqlManager.Instance.DEBUG("数据库打开成功");
            }
            catch (Exception e)
            {
                if (SqlManager.Instance.ERROR != null)
                    SqlManager.Instance.ERROR("打开数据库失败" + e);
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if(SqlConnection !=null)
            {
                //关闭
                SqlConnection.Close();
                //释放
                SqlConnection.Dispose();
                //赋空值
                SqlConnection = null;
            }
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="value">数据</param>
        public int InsertData(string tablename,string[] value)
        {
            //<id,2><name,ssss><year,18>
            //insert into test values('2','fei','3');
            //查询语句
            string query = "insert into " + tablename + " values(";
            for (int i = 0; i < value .Length-1; i++)
            {
                query += "'" + value[i] + "',";
            }
            query += "'" + value[value.Length - 1] + "'";
            query += ");";
            return ExNonQuery(query);
        }

        /// <summary>
        /// 根据字段名插入一条语句
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="value">数据</param>
        /// <param name="cols">字段</param>
        /// <returns></returns>
        public int InsertData(string tablename, string[] value,string [] cols)
        {
            //insert into test values('2','fei','3');
            //查询语句
            //insert into (字段) values('增加数据');
            //可调换顺序,可不用全写
            //insert into (id,name,year) values('2','fei','3');
            //insert into (id,year,name) values('2','3','fei');
            //insert into (id,name) values('2','fei');
            string query = "insert into " + tablename + " (";
            for (int i = 0; i < value.Length - 1; i++)
            {
                query += cols[i]+",";
            }
            query += cols[value.Length - 1] +") values(";
            for (int i = 0; i < value.Length - 1; i++)
            {
                query += "'" + value[i] + "',";
            }
            query += "'" + value[value.Length - 1] + "');";
            return ExNonQuery(query);
        }

        /// <summary>
        /// 删除一行数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="col">字段名</param>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public int DeleteData(string tablename ,string col,string value) {
            //根据传入的字段对应的值，删除一行数据
            //delete from 表名 where 字段名=‘数据’;
            //delete from test where id='1';
            //删除语句
            string query = "delete from " + tablename + " where " + col + " ='" + value + "';";
            return ExNonQuery(query);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="cols">要修改的字段名</param>
        /// <param name="value">要修改的值</param>
        /// <param name="wherecol">搜寻字段名</param>
        /// <param name="wherevalue">搜寻值</param>
        /// <returns></returns>
        public int UpdateData(string tablename,string [] cols,string [] value,string wherecol,string wherevalue) {
            //1 根据表名找到数据表                test  (tablename)
            //2 根据搜寻的字段名找到数据列         id(wherecol)
            //3 根据搜寻值找到数据行               1(wherevalue)
            //4 根据要修改的字段名找到其他数据列    name,year(cols)
            //5 根据数据行和其他数据列更改数据      aaaa,28(value)
            //修改的sql语句
            //update 表名 set 字段=‘数据’ where 搜寻字段=‘搜寻值’
            //update test set name='aa',year='22' where id='1';
            string query = "update " + tablename + " set ";
            for (int i = 0; i < cols .Length-1 ; i++)
            {
                query += cols[i] + "='" + value[i] + "',";
            }
            query += cols[cols.Length - 1] + "='" + value[cols.Length - 1] + "'";
            query += "where " + wherecol + " = '" + wherevalue + "';";
            return ExNonQuery(query);
        }

        /// <summary>
        /// 查询语句
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="cols">字段名</param>
        /// <param name="wherecol">搜寻的字段名</param>
        /// <param name="wherevalue">搜寻的数据</param>
        /// <returns></returns>
        public DataSet SelectData(string tablename,string [] cols,string wherecol,string wherevalue) {
            //根据搜寻的字段和数据，查找同行其他字段名的数据
            //select 字段名 from 表名 where 字段=‘数据’;
            //在不做强制性规定下，MySQL中所以指令，字段，表名等等都不做大小写区分,数据区分（wherevalue）
            //select name,year from test where id='2';
            //SELECT name,year FROM test WHERE id='2';
            //查询sql语句
            string query = "select ";
            for (int i = 0; i < cols .Length -1; i++)
            {
                query += cols[i] + ",";
            }
            query += cols[cols.Length - 1] + " from " + tablename + " where " + wherecol + " = '" + wherevalue + "';";
            return ExQuery (query);
        }

        /// <summary>
        /// 查询某列所有的值
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public DataSet SelectColumn(string tablename,string[] cols)
        {
            //根据某表，获取一列的数据
            //select （字段，字段，字段） from 表名
            //select id from test
            //组装sql语句
            string query = "select ";
            for (int i = 0; i < cols .Length -1; i++)
            {
                query += cols[i] + ",";
            }
            query += cols[cols.Length - 1] + " from " + tablename + ";";
            return ExQuery(query); 
        }

        /// <summary>
        /// 查找某一列最后的值
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public DataSet SelectMaxRow(string tablename,string col)
        {
            //根据某表，某列获取最后一行的数据
            //select max（字段） from 表名
            //select max（id） from test
            //组装sql语句
            string query = "select max(" + col + ") from " + tablename+";";
            return ExQuery(query);
        }

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        DataSet ExQuery(string sqlQuery)
        {
            //不使用ServerTools工具中的打印
            //DebugUtil.Instance.LogToTime("ExQuery:" + sqlQuery);
            //SqlManager.Instance.DEBUG?.Invoke("ExQuery:" + sqlQuery);
            if (SqlManager.Instance.DEBUG != null)
                SqlManager.Instance.DEBUG("ExQuery:" + sqlQuery);
            //判断数据库连接是否打开
            if (SqlConnection !=null && SqlConnection .State == ConnectionState.Open)
            {
                //执行的结果
                DataSet dataset = new DataSet();
                try
                {
                    //建立一个MYSQL数据适配器,来进行查询语句的执行
                    MySqlDataAdapter sql = new MySqlDataAdapter(sqlQuery, SqlConnection);
                    sql.Fill(dataset);
                }catch (Exception e)
                {
                    if (SqlManager.Instance.ERROR != null)
                        SqlManager.Instance.ERROR(sqlQuery + "Error:" + e);
                    Connect();
                    return null;
                }
                return dataset;
            }
            return null;
        }

        /// <summary>
        /// 执行非查询语句
        /// </summary>
        /// <param name="sqlNonQuery"></param>
        /// <returns>返回受到影响的记录数</returns>
        int ExNonQuery(string sqlNonQuery)
        {
            if (SqlManager.Instance.DEBUG != null)
                SqlManager.Instance.DEBUG("NonQuery:" + sqlNonQuery);
            if(SqlConnection !=null && SqlConnection .State == ConnectionState.Open)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqlNonQuery, SqlConnection);
                    //执行非查询语句，并返回影响记录数
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }catch (Exception e)
                {
                    if (SqlManager.Instance.ERROR != null)
                        SqlManager.Instance.ERROR(e);
                    Connect();
                }
            }
            return 0;
        }
    }
}
