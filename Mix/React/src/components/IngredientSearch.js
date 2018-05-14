import React from 'react';
import Ingredients from './Ingredients'

class IngredientSearch extends React.Component {
    constructor(props) {
        super(props);
        this.state = { showIngredientSearch: false };

        this.handleCategoryChange = this.handleCategoryChange.bind(this);
        this.handleClearAll = this.handleClearAll.bind(this);
        this.handleClickAway = this.handleClickAway.bind(this);
        this.handleIngredientSelect = this.handleIngredientSelect.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleToggleShow = this.handleToggleShow.bind(this);

        document.body.addEventListener('click', this.handleClickAway);
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

    handleToggleShow() {
        this.setState({ showIngredientSearch: !this.state.showIngredientSearch });
    }

    handleClickAway(e) {
        if (this.state.showIngredientSearch &&
            !this.ingredientSearchEl.contains(e.target) &&
            !this.ingredientSearchButtonEl.contains(e.target)) {

            e.preventDefault();
            this.setState({ showIngredientSearch: false });
        }
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

    handleClearAll() {
        const ingredients = this.state.ingredients;
        ingredients.forEach(c => c.ingredients.forEach(i => {
            i.selection = undefined;
        }));
        this.setState({ ingredients });
    }

    handleSubmit(e) {
        e.preventDefault();
        const selectedIngredients = [];
        this.state.ingredients.forEach(c => c.ingredients.forEach(i => {
            if (i.selection === true) selectedIngredients.push(i.id);
            else if (i.selection === false) selectedIngredients.push(-i.id);
        }));
        this.setState({ showIngredientSearch: false });
        this.props.history.push('/?' + selectedIngredients.map(i => 'i=' + i).join('&'));
    }

    render() {
        if (!this.state.ingredients) {
            return null;
        }

        return (
            <div id="ingredient-search-wrapper">
                <svg id='ingredient-search-button' width='40' height='40' viewBox=' 0 0 100 100'
                    ref={(el) => this.ingredientSearchButtonEl = el}
                    onClick={this.handleToggleShow}>
                    <IngredientSearchButton />
                </svg>
                {this.state.showIngredientSearch &&
                    <form id="ingredient-search"
                        ref={(el) => this.ingredientSearchEl = el}
                        onSubmit={this.handleSubmit}>
                        <Ingredients
                            ingredients={this.state.ingredients}
                            selectedCategory={this.state.selectedCategory}
                            handleCategoryChange={this.handleCategoryChange}
                            handleIngredientSelect={this.handleIngredientSelect} />
                        <input id="ingredient-clear-button" type="button"
                            disabled={this.state.ingredients.every(c =>
                                c.ingredients.every(i => i.selection === undefined))}
                            value="Clear All"
                            onClick={this.handleClearAll} />
                        <input type="submit" value="Search" />
                    </form>
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

const setSelectedIngredients = (ingredients, selectedIngredients) => {
    ingredients.forEach(c => c.ingredients.forEach(i => {
        i.selection = selectedIngredients.includes(i.id.toString()) ? true
                    : selectedIngredients.includes((-i.id).toString()) ? false
                    : undefined;
    }));
};

export default IngredientSearch;