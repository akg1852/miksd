using System.Collections.Generic;

namespace Mix.Models
{
    public class Ingredient
    {
        public Ingredients Id;
        public string Name;
        public IEnumerable<Ingredients> Children;

        public Ingredient() { }
        public Ingredient(Ingredients id, string name, params Ingredients[] children)
        {
            Id = id;
            Name = name;
            Children = children;
        }
    }

    public enum Ingredients : long // todo: explicitly number the ingredients
    {
        None,
        Whisky,
        Scotch,
        Bourbon,
        Rye,
        Rum,
        WhiteRum,
        DarkRum,
        Gin,
        Angostura,
        Campari,
        Vermouth,
        RedVermouth,
        WhiteVermouth,
        OrangeLiqueur,
        TripleSec,
        Cointreau,
        Citrus,
        LemonJuice,
        LimeJuice,
        Sugar,
        SimpleSyrup,
        Soda,
        Water,
        EggWhite,
    }
}