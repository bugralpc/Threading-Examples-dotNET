using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp_ThreadingBinding_2
{

    public partial class MainWindow : Window
    {
        private int progressValue = 20;
        private ProgressBarInfo progressBarInfo;

        private int number = 1;

        public MainWindow()
        {
            InitializeComponent();

            progressBarInfo = new ProgressBarInfo() { Progress = 0 };

            Binding binding = new Binding("Progress");
            binding.Source = progressBarInfo;
            progressBar1.SetBinding(ProgressBar.ValueProperty, binding);

        }

        private void ProgressClick(object sender, RoutedEventArgs e)
        {
            progressBar1.Value += progressValue;

        }

        private void ProgressThreadClick(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                //progressBar1.Value += progressValue;

                progressBarInfo.Progress += progressValue;

            });

            thread.Start();
        }

        private void DebugClick(object sender, RoutedEventArgs e)
        {
            string debug = "debug";
        }

        private void ProgressClick2(object sender, RoutedEventArgs e)
        {
            //Thread thread = new Thread(() =>Method(ref number));
            //thread.Start();

            Thread thread = new Thread(() => PassByReference(ref progressBar1));
            thread.Start();

            //PassByReference(ref progressBar1);

            //Method(ref number);

            //MessageBox.Show(number.ToString());
        }

        private void PassByReference(ref ProgressBar bar)
        {
            bar.Value += 20;
        }

        private void Method(ref int refArgument)
        {
            refArgument = refArgument + 44;

            MessageBox.Show(refArgument.ToString());
        }
    }

    public class ProgressBarInfo :INotifyPropertyChanged
    {
        private int progress;
        public int Progress
        { get
            {
                return progress;
            }
          set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
