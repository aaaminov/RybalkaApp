using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для ProductsListPage.xaml
    /// </summary>
    public partial class ProductsListPage : Page {

        Product selectedProduct;

        public ProductsListPage(User user) {
            InitializeComponent();
            ((Manager.MyFrame.Parent as Grid).FindName("TbkFIO") as TextBlock).Text = user.getFIO();
            var allManufs = tradeContext.Instance.Productmanufacturers
                .Select(pf => pf.ProductManufacturerName).ToList();
            allManufs.Insert(0, "Все" );
            CmbxFilter.ItemsSource = allManufs;
            CmbxFilter.SelectedIndex = 0;
            CmbxSort.SelectedIndex = 0;

            if (user.UserRole == 1) {
                SpForAdmin.Visibility = Visibility.Visible;
            } else {
                SpForAdmin.Visibility = Visibility.Collapsed;
            }
        }

        private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e) {
            RefreshList();
        }

        private void CmbxSort_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            RefreshList();
        }

        private void CmbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            RefreshList();
        }

        private void RefreshList() {
            var products = tradeContext.Instance.Products.Include(p => p.ProductManufacturer).ToList();
            TbkCountPorducts.Text = products.Count + "";
            if (!TbxSearch.Text.IsNullOrEmpty()) {
                products = products.Where(p => p.ProductName.ToLower()
                    .Contains(TbxSearch.Text.ToLower())).ToList();
            }
            if (CmbxSort.SelectedIndex != 0) {
                switch (CmbxSort.SelectedIndex) {
                    case 1: {
                        products = products.OrderBy(p => p.ProductCost).ToList(); 
                        break;
                    }
                    case 2: {
                        products = products.OrderByDescending(p => p.ProductCost).ToList(); 
                        break;
                    }
                }
            }
            if (CmbxFilter.SelectedIndex != 0) { 
                products = products.Where(p=>p.ProductManufacturerId == CmbxFilter.SelectedIndex).ToList();
            }
            LvProducts.ItemsSource = products;
            TbkCountPorducts.Text = products.Count + " из " + TbkCountPorducts.Text;
        }

        private void LvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (LvProducts.SelectedItem != null) {
                selectedProduct = LvProducts.SelectedItem as Product;
                BtnEditProduct.IsEnabled = true;
                //BtnRemoveProduct.IsEnabled = true;
            } else {
                selectedProduct = null;
                BtnEditProduct.IsEnabled = false;
                //BtnRemoveProduct.IsEnabled = false;
            }
        }

        private void BtnEditProduct_Click(object sender, RoutedEventArgs e) {
            Manager.MyFrame.Navigate(new AddEditProductPage(selectedProduct));
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e) {
            Manager.MyFrame.Navigate(new AddEditProductPage());
        }

    }
}
