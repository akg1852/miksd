namespace Mix.Models
{
    public class Vessel
    {
        public Vessels Id;
        public string Name;

        public Vessel() { }
        public Vessel(Vessels id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public enum Vessels
    {
        None = 0,
        Cocktail = 1,
        Rocks = 2,
        Highball = 3,
        Shot = 4,
        Flute = 5,
    }
}