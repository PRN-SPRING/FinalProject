using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IFeedbackRepository
    {
        Task<FeedbackDTO> GetFeedbackByIdAsync(int id);
        Task<IEnumerable<FeedbackDTO>> GetAllFeedbacksAsync();
        Task AddFeedbackAsync(FeedbackDTO feedback);
        Task UpdateFeedbackAsync(FeedbackDTO feedback);
        Task<bool> DeleteFeedbackAsync(int feedbackId);

    }
}
