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
    /// Bài viết
    /// </summary>
    public class BlogController : BaseApiController
    {
        VM_Blog blog = new VM_Blog();

        /// <summary>
        /// Lấy danh sách bài viết
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/Blog/GetList")]
        public IHttpActionResult GetList([FromHeader]string TokenLogin)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    List<Res_Blog> data = blog.GetList();
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
        /// Lấy thông tin 1 bài viết
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/Blog/Get/{Alias}")]
        public IHttpActionResult Get([FromHeader]string TokenLogin, string Alias)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(Alias))
                    {
                        var data = blog.Get(Alias);
                        return Content(HttpStatusCode.OK, res.Ok(data, "Thành công!"));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Bài viết không có.", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Tạo mới 1 bài viết
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/Blog/Create")]
        public IHttpActionResult Create([FromHeader]string TokenLogin, VM_Blog item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    var data = blog.Create(item);
                    if (data != null)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Tạo mới bài viết thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Bài viết đã tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin bài viết
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="GUID"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("API/Blog/Update/{Alias}")]
        public IHttpActionResult Update([FromHeader]string TokenLogin, string Alias, VM_Blog item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    if (!string.IsNullOrEmpty(Alias))
                    {
                        var data = blog.Update(Alias, item);
                        if (data != null)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(data, "Cập nhật bài viết thành công."));
                        }
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Bài viết không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật trạng thái 1 bài viết
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("API/Blog/UpdateStatus/{Alias}")]
        public IHttpActionResult UpdateStatus([FromHeader]string TokenLogin, string Alias)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(Alias))
                    {
                        var data = blog.UpdateStatus(Alias);
                        if (data)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật trạng thái bài viết thành công"));
                        }
                        return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật trạng thái bài viết không thành công", false));
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
        /// Xóa 1 bài viết
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("API/Blog/Delete/{Code}")]
        public IHttpActionResult Delete([FromHeader]string TokenLogin, string Code)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    var data = blog.Delete(Code);
                    if (data)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Xóa bài viết thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Bài viết không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
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
