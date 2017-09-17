using System.Web;
using Mix.Models;

namespace Mix.Services
{
    public static class ImageService
    {
        public static HtmlString CocktailImage(Cocktail cocktail, float size, string cssClass)
        {
            var svg = $"<svg width='{size}' height='{size}' viewBox='0 0 100 100' class='{cssClass}'>";

            switch (cocktail.Vessel)
            {
                case Vessels.Cocktail:
                    svg += $@"
                        <polyline fill='{cocktail.Color}' points='23,15 47,47 53,47 77,15' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='20,10 47,47 53,47 80,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='47,47 47,80 53,80 53,47' /> <!-- stem -->
                        <polyline fill='none' stroke='black' points='47,80 30,90 70,90 53,80' /> <!-- base -->
                    ";
                    break;
                case Vessels.Rocks:
                    svg += $@"
                        <polyline fill='{cocktail.Color}' points='20,30 20,80 80,80 80,30' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='20,10 20,80 80,80 80,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='20,80 20,90 80,90 80,80' /> <!-- base -->
                    ";
                    break;
                case Vessels.Highball:
                    svg += $@"
                        <polyline fill='{cocktail.Color}' points='30,15 30,80 70,80 70,15' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='30,10 30,80 70,80 70,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='30,80 30,90 70,90 70,80' /> <!-- base -->
                    ";
                    break;
                case Vessels.Shot:
                    svg += $@"
                        <polyline fill='{cocktail.Color}' points='30,15 40,60 60,60 70,15' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='30,10 40,60 60,60 70,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='40,60 35,90 65,90 60,60' /> <!-- base -->
                    ";
                    break;
                case Vessels.Flute:
                    svg += $@"
                        <polyline fill='{cocktail.Color}' points='40,15 39,25 40,35 47,55 53,55 60,35 61,25 60,15' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='40,10 39,25 40,35 47,55 53,55 60,35 61,25 60,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='47,55 47,80 53,80 53,55' /> <!-- stem -->
                        <polyline fill='none' stroke='black' points='47,80 35,90 65,90 53,80' /> <!-- base -->
                    ";
                    break;
            }

            svg += "</svg>";
            return new HtmlString(svg);
        }
    }
}