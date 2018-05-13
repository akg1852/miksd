import React from 'react';
import Ingredients from './Ingredients'

class IngredientSearch extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};

        this.handleCategoryChange = this.handleCategoryChange.bind(this);
        this.handleIngredientSelect = this.handleIngredientSelect.bind(this);
        this.unselectAllIngredients = this.unselectAllIngredients.bind(this);
    }

    componentDidMount() {
        fetch('/Cocktail/Ingredients')
            .then(response => {
                if (response.status == 200) {
                    response.json().then(ingredients => {
                        setSelectedIngredients(ingredients, this.props.selectedIngredients);
                        this.setState({
                            ingredients,
                            selectedCategory: ingredients[0].category
                        });
                    });
                }
            });
    }

    handleCategoryChange(category) {
        this.setState({ selectedCategory: category });
    }

    handleIngredientSelect(id, selection) {
        const ingredients = this.state.ingredients;
        for (var c = 0; c < ingredients.length; c++) {
            const ii = ingredients[c].ingredients;
            for (var i = 0; i < ii.length; i++) {
                const ingredient = ii[i];
                if (ingredient.id === id) {
                    ingredient.selection = selection;
                    this.setState({ ingredients });
                    return;
                }
            }
        }
    }

    unselectAllIngredients() {
        const ingredients = this.state.ingredients;
        ingredients.forEach(c => c.ingredients.forEach(i => {
            i.selection = undefined;
        }));
        this.setState({ ingredients });
    }

    render() {
        if (!this.state.ingredients) {
            return null;
        }

        return (
            <form id="ingredient-search" action="/">
                <Ingredients
                    ingredients={this.state.ingredients}
                    selectedCategory={this.state.selectedCategory}
                    handleCategoryChange={this.handleCategoryChange}
                    handleIngredientSelect={this.handleIngredientSelect} />
                <input id="ingredient-clear-button" type="button"
                    disabled={this.state.ingredients.every(c =>
                        c.ingredients.every(i => i.selection === undefined))}
                    value="Clear All"
                    onClick={this.unselectAllIngredients} />
                <input type="submit" value="Search" />
            </form>
        )
    }
}

const setSelectedIngredients = (ingredients, selectedIngredients) => {
    ingredients.forEach(c => c.ingredients.forEach(i => {
        i.selection = selectedIngredients.includes(i.id.toString()) ? true
                    : selectedIngredients.includes((-i.id).toString()) ? false
                    : undefined;
    }));
};

export default IngredientSearch;