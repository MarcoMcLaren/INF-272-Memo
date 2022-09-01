using System.Collections.Generic;

namespace inf272Semester2SectionAQ2.Models
{
    public class SouthAfrica : Country
    {
        public override List<string> Capitals { get; set; }
        public override List<string> OfficialLanguages { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override Contintent Location { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override string DisplayCountryInfo()
        {
            return $"Location: {Contintent.Unselected}  Capitals: {Contintent.Unselected}  OfficialLanguages: {Contintent.Unselected}";
        }

        public override string DisplayCountryInfo(string ..., int...., string  ...)
        {
            return $"{base.DisplayCountryInfo()} Capitals: {Contintent.Unselected}  OfficialLanguages: {Contintent.Unselected}";
        }
    }
}
