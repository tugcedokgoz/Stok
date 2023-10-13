using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class RequestRepository : RepositoryBase<Requests>
    {
        private readonly RepositoryContext _context;
        public RequestRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetRequestAsync()
        {
            var request = await _context.Requests
               .Include(r => r.User)
               .Include(r => r.Category)
               .Include(r => r.Product)
               .Include(r => r.RequestStatus)

               .ToListAsync();

            var requestDetails = request.Select(request => new
            {
                request.Id,
                request.Amount,
                request.UserId,
                request.CategoryId,
                request.ProductId,
                request.CreateDate,
                request.UpdateDate,
                request.RequestStatusId,
                request.UserSuperiorId,
                UserFullName = request.User != null ? request.User.UserFullName : null,
                UserEmail = request.User != null ? request.User.UserEmail : null,
                CategoryName = request.Category != null ? request.Category.CategoryName : null,
                ProductName = request.Product != null ? request.Product.ProductName : null,
                ProductAmount = request.Product?.Amount ?? 0,
                Price = request.Product?.Price, //decimal olduğu için
                StatusRequest = request.RequestStatus != null ? request.RequestStatus.StatusRequest : null,
                Superior = request.User != null ? request.User.UserFullName : null,
                RequestStatusName = request.RequestStatus != null ? request.RequestStatus.StatusRequest : null,

            });
            return requestDetails;
        }

        public async Task<IEnumerable<object>> GetRequestsByUserIdAsync(int userId)
        {
            var requests = await _context.Requests
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Category)
                .Include(r => r.Product)
                .Include(r => r.RequestStatus)
                .ToListAsync();

            var requestDetails = requests.Select(request => new
            {
                request.Id,
                request.Amount,
                request.UserId,
                request.CategoryId,
                request.ProductId,
                request.CreateDate,
                request.UpdateDate,
                request.RequestStatusId,
                request.UserSuperiorId,
                UserFullName = request.User != null ? request.User.UserFullName : null,
                UserEmail = request.User != null ? request.User.UserEmail : null,
                CategoryName = request.Category != null ? request.Category.CategoryName : null,
                ProductName = request.Product != null ? request.Product.ProductName : null,
                ProductAmount = request.Product?.Amount ?? 0,
                Price = request.Product?.Price, // decimal olduğu için
                StatusRequest = request.RequestStatus != null ? request.RequestStatus.StatusRequest : null,
                Superior = request.UserSuperiorId != null
                 ? _context.Users
                .Where(u => u.Id == request.UserSuperiorId)
                .Select(u => u.UserFullName)
                .FirstOrDefault()
            : null,
                RequestStatusName = request.RequestStatus != null ? request.RequestStatus.StatusRequest : null,
            });

            return requestDetails;
        }
        public async Task<IEnumerable<object>> GetRequestsBySuperiorIdAsync(int id)
        {
            var requests = await _context.Requests
                .Where(r => r.UserSuperiorId == id)
                .Include(r => r.User)
                .Include(r => r.Category)
                .Include(r => r.Product)
                .Include(r => r.RequestStatus)
                .ToListAsync();

            var requestDetails = requests.Select(request => new
            {
                request.Id,
                request.Amount,
                request.UserId,
                request.CategoryId,
                request.ProductId,
                request.CreateDate,
                request.UpdateDate,
                request.RequestStatusId,
                request.UserSuperiorId,
                UserFullName = request.User != null ? request.User.UserFullName : null,
                UserEmail = request.User != null ? request.User.UserEmail : null,
                CategoryName = request.Category != null ? request.Category.CategoryName : null,
                ProductName = request.Product != null ? request.Product.ProductName : null,
                ProductAmount = request.Product?.Amount ?? 0,
                Price = request.Product?.Price,
                StatusRequest = request.RequestStatus != null ? request.RequestStatus.StatusRequest : null,
                Superior = request.UserSuperiorId != null
                    ? _context.Users
                        .Where(u => u.Id == request.UserSuperiorId)
                        .Select(u => u.UserFullName)
                        .FirstOrDefault()
                    : null,
                RequestStatusName = request.RequestStatus != null ? request.RequestStatus.StatusRequest : null,
            });

            return requestDetails;
        }

        //muhasebe
        public async Task<IEnumerable<object>> GetRequestsByStatusAsync(int statusId)
        {
            var requests = await _context.Requests
                .Where(r => r.RequestStatusId == statusId)
                .Include(r => r.User)
                .Include(r => r.Category)
                .Include(r => r.Product)
                .Include(r => r.RequestStatus)
                .ToListAsync();

            var requestDetails = requests.Select(request => new
            {
                request.Id,
                request.Amount,
                request.UserId,
                request.CategoryId,
                request.ProductId,
                request.CreateDate,
                request.UpdateDate,
                request.RequestStatusId,
                request.UserSuperiorId,
                UserFullName = request.User != null ? request.User.UserFullName : null,
                UserEmail = request.User != null ? request.User.UserEmail : null,
                CategoryName = request.Category != null ? request.Category.CategoryName : null,
                ProductName = request.Product != null ? request.Product.ProductName : null,
                ProductAmount = request.Product?.Amount ?? 0,
                Price = request.Product?.Price,
                StatusRequest = request.RequestStatus != null ? request.RequestStatus.StatusRequest : null,
                Superior = request.UserSuperiorId != null
                    ? _context.Users
                        .Where(u => u.Id == request.UserSuperiorId)
                        .Select(u => u.UserFullName)
                        .FirstOrDefault()
                    : null,
                RequestStatusName = request.RequestStatus != null ? request.RequestStatus.StatusRequest : null,
            });

            return requestDetails;
        }


        public async Task<Requests> FindByIdAsync(int requestId)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
            return request;
        }

        public int GetSuperiorIdForUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return user.SuperiorId ?? 0;
            }

            return 0;
        }

        //silme
        public void Delete(int id)
        {
            var entityToDelete = RepositoryContext.Set<Requests>().Find(id);
            if (entityToDelete != null)
            {
                RepositoryContext.Set<Requests>().Remove(entityToDelete);
                RepositoryContext.SaveChanges();
            }
        }


    }
}
