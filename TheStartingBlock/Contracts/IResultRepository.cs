using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStartingBlock.Models;



namespace TheStartingBlock.Contracts
{
    public interface IResultRepository
    {
        Task<List<Result>> GetResultsAsync();
        Task<Result> GetResultByIdAsync(int resultId);
        Task AddResultAsync(ResultInputModel newResultInput);
        Task UpdateResultAsync(ResultInputModel newResult);
        Task DeleteResultAsync(int resultId);
        Task UpdatePositionsForEventAsync(int eventId);
    }
}
