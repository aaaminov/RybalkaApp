using RybalkaApp.Classes;
using RybalkaApp.Pages;
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

namespace RybalkaApp.Windows {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window {

        public HomeWindow() {
            InitializeComponent();
            Manager.MyFrame = MyFrame;
            Manager.MyFrame.Navigate(new AuthPage());
        }

        private void MyFrame_ContentRendered(object sender, EventArgs e) {
            if (MyFrame.CanGoBack) {
                btnBack.Visibility = Visibility.Visible;
            } else {
                btnBack.Visibility = Visibility.Collapsed;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) {
            Manager.MyFrame.GoBack();
            if (!MyFrame.CanGoBack) {
                TbkFIO.Text = string.Empty;
            }
        }
    }
}
