import React from 'react';
import Ingredients from './Ingredients'

class IngredientSearch extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};

        this.handleCategoryChange = this.handleCategoryChange.bind(this);
        this.handleIngredientSelect = this.handleIngredientSelect.bind(this);
    }

    componentDidMount() {
        this.mounted = true;
        fetch('/Cocktail/Ingredients')
            .then(response => {
                if (response.status == 200) {
                    response.json().then(ingredients => {
                        if (this.mounted) {
                            this.setState({
                                ingredients,
                                selectedCategory: ingredients[0].category
                            });
                        }
                    });
                }
            });
    }

    componentWillUnmount() {
        this.mounted = false;
    }

    handleCategoryChange(category) {
        this.setState({ selectedCategory: category });
    }

    handleIngredientSelect(id, selection) {
        const selectedIngredients = [];
        this.state.ingredients.forEach(c => c.ingredients.forEach(i => {
            if (i.id === id) i.selection = selection;

            if (i.selection === true) selectedIngredients.push(i.id);
            else if (i.selection === false) selectedIngredients.push(-i.id);
        }));

        this.props.handleIngredientSearch(selectedIngredients);
    }

    render() {
        if (!this.state.ingredients) {
            return null;
        }

        const ingredients = ingredientsWithSelection(this.state.ingredients, this.props.selectedIngredients);

        return (
            <React.Fragment>
                <div id="ingredient-search"
                    style={{ display: this.props.showIngredientSearch ? 'block' : 'none' }}
                >
                    <Ingredients
                        ingredients={ingredients}
                        selectedCategory={this.state.selectedCategory}
                        handleCategoryChange={this.handleCategoryChange}
                        handleIngredientSelect={this.handleIngredientSelect}
                    />
                    <input id="ingredient-clear-button" type="button"
                        disabled={!this.props.selectedIngredients.length}
                        value="Clear All"
                        onClick={() => this.props.handleIngredientSearch([])}
                    />
                    <input id="ingredient-done-button" type="button"
                        value="Done"
                        onClick={this.props.hideIngredientSearch}
                    />
                </div>
                {this.props.showIngredientSearch &&
                    <div
                        className="modal-overlay"
                        onClick={this.props.hideIngredientSearch}
                    ></div>
                }
            </React.Fragment>
        )
    }
}

const ingredientsWithSelection = (ingredients, selectedIngredients) => {
    ingredients.forEach(c => c.ingredients.forEach(i => {
        i.selection = selectedIngredients.includes(i.id.toString()) ? true
                    : selectedIngredients.includes((-i.id).toString()) ? false
                    : undefined;
    }));
    return ingredients;
};

export default IngredientSearch;