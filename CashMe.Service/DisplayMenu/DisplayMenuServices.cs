using CashMe.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMe.Service.DisplayMenu
{
    public interface IDisplayMenuServices
    {
        IEnumerable<Display_MenuAdmin> GetAll();
        Display_MenuAdmin GetById(int id);
    }
    public class DisplayMenuServices : IDisplayMenuServices
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public IEnumerable<Display_MenuAdmin> GetAll()
        {
            return _unitOfWork.DisplayMenuRepository.GetAll();
        }
        public Display_MenuAdmin GetById(int id)
        {
            return _unitOfWork.DisplayMenuRepository.GetById(id);
        }
        public bool AddOrUpdate(Display_MenuAdmin model)
        {
            var result = false;
            return result;
        }
    }
}
