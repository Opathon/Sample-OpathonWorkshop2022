using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Xncf.WeixinManagerBase.Domain.Models.DatabaseModel.Dto;
using Senparc.Xncf.WeixinManagerBase.Domain.Services;
using System.Threading.Tasks;

namespace Senparc.Xncf.WeixinManagerMP.Areas.MP.Pages
{
    public class OAuthModel : PageModel
    {
        //���滻���˺Ŷ�Ӧ����Ϣ��Ҳ���Է���web.config�ȵط��������ú͸���
        public readonly string appId = Config.SenparcWeixinSetting.WeixinAppId;//��΢�Ź����˺ź�̨��AppId���ñ���һ�£����ִ�Сд��
        private readonly string appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;//��΢�Ź����˺ź�̨��AppId���ñ���һ�£����ִ�Сд��

        private UserService _userService;
        private IHttpContextAccessor _httpContextAccessor;

        public User_ViewDto User_ViewDto { get; set; }

        public OAuthModel(UserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var user = _userService.GetObject(z => z.Id == userId.Value);
                User_ViewDto = _userService.Mapper.Map<User_ViewDto>(user);
            }
        }

        public IActionResult OnGetLogin(string returnUrl)
        {
            var url = OAuthApi.GetAuthorizeUrl(appId,
                   "https://707a-122-97-147-54.ngrok.io/MP/OAuth?handler=CallBack&returnUrl=" + returnUrl.UrlEncode(),
                   null, OAuthScope.snsapi_userinfo);//snsapi_userinfo��ʽ�ص���ַ

            return Redirect(url);
        }

        public async Task<IActionResult> OnGetCallBackAsync(string code, string returnUrl)
        {
            if (code.IsNullOrEmpty())
            {
                return Content("���ܾ�����Ȩ��");
            }

            var oauthAccessTokenResult = await OAuthApi.GetAccessTokenAsync(appId, appSecret, code);

            var openId = oauthAccessTokenResult.openid;
            var accessToken = oauthAccessTokenResult.access_token;

            var userInfo = await OAuthApi.GetUserInfoAsync(accessToken, openId);

            var userInfoDto = await _userService.CreateOrUpdateFromOAuthAsync(userInfo.openid, userInfo.nickname, userInfo.headimgurl, userInfo.unionid);

            //TODO����¼�û���¼��Ϣ
            _httpContextAccessor.HttpContext.Session.SetInt32("UserId", userInfoDto.Id);

            if (returnUrl.IsNullOrEmpty())
            {
                return Redirect("/MP/OAuth");
            }

            return Redirect(returnUrl);
        }

        public IActionResult OnGetLogout()
        {
            _httpContextAccessor.HttpContext.Session.Remove("UserId");
            return Redirect("/MP/OAuth");
        }
    }

    internal class UserInfoDto
    {
    }
}
