namespace Mix.Models
{
    public class SpecialPrep
    {
        public SpecialPreps Id;
        public string Name;

        public SpecialPrep() { }
        public SpecialPrep(SpecialPreps id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public enum SpecialPreps
    {
        None = 0,
        Rinse = 1,
        Muddle = 2,
        Float = 3,
        Squeeze = 4,
    }
}