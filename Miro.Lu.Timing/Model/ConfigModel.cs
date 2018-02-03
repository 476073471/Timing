using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Lu.Timing.Model
{
    /// <summary>
    /// 我的任务配置
    /// </summary>
    public class TaskConfigModel
    {
        /// <summary>
        /// 间隔时间
        /// </summary>
        public int IntervalTime { get; set; } = 10;

        /// <summary>
        /// 间隔类型（1-时；2-分；3-秒）
        /// </summary>
        public int IntervalType { get; set; } = 2;

        /// <summary>
        /// 弹窗内容
        /// </summary>
        public string AlertContent { get; set; }

        /// <summary>
        /// 弹框大小类型（1-全屏；2-自定义）
        /// </summary>
        public int CustAlertType { get; set; }

        /// <summary>
        /// 自定义弹框宽度
        /// </summary>
        public int CustAlertWidth { get; set; }

        /// <summary>
        /// 自定义弹框高度
        /// </summary>
        public int CustAlertHeight { get; set; }

        /// <summary>
        /// 弹窗背景（1,2,3,4）
        /// </summary>
        public int AlertBackgroud { get; set; }

        /// <summary>
        /// 自定义弹窗图片路径
        /// </summary>
        public string CustAlertBackgroudPath { get; set; }
    }
}
