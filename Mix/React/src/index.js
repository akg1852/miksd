import React from 'react';
import ReactDOM from 'react-dom';

import CocktailList from './components/CocktailList';
import CocktailSearch from './components/CocktailSearch';
import IngredientSearch from './components/IngredientSearch';

const cocktailList = document.getElementById('cocktail-list');
if (cocktailList) {
    fetch('/Cocktail/List' + window.location.search)
        .then(response => {
            if (response.status == 200) {
                response.json().then(data => {
                    ReactDOM.render(
                        <CocktailList cocktails={data} />,
                        cocktailList
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
                    <IngredientSearch ingredients={data} />,
                    document.getElementById('ingredient-search')
                );
            });
        }
    });

ReactDOM.render(
    <CocktailSearch />,
    document.getElementById('cocktail-search')
);