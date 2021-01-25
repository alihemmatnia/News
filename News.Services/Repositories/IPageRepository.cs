using News.DomainClasses.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Services.Repositories
{
    public interface IPageRepository
    {
        IEnumerable<Page> GetAllPage();
        Page GetPageById(int Id);
        void InsertPage(Page page);
        void UpdatePage(Page page);
        void DeletePage(Page page);
        void DeletePage(int Id);
        bool PageExists(int id);
        void Save();
    }
}
