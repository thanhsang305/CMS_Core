using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class Req_Group_User
    {
        [Required(ErrorMessage = "The code is required!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "The name is required!")]
        public string Name { get; set; }
        public Boolean Active { get; set; }
    }
    public class Res_Group_User
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Res_Group_User> GetList()
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.GroupUsers.Select(y => new Res_Group_User
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
        public Res_Group_User Get(string Code)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.GroupUsers.Where(x=>x.Code.Equals(Code)).Select(y => new Res_Group_User
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
        public Res_Group_User Create(Req_Group_User item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (!_context.GroupUsers.Any(x => x.Code.Equals(item.Code)))
                    {
                        var Group_User = new GroupUser();
                        Group_User.Code = item.Code;
                        Group_User.Name = item.Name;
                        Group_User.Active = item.Active;
                        Group_User.DateCreated = DateTime.UtcNow;
                        _context.GroupUsers.Add(Group_User);
                        _context.SaveChanges();

                        return _context.GroupUsers.Where(x => x.Code.Equals(Group_User.Code)).Select(y => new Res_Group_User
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
        public Res_Group_User Update(string Code, Req_Group_User item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.GroupUsers.Any(x => x.Code.Equals(Code)))
                    {
                        var Group_User = _context.GroupUsers.SingleOrDefault(x => x.Code.Equals(Code));
                        Group_User.Name = item.Name;
                        Group_User.Active = item.Active;
                        _context.SaveChanges();
                        return _context.GroupUsers.Where(x => x.Code.Equals(Group_User.Code)).Select(y => new Res_Group_User
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
                    if (_context.GroupUsers.Any(x => x.Code.Equals(Code)))
                    {
                        var Group_User = _context.GroupUsers.SingleOrDefault(x => x.Code.Equals(Code));
                        _context.GroupUsers.Remove(Group_User);
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
                    if (_context.GroupUsers.Any(x => x.Code.Equals(Code)))
                    {
                        var Group_User = _context.GroupUsers.SingleOrDefault(x => x.Code.Equals(Code));
                        Group_User.Active = !Group_User.Active;
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
