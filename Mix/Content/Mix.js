/* Miksd */
(function () {

    // text search
    var textSearch = document.getElementById('text-search');
    var searchField = document.getElementById('search-field');
    var searchResults = document.getElementById('search-results');
    var searchCocktails = null;
    var selectedSearch = 0;

    searchField.oninput = function () {
        var query = searchField.value;

        if (!query) {
            searchResults.style.display = 'none';
            return;
        }

        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState == XMLHttpRequest.DONE && xhr.status == 200) {
                searchCocktails = JSON.parse(xhr.responseText);
                selectedSearch = 0;

                if (searchCocktails === null) {
                    searchResults.style.display = 'none';
                    return;
                }

                searchResults.style.display = 'block';
                var results = (searchCocktails.length === 0) ? 'No results' :
                    searchCocktails.map(function (c, i) {
                        return '<a href="/Cocktail/' + c.Id + '" class="search-result' +
                            (i == 0 ? ' search-result-selected' : '') + '">' + c.Name + '</a>';
                    }).join('');

                searchResults.innerHTML = results;
            }
        }

        xhr.open("GET", "/Cocktail/Search/?q=" + encodeURIComponent(query), true);
        xhr.send();
    }

    textSearch.onsubmit = function (e) {
        e.preventDefault();
        if (searchCocktails && searchCocktails.length) {
            window.location = '/Cocktail/' + searchCocktails[selectedSearch].Id;
        }
    }

    searchField.addEventListener('keydown', function (e) {
        e = e || window.event;
        var keyCode = e.keyCode || e.which;

        if (keyCode == '38' || keyCode == '40') {
            e.preventDefault();
            var results = document.getElementsByClassName('search-result');
            [].forEach.call(results, function (el) { el.classList.remove('search-result-selected'); });

            selectedSearch = (results.length + selectedSearch + (keyCode == '38' ? -1 : 1)) % results.length;
            results[selectedSearch].classList.add('search-result-selected');
        }
    });

    // ingredient search
    var ingredientSearchButton = document.getElementById('ingredient-search-button');
    var ingredientSearch = document.getElementById('ingredient-search');
    ingredientSearchButton.addEventListener('click', function () {
        ingredientSearch.style.visibility =
            ingredientSearch.style.visibility === 'visible' ? 'hidden' : 'visible';
    });

    // scrolling magic
    var header = document.getElementById('header');
    var ingredientSearch = document.getElementById('ingredient-search');
    var headerBottom = document.getElementById('header-bottom');
    window.addEventListener('scroll', function () {
        if (window.pageYOffset > headerBottom.offsetTop - 10) {
            ingredientSearch.style.position = 'fixed';
            ingredientSearch.style.top = '10px';
            ingredientSearch.style.right = (header.offsetLeft + 10) + 'px';
        }
        else {
            ingredientSearch.style.position = 'absolute';
            ingredientSearch.style.top = '';
            ingredientSearch.style.right = '';
        }
    })

    // click away from modals
    document.body.addEventListener('click', function (e) {
        if (!textSearch.contains(e.target)) {
            searchResults.style.display = 'none';
        }
        if (!ingredientSearchButton.contains(e.target) &&
            !ingredientSearch.contains(e.target)) {
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