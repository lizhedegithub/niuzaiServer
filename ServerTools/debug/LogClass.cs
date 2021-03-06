﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTools
{
    public class LogClass
    {
        /// <summary>
        /// 待输出的日志消息
        /// </summary>
        public object msg = "";
        /// <summary>
        /// 待输出的日志级别
        /// </summary>
        public LogType type = LogType.DEBUG;

        public LogClass() { }
        public LogClass(object Msg)
        {
            this.msg = Msg;
        }
        public LogClass(object Msg,LogType Type)
        {
            this.msg = Msg;
            this.type = Type;
        }

    }
}
