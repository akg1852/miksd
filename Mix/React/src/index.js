import React from 'react';
import ReactDOM from 'react-dom';

import CocktailList from './components/CocktailList';
import Cocktail from './components/Cocktail';
import CocktailSearch from './components/CocktailSearch';
import IngredientSearch from './components/IngredientSearch';

const getQueryString = () => {
    var result = {};
    var vars = window.location.search.substring(1).split('&');

    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split('=');
        var key = pair[0];
        var val = pair[1];

        result[key] = result[key] || [];
        result[key].push(val);
    }
    return result;
};
const query = getQueryString();

const cocktailList = document.getElementById('cocktail-list');
const cocktail = document.getElementById('cocktail');
const titleBase = ' - Miksd';

if (cocktailList) {
    let title = (query['title'] || [])[0] || 'Cocktails';
    title = decodeURIComponent(title.replace(/\+/g, ' '));
    document.title = title + titleBase;
    fetch('/Cocktail/List' + window.location.search)
        .then(response => {
            if (response.status == 200) {
                response.json().then(data => {
                    ReactDOM.render(
                        <CocktailList cocktails={data} title={title} />,
                        cocktailList
                    );
                });
            }
        });
}
else if (cocktail) {
    const path = window.location.pathname.split('/');
    const id = path[path.length - 1];
    fetch('/Cocktail/Data/' + id)
        .then(response => {
            if (response.status == 200) {
                response.json().then(data => {
                    document.title = data.name + titleBase;
                    ReactDOM.render(
                        <Cocktail {...data} />,
                        cocktail
                    );
                });
            }
        });
}

fetch('/Cocktail/Ingredients')
    .then(response => {
        if (response.status == 200) {
            response.json().then(data => {
                ReactDOM.render(
                    <IngredientSearch ingredients={data}
                        selectedIngredients={query['i'] || []} />,
                    document.getElementById('ingredient-search')
                );
            });
        }
    });

ReactDOM.render(
    <CocktailSearch />,
    document.getElementById('cocktail-search')
);
