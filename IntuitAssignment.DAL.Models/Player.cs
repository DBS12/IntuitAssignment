using CsvHelper.Configuration.Attributes;

namespace IntuitAssignments.DAL.Models
{
    public class Player
    {
        [Name("playerID")]
        public string PlayerID { get; set; }

        [Name("birthYear")]
        [TypeConverter(typeof(SafeInt32Converter))]
        public int BirthYear { get; set; }

        [Name("birthMonth")]
        [TypeConverter(typeof(SafeInt32Converter))]
        public int BirthMonth { get; set; }

        [Name("birthDay")]
        [TypeConverter(typeof(SafeInt32Converter))]
        public int BirthDay { get; set; }

        [Name("birthCountry")]
        public string BirthCountry { get; set; }

        [Name("birthState")]
        public string BirthState { get; set; }

        [Name("birthCity")]
        public string BirthCity { get; set; }

        [Name("deathYear")]
        [TypeConverter(typeof(SafeInt32Converter))]
        public int DeathYear { get; set; }

        [Name("deathMonth")]
        [TypeConverter(typeof(SafeInt32Converter))]
        public int DeathMonth { get; set; }

        [Name("deathDay")]
        [TypeConverter(typeof(SafeInt32Converter))]
        public int DeathDay { get; set; }

        [Name("deathCountry")]
        public string DeathCountry { get; set; }

        [Name("deathState")]
        public string DeathState { get; set; }

        [Name("deathCity")]
        public string DeathCity { get; set; }

        [Name("nameFirst")]
        public string NameFirst { get; set; }

        [Name("nameLast")]
        public string NameLast { get; set; }

        [Name("nameGiven")]
        public string NameGiven { get; set; }

        [Name("weight")]
        [TypeConverter(typeof(SafeInt32Converter))]
        public int Weight { get; set; }

        [Name("height")]
        [TypeConverter(typeof(SafeInt32Converter))]
        public int Height { get; set; }

        [Name("bats")]
        public char? Bats { get; set; }

        [Name("throws")]
        public char? Throws { get; set; }

        [Name("debut")]
        public DateTime? Debut { get; set; }

        [Name("finalGame")]
        public DateTime? FinalGame { get; set; }

        [Name("retroID")]
        public string RetroID { get; set; }

        [Name("bbrefID")]
        public string BbrefID { get; set; }
    }
}