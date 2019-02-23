import React from 'react';
import Ingredients from './Ingredients'

class IngredientSearch extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showIngredientSearch: false
        };

        this.toggleIngredientSearch = this.toggleIngredientSearch.bind(this);
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

    toggleIngredientSearch() {
        this.setState({ showIngredientSearch: !this.state.showIngredientSearch });
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
            <div id="ingredient-search-wrapper">
                <svg id='ingredient-search-button' width='40' height='40' viewBox=' 0 0 100 100'
                    onClick={this.toggleIngredientSearch}
                    style={{ zIndex: this.state.showIngredientSearch ? '101' : null }}
                >
                    <IngredientSearchButton />
                </svg>
                <div id="ingredient-search"
                    style={{ display: this.state.showIngredientSearch ? 'block' : 'none' }}
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
                </div>
                {this.state.showIngredientSearch &&
                    <div
                        className="modal-overlay"
                        onClick={this.toggleIngredientSearch}
                    ></div>
                }
            </div>
        )
    }
}

const IngredientSearchButton = () => (
    <g>
        <circle cx='50' cy='50' r='50' />
        <path fill='none' stroke='#FFFFFF' strokeWidth='36' strokeLinecap='round'
            d='m280,278a153,153 0 1,0-2,2l170,170m-91-117 110,110-26,26-110-110'
            transform='scale(0.1) translate(280 250)' />
    </g>
);

const ingredientsWithSelection = (ingredients, selectedIngredients) => {
    ingredients.forEach(c => c.ingredients.forEach(i => {
        i.selection = selectedIngredients.includes(i.id.toString()) ? true
                    : selectedIngredients.includes((-i.id).toString()) ? false
                    : undefined;
    }));
    return ingredients;
};

export default IngredientSearch;