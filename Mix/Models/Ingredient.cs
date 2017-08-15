using System.Collections.Generic;

namespace Mix.Models
{
    public class Ingredient
    {
        public Ingredients Id;
        public string Name;
        public IEnumerable<Ingredients> Children;
        public bool Equivalence;

        public Ingredient() { }

        public Ingredient(Ingredients id, string name, params Ingredients[] children)
            : this(id, name, false, children) { }

        public Ingredient(Ingredients id, string name, bool equivalence, params Ingredients[] children)
        {
            Id = id;
            Name = name;
            Children = children;
            Equivalence = equivalence;
        }
    }

    public enum Ingredients : long // todo: explicitly number the ingredients
    {
        None,
        Spirit,
        Whisky,
        Scotch,
        AmericanWhiskey,
        Bourbon,
        Rye,
        Rum,
        WhiteRum,
        DarkRum,
        Vodka,
        Gin,
        OldTom,
        LondonDry,
        Brandy,
        Cognac,
        Pisco,
        Tequila,
        Absinthe,
        Bitters,
        Angostura,
        Peychauds,
        OrangeBitters,
        Campari,
        Vermouth,
        SweetVermouth,
        DryVermouth,
        OrangeLiqueur,
        TripleSec,
        Cointreau,
        Maraschino,
        Drambuie,
        Sugar,
        SimpleSyrup,
        Grenadine,
        Citrus,
        LemonJuice,
        LimeJuice,
        OrangeJuice,
        PineappleJuice,
        CranberryJuice,
        Soda,
        Water,
        Cola,
        EggWhite,
    }
}