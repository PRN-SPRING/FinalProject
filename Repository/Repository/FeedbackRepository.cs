using Data;
using BussinessObject.DTOs;
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
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly PRNFinalProjectDBContext _context;

        public FeedbackRepository(PRNFinalProjectDBContext context)
        {
            _context = context;
        }

        public async Task<FeedbackDTO> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _context.Feedbacks
                .Include(f => f.Customer)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (feedback == null) return null;

            return new FeedbackDTO
            {
                Id = feedback.Id,
                AppointmentId = feedback.AppointmentId,
                CustomerId = feedback.CustomerId,
                CustomerName = feedback.Customer?.FullName,
                Rating = feedback.Rating,
                Comments = feedback.Comments
            };
        }

        public async Task<IEnumerable<FeedbackDTO>> GetAllFeedbacksAsync()
        {
            try
            {
                using (var _context = new PRNFinalProjectDBContext())
                {
                    var feedbacks = await _context.Feedbacks
                        .Include(f => f.Customer)
                        .ToListAsync();
                    var feedbackDTOs = new List<FeedbackDTO>();

                    foreach (var feedback in feedbacks)
                    {
                        feedbackDTOs.Add(new FeedbackDTO
                        {
                            Id = feedback.Id,
                            AppointmentId = feedback.AppointmentId,
                            CustomerId = feedback.CustomerId,
                            CustomerName = feedback.Customer?.FullName,
                            Rating = feedback.Rating,
                            Comments = feedback.Comments
                        });
                    }

                    return feedbackDTOs;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error getting all feedbacks", ex);
            }
        }

        public async Task AddFeedbackAsync(FeedbackDTO feedbackDTO)
        {
            var feedback = new Feedback
            {
                AppointmentId = feedbackDTO.AppointmentId,
                CustomerId = feedbackDTO.CustomerId,
                Rating = feedbackDTO.Rating,
                Comments = feedbackDTO.Comments
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeedbackAsync(FeedbackDTO feedbackDTO)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackDTO.Id);
            if (feedback != null)
            {
                feedback.AppointmentId = feedbackDTO.AppointmentId;
                feedback.CustomerId = feedbackDTO.CustomerId;
                feedback.Rating = feedbackDTO.Rating;
                feedback.Comments = feedbackDTO.Comments;

                _context.Entry(feedback).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteFeedbackAsync(int feedbackId)
        {
            try
            {
                using (var _context = new PRNFinalProjectDBContext())
                {
                    var feedback = await _context.Feedbacks.FindAsync(feedbackId);
                    if (feedback != null)
                    {
                        _context.Feedbacks.Remove(feedback);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error deleting feedback", ex);
            }
        }

    }
}
