using Senparc.CO2NET;
using Senparc.Ncf.Core.AppServices;
using Opathon.Xncf.WeixinManagerWxOpen.Domain.Services;
using Opathon.Xncf.WeixinManagerWxOpen.Models.DatabaseModel.Dto;
using Opathon.Xncf.WeixinManagerWxOpen.OHS.Local.PL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.WebApi;

namespace Opathon.Xncf.WeixinManagerWxOpen.OHS.Local.AppService
{
    public class ColorAppService : AppServiceBase
    {
        private readonly ColorService _colorService;

        public ColorAppService(IServiceProvider serviceProvider, ColorService colorService) : base(serviceProvider)
        {
            this._colorService = colorService;
        }


        /// <summary>
        /// 获取或初始化一个 ColorDto 对象
        /// </summary>
        /// <returns></returns>
        public async Task<AppResponseBase<Color_GetOrInitColorResponse>> GetOrInitColorAsync()
        {
            return await this.GetResponseAsync<AppResponseBase<Color_GetOrInitColorResponse>, Color_GetOrInitColorResponse>(async (response, logger) =>
            {
                var dt1 = SystemTime.Now;//开始计时

                var colorDto = await _colorService.GetOrInitColor();//获取或初始化颜色参数

                var costMs = SystemTime.DiffTotalMS(dt1);//记录耗时

                Color_GetOrInitColorResponse result = new(colorDto.Red, colorDto.Green, colorDto.Blue, costMs);

                return result;
            });
        }


        /// <summary>
        /// 变亮
        /// </summary>
        /// <returns></returns>
        [ApiBind(ApiRequestMethod = ApiRequestMethod.Get)]
        public async Task<AppResponseBase<ColorDto>> GetBrightenAsync()
        {
            return await this.GetResponseAsync<AppResponseBase<ColorDto>, ColorDto>(async (response, logger) =>
            {
                var colorDto = await _colorService.Brighten().ConfigureAwait(false);
                return colorDto;
            });
        }

        /// <summary>
        /// 变暗
        /// </summary>
        /// <returns></returns>
        [ApiBind]
        public async Task<AppResponseBase<ColorDto>> GetDarkenAsync()
        {
            return await this.GetResponseAsync<AppResponseBase<ColorDto>, ColorDto>(async (response, logger) =>
            {
                var colorDto = await _colorService.Darken().ConfigureAwait(false);
                return colorDto;
            });
        }

        /// <summary>
        /// 变暗
        /// </summary>
        /// <returns></returns>
        [ApiBind]
        public async Task<AppResponseBase<ColorDto>> GetRandomAsync()
        {
            return await this.GetResponseAsync<AppResponseBase<ColorDto>, ColorDto>(async (response, logger) =>
            {
                var colorDto = await _colorService.Random().ConfigureAwait(false);
                return colorDto;
            });
        }
    }
}
