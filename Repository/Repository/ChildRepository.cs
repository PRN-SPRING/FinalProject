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

        public async Task<bool> DeleteChildAsync(int child)
        {
            var existingChild = _context.Children.Find(child);
            if (existingChild == null)
            {
                return false;
            }
            _context.Children.Remove(existingChild);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ChildDTO>> GetAllChildrenAsync()
        {
            try
            {
                using (var _context = new PRNFinalProjectDBContext())
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
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error getting all children", ex);
            }
        }

        public async Task<List<Child>> GetChildrenByCustomerIdAsync(int customerId)
        {
            return await _context.Children
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<ChildDTO>> GetChildrenByUserIdAsync(int customerId)
        {
            return await _context.Children
                .Where(c => c.CustomerId == customerId)
                .Select(c => new ChildDTO
                {
                    Id = c.Id,
                    CustomerId = c.CustomerId,
                    CustomerName = c.Customer.FullName, // Assuming `Customer` has `FullName`
                    FullName = c.FullName,
                    Birthdate = c.Birthdate,
                    Gender = c.Gender
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateChildAsync(Child child)
        {
            var existingChild = _context.Children.Find(child.Id);
            if (existingChild == null)
            {
                return false;
            }

            existingChild.FullName = child.FullName;
            existingChild.Birthdate = child.Birthdate;
            existingChild.Gender = child.Gender;

            _context.Children.Update(existingChild);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
