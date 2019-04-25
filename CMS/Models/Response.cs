﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Response
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public Object Data { get; set; }
        public string Message { get; set; }

        public Response Ok(Object o, string Msg, Boolean status = true)
        {
            if (status == true)
            {
                this.Code = 200;
                this.Status = "The request was successful";
                this.Data = o;
                this.Message = Msg;
            }
            else
            {
                this.Code = 200;
                this.Status = "The request was successful but there is no representation to return";
                this.Message = Msg;
            }
            return this;
        }

        public Response Create(Object o, string Msg)
        {
            this.Code = 201;
            this.Status = "The request was successful and a resource was created.eate";
            this.Data = o;
            this.Message = Msg;
            return this;
        }

        public Response NoContent(string Msg)
        {
            this.Code = 204;
            this.Status = "The request was successful but there is no representation to return";
            this.Message = Msg;
            return this;
        }

        public Response BadRequest(string Msg)
        {
            this.Code = 400;
            this.Status = "The request could not be understood or was missing required parameters.";
            this.Message = Msg;
            return this;
        }

        public Response UnAuthorize(string Msg)
        {
            this.Code = 401;
            this.Status = "Authentication failed or user doesn't have permissions for requested operation.";
            this.Message = Msg;
            return this;
        }

        public Response Forbidden(string Msg)
        {
            this.Code = 403;
            this.Status = "Access denied.";
            this.Message = Msg;
            return this;
        }

        public Response NotFound(string Msg)
        {
            this.Code = 404;
            this.Status = "The requested resource could not be found.";
            this.Message = Msg;
            return this;
        }

        public Response NotAllow(string Msg)
        {
            this.Code = 405;
            this.Status = "Requested method is not supported for resource.";
            this.Message = Msg;
            return this;
        }
    }
}