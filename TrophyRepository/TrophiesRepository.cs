using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrophyRepository
{
    public class TrophiesRepository
    {
        private int _nextId = 6;
        private List<Trophy> _trophies = new List<Trophy>()
        {
            new Trophy()
                {
                    Id = 1, Competition = "a competition", Year = 2000
                },
            new Trophy()
                {
                    Id = 2, Competition = "b competition", Year = 2004
                },
            new Trophy()
                {
                    Id = 3, Competition = "c competition", Year = 2004
                },
            new Trophy()
                {
                    Id = 4, Competition = "e competition", Year = 2012
                },
            new Trophy()
                {
                    Id = 5, Competition = "d competition", Year = 2008
                },
        };
        public List<Trophy> Get(int? yearMin = null, int? yearMax = null, string sort = null)
        {
            if (yearMin != null && yearMax != null && yearMin > yearMax)
                throw new ArgumentException("yearMin must be lower that yearMax");
            //IEnumerable<Trophy> trophies = new List<Trophy>(_trophies);
            IEnumerable<Trophy> trophies = _trophies;
            //if (yearMin != null && yearMax != null)
            //    trophies = _trophies.Where((t) => { return t.Year >= yearMin && t.Year <= yearMax; }).ToList();
            if (yearMin != null)
                trophies = trophies.Where((t) => { return t.Year >= yearMin; });
            if (yearMax != null)
                trophies = trophies.Where((t) => { return t.Year <= yearMax; });
            if (sort != null)
            {
                switch (sort.ToLower())
                {
                    case "competition":
                    case "competition_asc":
                        trophies = trophies.OrderBy(t => { return t.Competition; });
                        break;
                    case "competition_dec":
                        trophies = trophies.OrderBy(t => { return t.Competition; }).Reverse();
                        break;

                    case "year":
                    case "year_asc":
                        trophies = trophies.OrderBy(t => { return t.Year; });
                        break;
                    case "year_dec":
                        trophies = trophies.OrderBy(t => { return -t.Year; });
                        break;
                }
            }
            return trophies.ToList();
        }
        public Trophy? GetById(int id)
        {
            return _trophies.FirstOrDefault(t => t.Id == id);
        }
        public Trophy Add(Trophy  trophy)
        {
            trophy.Id = _nextId++;
            _trophies.Add(trophy);
            return trophy;
        }
        public Trophy Remove(int id)
        {
            Trophy trophy = GetById(id);
            if (trophy == null)
                return null;
            _trophies.Remove(trophy);
            return trophy;
        }
        public Trophy Update(int id, Trophy values)
        {
            if (values == null)
                throw new ArgumentNullException("values cannot be null");
            Trophy trophy = GetById(id);
            if (trophy == null)
                return null;
            trophy.Competition = values.Competition;
            trophy.Year = values.Year;
            return trophy;
        }

        /*
    Trophy objektet med det angivne id opdateres med values.
    Returnerer det opdaterede Trophy objekt - eller null.
         */
    }
}
