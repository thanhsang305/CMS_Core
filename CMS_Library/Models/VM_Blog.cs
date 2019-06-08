using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class Res_Blog
    {
        public int ID { get; set; }
        public string Alias { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Tags { get; set; }
        public Boolean Active { get; set; }
        public string CategoryName { get; set; }
        public List<Res_Blog> GetList()
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Blogs.Select(y => new Res_Blog
                    {
                        Active = (Boolean)y.Active,
                        Alias = y.Alias,
                        CategoryName = y.Category.Name,
                        Description = y.Description,
                        ID = y.ID,
                        Tags = y.Tags,
                        Thumbnail = y.Thumbnail,
                        Title = y.Title
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Blog Get(string Alias)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Blogs.Where(x => x.Alias.Equals(Alias)).Select(y => new Res_Blog
                    {
                        Active = (Boolean)y.Active,
                        Alias = y.Alias,
                        CategoryName = y.Category.Name,
                        Description = y.Description,
                        ID = y.ID,
                        Tags = y.Tags,
                        Thumbnail = y.Thumbnail,
                        Title = y.Title
                    }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Blog Create(Req_Blog item)
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
                        return _context.Blogs.Where(x => x.Alias.Equals(item.Alias)).Select(y => new Res_Blog
                        {
                            Active = (Boolean)y.Active,
                            Alias = y.Alias,
                            CategoryName = y.Category.Name,
                            Description = y.Description,
                            ID = y.ID,
                            Tags = y.Tags,
                            Thumbnail = y.Thumbnail,
                            Title = y.Title
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

        public Res_Blog Update(string Alias, Req_Blog item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Blogs.Any(x => x.Alias.Equals(Alias)))
                    {
                        var blog = _context.Blogs.SingleOrDefault(x => x.Alias.Equals(Alias));
                        blog.Description = item.Description;
                        blog.Content = item.Content;
                        blog.Tags = item.Tags;
                        blog.Active = item.Active;
                        blog.CategoryID = item.CategoryID;
                        _context.SaveChanges();
                        return _context.Blogs.Where(x => x.Alias.Equals(Alias)).Select(y => new Res_Blog
                        {
                            Active = (Boolean)y.Active,
                            Alias = y.Alias,
                            CategoryName = y.Category.Name,
                            Description = y.Description,
                            ID = y.ID,
                            Tags = y.Tags,
                            Thumbnail = y.Thumbnail,
                            Title = y.Title
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

        public bool Delete(string Alias)
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

        public bool UpdateStatus(string Alias)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Blogs.Any(x => x.Alias.Equals(Alias)))
                    {
                        var blog = _context.Blogs.SingleOrDefault(x => x.Alias.Equals(Alias));
                        blog.Active = !blog.Active;
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
    public class Req_Blog
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "The alias is required!")]
        public string Alias { get; set; }
        [Required(ErrorMessage = "The title is required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The description is required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The content is required!")]
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public string Tags { get; set; }
        public Boolean Active { get; set; }
        public int CategoryID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DatePost { get; set; }
    }
}
