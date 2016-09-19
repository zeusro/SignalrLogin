﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SignalrLogin.Controllers
{
    /// <summary>
    /// 二维码接口
    /// </summary>
    public class QRCodeController : ApiController
    {
        /// <summary>
        /// 二维码登录认证
        /// </summary>
        /// <returns>
        /// 0:登录成功；-1:参数错误 -2:ConnectionId、UUID、UserName、Password不允许为空-3:ConnectionId回话id不存在-4:UUID输入错误-5:UUID已过期
        /// -6:本UUID已登录-7:登录账号已停用-8:登录账号已删除-9:登录密码输入错误-10:登录账号不存在
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<TxSmsOutputDto> QRCodeVerify([FromBody]QRCodeVerifyInput model)
        {
            TxSmsOutputDto result = new TxSmsOutputDto();

            #region 参数验证

            if (model.IsNull())
            {
                result.Result = -1;
                result.Message = "参数错误";
                return result;
            }
            if (model.ConnectionId.IsNullOrEmpty() || model.UUID.Equals(Guid.Empty) || model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
            {
                result.Result = -2;
                result.Message = "ConnectionId、UUID、UserName、Password不允许为空";
                return result;
            }

            #endregion 参数验证

            #region 有效性判断

            //验证ConnectionId合法性
            if (QRCodeAction.QRCodeLists.ContainsKey(model.ConnectionId))
            {
                result.Result = -3;
                result.Message = "ConnectionId回话id不存在";
                return result;
            }
            //验证UUID有效性
            var findCode = QRCodeAction.QRCodeLists[model.ConnectionId];
            if (!model.UUID.Equals(findCode.UUID))
            {
                result.Result = -4;
                result.Message = "UUID输入错误";
                return result;
            }
            if (!findCode.IsValid())
            {
                result.Result = -5;
                result.Message = "UUID已过期";
                return result;
            }
            if (findCode.IsLogin)
            {
                result.Result = -6;
                result.Message = "本UUID已登录";
                return result;
            }

            #endregion 有效性判断

            LoginUserNameInput loginParam = new LoginUserNameInput
            {
                UserName = model.UserName,
                Password = model.Password,
                Platform = model.Platform
            };
            LoginOutput loginResult = await new SessionController().LoginUserName(loginParam);
            switch (loginResult.Result)
            {
                case -1:
                    result.Result = -7;
                    result.Message = "登录账号已停用";
                    break;

                case -2:
                    result.Result = -8;
                    result.Message = "登录账号已删除";
                    break;

                case -3:
                    result.Result = -9;
                    result.Message = "登录密码输入错误";
                    break;

                case -4:
                    result.Result = -10;
                    result.Message = "登录账号不存在";
                    break;
            }
            if (loginResult.Result > 0) //登录成功，值为AccId
            {
                result.Result = 0;
                findCode.IsLogin = true; //更改登录状态
                result.Message = "成功登录";
            }
            return result;
        }
    }
}