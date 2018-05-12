/* Miksd */
(function () {
    // ingredient search
    var ingredientSearchButton = document.getElementById('ingredient-search-button');
    var ingredientSearch = document.getElementById('ingredient-search');
    ingredientSearchButton.addEventListener('click', function () {
        ingredientSearch.style.visibility =
            ingredientSearch.style.visibility === 'visible' ? 'hidden' : 'visible';
    });

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

        if (ingredientSearch.style.visibility == 'visible' &&
            !ingredientSearchButton.contains(e.target) &&
            !ingredientSearch.contains(e.target)) {
            e.preventDefault();
            ingredientSearch.style.visibility = 'hidden';
        }
    });

    // search results
    var showMore = document.getElementById('show-more');
    if (showMore != null) {
        showMore.onclick = function () {
            var more = document.getElementsByClassName('more');
            showMore.style.display = 'none';
            [].forEach.call(more, function (el) {
                el.style.display = 'block';
            });
        };
    }

})();