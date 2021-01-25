using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using News.Services.Repositories;
using System.Threading.Tasks;
using News.DomainClasses.PageGroups;
using News.DataLayer.Context;

namespace News.Services.Services
{
    public class PageGroupRepository : IPageGroupRepository
    {
        private AppDbContext _context;
        public PageGroupRepository(AppDbContext context)
        {
            _context = context;
        }
        public void DeletePageGroup(PageGroup pageGroup)
        {
            _context.Entry(pageGroup).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

        }

        public void DeletePageGroup(int Id)
        {
            _context.Entry(GetPageGroupById(Id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public PageGroup GetPageGroupById(int Id)
        {
            return _context.PageGroups.Find(Id);
        }

        public void InsertPageGroup(PageGroup pageGroup)
        {
            _context.PageGroups.Add(pageGroup);
        }

        public IEnumerable<PageGroup> GetAllPageGroups()
        {
            return _context.PageGroups.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
       
        public void UpdatePageGroup(PageGroup pageGroup)
        {
            _context.Entry(pageGroup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public bool GroupExists(int id)
        {
            return _context.PageGroups.Any(i => i.GroupId == id);
        }

        public void DeltePageGroup(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
