using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Miro.Lu.Timing.Core;
using Miro.Lu.Timing.View.Component;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Miro.Lu.Timing.View.Home
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 私有变量

        /// <summary>
        /// 是否开灯
        /// </summary>
        private bool _isLight = true;

        /// <summary>
        /// 系统托盘
        /// </summary>
        private NotifyIcon _notifyIcon;

        #endregion

        #region 初始化

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
            LbDate.Content = DateTime.Now.ToString("dd");

            //初始化系统托盘
            _notifyIcon = new NotifyIcon
            {
                BalloonTipText = "我在这里哟！旺旺旺！",
                Text = "百宝箱",
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath),
                Visible = true
            };
            //打开菜单项
            System.Windows.Forms.MenuItem open = new System.Windows.Forms.MenuItem("打开");
            open.Click += (o, events) => ShowWindow();
            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            exit.Click += (o, events) => Close();
            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = { open, exit };
            _notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);           
            //双击打开
            _notifyIcon.MouseDoubleClick += (o, events) =>
            {
                if (events.Button == MouseButtons.Left) ShowWindow();
            };
        }



        #endregion

        #region 私有公共

        /// <summary>
        /// 开关灯
        /// </summary>
        private void TurnOfflights()
        {
            _isLight = !_isLight;
            LayerDark.Visibility = _isLight ? Visibility.Hidden : Visibility.Visible;
            var imgType = _isLight ? "" : "_dark";

            foreach (var child in MainGrid.Children)
            {
                if (child is Image image && image.Tag != null &&
                    (image.Tag.ToString() == "ImageBtn" || image.Tag.ToString() == "ImageFur"))
                {
                    image.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(image.Uid + imgType), UriKind.Relative));
                }
            }
        }

        /// <summary>
        /// 是否可以拖动窗口
        /// </summary>
        /// <returns></returns>
        private bool IsCanMouseWindow()
        {
            //如果我的任务窗口打开
            if(ChildMyTask.Visibility == Visibility.Visible)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 显示主窗口
        /// </summary>
        private void ShowWindow()
        {
            Visibility = Visibility.Visible;
            ShowInTaskbar = true;
            Activate();
        }

        /// <summary>
        /// 隐藏主窗口至托盘
        /// </summary>
        private void HideWindow()
        {
            ShowInTaskbar = false;
            Visibility = Visibility.Hidden;
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
            dialog.CancelFun = () => MainGrid.Children.Remove(dialog);
            MainGrid.Children.Add(dialog);
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
        /// 最小化到托盘按钮（按钮点击）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _notifyIcon.ShowBalloonTip(10);
            HideWindow();
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
                img.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(img.Uid + "_hover"), UriKind.Relative));
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
                img.Source = new BitmapImage(new Uri(CommonConst.GetImgUrl(img.Uid + (_isLight ? "" : "_dark")), UriKind.Relative));
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
                DragMove();
            }
        }

        /// <summary>
        /// 程序关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            //移除托盘
            _notifyIcon.Visible = false;
            //保存任务配置
            ChildMyTask.SaveTaskConfig();
        }

        #endregion


    }
}
