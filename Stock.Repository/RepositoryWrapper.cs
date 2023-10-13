using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class RepositoryWrapper
    {
        private RepositoryContext _context;

        private CategoryRepository _categoryRepository;

        private CompanyDepartmentRepository _companydepartmentRepository;

        private ProductRepository _productRepository;

        private ProductStockRepository _productStockRepository;

        private ReportRepository _reportRepository;

        private RequestRepository _requestRepository;

        private RoleRepository _roleRepository;

        private RoleUserRepository _roleUserRepository;

        private UserRepository _userRepository;

        private SupplierCompanyRepository _supplierCompanyRepository;

        private DepartmentStockRepository _departmentStockRepository;

        private BillRepository _billRepository;

        private CompanyRepository _companyRepository;

        private BidRepository _billBidRepository;

        private CompanyUserRepository _companyUserRepository;



        public RepositoryWrapper(RepositoryContext context)
        {
            this._context = context;
        }
        public CategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_context);
                return _categoryRepository;
            }
        }
        public CompanyDepartmentRepository CompanyDepartmentRepository
        {
            get
            {
                if (_companydepartmentRepository == null)
                    _companydepartmentRepository = new CompanyDepartmentRepository(_context);
                return _companydepartmentRepository;
            }
        }
        public ProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_context);
                return _productRepository;
            }
        }
        public ProductStockRepository ProductStockRepository
        {
            get
            {
                if (_productStockRepository == null)
                    _productStockRepository = new ProductStockRepository(_context);
                return _productStockRepository;
            }
        }
        public ReportRepository ReportRepository
        {
            get
            {
                if (_reportRepository == null)
                    _reportRepository = new ReportRepository(_context);
                return _reportRepository;
            }
        }
        public RequestRepository RequestRepository
        {
            get
            {
                if (_requestRepository == null)
                    _requestRepository = new RequestRepository(_context);
                return _requestRepository;
            }
        }
        public RoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                    _roleRepository = new RoleRepository(_context);
                return _roleRepository;
            }
        }
        public SupplierCompanyRepository SupplierCompanyRepository
        {
            get
            {
                if (_supplierCompanyRepository == null)
                    _supplierCompanyRepository = new SupplierCompanyRepository(_context);
                return _supplierCompanyRepository;
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }
        public DepartmentStockRepository DepartmentStockRepository
        {
            get
            {
                if (_departmentStockRepository == null)
                    _departmentStockRepository = new DepartmentStockRepository(_context);
                return _departmentStockRepository;
            }
        }

        public BillRepository BillRepository
        {
            get
            {
                if (_billRepository == null)
                    _billRepository = new BillRepository(_context);
                return _billRepository;
            }
        }
        public CompanyRepository CompanyRepository
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_context);
                return _companyRepository;
            }
        }
        public RoleUserRepository RoleUserRepository
        {
            get
            {
                if(_roleUserRepository == null)
                    _roleUserRepository = new RoleUserRepository(_context);
                return _roleUserRepository;
            }
        }
        public BidRepository BidRepository
        {
            get
            {
                if(_billBidRepository == null)
                    _billBidRepository = new BidRepository(_context);
                return _billBidRepository;
            }
        }
        public CompanyUserRepository CompanyUserRepository
        {
            get
            {
                if (_companyUserRepository == null)
                    _companyUserRepository = new CompanyUserRepository(_context);
                return _companyUserRepository;
            }
        }



        public void SaveChanges()
        {
            _context.SaveChanges();

        }
    }
}
