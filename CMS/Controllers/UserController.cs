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
    /// Tài khoản người dùng
    /// </summary>
    public class UserController : BaseApiController
    {
        Res_User user = new Res_User();

        /// <summary>
        /// Lấy danh sách tài khoản người dùng
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        [Route("API/User/GetUsers")]
        [HttpGet]
        public IHttpActionResult GetList([FromHeader]string TokenLogin)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    List<Res_User> data = user.GetList();
                    if (data != null)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.SUCCESS));
                    }
                    return Content(HttpStatusCode.BadRequest, CommonConst.USER_BADREQUEST);
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize(CommonConst.UNAUTHENTICATION));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Lấy thông tin tài khoản người dùng
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        [Route("API/User/GetUser/{GUID}")]
        [HttpGet]
        public IHttpActionResult Get([FromHeader]string TokenLogin, string GUID)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(GUID))
                    {
                        Res_User data = user.Get(GUID);
                        if (data != null)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.SUCCESS));
                        }
                        return Content(HttpStatusCode.OK, res.Ok(null, CommonConst.NOTEXISTS, false));
                    }
                    return Content(HttpStatusCode.BadRequest, res.BadRequest(CommonConst.DATA_NOT_BLANK));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize(CommonConst.UNAUTHENTICATION));                
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
        [Route("API/User/Login")]
        [HttpPost]
        public IHttpActionResult Login(Req_User_Login item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                }
                var data = user.Login(item);
                if (data != null)
                {
                    return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.SUCCESS));
                }
                return Content(HttpStatusCode.BadRequest, res.BadRequest(CommonConst.FAIL));
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
        [Route("API/User/Logout")]
        public IHttpActionResult Logout([FromHeader]string TokenLogin)
        {
            try
            {
                var data = user.LogOut(TokenLogin);
                if (data)
                {
                    return Content(HttpStatusCode.OK, res.Ok(null, CommonConst.SUCCESS));
                }
                return Content(HttpStatusCode.OK, res.Ok(null, CommonConst.FAIL, false));
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
        [Route("API/User/ForgotPassword")]
        public IHttpActionResult ForgotPassword(string Username)
        {
            try
            {
                if (!string.IsNullOrEmpty(Username))
                {
                    var data = user.ForgotPassword(Username);
                    if (data)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(null, CommonConst.SUCCESS));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, CommonConst.FAIL, false));
                }
                return Content(HttpStatusCode.BadRequest, res.BadRequest(CommonConst.DATA_NOT_BLANK));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Đăng ký tài khoản người dùng
        /// </summary>
        /// <param name="token"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/User/Register")]
        public IHttpActionResult Create(Req_User item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                }
                var data = user.Create(item);
                if (data != null)
                {
                    return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.SUCCESS));
                }
                return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.FAIL, false));
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
        [Route("API/User/Update/{GUID}")]
        public IHttpActionResult Update([FromHeader]string TokenLogin, string GUID, Req_User item)
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
                        var data = user.Update(GUID, item);
                        if (data != null)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.SUCCESS));
                        }
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, CommonConst.FAIL, false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize(CommonConst.UNAUTHENTICATION));
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
        [Route("API/User/UpdateStatus/{GUID}")]
        public IHttpActionResult UpdateStatus([FromHeader]string TokenLogin, string GUID)
        {
            try
            {
                if (checkAuth(TokenLogin)) {
                    if (!string.IsNullOrEmpty(GUID))
                    {
                        var data = user.UpdateStatus(GUID);
                        if (data)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(null, CommonConst.SUCCESS));
                        }
                        return Content(HttpStatusCode.OK, res.Ok(null, CommonConst.FAIL, false));
                    }
                }  
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize(CommonConst.UNAUTHENTICATION));
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
        [Route("API/User/ChangePassword")]
        public IHttpActionResult ChangePassword([FromHeader]string TokenLogin, Req_User_ChangePassword item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    var data = user.ChangePassword(TokenLogin, item.NewPassword);
                    if (data != null)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.SUCCESS));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.FAIL, false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize(CommonConst.UNAUTHENTICATION));
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
        [Route("API/User/Delete/{GUID}")]
        public IHttpActionResult Delete([FromHeader]string TokenLogin, string GUID)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    var data = user.Delete(GUID);
                    if (data)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.SUCCESS));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, CommonConst.FAIL, false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize(CommonConst.UNAUTHENTICATION));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }
    }
}
