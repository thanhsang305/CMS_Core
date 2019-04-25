using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class VM_Blog
    {
        public int ID { get; set; }
        public string Alias { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public string Tags { get; set; }
        public Boolean Active { get; set; }
        public int CategoryID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DatePost { get; set; }

        public string Create(VM_Blog item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (!_context.Blogs.Any(x => x.Alias.Equals(item.Alias)))
                    {
                        var blog = new Blog();
                        blog.Alias = item.Alias;
                        blog.Title = item.Title;
                        blog.Description = item.Description;
                        blog.Content = item.Content;
                        blog.Tags = item.Tags;
                        blog.Active = item.Active;
                        blog.DateCreated = DateTime.UtcNow;
                        blog.CategoryID = item.CategoryID;
                        _context.Blogs.Add(blog);
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

        public string Update(VM_Blog item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Blogs.Any(x => x.Alias.Equals(item.Alias)))
                    {
                        var blog = _context.Blogs.SingleOrDefault(x => x.Alias.Equals(item.Alias));
                        blog.Description = item.Description;
                        blog.Content = item.Content;
                        blog.Tags = item.Tags;
                        blog.Active = item.Active;
                        blog.CategoryID = item.CategoryID;
                        _context.SaveChanges();
                        return CommonConst.SUCCESS;
                    }
                    else
                    {
                        return CommonConst.NOTEXISTS;
                    }
                }
            }
            catch (Exception e)
            {
                return CommonConst.ERROR;
            }
        }

        public string Delete(string Alias)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Blogs.Any(x => x.Alias.Equals(Alias)))
                    {
                        var blog = _context.Blogs.SingleOrDefault(x => x.Alias.Equals(Alias));
                        _context.Blogs.Remove(blog);
                        _context.SaveChanges();
                        return CommonConst.SUCCESS;
                    }
                    else
                    {
                        return CommonConst.NOTEXISTS;
                    }
                }
            }
            catch (Exception e)
            {
                return CommonConst.ERROR;
            }
        }

        public string UpdateStatus(string Aliass)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Blogs.Any(x => x.Alias.Equals(Alias)))
                    {
                        var blog = _context.Blogs.SingleOrDefault(x => x.Alias.Equals(Alias));
                        _context.Blogs.Remove(blog);
                        _context.SaveChanges();
                        return CommonConst.SUCCESS;
                    }
                    else
                    {
                        return CommonConst.NOTEXISTS;
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
