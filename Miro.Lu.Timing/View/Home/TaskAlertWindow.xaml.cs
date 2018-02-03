using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Miro.Lu.Timing.Model;

namespace Miro.Lu.Timing.View.Home
{
    /// <summary>
    /// TaskAlertWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TaskAlertWindow : Window
    {
        /// <summary>
        /// 确认回调
        /// </summary>
        public Action ConfirmFun { get; set; }

        //弹框配置
        public TaskConfigModel TaskConfig { get; set; }

        public TaskAlertWindow(TaskConfigModel taskConfig)
        {
            InitializeComponent();
            TaskConfig = taskConfig;
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //设置宽和高
            if (TaskConfig.CustAlertType == (int) TaskAlertTpyeEnum.Full)
            {
                Topmost = true;
                WindowState = WindowState.Maximized;
            }
            else
            {
                //设置最小宽高
                var width = TaskConfig.CustAlertWidth > SystemParameters.WorkArea.Size.Width
                    ? SystemParameters.WorkArea.Size.Width
                    : TaskConfig.CustAlertWidth;
                var height = TaskConfig.CustAlertHeight > SystemParameters.WorkArea.Size.Height
                    ? SystemParameters.WorkArea.Size.Height
                    : TaskConfig.CustAlertHeight;
                width = width < 50 ? 50 : width;
                height = height < 50 ? 50 : height;
                Width = width;
                Height = height;
            }

            //设置背景图片
            ImgBackground.Source = new BitmapImage(new Uri(TaskConfig.AlertBackgroudPath, UriKind.Absolute));

            //设置内容
            if (!string.IsNullOrEmpty(TaskConfig.AlertContent))
            {
                ContentBorder.Visibility = Visibility.Visible;
                ContentBorder.Margin = new Thickness(Width * 0.2, Height * 0.4, Width * 0.2, Height * 0.4);
                TxtAlertContent.Text = TaskConfig.AlertContent;
            }
            
        }
    }
}
