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
        IEnumerable<Page> GetTopPage(int take = 4);
        void InsertPage(Page page);
        IEnumerable<Page> Search(string key);
        void UpdatePage(Page page);
        IEnumerable<Page> GetPageByGroupId(int id);
        IEnumerable<Page> GetPagesInSlider();
        IEnumerable<Page> GetLatesPage();
        void DeletePage(Page page);
        void DeletePage(int Id);
        bool PageExists(int id);
        void Save();
    }
}
