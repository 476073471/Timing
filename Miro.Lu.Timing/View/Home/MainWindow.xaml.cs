using Miro.Lu.Timing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
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

namespace Miro.Lu.Timing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 拖动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        #region 按钮事件

        /// <summary>
        /// 我的任务按钮（鼠标移入）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetTask_MouseEnter(object sender, MouseEventArgs e)
        {
            btnSetTask.Source = new BitmapImage(new Uri(CommonConst.BtnTask_hover, UriKind.Relative));
        }

        /// <summary>
        /// 我的任务按钮（鼠标离开）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetTask_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSetTask.Source = new BitmapImage(new Uri(CommonConst.BtnTask, UriKind.Relative));
        }

        /// <summary>
        /// 设置按钮（鼠标移入）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfig_MouseEnter(object sender, MouseEventArgs e)
        {
            btnConfig.Source = new BitmapImage(new Uri(CommonConst.BtnConfig_hover, UriKind.Relative));
        }

        /// <summary>
        /// 设置按钮（鼠标离开）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfig_MouseLeave(object sender, MouseEventArgs e)
        {
            btnConfig.Source = new BitmapImage(new Uri(CommonConst.BtnConfig, UriKind.Relative));
        }

        /// <summary>
        /// 退出按钮（鼠标移入）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseEnter(object sender, MouseEventArgs e)
        {
            btnExit.Source = new BitmapImage(new Uri(CommonConst.BtnExit_hover, UriKind.Relative));
        }

        /// <summary>
        /// 退出按钮（鼠标离开）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseLeave(object sender, MouseEventArgs e)
        {
            btnExit.Source = new BitmapImage(new Uri(CommonConst.BtnExit, UriKind.Relative));
        }

        #endregion

    }
}
