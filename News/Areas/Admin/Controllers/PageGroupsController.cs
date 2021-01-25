using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using News.DataLayer.Context;
using News.DomainClasses.PageGroups;
using News.Services.Repositories;

namespace News.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PageGroupsController : Controller
    {
        private readonly IPageGroupRepository _pageGroupRepository;

        public PageGroupsController(AppDbContext context, IPageGroupRepository pageGroupRepository) 
        {
            _pageGroupRepository = pageGroupRepository;
        }

        public IActionResult Index()
        {
            return View(_pageGroupRepository.GetAllPageGroups());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroup = _pageGroupRepository.GetPageGroupById(id.Value);
            if (pageGroup == null)
            {
                return NotFound();
            }

            return View(pageGroup);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("GroupId,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                _pageGroupRepository.InsertPageGroup(pageGroup);
                _pageGroupRepository.Save();
                return Redirect("Index");
            }
            return View(pageGroup);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroup = _pageGroupRepository.GetPageGroupById(id.Value);
            if (pageGroup == null)
            {
                return NotFound();
            }
            return View(pageGroup);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("GroupId,GroupTitle")] PageGroup pageGroup)
        {
            if (id != pageGroup.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _pageGroupRepository.UpdatePageGroup(pageGroup);
                    _pageGroupRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageGroupExists(pageGroup.GroupId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("Index");

            }
            return View(pageGroup);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroup = _pageGroupRepository.GetPageGroupById(id.Value);
            if (pageGroup == null)
            {
                return NotFound();
            }

            return View(pageGroup);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var pageGroup = _pageGroupRepository.GetPageGroupById(id);
            _pageGroupRepository.DeletePageGroup(pageGroup);
            _pageGroupRepository.Save();
            return Redirect("Index");
        }

        private bool PageGroupExists(int id)
        {
            return _pageGroupRepository.GroupExists(id);
        }
    }
}
