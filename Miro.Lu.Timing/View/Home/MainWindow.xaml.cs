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
        //是否开灯
        private bool IsLight = true;

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

        #region 公共

        /// <summary>
        /// 开关灯
        /// </summary>
        /// <param name="isTrunOn">是否开灯</param>
        public void TurnOfflights()
        {
            IsLight = !IsLight;
            layerDark.Visibility = IsLight ? Visibility.Hidden : Visibility.Visible;
            var imgType = IsLight ? "" : "_dark";
            btnConfig.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(btnConfig.Name + imgType), UriKind.Relative));
            btnTask.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(btnTask.Name + imgType), UriKind.Relative));
            btnExit.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(btnExit.Name + imgType), UriKind.Relative));
            btnSwitch.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(btnSwitch.Name + imgType), UriKind.Relative));

            furWindow.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(furWindow.Name + imgType), UriKind.Relative));
            furDesk.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(furDesk.Name + imgType), UriKind.Relative));
            furLight.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(furLight.Name + imgType), UriKind.Relative));
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
            DialogPage.SetLbMessage("确定要退出吗？");
            DialogPage.SetConfirmFun(() => Close());
            DialogPage.Visibility = Visibility.Visible;
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
            Image img = sender as Image;
            img.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(img.Name + "_hover"), UriKind.Relative));
        }

        /// <summary>
        /// 鼠标移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHover_MouseLeave(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(img.Name + (IsLight ? "" : "_dark")), UriKind.Relative));
        }


        #endregion
    }
}
