using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class VM_ProductType
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public Boolean Active { get; set; }
        public DateTime DateCreated { get; set; }

        public List<VM_ProductType> GetList()
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.ProductTypes.Select(y => new VM_ProductType
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

        public VM_ProductType Get(string Code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.ProductTypes.Where(x=>x.Code.Equals(Code)).Select(y => new VM_ProductType
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

        public VM_ProductType Create(VM_ProductType item)
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
                        return _context.ProductTypes.Where(x => x.Code.Equals(item.Code)).Select(y => new VM_ProductType
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

        public VM_ProductType Update(string Code, VM_ProductType item)
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
                        return _context.ProductTypes.Where(x => x.Code.Equals(Code)).Select(y => new VM_ProductType
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
