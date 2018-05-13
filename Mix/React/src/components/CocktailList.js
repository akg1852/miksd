﻿import React from 'react';

class CocktailList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};

        this.handleShowMore = this.handleShowMore.bind(this);
    }

    static getDerivedStateFromProps(nextProps, prevState) {
        if (nextProps.cocktails === prevState.cocktails) {
            return null;
        }

        return {
            title: nextProps.title,
            show: 10,
            cocktails: nextProps.cocktails
        };
    }

    handleShowMore() {
        this.setState({ show: this.state.show + 10 });
    }

    render() {
        if (this.props.cocktails.length === 0) {
            return <span>Sorry! No cocktails available with selected ingredients.</span>
        }

        return (
            <div>
                <h2>{this.state.title}</h2>
                {this.state.cocktails.slice(0, this.state.show).map(c => <Cocktail key={c.id} {...c} />)}
                {(this.state.cocktails.length > this.state.show) &&
                    <div id="show-more"
                        onClick={this.handleShowMore}>
                        Show More…
                    </div>}
            </div>
        );
    }
}

const Cocktail = ({ id, name, recipe, description, thumbnail }) => (
    <a href={"/Cocktail/" + id}>
        <div className="cocktail-result">
            <div className="cocktail-name">{name}</div>
            <div className="cocktail-summary">
                <div dangerouslySetInnerHTML={{ __html: thumbnail }} />
                <p>{recipe.join(', ')}<br />
                {description}</p>
            </div>
        </div>
    </a>
);

export default CocktailList;