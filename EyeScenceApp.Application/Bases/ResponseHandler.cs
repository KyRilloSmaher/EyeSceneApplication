﻿using EyeScenceApp.Domain.Shared;

namespace EyeScenceApp.Application.Bases
{
        public class ResponseHandler
        {
            #region Feilds



            #endregion

            #region Constructors

            public ResponseHandler()
            {

            }
            #endregion


            #region Methods
            public Response<T> Deleted<T>(T data ,string message = null)
            {
                return new Response<T>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Succeeded = true,
                    Message = message ??"DeletedSuccessfully"
                };
            }
            public Response<T> Success<T>(T entity, string message = null, object Meta = null)
            {
                return new Response<T>()
                {
                    Data = entity,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Succeeded = true,
                    Message = message ?? "Success",
                    Meta = Meta
                };
            }
            public Response<T> Unauthorized<T>(string message = null)
            {
                return new Response<T>()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                    Succeeded = true,
                    Message = message ?? "Unauthorized"
                };
            }
            public Response<T> BadRequest<T>(string message = null)
            {
                return new Response<T>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Succeeded = false,
                    Message = message ??"NotFound"
                };
            }
        public Response<T> BadRequest<T>(T data,string message = null)
        {
            return new Response<T>()
            {
                Data = data,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = message ??"Not Found"
            };
        }
        public Response<T> UnprocessableEntity<T>(string message = null)
            {
                return new Response<T>()
                {
                    StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                    Succeeded = false,
                    Message = message ?? "InternalServerError"
                };
            }
            public Response<T> Failed<T>(string message = null)
            {
                return new Response<T>()
                {
                    StatusCode = System.Net.HttpStatusCode.FailedDependency,
                    Succeeded = false,
                    Message = message ?? "InternalServerError"
                };
            }
            public Response<T> NotFound<T>(string message = null)
            {
                return new Response<T>()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Succeeded = false,
                    Message = message ?? "NotFound"
                };
            }

            public Response<T> Created<T>(T entity, object Meta = null)
            {
                return new Response<T>()
                {
                    Data = entity,
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Succeeded = true,
                    Message = "CreatedSuccessfully",
                    Meta = Meta
                };
            }
            public Response<T> Added<T>(string message = null)
            {
                return new Response<T>()
                {

                    StatusCode = System.Net.HttpStatusCode.OK,
                    Succeeded = true,
                    Message = message ??"AddedSuccessfully"

                };
            }
            #endregion
        }
    }

