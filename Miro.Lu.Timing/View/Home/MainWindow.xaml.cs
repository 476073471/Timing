using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Miro.Lu.Timing.Core;
using Miro.Lu.Timing.View.Component;

namespace Miro.Lu.Timing.View.Home
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //是否开灯
        private bool _isLight = true;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //初始化日期
            lbDate.Content = DateTime.Now.ToString("dd");
        }

       

        #region 公共

        /// <summary>
        /// 开关灯
        /// </summary>
        public void TurnOfflights()
        {
            _isLight = !_isLight;
            layerDark.Visibility = _isLight ? Visibility.Hidden : Visibility.Visible;
            var imgType = _isLight ? "" : "_dark";
            btnConfig.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(btnConfig.Name + imgType), UriKind.Relative));
            btnTask.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(btnTask.Name + imgType), UriKind.Relative));
            btnExit.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(btnExit.Name + imgType), UriKind.Relative));
            btnSwitch.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(btnSwitch.Name + imgType), UriKind.Relative));

            furWindow.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(furWindow.Name + imgType), UriKind.Relative));
            furDesk.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(furDesk.Name + imgType), UriKind.Relative));
            furLight.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(furLight.Name + imgType), UriKind.Relative));
        }

        /// <summary>
        /// 是否可以拖动窗口
        /// </summary>
        /// <returns></returns>
        public bool IsCanMouseWindow()
        {
            //如果我的任务窗口打开
            if(ChildMyTask.Visibility == Visibility.Visible)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 退出按钮（鼠标点击）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var dialog = new DialogPage("确定要退出吗？") {ConfirmFun = Close};
            dialog.CancelFun = () => Main_Grid.Children.Remove(dialog);
            Main_Grid.Children.Add(dialog);
        }

        /// <summary>
        /// 关灯按钮（鼠标点击）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwtich_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TurnOfflights();
        }

        /// <summary>
        /// 我的任务按钮（鼠标点击）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTask_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChildMyTask.Visibility = Visibility.Visible;
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
                img.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(img.Name + "_hover"), UriKind.Relative));
            }
        }

        /// <summary>
        /// 鼠标移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHover_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Image img)
            {
                img.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(img.Name + (_isLight ? "" : "_dark")), UriKind.Relative));
            }
        }

        /// <summary>
        /// 拖动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && IsCanMouseWindow())
            {
                this.DragMove();
            }
        }

        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            //保存任务配置
            ChildMyTask.SaveTaskConfig();
        }
    }
}
