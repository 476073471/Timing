using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Lu.Timing.Core
{
    /// <summary>
    /// 公共拓展方法类
    /// </summary>
    public static class CommonExtend
    {
        public static int ToInt32(this Object val, int defaultValue = 0)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
