/* Miksd */
(function () {
    // click away from modals
    document.body.addEventListener('click', function (e) {
        var searchResults = document.getElementById('search-results');
        if (searchResults) {
            if (!document.getElementById('cocktail-search-form').contains(e.target)) {
                searchResults.style.display = 'none';
            }
            else {
                searchResults.style.display = 'block';
            }
        }

        var ingredientSearchButton = document.getElementById('ingredient-search-button');
        var ingredientSearch = document.getElementById('ingredient-search');
        if (ingredientSearch && ingredientSearchButton &&
            !ingredientSearchButton.contains(e.target) &&
            !ingredientSearch.contains(e.target)) {
            e.preventDefault();
            ingredientSearch.style.visibility = 'hidden';
        }
    });

})();