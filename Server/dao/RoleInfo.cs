using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySQLTools;

/*****************************************************
 * 本脚本由MySQL代码自动生成工具自动生成
 * 生成日期:2018年6月29日14时21分23秒
 * 文件描述:玩家的数据库所对应的信息
 * 文件名:roleinfo.cs
 * 创建人:
 ****************************************************/
namespace Server.dao
{
    public partial class roleinfo
    {
        private string tablename = "roleinfo";
        #region Model

        private int _id = -1;
        private string _username;
        private string _password;
        private string _nickname;
        private string _head;
        private int _coin =1000;
        private int _cash;
        private int _sex;
        private string _phone;
        private int _rank;

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string head
        {
            set { _head = value; }
            get { return _head; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int coin
        {
            set { _coin = value; }
            get { return _coin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int cash
        {
            set { _cash = value; }
            get { return _cash; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int rank
        {
            set { _rank = value; }
            get { return _rank; }
        }

        #endregion Model

        #region Method
        public roleinfo() { }

        #region 获取对象
        /// <summary>
        /// 根据 Id 得到一个对象实体
        /// </summary>
        public void GetModelById(int id)
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            p.Add("username", null);
            p.Add("password", null);
            p.Add("nickname", null);
            p.Add("head", null);
            p.Add("coin", null);
            p.Add("cash", null);
            p.Add("sex", null);
            p.Add("phone", null);
            p.Add("rank", null);

            SqlManager.Instance.GetTable(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["id"]))
                { this.id = int.Parse(p["id"]);
                }
                if (!string.IsNullOrEmpty(p["username"]))
                { this.username = p["username"];
                }
                if (!string.IsNullOrEmpty(p["password"]))
                { this.password = p["password"];
                }
                if (!string.IsNullOrEmpty(p["nickname"]))
                { this.nickname = p["nickname"];
                }
                if (!string.IsNullOrEmpty(p["head"]))
                { this.head = p["head"];
                }
                if (!string.IsNullOrEmpty(p["coin"]))
                { this.coin = int.Parse(p["coin"]);
                }
                if (!string.IsNullOrEmpty(p["cash"]))
                { this.cash = int.Parse(p["cash"]);
                }
                if (!string.IsNullOrEmpty(p["sex"]))
                { this.sex = int.Parse(p["sex"]);
                }
                if (!string.IsNullOrEmpty(p["phone"]))
                { this.phone = p["phone"];
                }
                if (!string.IsNullOrEmpty(p["rank"]))
                { this.rank = int.Parse(p["rank"]);
                }
            }
        }
        /// <summary>
        /// 根据 Username 得到一个对象实体
        /// </summary>
        public void GetModelByUsername(string username)
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("username", username);
            p.Add("id", null);
            p.Add("password", null);
            p.Add("nickname", null);
            p.Add("head", null);
            p.Add("coin", null);
            p.Add("cash", null);
            p.Add("sex", null);
            p.Add("phone", null);
            p.Add("rank", null);

            SqlManager.Instance.GetTable(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["id"]))
                { this.id = int.Parse(p["id"]);
                }
                if (!string.IsNullOrEmpty(p["username"]))
                { this.username = p["username"];
                }
                if (!string.IsNullOrEmpty(p["password"]))
                { this.password = p["password"];
                }
                if (!string.IsNullOrEmpty(p["nickname"]))
                { this.nickname = p["nickname"];
                }
                if (!string.IsNullOrEmpty(p["head"]))
                { this.head = p["head"];
                }
                if (!string.IsNullOrEmpty(p["coin"]))
                { this.coin = int.Parse(p["coin"]);
                }
                if (!string.IsNullOrEmpty(p["cash"]))
                { this.cash = int.Parse(p["cash"]);
                }
                if (!string.IsNullOrEmpty(p["sex"]))
                { this.sex = int.Parse(p["sex"]);
                }
                if (!string.IsNullOrEmpty(p["phone"]))
                { this.phone = p["phone"];
                }
                if (!string.IsNullOrEmpty(p["rank"]))
                { this.rank = int.Parse(p["rank"]);
                }
            }
        }
        /// <summary>
        /// 根据 Nickname 得到一个对象实体
        /// </summary>
        public void GetModelByNickname(string nickname)
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("nickname", nickname);
            p.Add("id", null);
            p.Add("username", null);
            p.Add("password", null);
            p.Add("head", null);
            p.Add("coin", null);
            p.Add("cash", null);
            p.Add("sex", null);
            p.Add("phone", null);
            p.Add("rank", null);

            SqlManager.Instance.GetTable(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["id"]))
                { this.id = int.Parse(p["id"]);
                }
                if (!string.IsNullOrEmpty(p["username"]))
                { this.username = p["username"];
                }
                if (!string.IsNullOrEmpty(p["password"]))
                { this.password = p["password"];
                }
                if (!string.IsNullOrEmpty(p["nickname"]))
                { this.nickname = p["nickname"];
                }
                if (!string.IsNullOrEmpty(p["head"]))
                { this.head = p["head"];
                }
                if (!string.IsNullOrEmpty(p["coin"]))
                { this.coin = int.Parse(p["coin"]);
                }
                if (!string.IsNullOrEmpty(p["cash"]))
                { this.cash = int.Parse(p["cash"]);
                }
                if (!string.IsNullOrEmpty(p["sex"]))
                { this.sex = int.Parse(p["sex"]);
                }
                if (!string.IsNullOrEmpty(p["phone"]))
                { this.phone = p["phone"];
                }
                if (!string.IsNullOrEmpty(p["rank"]))
                { this.rank = int.Parse(p["rank"]);
                }
            }
        }
        /// <summary>
        /// 根据 Phone 得到一个对象实体
        /// </summary>
        public void GetModelByPhone(string phone)
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("phone", phone);
            p.Add("id", null);
            p.Add("username", null);
            p.Add("password", null);
            p.Add("nickname", null);
            p.Add("head", null);
            p.Add("coin", null);
            p.Add("cash", null);
            p.Add("sex", null);
            p.Add("rank", null);

            SqlManager.Instance.GetTable(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["id"]))
                { this.id = int.Parse(p["id"]);
                }
                if (!string.IsNullOrEmpty(p["username"]))
                { this.username = p["username"];
                }
                if (!string.IsNullOrEmpty(p["password"]))
                { this.password = p["password"];
                }
                if (!string.IsNullOrEmpty(p["nickname"]))
                { this.nickname = p["nickname"];
                }
                if (!string.IsNullOrEmpty(p["head"]))
                { this.head = p["head"];
                }
                if (!string.IsNullOrEmpty(p["coin"]))
                { this.coin = int.Parse(p["coin"]);
                }
                if (!string.IsNullOrEmpty(p["cash"]))
                { this.cash = int.Parse(p["cash"]);
                }
                if (!string.IsNullOrEmpty(p["sex"]))
                { this.sex = int.Parse(p["sex"]);
                }
                if (!string.IsNullOrEmpty(p["phone"]))
                { this.phone = p["phone"];
                }
                if (!string.IsNullOrEmpty(p["rank"]))
                { this.rank = int.Parse(p["rank"]);
                }
            }
        }
        #endregion 获取对象

        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            if (!string.IsNullOrEmpty(username)) p.Add("username", username);
            if (!string.IsNullOrEmpty(password)) p.Add("password", password);
            if (!string.IsNullOrEmpty(nickname)) p.Add("nickname", nickname);
            if (!string.IsNullOrEmpty(head)) p.Add("head", head);
            p.Add("coin", coin);
            p.Add("cash", cash);
            p.Add("sex", sex);
            if (!string.IsNullOrEmpty(phone)) p.Add("phone", phone);
            p.Add("rank", rank);

            return SqlManager.Instance.AddTableAtItems(p);
        }
        #endregion 增加一条数据

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);

            return SqlManager.Instance.Delete(p);
        }
        #endregion 删除一条数据

        #region 刷新值
        /// <summary>
        /// 根据主键刷新全部的值
        /// </summary>
        public bool Update()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            if (!string.IsNullOrEmpty(username))
                p.Add("username", username);
            if (!string.IsNullOrEmpty(password))
                p.Add("password", password);
            if (!string.IsNullOrEmpty(nickname))
                p.Add("nickname", nickname);
            if (!string.IsNullOrEmpty(head))
                p.Add("head", head);
            p.Add("coin", coin);
            p.Add("cash", cash);
            p.Add("sex", sex);
            if (!string.IsNullOrEmpty(phone))
                p.Add("phone", phone);
            p.Add("rank", rank);

            return SqlManager.Instance.UpdateInItems(p);
        }
        /// <summary>
        /// 根据主键刷新 Username 的值
        /// </summary>
        public bool UpdateByUsername()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            if (!string.IsNullOrEmpty(username))
                p.Add("username", username);

            return SqlManager.Instance.UpdateInItems(p);
        }
        /// <summary>
        /// 根据主键刷新 Password 的值
        /// </summary>
        public bool UpdateByPassword()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            if (!string.IsNullOrEmpty(password))
                p.Add("password", password);

            return SqlManager.Instance.UpdateInItems(p);
        }
        /// <summary>
        /// 根据主键刷新 Nickname 的值
        /// </summary>
        public bool UpdateByNickname()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            if (!string.IsNullOrEmpty(nickname))
                p.Add("nickname", nickname);

            return SqlManager.Instance.UpdateInItems(p);
        }
        /// <summary>
        /// 根据主键刷新 Head 的值
        /// </summary>
        public bool UpdateByHead()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            if (!string.IsNullOrEmpty(head))
                p.Add("head", head);

            return SqlManager.Instance.UpdateInItems(p);
        }
        /// <summary>
        /// 根据主键刷新 Coin 的值
        /// </summary>
        public bool UpdateByCoin()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            p.Add("coin", coin);

            return SqlManager.Instance.UpdateInItems(p);
        }
        /// <summary>
        /// 根据主键刷新 Cash 的值
        /// </summary>
        public bool UpdateByCash()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            p.Add("cash", cash);

            return SqlManager.Instance.UpdateInItems(p);
        }
        /// <summary>
        /// 根据主键刷新 Sex 的值
        /// </summary>
        public bool UpdateBySex()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            p.Add("sex", sex);

            return SqlManager.Instance.UpdateInItems(p);
        }
        /// <summary>
        /// 根据主键刷新 Phone 的值
        /// </summary>
        public bool UpdateByPhone()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            if (!string.IsNullOrEmpty(phone))
                p.Add("phone", phone);

            return SqlManager.Instance.UpdateInItems(p);
        }
        /// <summary>
        /// 根据主键刷新 Rank 的值
        /// </summary>
        public bool UpdateByRank()
        {
            DictionaryPara p = new DictionaryPara(tablename, true);
            p.Add("id", id);
            p.Add("rank", rank);

            return SqlManager.Instance.UpdateInItems(p);
        }
        #endregion 刷新值

        #region 获取列全部值
        /// <summary>
        /// 根据 Id 获取列全部值的值
        /// </summary>
        public List<int> GetRowById()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("id", id);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<int> list = new List<int>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["id"]))
                    list.Add(int.Parse(dp[i]["id"]));
            return list;
        }
        /// <summary>
        /// 根据 Username 获取列全部值的值
        /// </summary>
        public List<string> GetRowByUsername()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("username", username);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<string> list = new List<string>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["username"]))
                    list.Add(dp[i]["username"]);
            return list;
        }
        /// <summary>
        /// 根据 Password 获取列全部值的值
        /// </summary>
        public List<string> GetRowByPassword()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("password", password);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<string> list = new List<string>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["password"]))
                    list.Add(dp[i]["password"]);
            return list;
        }
        /// <summary>
        /// 根据 Nickname 获取列全部值的值
        /// </summary>
        public List<string> GetRowByNickname()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("nickname", nickname);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<string> list = new List<string>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["nickname"]))
                    list.Add(dp[i]["nickname"]);
            return list;
        }
        /// <summary>
        /// 根据 Head 获取列全部值的值
        /// </summary>
        public List<string> GetRowByHead()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("head", head);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<string> list = new List<string>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["head"]))
                    list.Add(dp[i]["head"]);
            return list;
        }
        /// <summary>
        /// 根据 Coin 获取列全部值的值
        /// </summary>
        public List<int> GetRowByCoin()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("coin", coin);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<int> list = new List<int>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["coin"]))
                    list.Add(int.Parse(dp[i]["coin"]));
            return list;
        }
        /// <summary>
        /// 根据 Cash 获取列全部值的值
        /// </summary>
        public List<int> GetRowByCash()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("cash", cash);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<int> list = new List<int>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["cash"]))
                    list.Add(int.Parse(dp[i]["cash"]));
            return list;
        }
        /// <summary>
        /// 根据 Sex 获取列全部值的值
        /// </summary>
        public List<int> GetRowBySex()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("sex", sex);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<int> list = new List<int>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["sex"]))
                    list.Add(int.Parse(dp[i]["sex"]));
            return list;
        }
        /// <summary>
        /// 根据 Phone 获取列全部值的值
        /// </summary>
        public List<string> GetRowByPhone()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("phone", phone);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<string> list = new List<string>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["phone"]))
                    list.Add(dp[i]["phone"]);
            return list;
        }
        /// <summary>
        /// 根据 Rank 获取列全部值的值
        /// </summary>
        public List<int> GetRowByRank()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("rank", rank);

            List<DictionaryPara> dp = SqlManager.Instance.GetColume(p);
            List<int> list = new List<int>();
            if (dp == null) return list; for (int i = 0; i < dp.Count; i++)
                if (!string.IsNullOrEmpty(dp[i]["rank"]))
                    list.Add(int.Parse(dp[i]["rank"]));
            return list;
        }
        #endregion 获取列全部值

        #region 根据key获取数据表最后一行的值
        /// <summary>
        /// 根据 Id 获取本列数据表最后一行的值
        /// </summary>
        public int GetEndLineValueById()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("id", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["id"]))
                {
                    return int.Parse(p["id"]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 根据 Username 获取本列数据表最后一行的值
        /// </summary>
        public string GetEndLineValueByUsername()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("username", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["username"]))
                {
                    return p["username"];
                }
            }
            return null;
        }
        /// <summary>
        /// 根据 Password 获取本列数据表最后一行的值
        /// </summary>
        public string GetEndLineValueByPassword()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("password", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["password"]))
                {
                    return p["password"];
                }
            }
            return null;
        }
        /// <summary>
        /// 根据 Nickname 获取本列数据表最后一行的值
        /// </summary>
        public string GetEndLineValueByNickname()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("nickname", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["nickname"]))
                {
                    return p["nickname"];
                }
            }
            return null;
        }
        /// <summary>
        /// 根据 Head 获取本列数据表最后一行的值
        /// </summary>
        public string GetEndLineValueByHead()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("head", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["head"]))
                {
                    return p["head"];
                }
            }
            return null;
        }
        /// <summary>
        /// 根据 Coin 获取本列数据表最后一行的值
        /// </summary>
        public int GetEndLineValueByCoin()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("coin", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["coin"]))
                {
                    return int.Parse(p["coin"]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 根据 Cash 获取本列数据表最后一行的值
        /// </summary>
        public int GetEndLineValueByCash()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("cash", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["cash"]))
                {
                    return int.Parse(p["cash"]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 根据 Sex 获取本列数据表最后一行的值
        /// </summary>
        public int GetEndLineValueBySex()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("sex", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["sex"]))
                {
                    return int.Parse(p["sex"]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 根据 Phone 获取本列数据表最后一行的值
        /// </summary>
        public string GetEndLineValueByPhone()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("phone", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["phone"]))
                {
                    return p["phone"];
                }
            }
            return null;
        }
        /// <summary>
        /// 根据 Rank 获取本列数据表最后一行的值
        /// </summary>
        public int GetEndLineValueByRank()
        {
            DictionaryPara p = new DictionaryPara(tablename);
            p.Add("rank", null);

            SqlManager.Instance.GetEndLineValue(ref p);
            if (p != null && p.Count > 0)
            {
                if (!string.IsNullOrEmpty(p["rank"]))
                {
                    return int.Parse(p["rank"]);
                }
            }
            return 0;
        }
        #endregion 根据key获取数据表最后一行的值

        #endregion Method

    }
}
