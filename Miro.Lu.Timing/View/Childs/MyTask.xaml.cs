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
using Miro.Lu.Timing.View.Home;

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
            if (!string.IsNullOrEmpty(config.AlertBackgroudPath))
            {
                Uri uri = new Uri(config.AlertBackgroudPath, UriKind.Absolute);
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
                    radioImg.Tag.ToInt32() == config.AlertBackgroudType)
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
        private string GetSelectRadioValue(string groupName,string defaultValue)
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

        /// <summary>
        /// 获取单选按钮选中的控件
        /// </summary>
        /// <param name="groupName">控件所属组</param>
        /// <returns></returns>
        private RadioButton GetSelectRadioButton(string groupName)
        {
            foreach (var mainGridChild in MainGrid.Children)
            {
                //时间间隔
                if (mainGridChild is RadioButton radio && radio.GroupName == groupName)
                {
                    if (radio.IsChecked != null && (bool)radio.IsChecked)
                        return radio;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取当前配置
        /// </summary>
        /// <returns></returns>
        private TaskConfigModel GetNowConfig()
        {
            var config = new TaskConfigModel
            {
                IntervalTime = TxtIntervalTime.Text.ToInt32(10),
                AlertContent = TxtAlertContent.Text,
                CustAlertWidth = TxtCustWidth.Text.ToInt32(SystemParameters.PrimaryScreenWidth.ToInt32()),
                CustAlertHeight = TxtCustHeight.Text.ToInt32(SystemParameters.PrimaryScreenHeight.ToInt32()),
                IntervalType = GetSelectRadioValue("rbSetTime", "2").ToInt32(),
                CustAlertType = GetSelectRadioValue("rbAlertSize", "1").ToInt32(),
                AlertBackgroudType = 1,
                AlertBackgroudPath = CommonConst.AlertBack2560_1440
            };
            var radio = GetSelectRadioButton("rbBackgroudImg");
            if (radio != null)
            {
                config.AlertBackgroudType = radio.Tag.ToInt32();
                config.AlertBackgroudPath = ((ImageBrush) radio.Background).ImageSource.ToString();
            }
 
            return config;
        }

        /// <summary>
        /// 设置控件有效性
        /// <param name="control">控件</param>
        /// <param name="isValid">是否有效</param>
        /// </summary>
        private void SetControlValid<T>(T control, bool isValid) where T : Control
        {
            //设为有效
            if (isValid)
            {
                control.IsEnabled = true;
                control.Opacity = 1;
            }
            else
            {
                control.IsEnabled = false;
                control.Opacity = 0.5;
            }
        }

        /// <summary>
        /// 设置图片控件有效性
        /// <param name="control">控件</param>
        /// <param name="isValid">是否有效</param>
        /// </summary>
        private void SetImgValid<T>(T control, bool isValid) where T : Image
        {
            //设为有效
            if (isValid)
            {
                control.IsEnabled = true;
                control.Opacity = 1;
            }
            else
            {
                control.IsEnabled = false;
                control.Opacity = 0.5;
            }
        }

        /// <summary>
        /// 弹出任务框
        /// <param name="isPreview">是否预览</param>
        /// </summary>
        private void AlertTaskWindow(bool isPreview)
        {
            var alertWindow = new TaskAlertWindow(GetNowConfig());
            if (!isPreview)
            {
                _timer.Stop();
                alertWindow.ConfirmFun = () => _timer.Start();
            }           
            alertWindow.ShowDialog();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 保存任务配置
        /// </summary>
        public void SaveTaskConfig()
        {
            CommonHelper.SaveConfig(GetNowConfig(), ConfigEnum.TaskConfig);
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
            //开始按钮设为无效
            SetImgValid(BtnOpen, false);
            SetControlValid(LbOpen, false);
            //停止按钮设为有效
            SetImgValid(BtnStop, true);
            SetControlValid(LbStop, true);
            //预览按钮设为无效
            SetImgValid(BtnPreview, false);
            SetControlValid(LbPreview, false);

            //获取弹框间隔类型
            var intervalType = GetSelectRadioValue("rbSetTime", "2").ToInt32(2);
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
            //开始按钮设为有效
            SetImgValid(BtnOpen, true);
            SetControlValid(LbOpen, true);
            //停止按钮设为无效
            SetImgValid(BtnStop, false);
            SetControlValid(LbStop, false);
            //预览按钮设为有效
            SetImgValid(BtnPreview, true);
            SetControlValid(LbPreview, true);

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
            AlertTaskWindow(true);
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
            AlertTaskWindow(false);
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
                img.Source = new BitmapImage(new Uri(CommonConst.DiaButtonHover, UriKind.Relative));
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
