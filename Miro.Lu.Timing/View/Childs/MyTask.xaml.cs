using Miro.Lu.Timing.Core;
using Miro.Lu.Timing.View.Component;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Miro.Lu.Timing.View.Childs
{

    /// <summary>
    /// MyTask.xaml 的交互逻辑
    /// </summary>
    public partial class MyTask : UserControl
    {
        #region 私有变量

        /// <summary>
        /// 定时弹窗间隔类型（分钟）
        /// </summary>
        private int _intervalType = 10;

        /// <summary>
        /// 定时器
        /// </summary>
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        #endregion

        public MyTask()
        {
            InitializeComponent();
            _timer.Tick += Timer_Tick;
        }

        #region 按钮事件

        /// <summary>
        /// 间隔选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radio)
            {
                _intervalType = Convert.ToInt32(radio.Tag ?? 3);
            }
            
        }

        /// <summary>
        /// 开启定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //保存配置数据
            _timer.Interval = new TimeSpan(0, 0, 5);
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
            if (sender is Image img) img.Source = new BitmapImage(new Uri(CommonConst.DiaButton_hover, UriKind.Relative));
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

        #endregion

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //屏蔽非法按键
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
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
    }
}
