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