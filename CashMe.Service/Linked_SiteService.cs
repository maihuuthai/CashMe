using System.Collections.Generic;
using CashMe.Core.Data;

namespace CashMe.Service
{
    public interface ILinked_SiteService
    {
        IEnumerable<Linked_Site> GetAllLinked_Site();
        Linked_Site GetLinked_Site(long id);
        void InsertLinked_Site(Linked_Site model);
        void UpdateLinked_Site(Linked_Site model);
        void DeleteLinked_Site(Linked_Site model);

        void Save();
        void Dispose();

    }
    public class Linked_SiteService : ILinked_SiteService
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<Linked_Site> GetAllLinked_Site()
       {
           return unitOfWork.Linked_SiteRepository.GetAll();
        }

       public Linked_Site GetLinked_Site(long id)
       {
           return unitOfWork.Linked_SiteRepository.GetById(id);
       }
        public void InsertLinked_Site(Linked_Site model)
       {
           unitOfWork.Linked_SiteRepository.Insert(model);
       }

        public void UpdateLinked_Site(Linked_Site model)
        {
            unitOfWork.Linked_SiteRepository.Update(model);
        }
        public void DeleteLinked_Site(Linked_Site model)
        {
            unitOfWork.Linked_SiteRepository.Delete(model);
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
