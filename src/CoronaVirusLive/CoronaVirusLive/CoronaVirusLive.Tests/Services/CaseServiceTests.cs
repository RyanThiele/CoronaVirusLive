using System.Threading.Tasks;
using Xunit;

namespace CoronaVirusLive.Tests.Services
{

    public class CaseServiceTests
    {
        CoronaVirusLive.Services.ICaseService caseService = new CoronaVirusLive.Services.CaseService();

        [Fact]
        public async Task DownloadDays()
        {
            // Arrange


            // Act
            await caseService.GetCasesAsync();


            // Assert
        }
    }
}
