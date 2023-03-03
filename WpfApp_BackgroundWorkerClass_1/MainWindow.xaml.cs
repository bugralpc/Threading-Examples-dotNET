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

namespace WpfApp_BackgroundWorkerClass_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private static BackgroundWorker backgroundWorker2 = new BackgroundWorker();

        private ProgressBarInfo progressBarInfo1 = new ProgressBarInfo() { Progress = 0 };
        public MainWindow()
        {
            InitializeComponent();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;

            //backgroundWorker1.RunWorkerAsync();

            Binding binding1 = new Binding("Progress");
            binding1.Source = progressBarInfo1;
            progressBar1.SetBinding(ProgressBar.ValueProperty, binding1);

            //are.WaitOne();

            //MessageBox.Show("ASdasdasd");

            //progressBarInfo1.Progress = 50;

            //progressBar1.Value = 50;
        }

        private void StartButton1Click(object sender, RoutedEventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start async operation
                backgroundWorker1.RunWorkerAsync();

            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(100);
                backgroundWorker1.ReportProgress(i);
            }

            //are.Set();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            //progressBarInfo1.Progress = e.ProgressPercentage;
        }

        private void CancelButton1Click(object sender, RoutedEventArgs e)
        {
            //if (backgroundWorker1.WorkerSupportsCancellation == true)
            //{
            //    // Cancel async operation
            //    backgroundWorker1.CancelAsync();
            //}
        }

        private void StartButton2Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton2Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class ProgressBarInfo : INotifyPropertyChanged
    {
        private int progress;

        public int Progress
        {
            get { return progress; }
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
