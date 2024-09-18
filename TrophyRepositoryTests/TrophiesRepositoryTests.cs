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
    public class TrophiesRepositoryTests
    {
        [TestMethod()]
        public void TrophiesRepositoryTest()
        {
            TrophiesRepository repository = new TrophiesRepository();
            Assert.AreEqual(repository.Get().Count, 5);
            Assert.AreEqual(repository.Get().Count, 5);
        }

        [TestMethod()]
        public void GetTest()
        {
            TrophiesRepository repository = new TrophiesRepository();
            List<Trophy> trophies = repository.Get();
            Assert.AreEqual(5, trophies.Count);
            trophies = repository.Get(2004);
            Assert.AreEqual(trophies.Count, 4);
            trophies = repository.Get(null, 2008);
            Assert.AreEqual(4, trophies.Count);
            Assert.ThrowsException<ArgumentException>(() => repository.Get(2012, 2000));
            // year sorting
            {
                trophies = repository.Get(null, null, "year");
                int previous = trophies[0].Year;
                for (int i = 1; i < trophies.Count; i++)
                {
                    Assert.IsTrue(previous <= trophies[i].Year);
                    previous = trophies[i].Year;
                }
                trophies = repository.Get(null, null, "year_dec");
                previous = trophies[0].Year;
                for (int i = 1; i < trophies.Count; i++)
                {
                    Assert.IsTrue(previous >= trophies[i].Year);
                    previous = trophies[i].Year;
                }
            }
            // competition sorting
            {
                trophies = repository.Get(null, null, "competition");
                string previous = trophies[0].Competition.ToLower();
                for (int i = 1; i < trophies.Count; i++)
                {
                    string newCompetition = trophies[i].Competition.ToLower();
                    for (int j = 0; j < previous.Length && j < newCompetition.Length; j++ )
                    {
                        if (previous[j] < newCompetition[j])
                            break;
                        if (previous[j] > newCompetition[j])
                            Assert.Fail();
                    }
                    previous = newCompetition;
                }
                trophies = repository.Get(null, null, "competition_dec");
                previous = trophies[0].Competition.ToLower();
                for (int i = 1; i < trophies.Count; i++)
                {
                    string newCompetition = trophies[i].Competition.ToLower();
                    for (int j = 0; j < previous.Length && j < newCompetition.Length; j++)
                    {
                        if (previous[j] > newCompetition[j])
                            break;
                        if (previous[j] < newCompetition[j])
                            Assert.Fail();
                    }
                    previous = newCompetition;
                }
            }
            // check the repository still contains 5 elements
            Assert.AreEqual(5, trophies.Count);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            TrophiesRepository repository = new TrophiesRepository();
            Trophy trophy = repository.GetById(1);
            Assert.AreEqual("a competition", trophy.Competition);
            Assert.AreEqual(2000, trophy.Year);
            Assert.AreEqual(null, repository.GetById(-1));
        }

        [TestMethod()]
        public void AddTest()
        {
            TrophiesRepository repository = new TrophiesRepository();
            Assert.AreEqual(5, repository.Get().Count);
            Trophy trophy = repository.Add(new Trophy { Competition="Important competition", Year = 2010 });
            Assert.AreEqual(6, repository.Get().Count);
            Trophy trophySame = repository.GetById(6);
            Assert.AreEqual(trophy, trophySame);
            // check each id is unique
            foreach (var trophyOuter in repository.Get())
            {
                foreach (var trophyInner in repository.Get())
                {
                    if (trophyOuter == trophyInner)
                        continue;
                    Assert.AreNotSame(trophyOuter.Id, trophyInner.Id);
                }
            }

        }

        [TestMethod()]
        public void RemoveTest()
        {
            TrophiesRepository repository = new TrophiesRepository();
            Assert.AreEqual(5, repository.Get().Count);
            Trophy trophy = repository.Remove(1);
            Assert.AreEqual(4, repository.Get().Count);
            Assert.AreEqual(1, trophy.Id);
            Assert.AreEqual("a competition", trophy.Competition);
            Assert.AreEqual(2000, trophy.Year);

        }

        [TestMethod()]
        public void UpdateTest()
        {
            TrophiesRepository repository = new TrophiesRepository();
            Trophy newTrophy = new Trophy() { Competition = "new competition", Year = 2024 };
            Trophy trophy = repository.Update(1, newTrophy);
            Assert.AreEqual(newTrophy.Competition, trophy.Competition);
            Assert.AreEqual(newTrophy.Year, trophy.Year);
            Assert.ThrowsException<ArgumentNullException>(() => { repository.Update(1, null); });
        }
    }
}