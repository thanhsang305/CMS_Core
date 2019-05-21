using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class Res_ShortProduct
    {
        public int ID { get; set; }
        public string ProductTypeName { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public Boolean Active { get; set; }
        public Boolean BestSeller { get; set; }
        public Boolean Newest { get; set; }
    }
    public class Res_Product
    {
        public int ID { get; set; }
        public string ProductTypeName { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string HashTag { get; set; }
        public string Thumbnail { get; set; }
        public Decimal Selling { get; set; }
        public Decimal DiscountPrice { get; set; }
        /*public int MediaID { get; set; }
        public int SupplierID { get; set; }*/
        public DateTime DateCreated { get; set; }
        public Boolean Active { get; set; }
        public Boolean BestSeller { get; set; }
        public Boolean Newest { get; set; }
    }
    public class VM_Product
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
        /*public int MediaID { get; set; }
        public int SupplierID { get; set; }*/
        public Boolean Active { get; set; }
        public Boolean BestSeller { get; set; }
        public Boolean Newest { get; set; }

        public List<Res_ShortProduct> GetList()
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Products.Select(y => new Res_ShortProduct
                    {
                        Active  = (Boolean) y.Active,
                        BestSeller = (Boolean) y.BestSeller,
                        ID = y.ID,
                        Name = y.Name,
                        Newest = (Boolean) y.Newest,
                        ProductTypeName = y.ProductType.Name,
                        ShortDescription = y.ShortDescription,
                        SKU = y.SKU
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Product Get(string SKU)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Products.Where(x => x.SKU.Equals(SKU)).Select(y => new Res_Product
                    {
                        Active = (Boolean) y.Active,
                        BestSeller = (Boolean) y.BestSeller,
                        Content = y.Content,
                        DateCreated = (DateTime)y.DateCreated,
                        Description = y.Description,
                        DiscountPrice = (Decimal)y.DiscountPrice,
                        HashTag = y.HashTag,
                        ID = y.ID,
                        Name = y.Name,
                        Newest = (Boolean) y.Newest,
                        ProductTypeName = y.ProductType.Name,
                        Selling = (Decimal) y.SellingPrice,
                        ShortDescription = y.ShortDescription,
                        SKU = y.SKU,
                        Thumbnail = y.Thumbnail
                    }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Product Create(VM_Product item)
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
                        product.HashTag = item.HashTag;
                        product.Name = item.Name;
                        product.Newest = item.Newest;
                        product.ProductTypeID = item.ProductTypeID;
                        product.SellingPrice = item.Selling;
                        product.ShortDescription = item.ShortDescription;
                        product.SKU = item.SKU;
                        _context.Products.Add(product);
                        _context.SaveChanges();
                        return _context.Products.Where(x => x.SKU.Equals(item.SKU)).Select(y => new Res_Product
                        {
                            Active = (Boolean)y.Active,
                            BestSeller = (Boolean)y.BestSeller,
                            Content = y.Content,
                            DateCreated = (DateTime)y.DateCreated,
                            Description = y.Description,
                            DiscountPrice = (Decimal)y.DiscountPrice,
                            HashTag = y.HashTag,
                            ID = y.ID,
                            Name = y.Name,
                            Newest = (Boolean)y.Newest,
                            ProductTypeName = y.ProductType.Name,
                            Selling = (Decimal)y.SellingPrice,
                            ShortDescription = y.ShortDescription,
                            SKU = y.SKU,
                            Thumbnail = y.Thumbnail
                        }).SingleOrDefault();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Res_Product Update(string SKU, VM_Product item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Products.Any(x => x.SKU.Equals(SKU)))
                    {
                        var product = _context.Products.SingleOrDefault(x => x.SKU.Equals(SKU));
                        product.Active = item.Active;
                        product.BestSeller = item.BestSeller;
                        product.Content = item.Content;
                        product.Description = item.Description;
                        product.DiscountPrice = item.DiscountPrice;
                        product.HashTag = item.HashTag;
                        product.Name = item.Name;
                        product.Newest = item.Newest;
                        product.ProductTypeID = item.ProductTypeID;
                        product.SellingPrice = item.Selling;
                        product.ShortDescription = item.ShortDescription;
                        _context.SaveChanges();
                        return _context.Products.Where(x => x.SKU.Equals(SKU)).Select(y => new Res_Product
                        {
                            Active = (Boolean)y.Active,
                            BestSeller = (Boolean)y.BestSeller,
                            Content = y.Content,
                            DateCreated = (DateTime)y.DateCreated,
                            Description = y.Description,
                            DiscountPrice = (Decimal)y.DiscountPrice,
                            HashTag = y.HashTag,
                            ID = y.ID,
                            Name = y.Name,
                            Newest = (Boolean)y.Newest,
                            ProductTypeName = y.ProductType.Name,
                            Selling = (Decimal)y.SellingPrice,
                            ShortDescription = y.ShortDescription,
                            SKU = y.SKU,
                            Thumbnail = y.Thumbnail
                        }).SingleOrDefault();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Delete(string SKU)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Products.Any(x => x.SKU.Equals(SKU)))
                    {
                        var product = _context.Products.SingleOrDefault(x => x.SKU.Equals(SKU));
                        _context.Products.Remove(product);
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateStatus(string SKU)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Products.Any(x => x.SKU.Equals(SKU)))
                    {
                        var prod = _context.Products.SingleOrDefault(x => x.SKU.Equals(SKU));
                        prod.Active = !prod.Active;
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
