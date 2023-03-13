using EStore.Models.Domain;
using EStore.Models.DTO;

namespace EStore.Repositories.Abstract
{
    public interface IUserOrderService
    {
        Task<IEnumerable<Order>> UserOrders();
    }
}
