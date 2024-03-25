using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Domain.Requests;
using System.Globalization;
using System.Text;

namespace Application.UseCases;

public class TimeRecordUseCase : ITimeRecordUseCase
{
    private readonly ITimeRecordRepository _repository;

    public TimeRecordUseCase(ITimeRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<TimeRecord> RecordTime(string employeeId)
    {
        var currentRecordTime = await _repository.GetCurrentRecordTime(employeeId);

        if (currentRecordTime == null)
        {
            var timeRecord = new TimeRecord(employeeId);

            await _repository.InsertRecordTime(timeRecord);

            return timeRecord;
        }

        var existsIntervals = currentRecordTime.Intervals.Any();

        if (existsIntervals)
        {
            var breakInProgress = currentRecordTime.Intervals.LastOrDefault(x => x.ExitTime == null);

            if (breakInProgress is not null)
            {
                currentRecordTime.AddExitTimeInterval(breakInProgress);

                await _repository.UpdateRecordTime(currentRecordTime);

                return currentRecordTime;
            }

            currentRecordTime.AddExitTimeRecord();

            await _repository.UpdateRecordTime(currentRecordTime);

            return currentRecordTime;
        }
       
        currentRecordTime.AddEntryTimeInterval();

        await _repository.UpdateRecordTime(currentRecordTime);

        return currentRecordTime;

    }

    public async Task<List<TimeRecord>> GetRecordTime(string employeeId, Filter filter)
    {
        return await _repository.GetAllRecordTime(employeeId, filter);
    }

    public async Task<string> GenerateRecordTimeEmail(string employeeId, Filter filter)
    {
        var records = await _repository.GetAllRecordTime(employeeId, filter);

        var emailBody = new StringBuilder();

        foreach (var record in records)
        {
            emailBody.AppendLine($"Data de Entrada: {record.EntryTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)}");
            emailBody.AppendLine($"Data de Saída: {record.ExitTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)}");

            foreach (var interval in record.Intervals)
            {
                emailBody.AppendLine($"Inicio Almoço: {interval.EntryTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)}");
                emailBody.AppendLine($"Retorno Almoço: {(interval.ExitTime?.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) ?? "Não registrado")}");
            }

            emailBody.AppendLine($"Total de Horas trabalhadas: {record.TotalHours.Hours}h {record.TotalHours.Minutes}min");
            emailBody.AppendLine($"Total de intervalo: {record.TotalHoursIntervals.Hours}h {record.TotalHoursIntervals.Minutes}min");
            emailBody.AppendLine("-----------------------------------------");
        }

        string emailContent = emailBody.ToString();

        // Agora você pode enviar emailContent por e-mail
        Console.WriteLine(emailContent);

        return emailContent;
    }


}