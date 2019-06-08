using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class Req_Group
    {
        [Required(ErrorMessage = "The code is required!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "The name is required!")]
        public string Name { get; set; }
        public Boolean Active { get; set; }
    }
    public class Res_Group
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Res_Group> GetList()
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Groups.Select(y => new Res_Group
                    {
                        Code = y.Code,
                        Name = y.Name,
                        Active = (bool)y.Active,
                        DateCreated = (DateTime)y.DateCreated
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Group Get(string Code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Groups.Where(x=>x.Code.Equals(Code)).Select(y => new Res_Group
                    {
                        Code = y.Code,
                        Name = y.Name,
                        Active = (bool)y.Active,
                        DateCreated = (DateTime)y.DateCreated
                    }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Group Create(Req_Group item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (!_context.Groups.Any(x => x.Code.Equals(item.Code)))
                    {
                        var group = new Group();
                        group.Code = item.Code;
                        group.Name = item.Name;
                        group.Active = item.Active;
                        group.DateCreated = DateTime.UtcNow;
                        _context.Groups.Add(group);
                        _context.SaveChanges();

                        return _context.Groups.Where(x => x.Code.Equals(group.Code)).Select(y => new Res_Group
                        {
                            Code = y.Code,
                            Name = y.Name,
                            Active = (bool)y.Active,
                            DateCreated = (DateTime)y.DateCreated
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
        public Res_Group Update(string Code, Req_Group item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Groups.Any(x => x.Code.Equals(Code)))
                    {
                        var group = _context.Groups.SingleOrDefault(x => x.Code.Equals(Code));
                        group.Name = item.Name;
                        group.Active = item.Active;
                        _context.SaveChanges();
                        return _context.Groups.Where(x => x.Code.Equals(group.Code)).Select(y => new Res_Group
                        {
                            Code = y.Code,
                            Name = y.Name,
                            Active = (bool)y.Active,
                            DateCreated = (DateTime)y.DateCreated
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
        public Boolean Delete(string Code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Groups.Any(x => x.Code.Equals(Code)))
                    {
                        var group = _context.Groups.SingleOrDefault(x => x.Code.Equals(Code));
                        _context.Groups.Remove(group);
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
        public Boolean UpdateStatus(string Code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Groups.Any(x => x.Code.Equals(Code)))
                    {
                        var group = _context.Groups.SingleOrDefault(x => x.Code.Equals(Code));
                        group.Active = !group.Active;
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
