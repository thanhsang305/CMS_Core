﻿using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class Req_Category
    {
        [Required(ErrorMessage = "The name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The code is required!")]
        public string Code { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public Boolean Active { get; set; }
    }
    public class Res_Category
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public Boolean Active { get; set; }

        public List<Res_Category> GetList()
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Categories.Select(y => new Res_Category
                    {
                        Active = (Boolean)y.Active,
                        Code = y.Code,
                        DateCreated = (DateTime) y.DateCreated,
                        Description = y.Description,
                        Name = y.Name,
                        Thumbnail = y.Thumbnail
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Category Get(string Code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Categories.Where(x=>x.Code.Equals(Code)).Select(y => new Res_Category
                    {
                        Active = (Boolean)y.Active,
                        Code = y.Code,
                        DateCreated = (DateTime)y.DateCreated,
                        Description = y.Description,
                        Name = y.Name,
                        Thumbnail = y.Thumbnail
                    }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Category Create(Req_Category item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (!_context.Categories.Any(x => x.Code.Equals(item.Code)))
                    {
                        var category = new Category();
                        category.Code = item.Code;
                        category.Name = item.Name;
                        category.Description = item.Description;
                        category.Active = item.Active;
                        category.DateCreated = DateTime.UtcNow;
                        _context.Categories.Add(category);
                        _context.SaveChanges();
                        return _context.Categories.Where(x => x.Code.Equals(item.Code)).Select(y => new Res_Category
                        {
                            Active = (Boolean)y.Active,
                            Code = y.Code,
                            DateCreated = (DateTime)y.DateCreated,
                            Description = y.Description,
                            Name = y.Name,
                            Thumbnail = y.Thumbnail
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
        public Res_Category Update(string Code, Req_Category item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Categories.Any(x => x.Code.Equals(item.Code)))
                    {
                        var category = _context.Categories.SingleOrDefault(x => x.Code.Equals(item.Code));
                        category.Name = item.Name;
                        category.Description = item.Description;
                        category.Active = item.Active;
                        _context.SaveChanges();
                        return _context.Categories.Where(x => x.Code.Equals(Code)).Select(y => new Res_Category
                        {
                            Active = (Boolean)y.Active,
                            Code = y.Code,
                            DateCreated = (DateTime)y.DateCreated,
                            Description = y.Description,
                            Name = y.Name,
                            Thumbnail = y.Thumbnail
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
                    if (_context.Categories.Any(x => x.Code.Equals(code)))
                    {
                        var category = _context.Categories.SingleOrDefault(x => x.Code.Equals(code));
                        _context.Categories.Remove(category);
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
                    if (_context.Categories.Any(x => x.Code.Equals(code)))
                    {
                        var category = _context.Categories.SingleOrDefault(x => x.Code.Equals(code));
                        category.Active = !category.Active;
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
