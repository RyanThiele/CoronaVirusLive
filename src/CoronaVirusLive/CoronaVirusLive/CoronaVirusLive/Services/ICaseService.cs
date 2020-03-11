using CoronaVirusLive.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoronaVirusLive.Services
{
    public interface ICaseService
    {
        Task<IEnumerable<Case>> GetCasesAsync();
        Task<IEnumerable<Case>> GetCasesByDate(DateTime date);

    }
}
