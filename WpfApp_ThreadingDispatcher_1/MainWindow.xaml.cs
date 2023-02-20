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
using System.Threading;

namespace WpfApp_ThreadingDispatcher_1
{

    // When you first click on method 2 and then method 1, progressBar1's update speed is faster than progressBar2's update speed.
    // An interesting case.
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            //
            progressBar2.Value = 0;
            progressBar2.Minimum = 0;
            progressBar2.Maximum = 100;

            textBox1.Text = "In Method 1, when you click \"Click 1\" you are using progressBar1's dispatcher and it works on background.";
            textBox2.Text = "In Method 2, when you click \"Click 1\" you are starting task/thread and main's dispathcer works on background.";
        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            
            for(int i = 0; i <= 100; i++)
            {
                progressBar1.Dispatcher.Invoke(() => progressBar1.Value = i, DispatcherPriority.Background);

                Thread.Sleep(100);
            }
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            string debugPoint = "debug";

            var task = new Task(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    //Dispatcher.Invoke(() => progressBar2.Value = i, DispatcherPriority.Background);

                    progressBar2.Dispatcher.Invoke(() => progressBar2.Value = i, DispatcherPriority.Background);

                    Thread.Sleep(100);
                }
            });

            task.Start();
        }
    }
}
