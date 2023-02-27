using System;
using System.Collections.Generic;
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

namespace WpfApp_Threading_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Country country;

        public MainWindow()
        {
            InitializeComponent();

            country = new Country("USSR", AppDomain.GetCurrentThreadId().ToString());

            string message = country.Name + " is created by Thread Id " + country.ChangedByThreadID + "\n";

            textBox1.Text += message;
        }

        private void CreateThreadClick(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(Execute);
            thread.Start();
        }

        private void UpdateUIClick(object sender, RoutedEventArgs e)
        {
            string message = country.Name + " is created by Thread Id " + country.ChangedByThreadID + "\n";

            textBox1.Text += message;
        }

        private void Execute()
        {
            country.Name = "ABD";
            country.ChangedByThreadID = AppDomain.GetCurrentThreadId().ToString();
        }
    }

    public class Country
    {
        public string Name { get; set; }
        public string ChangedByThreadID { get; set; }

        public Country(string name, string changedByThreadID)
        {
            Name = name;
            ChangedByThreadID = changedByThreadID;
        }
    }
}
