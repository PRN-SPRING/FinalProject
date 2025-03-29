using Data;
using BussinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PRNFinalProjectDBContext _context;

        public PaymentRepository(PRNFinalProjectDBContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            var newPayment = new Payment
            {
                AppointmentId = payment.AppointmentId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
            };
            _context.Payments.Add(newPayment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _context.Payments
                    .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving payments.", ex);
            }
        }
    }
}
