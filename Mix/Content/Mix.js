/* Miksd */

// ingredient search button
var ingredientSearchButton = document.getElementById('ingredient-search-button');
var ingredientSearchForm = document.getElementById('ingredient-search-form');
ingredientSearchButton.addEventListener('click', function ()
{
    ingredientSearchForm.style.visibility = ingredientSearchForm.style.visibility === 'visible' ? 'hidden' : 'visible';
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

// ingredient clear button
var ingredientClearButton = document.getElementById('ingredient-clear-button');
var ingredientCheckboxes = document.getElementsByClassName('ingredient-checkbox');
ingredientClearButton.addEventListener('click', function () {
    [].forEach.call(ingredientCheckboxes, function (checkbox) {
        checkbox.checked = false;
    });
});