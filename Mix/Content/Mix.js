﻿/* Miksd */

// search field
var textSearch = document.getElementById('text-search');
var searchField = document.getElementById('search-field');
var searchResults = document.getElementById('search-results');
var searchCocktails = null;

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

            if (searchCocktails === null) {
                searchResults.style.display = 'none';
                return;
            }

            searchResults.style.display = 'block';
            var results = (searchCocktails.length === 0) ? 'No results' :
                searchCocktails.map(function (c) {
                    return '<a href="/Cocktail/' + c.Id + '">' + c.Name + '</a>';
                }).join('<br>');

            searchResults.innerHTML = results;
        }
    }

    xhr.open("GET", "/Cocktail/Search/?q=" + encodeURIComponent(query), true);
    xhr.send();
}

textSearch.onsubmit = function (e) {
    e.preventDefault();
    if (searchCocktails && searchCocktails.length) {
        window.location = '/Cocktail/' + searchCocktails[0].Id;
    }
}

document.body.addEventListener('click', function (e) {
    if (!textSearch.contains(e.target)) {
        searchResults.style.display = 'none';
    }
});

// ingredient search button
var ingredientSearchButton = document.getElementById('ingredient-search-button');
var ingredientSearchForm = document.getElementById('ingredient-search-form');
ingredientSearchButton.addEventListener('click', function ()
{
    ingredientSearchForm.style.visibility =
        ingredientSearchForm.style.visibility === 'visible' ? 'hidden' : 'visible';
});

// ingredient search categories
var ingredientCategories = [];
var ingredientCategoryHeaders = document.getElementsByClassName('ingredient-category-header');
var ingredientTabWidth = ingredientSearchForm.offsetWidth / ingredientCategoryHeaders.length;
[].forEach.call(ingredientCategoryHeaders, function (ingredientCategoryHeader, index) {
    var ingredientCategory = ingredientCategoryHeader.nextElementSibling;
    ingredientCategories.push(ingredientCategory);

    ingredientCategoryHeader.style.left = ingredientTabWidth * index + 'px';
    ingredientCategoryHeader.style.width = ingredientTabWidth + 'px';

    ingredientCategoryHeader.addEventListener("click", function () {
        [].forEach.call(ingredientCategoryHeaders, function (h) { h.classList.remove('current'); });
        ingredientCategoryHeader.classList.add('current');
        ingredientCategories.forEach(function (c) { c.style.display = 'none'; });
        ingredientCategory.style.display = 'block';
    })
});
ingredientCategoryHeaders[0].click();

document.body.addEventListener('click', function (e) {
    if (!ingredientSearchButton.contains(e.target) &&
        !ingredientSearchForm.contains(e.target)) {
        ingredientSearchForm.style.visibility = 'hidden';
    }
});

// ingredient clear button
var ingredientClearButton = document.getElementById('ingredient-clear-button');
var ingredientCheckboxes = document.getElementsByClassName('ingredient-checkbox');
ingredientClearButton.addEventListener('click', function () {
    [].forEach.call(ingredientCheckboxes, function (checkbox) {
        checkbox.checked = false;
    });
});