using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    class VM_Product
    {
        public int ID { get; set; }
        public int ProductTypeID { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string HashTag { get; set; }
        public string Thumbnail { get; set; }
        public Decimal Selling { get; set; }
        public Decimal DiscountPrice { get; set; }
        public int MediaID { get; set; }
        public int SupplierID { get; set; }
        public DateTime DateCreated { get; set; }
        public Boolean Active { get; set; }
        public Boolean BestSeller { get; set; }
        public Boolean Newest { get; set; }

        public string Create(VM_Product item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (!_context.Products.Any(x => x.SKU.Equals(item.SKU)))
                    {
                        var product = new Product();
                        product.Active = item.Active;
                        product.BestSeller = item.BestSeller;
                        product.Content = item.Content;
                        product.DateCreated = DateTime.UtcNow;
                        product.Description = item.Description;
                        product.DiscountPrice = item.DiscountPrice;
                        product.Name = item.Name;
                        product.Newest = item.Newest;
                        product.ProductTypeID = item.ProductTypeID;
                        product.SellingPrice = item.Selling;
                        product.ShortDescription = item.ShortDescription;
                        product.SKU = item.SKU;
                        product.SupplierID = item.SupplierID;
                        _context.Products.Add(product);
                        _context.SaveChanges();
                        return CommonConst.SUCCESS;
                    }
                    else
                    {
                        return CommonConst.EXISTS;
                    }
                }
            }
            catch (Exception e)
            {
                return CommonConst.ERROR;
            }
        }
    }
}
