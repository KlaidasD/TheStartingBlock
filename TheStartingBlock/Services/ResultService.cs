﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStartingBlock.Models;
using TheStartingBlock.Contracts;
using Serilog;

namespace TheStartingBlock.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task AddResultAsync(ResultInputModel newResult)
        {
            try
            {
                Log.Information("Calling AddResultAsync from resultRepository at {Time}", DateTime.UtcNow);
                await _resultRepository.AddResultAsync(newResult);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling AddResultAsync from resultRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task DeleteResultAsync(int resultId)
        {
            try
            {
                Log.Information("Calling DeleteResultAsync from resultRepository at {Time}", DateTime.UtcNow);
                await _resultRepository.DeleteResultAsync(resultId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling DeleteResultAsync from resultRepository: {Error}", ex.Message);
                throw;
            }
        }

        public Task<string> GenerateReportAsync()
        {
            try
            {
                Log.Information("Calling GenerateReportAsync from resultRepository at {Time}", DateTime.UtcNow);
                return _resultRepository.GenerateReportAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error calling GenerateReportAsync from resultRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task<Result> GetResultByIdAsync(int resultId)
        {
            try
            {
                Log.Information("Calling GetResultByIdAsync from resultRepository at {Time}", DateTime.UtcNow);
                return await _resultRepository.GetResultByIdAsync(resultId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling GetResultByIdAsync from resultRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task<List<Result>> GetResultsAsync()
        {
            try
            {
                Log.Information("Calling GetResultsAsync from resultRepository at {Time}", DateTime.UtcNow);
                return await _resultRepository.GetResultsAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error calling GetResultsAsync from resultRepository: {Error}", ex.Message);
                throw;
            }
        }

        public Task UpdatePositionsForEventAsync(int eventId)
        {
            try
            {
                Log.Information("Calling UpdatePositionsForEventAsync from resultRepository at {Time}", DateTime.UtcNow);
                return _resultRepository.UpdatePositionsForEventAsync(eventId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling UpdatePositionsForEventAsync from resultRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task UpdateResultAsync(ResultInputModel newResult)
        {
            try
            {
                Log.Information("Calling UpdateResultAsync from resultRepository at {Time}", DateTime.UtcNow);
                await _resultRepository.UpdateResultAsync(newResult);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling UpdateResultAsync from resultRepository: {Error}", ex.Message);
                throw;
            }
        }


    }
}
