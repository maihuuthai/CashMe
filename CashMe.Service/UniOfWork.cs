using CashMe.Core.Data;
using CashMe.Data.DAL;
using CashMe.Service.Models;
using CashMe.Shared.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace CashMe.Service
{
    public class UnitOfWork : IDisposable
    {
        #region Fields

        //public readonly CashMeContext Context = new CashMeContext();
        public readonly CashMeContext _context = new CashMeContext();
        //public readonly IdentityDbContext<ApplicationUser> _identityContext = new IdentityDbContext<ApplicationUser>;
        private BaseRepository<Category> _CategoriesRepository;
        private BaseRepository<Percent> _PercentRepository;
        private BaseRepository<Linked_Site> _Linked_SiteRepository;
        private BaseRepository<Voucher> _VoucherRepository;
        private BaseRepository<VoucherModel> _VoucherViewRepository;
        private BaseRepository<Main_Cashback> _CashbackRepository;
        private BaseRepository<CashbackModel> _CashbackViewRepository;
        private BaseRepository<GroupSite> _GroupSiteRepository;
        private BaseRepository<History_Checkout> _CashoutRepository;
        private BaseRepository<CashoutModel> _CashoutModelRepository;
        private BaseRepository<Message> _MessageRepository;
        private BaseRepository<MessageModel> _MessageViewRepository;
        private BaseRepository<Display_MenuAdmin> _displayMenuRepository;

        private BaseRepository<IdentityRole> _identityRoleRepository;
        private BaseRepository<IdentityUser> _identityUserRepository;
        private BaseRepository<ApplicationUser> _applicationUserRepository;
        private BaseRepository<IdentityUserRole> _identityUserRoleRepository;


        #endregion

        #region Constructors and Destructors  
        public BaseRepository<Display_MenuAdmin> DisplayMenuRepository
        {
            get
            {
                if (this._displayMenuRepository == null)
                    this._displayMenuRepository = new BaseRepository<Display_MenuAdmin>(_context);
                return _displayMenuRepository;
            }
        }
        public BaseRepository<ApplicationUser> ApplicationUserRepository
        {
            get
            {
                if (this._applicationUserRepository == null)
                    this._applicationUserRepository = new BaseRepository<ApplicationUser>(_context);
                return _applicationUserRepository;
            }
        }
        public BaseRepository<IdentityRole> IdentityRoleRepository
        {            
            get
            {
                if (this._identityRoleRepository == null)
                    this._identityRoleRepository = new BaseRepository<IdentityRole>(_context);
                return _identityRoleRepository;
            }
        }
        public BaseRepository<IdentityUser> IdentityUserRepository
        {
            get
            {
                if (this._identityUserRepository == null)
                    this._identityUserRepository = new BaseRepository<IdentityUser>(_context);
                return _identityUserRepository;
            }
        }
        public BaseRepository<IdentityUserRole> IdentityUserRoleRepository
        {
            get
            {
                if (this._identityUserRoleRepository == null)
                    this._identityUserRoleRepository = new BaseRepository<IdentityUserRole>(_context);
                return _identityUserRoleRepository;
            }
        }
        public BaseRepository<Category> CategoriesRepository
        {
            get
            {                
              
                if (this._CategoriesRepository == null)
                    this._CategoriesRepository = new BaseRepository<Category>(_context);
                return _CategoriesRepository;
            }
        }
        public BaseRepository<Percent> PercentRepository
        {
            get
            {
                if (this._PercentRepository == null)
                    this._PercentRepository = new BaseRepository<Percent>(_context);
                return _PercentRepository;
            }
        }
        public BaseRepository<Linked_Site> Linked_SiteRepository
        {
            get
            {
                if (this._Linked_SiteRepository == null)
                    this._Linked_SiteRepository = new BaseRepository<Linked_Site>(_context);
                return _Linked_SiteRepository;
            }
        }

        public BaseRepository<Voucher> VoucherRepository
        {
            get
            {
                if (this._VoucherRepository == null)
                    this._VoucherRepository = new BaseRepository<Voucher>(_context);
                return _VoucherRepository;
            }
        }
        public BaseRepository<VoucherModel> VoucherViewRepository
        {
            get
            {
                if (this._VoucherViewRepository == null)
                    this._VoucherViewRepository = new BaseRepository<VoucherModel>(_context);
                return _VoucherViewRepository;
            }
        }
        public BaseRepository<Main_Cashback> CashbackRepository
        {
            get
            {
                if (this._CashbackRepository == null)
                    this._CashbackRepository = new BaseRepository<Main_Cashback>(_context);
                return _CashbackRepository;
            }
        }
        public BaseRepository<CashbackModel> CashbackViewRepository
        {
            get
            {
                if (this._CashbackViewRepository == null)
                    this._CashbackViewRepository = new BaseRepository<CashbackModel>(_context);
                return _CashbackViewRepository;
            }
        }

        public BaseRepository<History_Checkout> CashoutRepository
        {
            get
            {
                if (this._CashoutRepository == null)
                    this._CashoutRepository = new BaseRepository<History_Checkout>(_context);
                return _CashoutRepository;
            }
        }

        public BaseRepository<CashoutModel> CashoutModelRepository
        {
            get
            {
                if (this._CashoutModelRepository == null)
                    this._CashoutModelRepository = new BaseRepository<CashoutModel>(_context);
                return _CashoutModelRepository;
            }
        }

        public BaseRepository<GroupSite> GroupSiteRepository
        {
            get
            {
                if (this._GroupSiteRepository == null)
                    this._GroupSiteRepository = new BaseRepository<GroupSite>(_context);
                return _GroupSiteRepository;
            }
        }
        public BaseRepository<Message> MessageRepository
        {
            get
            {
                if (this._MessageRepository == null)
                    this._MessageRepository = new BaseRepository<Message>(_context);
                return _MessageRepository;
            }
        }

        public BaseRepository<MessageModel> MessageViewRepository
        {
            get
            {
                if (this._MessageViewRepository == null)
                    this._MessageViewRepository = new BaseRepository<MessageModel>(_context);
                return _MessageViewRepository;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Save()
        {
            _context.SaveChanges();
        }
        #endregion

        #region Disposed

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}
