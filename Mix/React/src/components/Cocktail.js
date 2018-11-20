import React from 'react';
import CocktailList from './CocktailList';

class Cocktail extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};

        this.getCocktail();

        this.handleAddCocktailToMenu = this.handleAddCocktailToMenu.bind(this);
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevProps !== this.props) {
            window.scrollTo(0, 0);
            this.getCocktail();
        }
    }

    getCocktail() {
        fetch('/Cocktail/Data/' + this.props.match.params.id)
            .then(response => {
                if (response.status == 200) {
                    response.json().then(cocktail => {
                        this.setState({ cocktail });
                    });
                }
            });
    }

    handleAddCocktailToMenu(e) {
        const select = e.target;
        const menu = select.value;
        const cocktail = this.state.cocktail.id;
        select.options[select.selectedIndex].disabled = true;
        this.props.handleAddCocktailToMenu(menu, cocktail);
    }

    render() {
        if (!this.state.cocktail) {
            return null;
        }

        const { id, name, recipe, description, image, similar } = this.state.cocktail;
        document.title = name + ' - Miksd';

        return (
            <div>
                <h2>{name}</h2>
                <div dangerouslySetInnerHTML={{ __html: image }} />

                <h3>Recipe</h3>
                <ul>{recipe.map(i => <Ingredient key={i.name} {...i} />)}</ul>

                <p>{description}</p>

                <select className="add-to-menu"
                    value=""
                    onChange={this.handleAddCocktailToMenu}
                >
                    <option hidden>Add to Menu</option>
                    <React.Fragment>
                        {this.props.menus.map(menu =>
                            <option
                                key={menu.id}
                                value={menu.id}
                                disabled={menu.cocktailIds.includes(id)}
                            >
                                {menu.name}
                            </option>
                        )}
                    </React.Fragment>
                </select>

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