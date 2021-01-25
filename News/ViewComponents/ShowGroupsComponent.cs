using Microsoft.AspNetCore.Mvc;
using News.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.ViewComponnets
{
    public class ShowGroupsComponent:ViewComponent
    {
        private readonly IPageGroupRepository _pageGroupRepository;
        public ShowGroupsComponent(IPageGroupRepository pageGroupRepository)
        {
            _pageGroupRepository = pageGroupRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowGroupComponent", _pageGroupRepository.GetListGroups()));
        }
    }
}
