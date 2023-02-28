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

namespace WpfApp_ThreadingBinding_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int progressValue = 20;
        private ProgressBarInfo progressBarInfo1 = new ProgressBarInfo() { Progress = 0 };
        private ProgressBarInfo progressBarInfo2 = new ProgressBarInfo() { Progress = 0 };
        public MainWindow()
        {
            InitializeComponent();

            Binding binding1 = new Binding("Progress");
            binding1.Source = progressBarInfo1;
            progressBar1.SetBinding(ProgressBar.ValueProperty, binding1);

            Binding binding2 = new Binding("Progress");
            binding2.Source = progressBarInfo2;
            progressBar2.SetBinding(ProgressBar.ValueProperty, binding2);


        }

        private void ProgressStepMainThreadClick(object sender, RoutedEventArgs e)
        {
            progressBar1.Value += progressValue;

            if (progressBar1.Value >= 100)
            {
                MessageBox.Show("Progress bar reachs its limit.");
            }
        }

        private void ProgressStepChildThreadClick(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                //progressBar1.Value += progressValue; // The calling thread cannot access this object because a different thread owns it.

                progressBarInfo1.Progress += progressValue;

                if (progressBarInfo1.Progress >= 100)
                {
                    MessageBox.Show("Progress bar reachs its limit.");
                }
            });

            thread.Start();
        }

        private void ProgressContinuousMainThreadClick(object sender, RoutedEventArgs e)
        {
            while(progressBar2.Value < 100)
            {
                progressBar2.Value += progressValue;

                Thread.Sleep(500);

            }

            MessageBox.Show("This freezes the UI");
        }

        private void ProgressContinuosChildThreadClick(object sender, RoutedEventArgs e)
        {
            // If you use data-binded object, progressBarInfo2 here, on Main Thread it also freezes UI.

            //while (progressBarInfo2.Progress < 100)
            //{
            //    progressBarInfo2.Progress += progressValue;

            //    Thread.Sleep(500);
            //}

            //MessageBox.Show("This does not freezes the UI");


            // If you use data-binded object, progressBarInfo here, on Child Thread it does not freeze UI.

            Thread thread = new Thread(() =>
            {
                while (progressBarInfo2.Progress < 100)
                {
                    progressBarInfo2.Progress += progressValue;

                    Thread.Sleep(500);
                }

                MessageBox.Show("This does not freezes the UI");
            });

            thread.Start();


        }


        private void ClearProgressBarsClick(object sender, RoutedEventArgs e)
        {
            progressBar1.Value = 0;
            progressBar2.Value = 0;
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
