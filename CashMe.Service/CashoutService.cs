using System.Collections.Generic;
using CashMe.Core.Data;
using CashMe.Service.Models;

namespace CashMe.Service
{
    public interface ICashoutService
    {
        IEnumerable<History_Checkout> GetAllCashout();
        History_Checkout GetCashout(long id);
        void InsertCashout(History_Checkout model);
        void UpdateCashout(History_Checkout model);
        void DeleteCashout(History_Checkout model);
        IEnumerable<CashoutModel> GetAllCashoutView();
        void Save();
        void Dispose();

    }
    public class CashoutService : ICashoutService
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<History_Checkout> GetAllCashout()
       {
           return unitOfWork.CashoutRepository.GetAll();
        }

       public History_Checkout GetCashout(long id)
       {
           return unitOfWork.CashoutRepository.GetById(id);
       }
        public void InsertCashout(History_Checkout model)
       {
           unitOfWork.CashoutRepository.Insert(model);
       }

        public void UpdateCashout(History_Checkout model)
        {
            unitOfWork.CashoutRepository.Update(model);
        }
        public void DeleteCashout(History_Checkout model)
        {
            unitOfWork.CashoutRepository.Delete(model);
        }
        public IEnumerable<CashoutModel> GetAllCashoutView()
        {
            return unitOfWork.CashoutModelRepository.ExecWithStoreProcedure("SELECT * FROM CashoutView");
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
