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
    public partial class DialogConfirm : UserControl
    {
        public Action ConfirmFun { get; set; }

        public DialogConfirm()
        {
            InitializeComponent();
        }

        public DialogConfirm(string message = "")
        {
            InitializeComponent();
            SetLbMessage(message);
        }

        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="message"></param>
        public void SetLbMessage(string message)
        {
            lbMessage.Text = message;
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
        /// 鼠标移入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHover_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Image img) img.Source = new BitmapImage(new Uri(CommonConst.DiaButtonHover, UriKind.Relative));
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
    }
}
