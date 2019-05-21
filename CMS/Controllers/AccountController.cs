using CMS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMS.Controllers
{
    /// <summary>
    /// Tài khoản quản trị viên
    /// </summary>
    public class AccountController : BaseApiController
    {
        VM_Account acc = new VM_Account();

        /// <summary>
        /// Lấy danh sách tài khoản
        /// </summary>
        /// <returns>
        /// string:abc
        /// </returns>
        [Route("API/Account/GetAccounts")]
        [HttpGet]
        public IHttpActionResult GetList([FromHeader]string TokenLogin)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    List<Res_Account> data = acc.GetList();
                    return Content(HttpStatusCode.OK, res.Ok(data, "Thành công!"));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Lấy thông tin tài khoản
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        [Route("API/Account/GetAccount/{GUID}")]
        [HttpGet]
        public IHttpActionResult Get([FromHeader]string TokenLogin, string GUID)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(GUID))
                    {
                        Res_Account data = acc.Get(GUID);
                        return Content(HttpStatusCode.OK, res.Ok(data, "Thành công"));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Tài khoản không có.", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));                
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Route("API/Account/Login")]
        [HttpPost]
        public IHttpActionResult Login(VM_Account_Login item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Content(HttpStatusCode.BadRequest, res.BadRequest("Tên đăng nhập hoặc mật khẩu không đúng vui lòng kiểm tra lại."));
                }
                var data = acc.Login(item);
                return Content(HttpStatusCode.OK, res.Ok(data, "Đăng nhập thành công!"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/Account/Logout")]
        public IHttpActionResult Logout([FromHeader]string TokenLogin)
        {
            try
            {
                var data = acc.LogOut(TokenLogin);
                if (data)
                {
                    return Content(HttpStatusCode.OK, res.Ok(null, "Đăng xuất thành công!"));
                }
                return Content(HttpStatusCode.OK, res.Ok(null, "Đăng xuất không thành công!", false));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Quên mật khẩu
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/Account/ForgotPassword")]
        public IHttpActionResult ForgotPassword(string Username)
        {
            try
            {
                if (!string.IsNullOrEmpty(Username))
                {
                    var data = acc.ForgotPassword(Username);
                    if (data)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(null, "Mật khẩu đã được gửi về email hoặc sđt."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Email hoặc sđt không tồn tại trong hệ thống.", false));
                }
                return Content(HttpStatusCode.BadRequest, res.BadRequest("Vui lòng nhập email hoặc sđt."));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Tạo mới tài khoản
        /// </summary>
        /// <param name="token"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/Account/Create")]
        public IHttpActionResult Create([FromHeader]string TokenLogin, VM_Account item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    var data = acc.Create(item);
                    if (data!=null)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Tạo mới tài khoản thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Tài khoản đã tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// </summary>
        /// <param name="token"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("API/Account/Update/{GUID}")]
        public IHttpActionResult Update([FromHeader]string TokenLogin, string GUID, VM_Account item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    if (!string.IsNullOrEmpty(GUID))
                    {
                        var data = acc.Update(GUID, item);
                        if (data != null)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(data, "Cập nhật tài khoản thành công."));
                        }
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Tài khoản không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="token"></param>
        /// <param name="GUID"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("API/Account/UpdateStatus/{GUID}")]
        public IHttpActionResult UpdateStatus([FromHeader]string TokenLogin, string GUID)
        {
            try
            {
                if (checkAuth(TokenLogin)) {
                    if (!string.IsNullOrEmpty(GUID))
                    {
                        var data = acc.UpdateStatus(GUID);
                        if (data)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật trạng thái thành công"));
                        }
                        return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật trạng thái không thành công", false));
                    }
                }  
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền."));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Đổi mật khẩu tài khoản
        /// </summary>
        /// <param name="token"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("API/Account/ChangePassword")]
        public IHttpActionResult ChangePassword([FromHeader]string TokenLogin, VM_Account_ChangePassword item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    var data = acc.ChangePassword(TokenLogin, item.NewPassword);
                    if (data != null)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Đổi mật khẩu thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Tài khoản không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="token"></param>
        /// <param name="GUID"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("API/Account/Delete/{GUID}")]
        public IHttpActionResult Delete([FromHeader]string TokenLogin, string GUID)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    var data = acc.Delete(GUID);
                    if (data)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Xóa tài khoản thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Tài khoản không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }
    }
}
