namespace TrophyRepository
{
    public class Trophy
    {
        public int Id { get; set; }
        public string Competition { get; set; } = "";
        public int Year { get; set; }

        public override string ToString()
        {
            return $"This is a trophy for {Competition} from {Year}. id is {Id}";
        }

        public void ValidateCompetition()
        {
            if (Competition == null)
                throw new ArgumentNullException("Competition is null " +  Competition);
            if (Competition.Length < 3)
                throw new ArgumentException("Competition is too short " +  Competition);
        }
        public void ValidateYear()
        {
            if (Year < 1970 || Year > 2024)
                throw new ArgumentException("Competition is null or empty " +  Competition);
        }

    }
}
