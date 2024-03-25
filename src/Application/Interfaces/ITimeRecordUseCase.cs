using Domain.Entities;
using Domain.Requests;

namespace Application.Interfaces;

public interface ITimeRecordUseCase
{
    Task<List<TimeRecord>> GetRecordTime(string employeeId, Filter filter);
    Task<TimeRecord> RecordTime(string employeeId);
    Task<string> GenerateRecordTimeEmail(string employeeId, Filter filter);
}