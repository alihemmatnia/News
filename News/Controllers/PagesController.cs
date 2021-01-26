using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using News.DataLayer.Context;
using News.DomainClasses.Page;
using News.Services.Repositories;
namespace News.Web.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
        private readonly IPageRepository _pageRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPageGroupRepository _pageGroupRepository;

        public PagesController(IPageGroupRepository pageGroupRepository, IPageRepository pageRepository, UserManager<IdentityUser> userManager)
        {
            _pageRepository = pageRepository;
            _pageGroupRepository = pageGroupRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_pageRepository.GetAllPage());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Page page = _pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        public IActionResult Create()
        {
            ViewData["Groups"] = new SelectList(_pageGroupRepository.GetAllPageGroups(), "GroupId", "GroupTitle");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile imgup,[Bind("PageId,GroupId,PageTitle,ShortDescription,PageContent,Visit,ImageName,ShowSlider,PageTag,CreateDate,Writer")] Page page)
        {
            if (ModelState.IsValid)
            {
                page.CreateDate = DateTime.Now;
                page.Visit = 0;
                page.Writer = _userManager.GetUserName(User);
                if (imgup != null)
                {
                    page.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);
                    string savepath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Images", page.ImageName
                    );
                    using (FileStream st = new FileStream(savepath, FileMode.Create))
                    {
                        await imgup.CopyToAsync(st);
                    }
                }
                _pageRepository.InsertPage(page);
                _pageRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Page page = _pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            ViewData["Groups"] = new SelectList(_pageGroupRepository.GetAllPageGroups(), "GroupId", "GroupTitle", page.GroupId);
            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IFormFile imgup,int id, [Bind("PageId,GroupId,PageTitle,ShortDescription,PageContent,Visit,ImageName,ShowSlider,PageTag,CreateDate,Writer")] Page page)
        {
            if (id != page.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imgup != null)
                    {
                        if (page.ImageName == null)
                        {
                            page.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);

                        }
                        string savepath = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/Images", page.ImageName
                            );
                        using (FileStream st = new FileStream(savepath, FileMode.Create))
                        {
                            imgup.CopyTo(st);
                        }
                    }
                    _pageRepository.UpdatePage(page);
                    _pageRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Page page = _pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Page page = _pageRepository.GetPageById(id);
            _pageRepository.DeletePage(page);
            if (page.ImageName != null)
            {
                string imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", page.ImageName);
                if (System.IO.File.Exists(imgpath))
                {
                    System.IO.File.Delete(imgpath);
                }
            }
            _pageRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _pageRepository.PageExists(id);
        }
    }
}
