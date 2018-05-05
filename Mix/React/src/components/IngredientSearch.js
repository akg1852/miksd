import React from 'react';
import Ingredients from './Ingredients'

class IngredientSearch extends React.Component {
    constructor(props) {
        super(props);

        const ingredients = this.props.ingredients;
        const selectedCategory = ingredients[0].Category;
        setSelectedIngredients(ingredients);

        this.state = { selectedCategory, ingredients };

        this.handleCategoryChange = this.handleCategoryChange.bind(this);
        this.handleIngredientSelect = this.handleIngredientSelect.bind(this);
        this.unselectAllIngredients = this.unselectAllIngredients.bind(this);
    }

    handleCategoryChange(category) {
        this.setState({ selectedCategory: category });
    }

    handleIngredientSelect(id, isSelected) {
        const ingredients = this.state.ingredients;
        for (var c = 0; c < ingredients.length; c++) {
            const ii = ingredients[c].Ingredients;
            for (var i = 0; i < ii.length; i++) {
                const ingredient = ii[i];
                if (ingredient.Id === id) {
                    ingredient.isSelected = isSelected;
                    this.setState({ ingredients });
                    return;
                }
            }
        }
    }

    unselectAllIngredients() {
        const ingredients = this.state.ingredients;
        ingredients.forEach(c => c.Ingredients.forEach(i => {
            i.isSelected = false
        }));
        this.setState({ ingredients });
    }

    render() {
        return (
            <form action="/">
                <Ingredients
                    ingredients={this.state.ingredients}
                    selectedCategory={this.state.selectedCategory}
                    handleCategoryChange={this.handleCategoryChange}
                    handleIngredientSelect={this.handleIngredientSelect} />
                <input id="ingredient-clear-button" type="button"
                    value="Clear All"
                    onClick={this.unselectAllIngredients} />
                <input type="submit" value="Search" />
            </form>
        )
    }
}

const setSelectedIngredients = (ingredients) => {
    const selectedIngredients = getQueryString()['i'] || [];
    ingredients.forEach(c => c.Ingredients.forEach(i => {
        i.isSelected = selectedIngredients.includes(i.Id.toString())
    }));
};

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

export default IngredientSearch;