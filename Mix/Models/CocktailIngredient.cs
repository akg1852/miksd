using System;

namespace Mix.Models
{
    public class CocktailIngredient
    {
        public Ingredients Ingredient;
        public string Name;
        public bool IsOptional;
        public decimal Quantity;
        public bool IsDiscrete;
        public Ingredients Super;

        public SpecialPreps SpecialPrep;
        public string SpecialPrepName;

        public CocktailIngredient() { }
        public CocktailIngredient(Ingredients ingredient, decimal quantity,
            SpecialPreps specialPrep = SpecialPreps.None, bool isOptional = false)
        {
            Ingredient = ingredient;
            Quantity = quantity;
            IsOptional = isOptional;
            SpecialPrep = specialPrep;
        }

        public string QuantityWords()
        {
            string quantityString;

            if (IsDiscrete)
            {
                if (Quantity == 0.25M) quantityString = "¼";
                else if (Quantity == 0.5M) quantityString = "½";
                else return null;
            }
            else if (SpecialPrep == SpecialPreps.Squeeze)
            {
                quantityString = "A squeeze";
            }
            else if (Quantity < 3.5M * CommonQuantity.Dash)
            {
                var dashes = Math.Round(Quantity / CommonQuantity.Dash);
                if (dashes == 1) quantityString = "1 dash";
                else quantityString = dashes + " dashes";
            }
            else if (Quantity < CommonQuantity.Teaspoon) quantityString = "A splash";
            else if (Quantity <= 2 * CommonQuantity.Teaspoon)
            {
                var teaspoons = Math.Round(Quantity / CommonQuantity.Teaspoon);
                if (teaspoons == 1) quantityString = "1 teaspoon";
                else quantityString = teaspoons + " teaspoons";
            }
            else return null;

            return quantityString;
        }
    }
}