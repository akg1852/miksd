using Mix.Models;

namespace Mix.Services
{
    public static class ImageService
    {
        public static string CocktailImage(Cocktail cocktail, float size, string cssClass)
        {
            var colors = cocktail.Color.Split(',');
            var svg = $"<svg width='{size}' height='{size}' viewBox='0 0 100 100' class='{cssClass}'>";
            string fill;

            if (colors.Length == 1)
            {
                fill = colors[0];
            }
            else
            {
                var h = 100 / colors.Length;
                var pos = 0;
                var blur = (cocktail.PrepMethod == PrepMethods.Layer) ? h/10 : h/3;
                var gradientId = $"lg{cocktail.Id}";

                svg += $"<linearGradient id='{gradientId}' x1='0.5' y1='1' x2='0.5' y2='0'>";
                foreach (var color in colors)
                {
                    svg += $"<stop offset='{pos+blur}%' stop-color='{color}'/>";
                    pos += h;
                    svg += $"<stop offset='{pos-blur}%' stop-color='{color}'/>";
                }
                svg += "</linearGradient>";

                fill = $"url(#{gradientId})";
            }

            switch (cocktail.Vessel)
            {
                case Vessels.Cocktail:
                    svg += $@"
                        <polyline fill='{fill}' points='23,15 47,47 53,47 77,15' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='20,10 47,47 53,47 80,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='47,47 47,80 53,80 53,47' /> <!-- stem -->
                        <polyline fill='none' stroke='black' points='47,80 30,90 70,90 53,80' /> <!-- base -->
                    ";
                    break;
                case Vessels.Rocks:
                    svg += $@"
                        <polyline fill='{fill}' points='20,40 20,80 80,80 80,40' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='20,10 20,80 80,80 80,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='20,80 20,90 80,90 80,80' /> <!-- base -->
                    ";
                    break;
                case Vessels.Highball:
                    svg += $@"
                        <polyline fill='{fill}' points='30,15 30,80 70,80 70,15' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='30,10 30,80 70,80 70,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='30,80 30,90 70,90 70,80' /> <!-- base -->
                    ";
                    break;
                case Vessels.Shot:
                    svg += $@"
                        <polyline fill='{fill}' points='30,15 40,60 60,60 70,15' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='30,10 40,60 60,60 70,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='40,60 35,90 65,90 60,60' /> <!-- base -->
                    ";
                    break;
                case Vessels.Flute:
                    svg += $@"
                        <polyline fill='{fill}' points='40,15 39,25 40,35 47,55 53,55 60,35 61,25 60,15' /> <!-- liquid -->
                        <polyline fill='none' stroke='black' points='40,10 39,25 40,35 47,55 53,55 60,35 61,25 60,10' /> <!-- bowl -->
                        <polyline fill='none' stroke='black' points='47,55 47,80 53,80 53,55' /> <!-- stem -->
                        <polyline fill='none' stroke='black' points='47,80 35,90 65,90 53,80' /> <!-- base -->
                    ";
                    break;
            }

            svg += "</svg>";
            return svg;
        }

        public static string IngredientSearchIcon()
        {
            return @"
                <svg id='ingredient-search-button' width='40' height='40' viewBox=' 0 0 100 100'>
                    <circle cx='50' cy='50' r='50' />
                    <path fill='none' stroke='#FFFFFF' stroke-width='36' stroke-linecap='round'
                        d='m280,278a153,153 0 1,0-2,2l170,170m-91-117 110,110-26,26-110-110'
                        transform='scale(0.1) translate(280 250)' />
                </svg>
            ";
        }
    }
}