namespace Mix.Models
{
    public class PrepMethod
    {
        public PrepMethods Id;
        public string Name;

        public PrepMethod() { }
        public PrepMethod(PrepMethods id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public enum PrepMethods
    {
        None = 0,
        Shake = 1,
        Stir = 2,
        Build = 3,
        Layer = 4,
    }
}