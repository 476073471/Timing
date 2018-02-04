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
                //设置内容颜色
                if (!string.IsNullOrEmpty(TaskConfig.ContentColor))
                {
                    var fontColor = ColorConverter.ConvertFromString(TaskConfig.ContentColor);
                    TxtAlertContent.Foreground = new SolidColorBrush((Color?)fontColor ?? Colors.Black);
                }

                double a = Height - Height * 0.8;
                //设置按钮位置
                ConfrimBorder.Margin = new Thickness(Width * 0.4, Height * 0.4 + a, Width * 0.4, Height * 0.4 - a);
            }
            else
            {
                //设置按钮位置
                ConfrimBorder.Margin = new Thickness(Width * 0.4, Height * 0.4, Width * 0.4, Height * 0.4);
            }


        }

        #region 事件

        /// <summary>
        /// 确定按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfrimBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 确定按钮移入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfrimBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background.Opacity = 0.7;
            }
        }

        /// <summary>
        /// 确定按钮移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfrimBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background.Opacity = 0.5;
            }
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            ConfirmFun?.Invoke();
        }

        /// <summary>
        /// 组合键关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.C) && Keyboard.IsKeyDown(Key.L) && Keyboard.IsKeyDown(Key.S))
            {
                Close();
            }
        }

        #endregion


    }
}
