using CMS_Library.Data;
using CMS_Library.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Library.Models
{
    public class Req_Account_Login
    {
        [Required(ErrorMessage = "The username is required !")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The password is required !")]
        public string Password { get; set; }
    }
    public class Req_Account_ChangePassword
    {
        [Required(ErrorMessage = "The old password is required !")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "The new password is required !")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password confirm is not correct!")]
        public string ConfirmPassword { get; set; }
    }
    public class Res_Account
    {
        public string GUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string TokenLogin { get; set; }
        public Boolean Active { get; set; }

        public List<Res_Account> GetList()
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Accounts.Select(y => new Res_Account
                    {
                        GUID = y.GUID,
                        FirstName = y.FirstName,
                        LastName = y.LastName,
                        FullName = y.FullName,
                        Username = y.Username,
                        Email = y.Email,
                        PhoneNumber = y.PhoneNumber,
                        Address = y.Address,
                        DateOfBirth = (DateTime)y.DateOfBirth,
                        TokenLogin = y.TokenLogin,
                        Active = (Boolean)y.Active
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Account Get(string GUID)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    return _context.Accounts.Where(x => x.GUID.Equals(GUID)).Select(y => new Res_Account
                    {
                        GUID = y.GUID,
                        FirstName = y.FirstName,
                        LastName = y.LastName,
                        FullName = y.FullName,
                        Username = y.Username,
                        Email = y.Email,
                        PhoneNumber = y.PhoneNumber,
                        Address = y.Address,
                        DateOfBirth = (DateTime)y.DateOfBirth,
                        TokenLogin = y.TokenLogin,
                        Active = (Boolean)y.Active
                    }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Res_Account Create(Req_Account item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (!_context.Accounts.Any(x => (x.Username.Equals(item.Email) && x.IsVerifyEmail == true) || (x.PhoneNumber.Equals(item.PhoneNumber) && x.IsVerifyPhone == true)))
                    {
                        var acc = new Account();
                        acc.Active = item.Active;
                        acc.Address = item.Address;
                        acc.DateCreated = DateTime.UtcNow;
                        acc.DateOfBirth = item.DateOfBirth;
                        acc.Email = item.Email;
                        acc.PhoneNumber = item.PhoneNumber;
                        acc.FirstName = item.FirstName;
                        acc.LastName = item.LastName;
                        acc.FullName = item.FirstName + " " + item.LastName;
                        acc.GroupID = item.GroupID;
                        acc.GUID = CMS_Helpers.GenerateGUID();
                        acc.IdCardNumber = item.IdCardNumber;
                        acc.IsVerifyEmail = item.IsVerifyEmail;
                        acc.IsVerifyPhone = item.IsVerifyPhone;
                        acc.Username = item.Username;
                        acc.Password = CMS_Helpers.MD5(item.Password);
                        acc.TokenLogin = CMS_Helpers.GenerateGUID();
                        _context.Accounts.Add(acc);
                        _context.SaveChanges();

                        return _context.Accounts.Where(x => x.GUID.Equals(acc.GUID)).Select(y => new Res_Account
                        {
                            GUID = y.GUID,
                            FirstName = y.FirstName,
                            LastName = y.LastName,
                            FullName = y.FullName,
                            Username = y.Username,
                            Email = y.Email,
                            PhoneNumber = y.PhoneNumber,
                            Address = y.Address,
                            DateOfBirth = (DateTime)y.DateOfBirth,
                            TokenLogin = y.TokenLogin,
                            Active = (Boolean)y.Active
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
        public Res_Account Update(string GUID, Req_Account item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Accounts.Any(x => x.GUID.Equals(GUID)))
                    {
                        var acc = _context.Accounts.SingleOrDefault(x => x.GUID.Equals(GUID));
                        acc.Active = item.Active;
                        acc.Address = item.Address;
                        acc.DateOfBirth = item.DateOfBirth;
                        acc.Email = item.Email;
                        acc.PhoneNumber = item.PhoneNumber;
                        acc.FirstName = item.FirstName;
                        acc.LastName = item.LastName;
                        acc.FullName = item.FirstName + " " + item.LastName;
                        acc.GroupID = item.GroupID;
                        acc.IdCardNumber = item.IdCardNumber;
                        _context.SaveChanges();

                        return _context.Accounts.Where(x => x.GUID.Equals(GUID)).Select(y => new Res_Account
                        {
                            GUID = y.GUID,
                            FirstName = y.FirstName,
                            LastName = y.LastName,
                            FullName = y.FullName,
                            Username = y.Username,
                            Email = y.Email,
                            PhoneNumber = y.PhoneNumber,
                            Address = y.Address,
                            DateOfBirth = (DateTime)y.DateOfBirth,
                            TokenLogin = y.TokenLogin,
                            Active = (Boolean)y.Active
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
        public bool ChangePassword(string TokenLogin, string NewPassword)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Accounts.Any(x => x.TokenLogin.Equals(TokenLogin)))
                    {
                        var acc = _context.Accounts.SingleOrDefault(x => x.TokenLogin.Equals(TokenLogin));
                        acc.Password = CMS_Helpers.MD5(NewPassword);
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
        public Res_Account UpdateAvata(string GUID, string Img)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Accounts.Any(x => x.GUID.Equals(GUID)))
                    {
                        var acc = _context.Accounts.SingleOrDefault(x => x.GUID.Equals(GUID));
                        acc.Thumbnail = CMS_Helpers.ConvertBase64ToImage(Img, "Thumbnail-" + CMS_Helpers.MD5(DateTime.UtcNow.ToString()));
                        _context.SaveChanges();
                        return _context.Accounts.Where(x => x.GUID.Equals(acc.GUID)).Select(y => new Res_Account
                        {
                            GUID = y.GUID,
                            FirstName = y.FirstName,
                            LastName = y.LastName,
                            FullName = y.FullName,
                            Username = y.Username,
                            Email = y.Email,
                            PhoneNumber = y.PhoneNumber,
                            Address = y.Address,
                            DateOfBirth = (DateTime)y.DateOfBirth,
                            TokenLogin = y.TokenLogin,
                            Active = (Boolean)y.Active
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
        public bool UpdateStatus(string GUID)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Accounts.Any(x => x.GUID.Equals(GUID)))
                    {
                        var acc = _context.Accounts.SingleOrDefault(x => x.GUID.Equals(GUID));
                        acc.Active = !acc.Active;
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
        public bool Delete(string GUID)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Accounts.Any(x => x.GUID.Equals(GUID)))
                    {
                        var acc = _context.Accounts.SingleOrDefault(x => x.GUID.Equals(GUID));
                        _context.Accounts.Remove(acc);
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
        public Res_Account Login(Req_Account_Login item)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    item.Password = CMS_Helpers.MD5(item.Password);
                    if (_context.Accounts.Any(x => x.Username.Equals(item.Username) && x.Password.Equals(item.Password)))
                    {
                        var acc = _context.Accounts.SingleOrDefault(x => x.Username.Equals(item.Username) && x.Password.Equals(item.Password));
                        if (acc.ExpireTokenLogin == null || DateTime.Compare(DateTime.UtcNow, (DateTime)acc.ExpireTokenLogin) > 0)
                        {
                            acc.TokenLogin = CMS_Helpers.GenerateGUID();
                            acc.ExpireTokenLogin = DateTime.UtcNow.AddHours(12);
                        }
                        _context.SaveChanges();

                        return _context.Accounts.Where(x => x.GUID.Equals(acc.GUID)).Select(y => new Res_Account
                        {
                            GUID = y.GUID,
                            FirstName = y.FirstName,
                            LastName = y.LastName,
                            FullName = y.FullName,
                            Username = y.Username,
                            Email = y.Email,
                            PhoneNumber = y.PhoneNumber,
                            Address = y.Address,
                            DateOfBirth = (DateTime)y.DateOfBirth,
                            TokenLogin = y.TokenLogin,
                            Active = (Boolean)y.Active
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
        public bool LogOut(string TokenLogin)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Accounts.Any(x => x.TokenLogin.Equals(TokenLogin)))
                    {
                        var acc = _context.Accounts.SingleOrDefault(x => x.TokenLogin.Equals(TokenLogin));
                        acc.ExpireTokenLogin = DateTime.UtcNow;
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
        public bool ForgotPassword(string Username)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Accounts.Any(x => x.Username.Equals(Username)))
                    {
                        var acc = _context.Accounts.SingleOrDefault(x => x.Username.Equals(Username));
                        var NewPassword = CMS_Helpers.GeneratePassword();
                        acc.Password = CMS_Helpers.MD5(NewPassword);
                        if (string.IsNullOrEmpty(acc.Email))
                        {
                            CMS_Helpers.SendEmail(acc.Email, "Quên mật khẩu", "Mật khẩu mới là: " + NewPassword);
                            return true;
                        }
                        return false;
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
    public class Req_Account
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        [Required(ErrorMessage = "The first name is required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The last name is required!")]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "The email is required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The phone number is required!")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "The username is required!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The password is required!")]
        public string Password { get; set; }
        public string Thumbnail { get; set; }
        public string IdCardNumber { get; set; }
        public int GroupID { get; set; }
        public Boolean IsAppove { get; set; }
        public Boolean Active { get; set; }
        public Boolean IsVerifyEmail { get; set; }
        public Boolean IsVerifyPhone { get; set; }
        public Boolean IsBanned { get; set; }
        public DateTime DateBanned { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
