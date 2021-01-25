using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using News.Services.Repositories;
using News.DataLayer.Context;
using System.Threading.Tasks;
using News.DomainClasses.Page;

namespace News.Services.Services
{
    public class PageRepository : IPageRepository
    {
        private AppDbContext _context;
        public PageRepository(AppDbContext context)
        {
            _context = context;
        }
        public void DeletePage(Page page)
        {
            _context.Entry(page).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void DeletePage(int Id)
        {
            _context.Entry(GetPageById(Id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

        }

        public IEnumerable<Page> GetAllPage()
        {
            return _context.Pages.ToList();
        }

        public Page GetPageById(int Id)
        {
            return _context.Pages.Find(Id);
        }

        public void InsertPage(Page page)
        {
            _context.Pages.Add(page);
        }

        public bool PageExists(int id)
        {
            return _context.Pages.Any(i => i.PageId == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdatePage(Page page)
        {
            _context.Entry(page).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
