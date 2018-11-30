using System;
using System.Collections.Generic;
using System.Text;

namespace GameProtocol.model.login
{
    /// <summary>
    /// 注册返回结果
    /// 消息体要加Serializable，表示可被序列化，不然会报错
    /// </summary>
    [Serializable]//不加，无法序列化,则无法进行数据传输
    public class ResponseRegisterModel
    {
        /// <summary>
        /// 定义一个注册结果的状态码
        /// 0 成功  -1失败
        /// </summary>
        public int Status = 0;

        /// <summary>
        /// 返回给客户端一个加密后的密码
        /// </summary>
        public string password = "";


    }
}
