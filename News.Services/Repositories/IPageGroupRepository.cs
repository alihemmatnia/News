using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using News.DomainClasses.Page;
using News.DomainClasses.PageGroups;
using News.ViewModel.Page;

namespace News.Services.Repositories
{
    public interface IPageGroupRepository
    {
        IEnumerable<PageGroup> GetAllPageGroups();
        PageGroup GetPageGroupById(int Id);
        void InsertPageGroup(PageGroup pageGroup);
        void UpdatePageGroup(PageGroup pageGroup);
        void DeletePageGroup(int id);
        bool GroupExists(int id);
        List<ShowGroupsVM> GetListGroups();
        void Save();
    }
}
