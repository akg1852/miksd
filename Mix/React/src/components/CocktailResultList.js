import React from 'react';
import CocktailList from './CocktailList';
import Loading from './Loading';

class CocktailResultList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        this.getCocktails();
    }

    componentDidUpdate(prevProps, prevState) {
        if (this.props.query !== prevProps.query) {
            this.getCocktails();
        }
    }

    getCocktails() {
        this.setState({ isLoading: true }, () => {
            fetch('/Cocktail/List' + this.props.query)
                .then(response => {
                    if (response.status == 200) {
                        response.json().then(cocktails => {
                            this.setState({ cocktails, isLoading: false });
                        });
                    }
                });
        });
    }

    render() {
        return (
            <React.Fragment>
                {this.state.isLoading && <Loading isFixed />}
                <CocktailList
                    title={this.props.title}
                    cocktails={this.state.cocktails}
                    selectedIngredients={this.props.selectedIngredients}
                    handleIngredientSearch={this.props.handleIngredientSearch}
                />
            </React.Fragment>
        );
    }
}

export default CocktailResultList;