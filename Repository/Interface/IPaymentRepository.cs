using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsAsync(DateTime startDate, DateTime endDate);

    }
}
