using Data;
using Data.DTOs;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Repository
{
    public class ChildRepository : IChildRepository
    {
        private readonly PRNFinalProjectDBContext _context;

        public ChildRepository(PRNFinalProjectDBContext context)
        {
            _context = context;
        }

        public async Task AddChildAsync(Child child)
        {
            await _context.Children.AddAsync(child);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChildDTO>> GetAllChildrenAsync()
        {
            var children = await _context.Children.Include(f => f.Customer).ToListAsync();

            return children.Select(child => new ChildDTO
            {
                Id = child.Id,
                CustomerId = child.CustomerId,
                CustomerName = child.Customer?.FullName,
                FullName = child.FullName,
                Birthdate = child.Birthdate,
                Gender = child.Gender
            });
        }

        public async Task<List<Child>> GetChildrenByCustomerIdAsync(int customerId)
        {
            return await _context.Children
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
