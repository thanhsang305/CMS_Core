using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CMS_Library.Models;

namespace CMS.Controllers
{/// <summary>
/// Nhóm quản trị
/// </summary>
    public class GroupController : BaseApiController
    {
        Res_Group group = new Res_Group();

        /// <summary>
        /// Lấy danh sách nhóm quyền
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/Group/GetList")]
        public IHttpActionResult GetList([FromHeader]string TokenLogin)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    List<Res_Group> data = group.GetList();
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
        /// Lấy thông tin 1 nhóm quyền
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/Group/Get/{Code}")]
        public IHttpActionResult Get([FromHeader]string TokenLogin, string Code)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = group.Get(Code);
                        return Content(HttpStatusCode.OK, res.Ok(data, "Thành công!"));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Mã nhóm không có.", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Tạo mới 1 nhóm quyền
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/Group/Create")]
        public IHttpActionResult Create([FromHeader]string TokenLogin, Req_Group item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    var data = group.Create(item);
                    if (data != null)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Tạo mới nhóm quyền thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Nhóm quyền đã tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin 1 nhóm quyền
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="GUID"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("API/Group/Update/{Code}")]
        public IHttpActionResult Update([FromHeader]string TokenLogin, string Code, Req_Group item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = group.Update(Code, item);
                        if (data != null)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(data, "Cập nhật nhóm quyền thành công."));
                        }
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Nhóm quyền không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật trạng thái 1 nhóm quyền
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("API/Group/UpdateStatus/{Code}")]
        public IHttpActionResult UpdateStatus([FromHeader]string TokenLogin, string Code)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = group.UpdateStatus(Code);
                        if (data)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật nhóm quyền thành công"));
                        }
                        return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật nhóm quyền không thành công", false));
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
        /// Xóa 1 nhóm quyền
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("API/Group/Delete/{Code}")]
        public IHttpActionResult Delete([FromHeader]string TokenLogin, string Code)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    var data = group.Delete(Code);
                    if (data)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Xóa nhóm quyền thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Nhóm quyền không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
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
