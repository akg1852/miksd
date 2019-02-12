import React from 'react';
import { Link } from 'react-router-dom';
import IngredientSelect from './IngredientSelect';

const showSize = 10;

class CocktailList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: showSize
        };

        this.handleShowMore = this.handleShowMore.bind(this);
        this.handleIngredientSelect = this.handleIngredientSelect.bind(this);
        this.handleSelecting = this.handleSelecting.bind(this);
        this.handleClickAway = this.handleClickAway.bind(this);

        document.body.addEventListener('click', this.handleClickAway);
    }

    componentDidMount() {
        this.mounted = true;
    }

    componentWillUnmount() {
        this.mounted = false;
    }

    componentDidUpdate(prevProps, prevState) {
        if (this.props.cocktails !== prevProps.cocktails) {
            this.setState({
                show: showSize
            });
        }
    }

    handleShowMore() {
        this.setState({ show: this.state.show + showSize });
    }

    handleIngredientSelect(id, superId, selection) {
        const selectedIngredients = this.props.selectedIngredients
            .filter(i => i != id && i != -id && i != superId && i != -superId);

        if (selection !== undefined) {
            const selectId = superId || id;
            selectedIngredients.push(selection ? selectId : -selectId);
        }

        this.props.handleIngredientSearch(selectedIngredients);
    }

    handleSelecting(cocktailId, ingredientId) {
        this.setState({
            cocktailSelecting: cocktailId,
            ingredientSelecting: ingredientId
        });
    }

    handleClickAway(e) {
        if (this.mounted &&
            this.state.ingredientSelecting &&
            !e.target.className.includes('ingredient-select-icon') &&
            !e.target.className.includes('ingredient-select-option')
        ) {
            e.preventDefault();
            this.setState({
                cocktailSelecting: null,
                ingredientSelecting: null
            });
        }
    }

    render() {
        return (
            <div>
                <h2>{this.props.title}</h2>
                {!this.props.cocktails ? (
                    null
                ) : this.props.cocktails.length === 0 ? (
                    <div className="not-found">Sorry! No cocktails match your search criteria.</div>
                ) : (
                    <React.Fragment>
                        {this.props.cocktails.slice(0, this.state.show).map(c =>
                            <Cocktail key={c.id}
                                {...c}
                                selectedIngredients={this.props.selectedIngredients}
                                handleIngredientSelect={this.handleIngredientSelect}
                                ingredientSelecting={
                                    this.state.cocktailSelecting === c.id ?
                                    this.state.ingredientSelecting :
                                    null
                                }
                                handleSelecting={this.handleSelecting}
                            />
                        )}
                        {(this.props.cocktails.length > this.state.show) &&
                            <div id="show-more"
                                onClick={this.handleShowMore}>
                                Show More…
                            </div>
                        }
                    </React.Fragment>
                )}
            </div>
        );
    }
}

class Cocktail extends React.Component {
    render() {
        return (
            <Link to={"/Cocktail/" + this.props.id}>
                <div className="cocktail-result">
                    <div className="cocktail-name">{this.props.name}</div>
                    <div className="cocktail-summary">
                        <div dangerouslySetInnerHTML={{ __html: this.props.thumbnail }} />
                        <div>
                            {this.props.recipe.map(i => {
                                const isSelecting = this.props.ingredientSelecting === i.id;
                                return (
                                    <Ingredient key={i.id}
                                        id={i.id}
                                        name={i.name}
                                        super={i.super}
                                        selectedIngredients={this.props.selectedIngredients}
                                        handleSelect={this.props.handleIngredientSelect}
                                        isSelecting={isSelecting}
                                        toggleSelecting={() =>
                                            isSelecting ?
                                            this.props.handleSelecting() :
                                            this.props.handleSelecting(this.props.id, i.id)
                                        }
                                    />
                                );
                            }).reduce((acc, el) => [acc, ', ', el])}
                            <br />
                            {this.props.description}
                        </div>
                    </div>
                </div>
            </Link>
        );
    }
}

class Ingredient extends React.Component {
    render() {
        return (
            <span className="cocktail-summary-ingredient">
                <span>{this.props.name}</span>
                {this.props.selectedIngredients &&
                    <React.Fragment>
                        <span
                            className="ingredient-select-icon"
                            onClick={(e) => {
                                e.preventDefault();
                                this.props.toggleSelecting();
                            }}
                        >
                            🔍
                        </span>
                        {this.props.isSelecting &&
                            <div className="ingredient-select-wrapper">
                                <IngredientSelect
                                    selection={
                                        this.props.selectedIngredients.some(i => i == this.props.id || i == this.props.super) ? true :
                                        this.props.selectedIngredients.some(i => i == -this.props.id || i == -this.props.super) ? false :
                                        undefined
                                    }
                                    handleSelect={(selection) => this.props.handleSelect(this.props.id, this.props.super, selection)}
                                />
                            </div>
                        }
                    </React.Fragment>
                }
            </span>
        );
    }
}

export default CocktailList;