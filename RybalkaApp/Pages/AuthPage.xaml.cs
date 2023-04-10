using RybalkaApp.Classes;
using RybalkaApp.Models;
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

namespace RybalkaApp.Pages {
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page {

        public AuthPage() {
            InitializeComponent();
            WindowTitle = Page.TitleProperty.Name;
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrEmpty(TbxLogin.Text)
                && !string.IsNullOrEmpty(TbxPassword.Text)) {

                var user = tradeContext.Instance.Users.FirstOrDefault(
                    u => u.UserLogin == TbxLogin.Text
                    && u.UserPassword == TbxPassword.Text);

                if (user == null) {
                    MessageBox.Show("Введены неверные данные", "Ошибка");
                } else {
                    //TbxLogin.Text = "";
                    //TbxPassword.Text = "";
                    Manager.MyFrame.Navigate(new ProductsListPage(user));
                }
            }
        }
    }
}
