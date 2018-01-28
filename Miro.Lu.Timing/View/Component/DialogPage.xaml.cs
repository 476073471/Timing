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

namespace Miro.Lu.Timing.View.Component
{
    /// <summary>
    /// ExitDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DialogPage : UserControl
    {
        public Action ConfirmFun;

        public DialogPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="message"></param>
        public void SetLbMessage(string message)
        {
            lbMessage.Text = message;
        }

        /// <summary>
        /// 设置确认的方法
        /// </summary>
        /// <param name="fun"></param>
        public void SetConfirmFun(Action fun)
        {
            ConfirmFun = fun;
        }

        #region 按钮事件

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(ConfirmFun == null)
            {
                Visibility = Visibility.Hidden;
            }
            else
            {
                ConfirmFun();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Hidden;
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
