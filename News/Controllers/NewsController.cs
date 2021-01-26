using Microsoft.AspNetCore.Mvc;
using News.DomainClasses.Page;
using News.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Controllers
{
    public class NewsController : Controller
    {
        private IPageRepository _pageRepository;
        public NewsController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        [Route("News/{id}")]
        public IActionResult ShowNews(int id)
        {
            Page page = _pageRepository.GetPageById(id);
            if (page != null)
            {
                page.Visit += 1;
                _pageRepository.UpdatePage(page);
                _pageRepository.Save();
                return View(page);
            }
            if(page==null){
                return NotFound();
            }
            return View(page);
        }
        [Route("Group/{groupid}/{grouptitle}")]
        public IActionResult ShowPageGroupById(int groupid, string grouptitle)
        {
            ViewData["grouptitle"] = grouptitle;
            return View(_pageRepository.GetPageByGroupId(groupid));
        }

        [Route("Search")]
        public IActionResult Search(string key)
        {
            return View(_pageRepository.Search(key));
        }

        [Route("Archive")]
        public IActionResult Archive()
        {
            return View(_pageRepository.GetAllPage());
        }
    }
}
