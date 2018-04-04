using System.Collections.Generic;
using CashMe.Core.Data;

namespace CashMe.Service
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategories(long id);
        void InsertCategories(Category cat);
        void UpdateCategories(Category cat);
        void DeleteCategories(Category cat);

        void Save();
        void Dispose();

    }
    public class CategoriesService : ICategoriesService
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<Category> GetAllCategories()
       {
           return unitOfWork.CategoriesRepository.GetAll();
        }

       public Category GetCategories(long id)
       {
           return unitOfWork.CategoriesRepository.GetById(id);
       }
        public void InsertCategories(Category cat)
       {
           unitOfWork.CategoriesRepository.Insert(cat);
       }

        public void UpdateCategories(Category cat)
        {
            unitOfWork.CategoriesRepository.Update(cat);
        }
        public void DeleteCategories(Category cat)
        {
            unitOfWork.CategoriesRepository.Delete(cat);
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
