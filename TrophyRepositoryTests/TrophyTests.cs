using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrophyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrophyRepository.Tests
{
    [TestClass()]
    public class TrophyTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            Trophy trophy = new Trophy()
            {
                Id = 1,
                Competition = "Olympic games",
                Year = 2024
            };
            
            Assert.AreEqual("This is a trophy for Olympic games from 2024. id is 1", trophy.ToString());
        }

        [TestMethod()]
        public void ValidateCompetitionTest()
        {
            Trophy trophy = new Trophy()
            {
                Id = 1,
                Competition = "Olympic games",
                Year = 2024
            };
            trophy.ValidateCompetition();
            trophy.Competition = null;
            Assert.ThrowsException<ArgumentNullException>(() => trophy.ValidateCompetition());
            trophy.Competition = "";
            Assert.ThrowsException<ArgumentException>(() => trophy.ValidateCompetition());
        }

        [TestMethod()]
        public void ValidateYearTest()
        {
            Trophy trophy = new Trophy()
            {
                Id = 1,
                Competition = "Olympic games",
                Year = 2024
            };
            trophy.ValidateYear();
            trophy.Year = 1970;
            trophy.ValidateYear();

            trophy.Year = 2025;
            Assert.ThrowsException<ArgumentException>(() => trophy.ValidateYear());
            trophy.Year = 1969;
            Assert.ThrowsException<ArgumentException>(() => trophy.ValidateYear());
        }
    }
}