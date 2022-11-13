using Senparc.Ncf.Core.AppServices;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET;
using Senparc.CO2NET.WebApi;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Business.JsonResult;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Helpers;
using Senparc.Xncf.WeixinManagerBase.Domain.Services;
using Senparc.CO2NET.Cache;

namespace Opathon.Xncf.WeixinManagerWxOpen.OHS.Local.AppService
{
    public class WxOpenAppService : AppServiceBase
    {
        public string WxOpenAppId { get; set; }
        public string WxOpenAppSecret { get; set; }

        private UserService _userService;
        public WxOpenAppService(IServiceProvider serviceProvider, UserService userService) : base(serviceProvider)
        {
            WxOpenAppId = Senparc.Weixin.Config.SenparcWeixinSetting.WxOpenAppId;
            WxOpenAppSecret = Senparc.Weixin.Config.SenparcWeixinSetting.WxOpenAppSecret;
            _userService = userService;
        }

        [ApiBind(ApiRequestMethod = ApiRequestMethod.Post)]
        public async Task<AppResponseBase<string>> LoginAsync(string code)
        {
            return await this.GetResponseAsync<AppResponseBase<string>, string>(async (response, logger) =>
            {
                try
                {
                    var jsonResult = SnsApi.JsCode2Json(WxOpenAppId, WxOpenAppSecret, code);
                    if (jsonResult.errcode == ReturnCode.请求成功)
                    {
                        //Session["WxOpenUser"] = jsonResult;//使用Session保存登陆信息（不推荐）
                        //使用SessionContainer管理登录信息（推荐）
                        var unionId = "";
                        var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, unionId);

                        //注意：生产环境下SessionKey属于敏感信息，不能进行传输！
                        return sessionBag.Key;
                    }
                    else
                    {
                        response.Success = false;
                        return jsonResult.errmsg;
                    }
                }
                catch (Exception ex)
                {
                    logger.Append(ex.Message);
                    logger.Append(ex.StackTrace);
                    logger.SaveLogs("登录失败");

                    response.Success = false;
                    return ex.Message;
                }
            });
        }

        [ApiBind(ApiRequestMethod = ApiRequestMethod.Post)]
        public async Task<AppResponseBase<bool>> CheckSignAsync(string sessionId, string rawData, string signature)
        {
            return await this.GetResponseAsync<AppResponseBase<bool>, bool>(async (response, logger) =>
            {
                var checkSuccess = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.CheckSignature(sessionId, rawData, signature);
                return checkSuccess;
            });
        }

        [ApiBind(ApiRequestMethod = ApiRequestMethod.Post)]
        public async Task<AppResponseBase<Phone_Info>> GetUserPhoneNumberAsync(string code)
        {
            return await this.GetResponseAsync<AppResponseBase<Phone_Info>, Phone_Info>(async (response, logger) =>
            {
                try
                {
                    var result = await BusinessApi.GetUserPhoneNumberAsync(WxOpenAppId, code);
                    return result.phone_info;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    return null;
                }
            });
        }

        [ApiBind(ApiRequestMethod = ApiRequestMethod.Post)]
        public async Task<AppResponseBase<string>> DecodeEncryptedDataAsync(string type, string sessionId, string encryptedData, string iv)
        {
            return await this.GetResponseAsync<AppResponseBase<string>, string>(async (response, logger) =>
            {
                DecodeEntityBase decodedEntity = null;

                try
                {
                    switch (type.ToUpper())
                    {
                        case "USERINFO"://wx.getUserInfo()
                            decodedEntity = EncryptHelper.DecodeUserInfoBySessionId(
                                sessionId,
                                encryptedData, iv);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.SendCustomLog("EncryptHelper.DecodeUserInfoBySessionId 方法出错",
                        $@"sessionId: {sessionId}
encryptedData: {encryptedData}
iv: {iv}
sessionKey: {(await SessionContainer.CheckRegisteredAsync(sessionId)
                    ? (await SessionContainer.GetSessionAsync(sessionId)).SessionKey
                    : "未保存sessionId")}

异常信息：
{ex.ToString()}
");
                }

                //检验水印
                var checkWatermark = false;
                if (decodedEntity != null)
                {
                    checkWatermark = decodedEntity.CheckWatermark(WxOpenAppId);

                    //保存用户信息（可选）
                    if (checkWatermark && decodedEntity is DecodedUserInfo decodedUserInfo)
                    {
                        var sessionBag = await SessionContainer.GetSessionAsync(sessionId);
                        if (sessionBag != null)
                        {
                            await SessionContainer.AddDecodedUserInfoAsync(sessionBag, decodedUserInfo);
                        }

                        //保存到数据库
                        var cache = CacheStrategyFactory.GetObjectCacheStrategyInstance();
                        using (var cacheLock = await cache.BeginCacheLockAsync("Opathon", decodedUserInfo.unionId))
                        {
                            var user = await _userService.GetObjectAsync(z => z.UnionId == decodedUserInfo.unionId);
                            if (user == null)
                            {
                                user = new Senparc.Xncf.WeixinManagerBase.Domain.Models.DatabaseModel.User(null, "", "", "", "", decodedUserInfo.unionId, decodedUserInfo.nickName, 0, "", "", "", "", decodedUserInfo.avatarUrl);

                                await _userService.SaveObjectAsync(user);
                            }
                        }
                    }
                }

                response.Success = checkWatermark;

                return $"水印验证：{(checkWatermark ? "通过" : "不通过")}";
            });
        }
    }
}
