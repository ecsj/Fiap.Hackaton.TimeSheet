using Domain.Entities;
using Domain.Repositories;
using Domain.Repositories.Base;
using Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class TimeRecordRepository : ITimeRecordRepository
{
    private readonly IRepository<TimeRecord> _repository;

    public TimeRecordRepository(IRepository<TimeRecord> repository)
    {
        _repository = repository;
    }

    public async Task<List<TimeRecord>> GetAllRecordTime(string employeeId, Filter filter)
    {
        return await _repository.Get()
            .Where(x => x.EmployeeId == employeeId 
                        && x.EntryTime.Year == filter.Year
                        && x.EntryTime.Month >= filter.Month && x.EntryTime.Month <= filter.Month)
            .Include(x => x.Intervals)
            .ToListAsync();
    }

    public async Task<TimeRecord?> GetCurrentRecordTime(string employeeId)
    {
        DateTime today = DateTime.Today;

        return await _repository.Get().Include(x => x.Intervals)
            .FirstOrDefaultAsync(x =>
                x.EmployeeId == employeeId && x.EntryTime.Year == today.Year && x.EntryTime.Month == today.Month && x.EntryTime.Day == today.Day); ;
    }

    public async Task<TimeRecord> InsertRecordTime(TimeRecord timeRecord)
    {
        return await _repository.AddAsync(timeRecord);
    }

    public async Task UpdateRecordTime(TimeRecord timeRecord)
    {
        await _repository.UpdateAsync(timeRecord);
    }

}

