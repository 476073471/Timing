using Miro.Lu.Timing.Core;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Miro.Lu.Timing.View.Childs
{

    /// <summary>
    /// MyTask.xaml 的交互逻辑
    /// </summary>
    public partial class MyTask : UserControl
    {
        /// <summary>
        /// 定时弹窗间隔（分钟）
        /// </summary>
        private int _setTimeInterval = 10;

        /// <summary>
        /// 定时器
        /// </summary>
        private DispatcherTimer _timer = new DispatcherTimer();

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
            var radio = sender as RadioButton;
            _setTimeInterval = Convert.ToInt32(radio.Tag ?? 10);
        }

        /// <summary>
        /// 开启定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _timer.Interval = new TimeSpan(0, 0, 5);
            _timer.Start();
        }

        /// <summary>
        /// 停止定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _timer.Stop();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
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
            Image img = sender as Image;
            img.Source = new BitmapImage(new Uri(CommonConst.DiaButton_hover, UriKind.Relative));
        }

        /// <summary>
        /// 鼠标移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHover_MouseLeave(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Source = new BitmapImage(new Uri(CommonConst.DiaButton, UriKind.Relative));
        }



        #endregion


    }
}
