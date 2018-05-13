import React from 'react';
import CocktailList from './CocktailList';

class Cocktail extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        fetch('/Cocktail/Data/' + this.props.id)
            .then(response => {
                if (response.status == 200) {
                    response.json().then(cocktail => {
                        document.title = cocktail.name + ' - Miksd';
                        this.setState({ cocktail });
                    });
                }
            });
    }

    render() {
        if (!this.state.cocktail) {
            return null;
        }

        const { id, name, recipe, description, image, similar } = this.state.cocktail;
        return (
            <div>
                <h2>{name}</h2>
                <div dangerouslySetInnerHTML={{ __html: image }} />

                <h3>Recipe</h3>
                <ul>{recipe.map(i => <Ingredient key={i.name} {...i} />)}</ul>

                <p>{description}</p>

                <div className="spacer"></div>
                <CocktailList cocktails={similar} title="Similar Cocktails" />
            </div>
        );
    }
}

const Ingredient = ({ name, isOptional, quantity, quantityWords }) => (
    <li>
        <span title={quantity}>{quantityWords || quantity}</span>
        {' ' + name}
        {isOptional && ' (optional)'}
    </li>
);

export default Cocktail;