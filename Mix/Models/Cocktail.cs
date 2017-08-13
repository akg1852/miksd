using System.Collections.Generic;

namespace Mix.Models
{
    public class Cocktail
    {
        public Cocktails Id;
        public string Name;
        public IEnumerable<CocktailIngredient> Recipe;
        public IEnumerable<Cocktail> Similar;

        public Cocktail() { }
        public Cocktail(Cocktails id, string name, params CocktailIngredient[] recipe)
        {
            Id = id;
            Name = name;
            Recipe = recipe;
        }
    }

    public class CocktailIngredient
    {
        public Ingredients Ingredient;
        public string Name;
        public bool IsOptional;
        public decimal Quantity;

        public CocktailIngredient() { }
        public CocktailIngredient(Ingredients ingredient, decimal quantity, bool isOptional = false)
        {
            Ingredient = ingredient;
            Quantity = quantity;
            IsOptional = isOptional;
        }
    }

    public enum Cocktails : long
    {
        None,
        Americano,
        Daiquiri,
        Martini,
        Manhattan,
        Negroni,
        OldFashioned,
        WhiskeySour,
    }
}