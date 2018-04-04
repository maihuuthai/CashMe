using System.Collections.Generic;
using CashMe.Core.Data;

namespace CashMe.Service
{
    public interface IGroupSiteService
    {
        IEnumerable<GroupSite> GetAllGroupSite();
        GroupSite GetGroupSite(long id);
        void InsertGroupSite(GroupSite group);
        void UpdateGroupSite(GroupSite group);
        void DeleteGroupSite(GroupSite group);

        void Save();
        void Dispose();

    }
    public class GroupSiteService : IGroupSiteService
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<GroupSite> GetAllGroupSite()
       {
           return unitOfWork.GroupSiteRepository.GetAll();
        }

       public GroupSite GetGroupSite(long id)
       {
           return unitOfWork.GroupSiteRepository.GetById(id);
       }
        public void InsertGroupSite(GroupSite group)
       {
           unitOfWork.GroupSiteRepository.Insert(group);
       }

        public void UpdateGroupSite(GroupSite group)
        {
            unitOfWork.GroupSiteRepository.Update(group);
        }
        public void DeleteGroupSite(GroupSite group)
        {
            unitOfWork.GroupSiteRepository.Delete(group);
        }
        public void Save()
        {
            unitOfWork.Save();
        }
        public void Dispose()
        {
            unitOfWork.Dispose();
        }

    }
}
