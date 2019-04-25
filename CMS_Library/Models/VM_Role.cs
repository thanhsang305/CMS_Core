using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    class VM_Role
    {
        public int? ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }
        public DateTime DateCreated { get; set; }

        public string Create(VM_Role item)
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
                        role.Active = item.Active;
                        role.DateCreated = DateTime.UtcNow;
                        _context.Roles.Add(role);
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

        public string Update(VM_Role item)
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

        public string Delete(string code)
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
                        return CommonConst.SUCCESS;

                    }
                    else
                    {
                        return CommonConst.ERROR;
                    }
                }
            }
            catch (Exception e)
            {
                return CommonConst.ERROR;
            }
        }

        public string UpdateStatus(string code)
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
