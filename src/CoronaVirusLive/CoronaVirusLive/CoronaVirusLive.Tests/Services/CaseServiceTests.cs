﻿using CoronaVirusLive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoronaVirusLive.Tests.Services
{

    public class CaseServiceTests
    {
        CoronaVirusLive.Services.ICaseService caseService = new CoronaVirusLive.Services.JohnHopkinsCaseService();

        [Fact]
        public async Task GetCasesAsync()
        {
            // Arrange


            // Act
            IEnumerable<Case> cases = await caseService.GetCasesAsync();


            // Assert
            Assert.NotNull(cases);
            Assert.True(cases.Count() > 0);
        }


        [Fact]
        public async Task GetCasesByDate()
        {
            // Arrange


            // Act
            var model = await caseService.GetCasesByDate(DateTime.Today.AddDays(-1));


            // Assert
            Assert.NotNull(model);
        }

    }
}
