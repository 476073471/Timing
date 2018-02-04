using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Miro.Lu.Timing.Core
{
    /// <summary>
    /// 公共帮助类
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 配置存放路径（我的文档中）
        /// </summary>
        private static readonly string PersonalPaht = Environment.GetFolderPath(Environment.SpecialFolder.Personal)+ "/MiroTreasureChest/Config";

        /// <summary>
        /// 保存配置
        /// </summary>
        public static bool SaveConfig<T>(T config, ConfigEnum configName)
        {
            try
            {
                //将配置转换为JOSN字符串
                string json = JsonConvert.SerializeObject(config);
                if (!Directory.Exists(PersonalPaht))
                {
                    Directory.CreateDirectory(PersonalPaht);
                }
                File.WriteAllText($"{PersonalPaht}/{configName}.cfg", json, Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        public static T ReadConfig<T>(ConfigEnum configName)
        {
            try
            {
                var path = $"{PersonalPaht}/{configName}.cfg";
                if (File.Exists(path))
                {
                    string jsonConfig = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<T>(jsonConfig);
                }
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }
    }

    /// <summary>
    /// 配置文件名枚举
    /// </summary>
    public enum ConfigEnum
    {
        TaskConfig
    };
}
