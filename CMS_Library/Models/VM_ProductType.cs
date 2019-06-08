using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class Req_ProductType
    {
        [Required(ErrorMessage = "The code is required!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "The name is required!")]
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public Boolean Active { get; set; }
    }
    public class Res_ProductType
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public Boolean Active { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Res_ProductType> GetList()
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.ProductTypes.Select(y => new Res_ProductType
                    {
                        Code = y.Code,
                        Name = y.Name,
                        Description = y.Description,
                        Thumbnail = y.Thumbnail,
                        DateCreated = (DateTime)y.DateCreated,
                        Active = (Boolean)y.Active
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Res_ProductType Get(string Code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.ProductTypes.Where(x=>x.Code.Equals(Code)).Select(y => new Res_ProductType
                    {
                        Code = y.Code,
                        Name = y.Name,
                        Description = y.Description,
                        Thumbnail = y.Thumbnail,
                        DateCreated = (DateTime)y.DateCreated,
                        Active = (Boolean)y.Active
                    }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Res_ProductType Create(Req_ProductType item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (!_context.ProductTypes.Any(x => x.Code.Equals(item.Code)))
                    {
                        var productType = new ProductType();
                        productType.Code = item.Code;
                        productType.Name = item.Name;
                        productType.Description = item.Description;
                        productType.Active = item.Active;
                        productType.DateCreated = DateTime.UtcNow;
                        _context.ProductTypes.Add(productType);
                        _context.SaveChanges();
                        return _context.ProductTypes.Where(x => x.Code.Equals(item.Code)).Select(y => new Res_ProductType
                        {
                            Code = y.Code,
                            Name = y.Name,
                            Description = y.Description,
                            Thumbnail = y.Thumbnail,
                            DateCreated = (DateTime)y.DateCreated,
                            Active = (Boolean)y.Active
                        }).SingleOrDefault();
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Res_ProductType Update(string Code, Req_ProductType item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.ProductTypes.Any(x => x.Code.Equals(Code)))
                    {
                        var productType = _context.ProductTypes.SingleOrDefault(x => x.Code.Equals(Code));
                        productType.Name = item.Name;
                        productType.Description = item.Description;
                        productType.Active = item.Active;
                        _context.SaveChanges();
                        return _context.ProductTypes.Where(x => x.Code.Equals(Code)).Select(y => new Res_ProductType
                        {
                            Code = y.Code,
                            Name = y.Name,
                            Description = y.Description,
                            Thumbnail = y.Thumbnail,
                            DateCreated = (DateTime)y.DateCreated,
                            Active = (Boolean)y.Active
                        }).SingleOrDefault();
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Boolean Delete(string code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.ProductTypes.Any(x => x.Code.Equals(code)))
                    {
                        var productType = _context.ProductTypes.SingleOrDefault(x => x.Code.Equals(code));
                        _context.ProductTypes.Remove(productType);
                        _context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean UpdateStatus(string code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.ProductTypes.Any(x => x.Code.Equals(code)))
                    {
                        var productType = _context.ProductTypes.SingleOrDefault(x => x.Code.Equals(code));
                        productType.Active = !productType.Active;
                        _context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
