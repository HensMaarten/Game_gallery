using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Models
{
    public class Game
    {
#nullable disable
        [Key]
        public int GameID { get; set; }
        public string Naam { get; set; }
        public string Afbeelding { get; set; }
#nullable enable
        private string? _beschrijving;

        public string? Beschrijving
        {
            get { return _beschrijving; }
            set { _beschrijving = value; }
        }
        private string? _kortebeschrijving;
        public string? KorteBeschrijving
        {
            get
            {
                if (Beschrijving != null)
                {
                    if (Beschrijving.Length > 100)
                    {
                        return Beschrijving.Substring(0, 100);
                    }
                    else return Beschrijving;
                }
                else return "";
                   
            }
            private set { _kortebeschrijving = value; }
        }

        public string? Review { get; set; }
        public DateTime? ReleaseDatum { get; set; }
#nullable disable
        public bool DigitalOnly { get; set; }
#nullable enable
        public int? leeftijdCategorieID { get; set; }
        public LeeftijdCategorie? leeftijdCategorie { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Game game &&
                   Naam == game.Naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam);
        }
    }
}
