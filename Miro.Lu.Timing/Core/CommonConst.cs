using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Lu.Timing.Core
{
    public class CommonConst
    {
        public static readonly string BasePath = "/Miro.Lu.Timing;";

        //按钮图片路径
        public static Dictionary<string, string> ImgDic = new Dictionary<string, string> {
            //按钮图片
            { "btnTask", "component/Resources/Button/btnTask.png" },
            { "btnTask_hover", "component/Resources/Button/btnTask_hover.png" },
            { "btnTask_dark", "component/Resources/Button/btnTask_dark.png" },
            { "btnConfig", "component/Resources/Button/btnConfig.png" },
            { "btnConfig_hover", "component/Resources/Button/btnConfig_hover.png" },
            { "btnConfig_dark", "component/Resources/Button/btnConfig_dark.png" },
            { "btnExit", "component/Resources/Button/btnExit.png" },
            { "btnExit_hover", "component/Resources/Button/btnExit_hover.png" },
            { "btnExit_dark", "component/Resources/Button/btnExit_dark.png" },
            { "btnSwitch", "component/Resources/Button/btnSwitch.png" },
            { "btnSwitch_hover", "component/Resources/Button/btnSwitch_hover.png" },
            { "btnSwitch_dark", "component/Resources/Button/btnSwitch_dark.png" },

            //家具图片
            { "furDesk", "component/Resources/Furniture/furDesk.png" },
            { "furDesk_dark", "component/Resources/Furniture/furDesk_dark.png" },
            { "furWindow", "component/Resources/Furniture/furWindow.png" },
            { "furWindow_dark", "component/Resources/Furniture/furWindow_dark.png" },
            { "furLight", "component/Resources/Furniture/furLight.png" },
            { "furLight_dark", "component/Resources/Furniture/furLight_dark.png" }
        };

        /// <summary>
        /// 获取按钮图片路径
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetImgUrl(string key)
        {
            if (ImgDic.ContainsKey(key))
            {
                return BasePath + ImgDic[key];
            }
            return string.Empty;
        }

        //弹框图片路径
        public static readonly string DiaButton = BasePath + "component/Resources/Dialog/diaButton.png";
        public static readonly string DiaButton_hover = BasePath + "component/Resources/Dialog/diaButton_hover.png";
    }
}
