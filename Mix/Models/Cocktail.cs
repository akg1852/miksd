using System.Collections.Generic;
using System.Linq;

namespace Mix.Models
{
    public class Cocktail
    {
        public Cocktails Id;
        public string Name;
        public IEnumerable<CocktailIngredient> Recipe;
        public IEnumerable<Cocktail> Similar;
        public bool Ice;

        public Vessels Vessel;
        public string VesselName;
        public PrepMethods PrepMethod;
        public string PrepMethodName;
        public Garnishes Garnish;
        public string GarnishName;

        public Cocktail() { }
        public Cocktail(Cocktails id, string name, Vessels vessel, PrepMethods prepMethod, Garnishes garnish,
            params CocktailIngredient[] recipe) : this(id, name, false, vessel, prepMethod, garnish, recipe) { }
        public Cocktail(Cocktails id, string name, bool ice, Vessels vessel, PrepMethods prepMethod, Garnishes garnish,
            params CocktailIngredient[] recipe)
        {
            Id = id;
            Name = name;
            Ice = ice;

            Vessel = vessel;
            PrepMethod = prepMethod;
            Garnish = garnish;

            Recipe = recipe;
        }

        public string Description()
        {
            var description = "";
            var ingredients = Recipe.GroupBy(i => i.SpecialPrep)
                .ToDictionary(g => g.Key, g => g.ToList().Select(i => i.Name));
            var isRinsed = ingredients.ContainsKey(SpecialPreps.Rinse);
            var isMuddled = ingredients.ContainsKey(SpecialPreps.Muddle);
            var isFloated = ingredients.ContainsKey(SpecialPreps.Float);
            var vesselMentioned = false;

            // rinse
            if (isRinsed)
            {
                var rinseIngredients = string.Join(" and ", ingredients[SpecialPreps.Rinse]);
                description += $"Rinse {VesselName} with {rinseIngredients}. ";
                vesselMentioned = true;
            }

            // muddle
            if (isMuddled)
            {
                var muddleIngredients = string.Join(" and ", ingredients[SpecialPreps.Muddle]);
                var vessel = vesselMentioned ? "the glass" : VesselName;
                description += $"Muddle the {muddleIngredients} in {vessel}. ";
                vesselMentioned = true;
            }

            // combine
            if (ingredients.ContainsKey(SpecialPreps.None))
            {
                // ice (first attempt)
                if (Ice && vesselMentioned)
                {
                    description += "Fill the glass with ice. ";
                }

                var regularIngredients = ingredients[SpecialPreps.None].ToList();
                var remaining = isRinsed || isMuddled ? "remaining " : "";
                var ingredientWords = (regularIngredients.Count == 1)
                    ? regularIngredients.First()
                    : $"{remaining}ingredients";

                var vessel = vesselMentioned ? "the glass" : VesselName;

                if (PrepMethod == PrepMethods.Shake || PrepMethod == PrepMethods.Stir)
                {
                    description += $"{PrepMethodName} {ingredientWords} with ice, and strain into {vessel}";
                }
                else if (regularIngredients.Count == 1)
                {
                    description += $"Add {ingredientWords}";
                    description += vesselMentioned ? "" : $" to {vessel}";
                }
                else if (PrepMethod == PrepMethods.Build || PrepMethod == PrepMethods.Layer)
                {
                    description += $"{PrepMethodName} {ingredientWords} in {vessel}";
                }
                else return null;

                // ice (second attempt)
                if (Ice && !vesselMentioned)
                {
                    description += " filled with ice";
                }
                description += ". ";
                vesselMentioned = true;
            }

            // float
            if (isFloated)
            {
                var floatIngredients = string.Join(" and ", ingredients[SpecialPreps.Float]);
                description += $"Float the {floatIngredients} on top. ";
            }

            // garnish
            if (Garnish != Garnishes.None)
            {
                description += $"Garnish with {GarnishName}. ";
            }

            return description;
        }
    }

    public enum Cocktails : long
    {
        None = 0,
        Daiquiri = 1,
        Martini = 2,
        Martinez = 3,
        Manhattan = 4,
        Negroni = 5,
        Boulevardier = 6,
        OldFashioned = 7,
        WhiskeySour = 8,
        Americano = 9,
        Aviation = 10,
        Bacardi = 11,
        BetweenTheSheets = 12,
        Casino = 13,
        GinFizz = 14,
        TomCollins = 15,
        MonkeyGland = 16,
        PlantersPunch = 17,
        RustyNail = 18,
        Sazerac = 19,
        Screwdriver = 20,
        Sidecar = 21,
        Tuxedo = 22,
        WhiteLady = 23,
        Cosmopolitan = 24,
        CubaLibre = 25,
        MoscowMule = 26,
        Margarita = 27,
        Sunrise = 28,
        Kamikaze = 29,
        LemonDrop = 30,
        PiscoSour = 31,
        Kir = 32,
        Bellini = 33,
        ChampagneCocktail = 34,
        Mimosa = 35,
        French75 = 36,
        FrenchConnection = 37,
        Godfather = 38,
        Godmother = 39,
        SeaBreeze = 40,
        MaryPickford = 41,
        BlackRussian = 42,
        WhiteRussian = 43,
        LongIsland = 44,
        HemingwaySpecial = 45,
        HarveyWallbanger = 46,
        GoldenDream = 47,
        VieuxCarré = 48,
        Mojito = 49,
        GinAndTonic = 50,
        DarkNStormy = 51,
        B52 = 52,
        MintJulep = 53,
        MaiTai = 54,
        PiñaColada = 55,
        Gimlet = 56,
        Caipirinha = 57,
        AmarettoSour = 58,
        Brooklyn = 59,
        Vodkatini = 60,
        HorsesNeck = 61,
        BnB = 62,
        TiPunch = 63,
        Penicillin = 64,
        RumOldFashioned = 65,
        AperolSpritz = 66,
        LastWord = 67,
        ScotchSoda = 68,
        RobRoy = 69,
        Bramble = 70,
        SingaporeSling = 71,
        BloodAndSand = 72,
        BrandyAlexander = 73,
        AlabamaSlammer = 74,
        Vesper = 75,
        CorpseReviver2 = 76,
        PortoFlip = 77,
        SexOnTheBeach = 78,
        JapaneseSlipper = 79,
        YellowBird = 80,
    }
}