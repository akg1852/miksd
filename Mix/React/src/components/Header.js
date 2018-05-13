import React from 'react';
import CocktailSearch from './CocktailSearch';
import IngredientSearch from './IngredientSearch';

class Header extends React.Component {
    constructor(props) {
        super(props);

        this.state = { showIngredientSearch: false };

        this.toggleIngredientSearch = this.toggleIngredientSearch.bind(this);
    }

    toggleIngredientSearch() {
        this.setState({ showIngredientSearch: !this.state.showIngredientSearch });
    }

    render() {
        return (
            <div id="header">
                <img className="glass" src="/Content/glass.png" />
                <div id="header-main">
                    <div id="header-top">
                        <h1 id="title">Miksd</h1>
                        <CocktailSearch />
                    </div>

                    <div id="header-bottom">
                        <p id="subtitle">Your guide to cocktails / mixed drinks</p>
                        <div id="header-bottom-right">
                            <div id="ingredient-search-wrapper">
                                <IngredientSearchButton
                                    toggleIngredientSearch={this.toggleIngredientSearch} />
                                {this.state.showIngredientSearch &&
                                    <IngredientSearch
                                        selectedIngredients={this.props.selectedIngredients} />
                                }
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

const IngredientSearchButton = ({ toggleIngredientSearch }) => (
    <svg id='ingredient-search-button' width='40' height='40' viewBox=' 0 0 100 100'
        onClick={toggleIngredientSearch}>
        <circle cx='50' cy='50' r='50' />
        <path fill='none' stroke='#FFFFFF' strokeWidth='36' strokeLinecap='round'
            d='m280,278a153,153 0 1,0-2,2l170,170m-91-117 110,110-26,26-110-110'
            transform='scale(0.1) translate(280 250)' />
    </svg>
);

export default Header;