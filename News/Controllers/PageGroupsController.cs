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

namespace News.Web.Controllers
{
    [Authorize]
    public class PageGroupsController : Controller
    {
        private readonly IPageGroupRepository _pageGroupRepository;

        public PageGroupsController(IPageGroupRepository pageGroupRepository)
        {
            _pageGroupRepository = pageGroupRepository;
        }

        // GET: PageGroups
        public IActionResult Index()
        {
            return View(_pageGroupRepository.GetListGroups());
        }

        // GET: PageGroups/Details/5
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

        // GET: PageGroups/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GroupId,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                _pageGroupRepository.InsertPageGroup(pageGroup);
                _pageGroupRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(pageGroup);
        }

        // GET: PageGroups/Edit/5
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
        [ValidateAntiForgeryToken]
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
                return RedirectToAction(nameof(Index));
            }
            return View(pageGroup);
        }

        // GET: PageGroups/Delete/5
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

        // POST: PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _pageGroupRepository.DeletePageGroup(id);
            _pageGroupRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PageGroupExists(int id)
        {
            return _pageGroupRepository.GroupExists(id);
        }
    }
}
