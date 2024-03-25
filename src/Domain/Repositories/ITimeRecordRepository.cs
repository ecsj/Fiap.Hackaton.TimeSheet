using Domain.Entities;
using Domain.Requests;

namespace Domain.Repositories;

public interface ITimeRecordRepository
{
    Task<TimeRecord?> GetCurrentRecordTime(string employeeId);
    Task<List<TimeRecord>> GetAllRecordTime(string employeeId, Filter filter);
    Task<TimeRecord> InsertRecordTime(TimeRecord timeRecord);
    Task UpdateRecordTime(TimeRecord timeRecord);
}