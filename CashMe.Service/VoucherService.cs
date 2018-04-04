using System.Collections.Generic;
using CashMe.Core.Data;
using CashMe.Service.Models;
using CashMe.Data.DAL;

namespace CashMe.Service
{
    public interface IVoucherService
    {

        IEnumerable<Voucher> GetAllVoucher();
        Voucher GetVoucher(long id);
        void InsertVoucher(Voucher model);
        void UpdateVoucher(Voucher model);
        void DeleteVoucher(Voucher model);
        IEnumerable<VoucherModel> GetVoucherVIewStore();

        void Save();
        void Dispose();

    }
    public class VoucherService : IVoucherService
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<Voucher> GetAllVoucher()
        {
            return unitOfWork.VoucherRepository.GetAll();
        }

        public Voucher GetVoucher(long id)
       {
           return unitOfWork.VoucherRepository.GetById(id);
       }
        public void InsertVoucher(Voucher model)
       {
           unitOfWork.VoucherRepository.Insert(model);
       }

        public void UpdateVoucher(Voucher model)
        {
            unitOfWork.VoucherRepository.Update(model);
        }
        public void DeleteVoucher(Voucher model)
        {
            unitOfWork.VoucherRepository.Delete(model);
        }
        public IEnumerable<VoucherModel> GetVoucherVIewStore()
        {
            return unitOfWork.VoucherViewRepository.ExecWithStoreProcedure("SELECT * FROM VoucherView");
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
