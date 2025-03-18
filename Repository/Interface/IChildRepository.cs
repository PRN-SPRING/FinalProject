using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IChildRepository
    {
        Task AddChildAsync(Child child);
        Task<List<Child>> GetChildrenByCustomerIdAsync(int customerId);
        Task<IEnumerable<ChildDTO>> GetAllChildrenAsync();

        Task<List<ChildDTO>> GetChildrenByUserIdAsync(int customerId);
    }
}
