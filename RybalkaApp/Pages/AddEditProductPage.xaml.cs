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
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page {
        private Product product;

        public AddEditProductPage() {
            InitializeComponent();
            BtnEdit.Visibility = Visibility.Collapsed;
            BtnRemove.Visibility = Visibility.Collapsed;
        }

        public AddEditProductPage(Product _product) {
            InitializeComponent();
            BtnAdd.Visibility = Visibility.Collapsed;
            if (_product != null) {
                product = _product;

                TbxProductArticleNumber.Text = product.ProductArticleNumber;
                TbxProductName.Text = product.ProductName;
                TbxProductDescription.Text = product.ProductDescription;
                TbxProductCost.Text = product.ProductCost.ToString();
            }
        }

        private bool isProductInfotValid() {
            if (TbxProductArticleNumber.Text == "" || TbxProductName.Text == "" 
                || TbxProductDescription.Text == "" || TbxProductCost.Text == "") {
                return false;
            }
            return true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            if (!isProductInfotValid()) {
                return;
            }
            Product newProduct = new Product() {
                ProductArticleNumber = TbxProductArticleNumber.Text,
                ProductName = TbxProductName.Text,
                ProductDescription = TbxProductDescription.Text,
                ProductCategoryId = 1,
                ProductPhoto = "",
                ProductManufacturerId = 1,
                ProductCost = Convert.ToDouble(TbxProductCost.Text),
                ProductDiscountAmount = 10,
                ProductQuantityInStock = 10,
                ProductUnitId = 1,
                ProductStatus = ""
            };
            tradeContext.Instance.Products.Add(newProduct);
            tradeContext.Instance.SaveChanges();
            Manager.MyFrame.GoBack();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            if (!isProductInfotValid()) {
                return;
            }

            product.ProductName = TbxProductName.Text;
            product.ProductDescription = TbxProductDescription.Text;
            product.ProductCost = Convert.ToDouble(TbxProductCost.Text);
            tradeContext.Instance.Products.Update(product);
            tradeContext.Instance.SaveChanges();
            Manager.MyFrame.GoBack();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e) {
            //Product product = tradeContext.Instance.Products.Where(p => p.ProductArticleNumber.Equals(product.)).FirstOrDefault();
            tradeContext.Instance.Products.Remove(product);
            foreach (Orderproduct op in tradeContext.Instance.Orderproducts) {
                if (op.ProductArticleNumber.Equals(product.ProductArticleNumber)) {
                    tradeContext.Instance.Orderproducts.Remove(op);
                }
            }
            tradeContext.Instance.SaveChanges();
            Manager.MyFrame.GoBack();
        }
    }
}
