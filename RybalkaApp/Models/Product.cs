using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace RybalkaApp.Models
{
    public partial class Product
    {
        public Product()
        {
            Orderproducts = new HashSet<Orderproduct>();
        }

        public string ProductArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public int ProductCategoryId { get; set; }
        public string? ProductPhoto { get; set; }
        public int ProductManufacturerId { get; set; }
        public double ProductCost { get; set; }
        public int ProductDiscountAmountMax { get; set; }
        public int? ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public int ProductUnitId { get; set; }
        public string? ProductStatus { get; set; }

        public virtual Productcategory ProductCategory { get; set; } = null!;
        public virtual Productmanufacturer ProductManufacturer { get; set; } = null!;
        public virtual Unit ProductUnit { get; set; } = null!;
        public virtual ICollection<Orderproduct> Orderproducts { get; set; }

        public string photoPath {
            get {
                return $"/Resources/{ProductPhoto}";
            }
        }

        public SolidColorBrush colorBackgroundProduct {
            get {
                if (ProductQuantityInStock == 0) {
                    return new BrushConverter().ConvertFromString("#AAAAAA") as SolidColorBrush;
                }
                return new BrushConverter().ConvertFromString("#FFFFFF") as SolidColorBrush;
            }
        }
    }
}
