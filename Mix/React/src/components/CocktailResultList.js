import React from 'react';
import CocktailList from './CocktailList';

class CocktailResultList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};

        this.getCocktails();
    }

    componentDidUpdate(prevProps, prevState) {
        if (this.props.query !== prevProps.query) {
            this.getCocktails();
        }
    }

    getCocktails() {
        fetch('/Cocktail/List' + this.props.query)
            .then(response => {
                if (response.status == 200) {
                    response.json().then(cocktails => {
                        this.setState({ cocktails });
                    });
                }
            });
    }

    render() {
        return (
            <CocktailList
                title={this.props.title}
                cocktails={this.state.cocktails}
                selectedIngredients={this.props.selectedIngredients}
                handleIngredientSearch={this.props.handleIngredientSearch}
            />
        );
    }
}

export default CocktailResultList;