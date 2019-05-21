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
    /// Sản phẩm
    /// </summary>
    public class ProductController : BaseApiController
    {
        VM_Product prod = new VM_Product();

        /// <summary>
        /// Lấy danh sách sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/Product/GetList")]
        public IHttpActionResult GetList([FromHeader]string TokenLogin)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    List<Res_ShortProduct> data = prod.GetList();
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
        /// Lấy thông tin 1 sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/Product/Get/{SKU}")]
        public IHttpActionResult Get([FromHeader]string TokenLogin, string SKU)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(SKU))
                    {
                        var data = prod.Get(SKU);
                        return Content(HttpStatusCode.OK, res.Ok(data, "Thành công!"));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Sản phẩm không có.", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Tạo mới 1 sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("API/Product/Create")]
        public IHttpActionResult Create([FromHeader]string TokenLogin, VM_Product item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    var data = prod.Create(item);
                    if (data != null)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Tạo mới sản phẩm thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Sản phẩm đã tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="GUID"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("API/Product/Update/{SKU}")]
        public IHttpActionResult Update([FromHeader]string TokenLogin, string SKU, VM_Product item)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!ModelState.IsValid)
                    {
                        return Content(HttpStatusCode.BadRequest, res.BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
                    }
                    if (!string.IsNullOrEmpty(SKU))
                    {
                        var data = prod.Update(SKU, item);
                        if (data != null)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(data, "Cập nhật sản phẩm thành công."));
                        }
                    }
                    return Content(HttpStatusCode.OK, res.Ok(null, "Sản phẩm không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
                }
                return Content(HttpStatusCode.Unauthorized, res.UnAuthorize("Tài khoản không có quyền"));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, res.BadRequest(e.Message));
            }
        }

        /// <summary>
        /// Cập nhật trạng thái 1 sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("API/Product/UpdateStatus/{SKU}")]
        public IHttpActionResult UpdateStatus([FromHeader]string TokenLogin, string SKU)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    if (!string.IsNullOrEmpty(SKU))
                    {
                        var data = prod.UpdateStatus(SKU);
                        if (data)
                        {
                            return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật trạng thái sản phẩm thành công"));
                        }
                        return Content(HttpStatusCode.OK, res.Ok(null, "Cập nhật trạng thái sản phẩm không thành công", false));
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
        /// Xóa 1 sản phẩm
        /// </summary>
        /// <param name="TokenLogin"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("API/Product/Delete/{SKU}")]
        public IHttpActionResult Delete([FromHeader]string TokenLogin, string SKU)
        {
            try
            {
                if (checkAuth(TokenLogin))
                {
                    var data = prod.Delete(SKU);
                    if (data)
                    {
                        return Content(HttpStatusCode.OK, res.Ok(data, "Xóa sản phẩm thành công."));
                    }
                    return Content(HttpStatusCode.OK, res.Ok(data, "Sản phẩm không tồn tại trong hệ thống. Vui lòng kiểm tra lại", false));
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
