using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Miro.Lu.Timing.View.Home;

namespace Miro.Lu.Timing
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        System.Threading.Mutex _mutex;

        public App()
        {
            Startup += App_Startup;
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                String projectName = Assembly.GetExecutingAssembly().GetName().Name.ToString()+".Dll";
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(projectName + ".Newtonsoft.Json.dll"))
                {
                    if (stream != null)
                    {
                        Byte[] b = new Byte[stream.Length];
                        stream.Read(b, 0, b.Length);
                        return Assembly.Load(b);
                    }
                    return null;
                }
            };
            //异常绑定
            Current.DispatcherUnhandledException += App_OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            MainWindow m = new MainWindow();
            m.Show();
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            _mutex = new System.Threading.Mutex(true, "Miro.Lu.Timing", out var ret);

            if (!ret)
            {
                MessageBox.Show("你开那么多想干嘛！");
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                MessageBox.Show("我的天CPU要炸啦！" + e.Exception.Message);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("我的天CPU要炸啦！" + ex.Message);
            }
        }

        /// <summary>
        /// 非UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                if (e.ExceptionObject is Exception exception)
                {
                    MessageBox.Show("我的天CPU要炸啦！" + exception.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("我的天CPU要炸啦！" + ex.Message);
            }
        }

    }
}
