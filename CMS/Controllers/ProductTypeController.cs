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
    /// Loại sản phẩm
    /// </summary>
    public class ProductTypeController : BaseApiController
    {
        Res_ProductType prodType = new Res_ProductType();

        /// <summary>
        /// Lấy danh sách loại sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/ProductType/GetList")]
        public IHttpActionResult GetList([FromHeader]string TokenLogin)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    List<Res_ProductType> data = prodType.GetList();
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
        /// Lấy thông tin 1 loại sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/ProductType/Get/{Code}")]
        public IHttpActionResult Get([FromHeader]string TokenLogin, string Code)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = prodType.Get(Code);
                        return Content(HttpStatusCode.OK, res.Ok(data, "Thành công!"));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Loại sản phẩm không có.", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Tạo mới 1 loại sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/ProductType/Create")]
        public IHttpActionResult Create([FromHeader]string TokenLogin, Req_ProductType item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    var data = prodType.Create(item);
                    if (data != null)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Tạo mới loại sản phẩm thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Loại sản phẩm đã tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin 1 loại sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="GUID"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("API/ProductType/Update/{Code}")]
        public IHttpActionResult Update([FromHeader]string TokenLogin, string Code, Req_ProductType item)
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
                        var data = prodType.Update(Code, item);
                        if (data != null)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(data, "Cập nhật loại sản phẩm thành công."));
                        }
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Loại sản phẩm không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật trạng thái 1 loại sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("API/ProductType/UpdateStatus/{Code}")]
        public IHttpActionResult UpdateStatus([FromHeader]string TokenLogin, string Code)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = prodType.UpdateStatus(Code);
                        if (data)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật loại sản phẩm thành công"));
                        }
                        return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật loại sản phẩm không thành công", false));
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
        /// Xóa 1 loại sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("API/ProductType/Delete/{Code}")]
        public IHttpActionResult Delete([FromHeader]string TokenLogin, string Code)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    var data = prodType.Delete(Code);
                    if (data)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Xóa loại sản phẩm thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Loại sản phẩm không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
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
