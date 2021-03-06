﻿using BusinessLayer.Interface;
using Fundoo.Utilities;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.DTOs.AccountDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Fundoo.Controllers
{
    [ApiController]
    [Route("account/")]
    public class AccountController : ControllerBase
    {
        private IAccountService _service;

        public AccountController(IAccountService services)
        {
            _service = services;
        }

        [HttpGet()]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> Index()
        {
            int id = Convert.ToInt32(HttpContext.Items["userId"]);
            return Ok(new
            {
                Data = await _service.Get(id)
                ,
                StatusCode = (int)HttpStatusCode.OK
                ,
                Message = ResponseMessages.SUCCESS
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] AccountRequestDto account)
        {
            AccountResponseDto createdAccount = await _service.AddAccount(account);
            if (createdAccount == null)
            {
                return BadRequest(new
                {
                    Data = "Account already exists",
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ResponseMessages.FAILED
                });
            }
            return Ok(new { Data = createdAccount, StatusCode = (int)HttpStatusCode.Created, ResponseMessages.CREATED });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto credentials)
        {
            var (user, token, refreshToken) = await _service.Authenticate(credentials.Email, credentials.Password);
            HttpContext.Response.Cookies.Append("refresh-token", 
                        refreshToken, 
                        new CookieOptions{ HttpOnly = true,  
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTime.Now.AddDays(1)
                        });
            return Ok(new
            {
                Data =  new { user = user, token = token},
                StatusCode = (int)HttpStatusCode.OK,
                Message = ResponseMessages.SUCCESS
            });
        }

        [HttpPost]
        [Route("forgot")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var currentUrl = HttpContext.Request.Host;
            await _service.ForgotPassword(email, currentUrl.Value);
            return Ok(new
            {
                Data = (string)null,
                StatusCode = HttpStatusCode.OK,
                Message = ResponseMessages.MAIL_SENT
            });
        }

        [HttpPost]
        [Route("reset")]
        public async Task<IActionResult> Reset([FromForm] string password, [FromForm] string token)
        {

            if (await _service.ResetPassword(password, token) == 1)
                return Ok(new
                {
                    Data = (string)null,
                    StatusCode = HttpStatusCode.OK,
                    Message = ResponseMessages.SUCCESS
                });
            return BadRequest(new
            {
                Data = (string)null,
                StatusCode = HttpStatusCode.BadRequest,
                Message = ResponseMessages.FAILED
            });
        }

        [HttpPost]
        [Route("/authentication/refresh")]
        public async Task<IActionResult> GenerateRefreshToken()
        {
            var refreshToken = HttpContext.Request.Cookies
                                    .Where(cookie => cookie.Key == "refresh-token")
                                    .Select(cookie => cookie.Value)
                                    .FirstOrDefault();
            if (refreshToken == null)
            {
                return Unauthorized(new
                {
                    Data = (string) null,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = ResponseMessages.NOTOKEN
                });
            }
            string token = await _service.GetNewTokenAsync(refreshToken);
            return Unauthorized(new
            {
                Data = token,
                StatusCode = HttpStatusCode.OK,
                Message = ResponseMessages.SUCCESS
            });
        }
    }
}
