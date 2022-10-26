using Senparc.Ncf.Service;
using System;

namespace Opathon.Xncf.WeixinManagerWxOpen.Areas.WeixinManagerWxOpen.Pages
{
    public class Index : Senparc.Ncf.AreaBase.Admin.AdminXncfModulePageModelBase
    {
        public Index(Lazy<XncfModuleService> xncfModuleService) : base(xncfModuleService)
        {

        }

        public void OnGet()
        {
        }
    }
}
