using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTools
{
    /// <summary>
    /// 计时器任务模型
    /// </summary>
    public class TimeTaskModel
    {
        /// <summary>
        /// 任务id
        /// </summary>
        public int ID;

        /// <summary>
        /// 任务时间
        /// </summary>
        public TimeTask Event;

        /// <summary>
        /// 执行时间  单位ms
        /// </summary>
        public long Time;

        public TimeTaskModel() { }
        public TimeTaskModel(int id,TimeTask timeEvent,long Ttime) {
            this.ID = id;
            this.Event = timeEvent;
            this.Time = Ttime;
        }

        /// <summary>
        /// 执行函数
        /// </summary>
        public void Run()
        {

        }
    }
}
