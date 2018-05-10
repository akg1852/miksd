import React from 'react';
import ReactDOM from 'react-dom';
import IngredientSearch from './components/IngredientSearch';

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