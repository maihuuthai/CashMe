using System.Collections.Generic;
using CashMe.Core.Data;
using CashMe.Service.Models;
using CashMe.Data.DAL;

namespace CashMe.Service
{
    public interface ICashbackService
    {

        IEnumerable<Main_Cashback> GetAllCashback();
        Main_Cashback GetCashback(long id);
        void InsertCashback(Main_Cashback model);
        void UpdateCashback(Main_Cashback model);
        void DeleteCashback(Main_Cashback model);
        IEnumerable<CashbackModel> GetCashbackView();
        IEnumerable<CashbackModel> GetCashbackMaxValueView();

        void Save();
        void Dispose();

    }
    public class CashbackService : ICashbackService
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<Main_Cashback> GetAllCashback()
        {
            return unitOfWork.CashbackRepository.GetAll();
        }

        public Main_Cashback GetCashback(long id)
       {
           return unitOfWork.CashbackRepository.GetById(id);
       }
        public void InsertCashback(Main_Cashback model)
       {
           unitOfWork.CashbackRepository.Insert(model);
       }

        public void UpdateCashback(Main_Cashback model)
        {
            unitOfWork.CashbackRepository.Update(model);
        }
        public void DeleteCashback(Main_Cashback model)
        {
            unitOfWork.CashbackRepository.Delete(model);
        }
        public IEnumerable<CashbackModel> GetCashbackView()
        {
            return unitOfWork.CashbackViewRepository.ExecWithStoreProcedure("SELECT * FROM CashbackView");
        }
        public IEnumerable<CashbackModel> GetCashbackMaxValueView()
        {
            return unitOfWork.CashbackViewRepository.ExecWithStoreProcedure("SELECT * FROM CashbackMaxValueView");
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
