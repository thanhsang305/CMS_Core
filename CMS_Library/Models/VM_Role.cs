using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class Req_Role
    {
        public int? ID { get; set; }
        [Required(ErrorMessage = "The code is required !")]
        public string Code { get; set; }
        [Required(ErrorMessage = "The code name is required !")]
        public string Name { get; set; }
        public Boolean Active { get; set; }
    }
    public class Res_Role
    {
        public int? ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Res_Role> GetList()
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Roles.Select(y => new Res_Role
                    {
                        ID = y.ID,
                        Active = (Boolean)y.Active,
                        Code = y.Code,
                        DateCreated = (DateTime)y.DateCreated,
                        Name = y.Name
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Res_Role Get(string Code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (string.IsNullOrEmpty(Code))
                    {
                        return null;
                    }
                    return _context.Roles.Where(x=>x.Code.Equals(Code)).Select(y => new Res_Role
                    {
                        ID = y.ID,
                        Active = (Boolean)y.Active,
                        Code = y.Code,
                        DateCreated = (DateTime)y.DateCreated,
                        Name = y.Name
                    }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Res_Role Create(Req_Role item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (!_context.Roles.Any(x => x.Code.Equals(item.Code)))
                    {
                        var role = new Role();
                        role.Code = item.Code;
                        role.Name = item.Name;
                        role.Active = item.Active == null ? false : item.Active;
                        role.DateCreated = DateTime.UtcNow;
                        _context.Roles.Add(role);
                        _context.SaveChanges();
                        return _context.Roles.Where(x=>x.Code.Equals(item.Code)).Select(y => new Res_Role {
                            ID = y.ID,
                            Active = (Boolean)y.Active,
                            Code = y.Code,
                            DateCreated = (DateTime)y.DateCreated,
                            Name = y.Name
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

        public Res_Role Update(Req_Role item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Roles.Any(x => x.Code.Equals(item.Code)))
                    {
                        var role = _context.Roles.SingleOrDefault(x => x.Code.Equals(item.Code));
                        role.Name = item.Name;
                        role.Active = item.Active;
                        _context.SaveChanges();
                        return _context.Roles.Where(x => x.Code.Equals(item.Code)).Select(y => new Res_Role
                        {
                            ID = y.ID,
                            Active = (Boolean)y.Active,
                            Code = y.Code,
                            DateCreated = (DateTime)y.DateCreated,
                            Name = y.Name
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

        public bool Delete(string code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Roles.Any(x => x.Code.Equals(code)))
                    {
                        var role = _context.Roles.SingleOrDefault(x => x.Code.Equals(code));
                        _context.Roles.Remove(role);
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

        public bool UpdateStatus(string code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Roles.Any(x => x.Code.Equals(code)))
                    {
                        var role = _context.Roles.SingleOrDefault(x => x.Code.Equals(code));
                        role.Active = !role.Active;
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
