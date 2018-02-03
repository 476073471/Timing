using Miro.Lu.Timing.Core;
using Miro.Lu.Timing.View.Component;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Miro.Lu.Timing.Model;

namespace Miro.Lu.Timing.View.Childs
{

    /// <summary>
    /// MyTask.xaml 的交互逻辑
    /// </summary>
    public partial class MyTask : UserControl
    {
        #region 私有变量

        /// <summary>
        /// 定时器
        /// </summary>
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        #endregion

        public MyTask()
        {
            InitializeComponent();
            _timer.Tick += Timer_Tick;
            InitConfig();
        }

        #region 私有方法

        /// <summary>
        /// 初始化配置
        /// </summary>
        private void InitConfig()
        {
            //读取配置
            var config = CommonHelper.ReadConfig<TaskConfigModel>(ConfigEnum.TaskConfig);
            if (config == null)
            {
                TxtCustWidth.Text = SystemParameters.PrimaryScreenWidth.ToString("0");
                TxtCustHeight.Text = SystemParameters.PrimaryScreenHeight.ToString("0");
                return;
            }
            TxtIntervalTime.Text = config.IntervalTime.ToString();
            TxtAlertContent.Text = config.AlertContent;
            TxtCustWidth.Text = config.CustAlertWidth.ToString();
            TxtCustHeight.Text = config.CustAlertHeight.ToString();

            //加载自定义弹窗背景
            if (!string.IsNullOrEmpty(config.CustAlertBackgroudPath))
            {
                Uri uri = new Uri(config.CustAlertBackgroudPath, UriKind.Absolute);
                ImageBrush ib = new ImageBrush {ImageSource = new BitmapImage(uri)};
                RadioCustImg.Background = ib;
                RadioCustImg.Visibility = Visibility.Visible;
            }

            foreach (var mainGridChild in MainGrid.Children)
            {
                //时间间隔
                if (mainGridChild is RadioButton radioTime && radioTime.GroupName == "rbSetTime" &&
                    radioTime.Tag.ToInt32() == config.IntervalType)
                {
                    radioTime.IsChecked = true;
                }

                //弹框大小
                if (mainGridChild is RadioButton radioSize && radioSize.GroupName == "rbAlertSize" &&
                    radioSize.Tag.ToInt32() == config.CustAlertType)
                {
                    radioSize.IsChecked = true;
                }

                //背景图片
                if (mainGridChild is RadioButton radioImg && radioImg.GroupName == "rbBackgroudImg" &&
                    radioImg.Tag.ToInt32() == config.AlertBackgroud)
                {
                    radioImg.IsChecked = true;
                }
            }
        }

        /// <summary>
        /// 获取单选按钮选中的值
        /// </summary>
        /// <param name="groupName">控件所属组</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        private string GetSelectRadio(string groupName,string defaultValue)
        {
            foreach (var mainGridChild in MainGrid.Children)
            {
                //时间间隔
                if (mainGridChild is RadioButton radio && radio.GroupName == groupName)
                {
                    if(radio.IsChecked != null && (bool) radio.IsChecked)
                    return radio.Tag.ToString();
                }
            }
            return defaultValue;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 保存任务配置
        /// </summary>
        public void SaveTaskConfig()
        {
            var config = new TaskConfigModel
            {
                IntervalTime = TxtIntervalTime.Text.ToInt32(10),
                AlertContent = TxtAlertContent.Text,
                CustAlertWidth = TxtCustWidth.Text.ToInt32(SystemParameters.PrimaryScreenWidth.ToInt32()),
                CustAlertHeight = TxtCustHeight.Text.ToInt32(SystemParameters.PrimaryScreenHeight.ToInt32()),
                IntervalType = GetSelectRadio("rbSetTime", "2").ToInt32(),
                CustAlertType = GetSelectRadio("rbAlertSize", "1").ToInt32(),
                AlertBackgroud = GetSelectRadio("rbBackgroudImg", "1").ToInt32(),
                CustAlertBackgroudPath = ""
            };
            CommonHelper.SaveConfig(config, ConfigEnum.TaskConfig);
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 开启定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //TODO 保存配置数据

            //获取弹框间隔类型
            var intervalType = GetSelectRadio("rbSetTime", "2").ToInt32(2);
            var intervalTime = TxtIntervalTime.Text.ToInt32(10);
            _timer.Interval = new TimeSpan(intervalType == 1 ? intervalTime : 0,
                intervalType == 2 ? intervalTime : 0,
                intervalType == 3 ? intervalTime : 0);
            _timer.Start();
            var dialog = new DialogConfirm("开启成功");
            dialog.ConfirmFun = () => MainGrid.Children.Remove(dialog);
            MainGrid.Children.Add(dialog);
        }

        /// <summary>
        /// 停止定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _timer.Stop();
            var dialog = new DialogConfirm("计时已停止");
            dialog.ConfirmFun = () => MainGrid.Children.Remove(dialog);
            MainGrid.Children.Add(dialog);
        }

        /// <summary>
        /// 预览窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreview_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 定时器回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("1");
        }

        /// <summary>
        /// 鼠标移入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHover_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Image img)
            {
                TxtHide.Focus();
                img.Source = new BitmapImage(new Uri(CommonConst.DiaButton_hover, UriKind.Relative));
            }
        }

        /// <summary>
        /// 鼠标移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHover_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Image img) img.Source = new BitmapImage(new Uri(CommonConst.DiaButton, UriKind.Relative));
        }

        /// <summary>
        /// 屏蔽非法按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //屏蔽非法按键
            if (e.Key == Key.Back || e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9 && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 失去焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox text)
            {
                if (!string.IsNullOrEmpty(text.Text) && text.Text.ToInt32() > 0)
                {
                    return;
                }
                if (Equals(text, TxtIntervalTime))
                {
                    text.Text = "10";
                }
                if (Equals(text, TxtCustWidth))
                {
                    text.Text = SystemParameters.PrimaryScreenWidth.ToString("0");
                }
                if (Equals(text, TxtCustHeight))
                {
                    text.Text = SystemParameters.PrimaryScreenHeight.ToString("0");
                }
            }
        }

        /// <summary>
        /// 设置弹窗大小单选框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbAlertSize_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radio && GridCustSize!=null)
            {
                GridCustSize.Visibility = radio.Tag.ToInt32() == 1 ? Visibility.Hidden : Visibility.Visible;
            }
        }

        #endregion


    }
}
