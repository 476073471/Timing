using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Miro.Lu.Timing.View.Home;

namespace Miro.Lu.Timing
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        System.Threading.Mutex _mutex;

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
            MainWindow m = new MainWindow();
            m.Show();
        }



        public App()
        {
            Startup += App_Startup;
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

    }
}
