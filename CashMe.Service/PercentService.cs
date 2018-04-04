using System.Collections.Generic;
using CashMe.Core.Data;

namespace CashMe.Service
{
    public interface IPercentService
    {
        IEnumerable<Percent> GetAllPercent();
        Percent GetPercent(long id);
        void InsertPercent(Percent model);
        void UpdatePercent(Percent model);
        void DeletePercent(Percent model);

        void Save();
        void Dispose();

    }
    public class PercentService : IPercentService
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<Percent> GetAllPercent()
       {
           return unitOfWork.PercentRepository.GetAll();
        }

       public Percent GetPercent(long id)
       {
           return unitOfWork.PercentRepository.GetById(id);
       }
        public void InsertPercent(Percent model)
       {
           unitOfWork.PercentRepository.Insert(model);
       }

        public void UpdatePercent(Percent model)
        {
            unitOfWork.PercentRepository.Update(model);
        }
        public void DeletePercent(Percent model)
        {
            unitOfWork.PercentRepository.Delete(model);
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
