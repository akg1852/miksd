import React from 'react';
import CocktailList from './CocktailList';
import { Link } from 'react-router-dom';

class Cocktail extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};

        this.getCocktail();
    }

    componentDidUpdate(prevProps, prevState) {
        if (this.props.match.params.id !== prevProps.match.params.id) {
            window.scrollTo(0, 0);
            this.setState({ cocktail: null }, () => {
                this.getCocktail();
            });
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

                <Menus
                    cocktailId={id}
                    {...this.props}
                />

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

class Menus extends React.Component {
    constructor(props) {
        super(props);

        this.handleAddCocktailToMenu = this.handleAddCocktailToMenu.bind(this);
    }

    handleAddCocktailToMenu(e) {
        const menu = e.target.value;
        const cocktail = this.props.cocktailId;
        this.props.handleAddCocktailToMenu(menu, cocktail);
    }

    render() {
        const menusIncluded = [];
        const menusNotIncluded = [];
        this.props.menus.forEach(menu => {
            if (menu.cocktailIds.includes(this.props.cocktailId)) {
                menusIncluded.push(menu);
            }
            else {
                menusNotIncluded.push(menu);
            }
        });

        return (
            <div>
                <select className="add-to-menu"
                    value=""
                    onChange={this.handleAddCocktailToMenu}
                >
                    <option hidden>Add to Menu</option>
                    {!menusNotIncluded.length ?
                        <option disabled>No more menus</option> :
                        menusNotIncluded.map(menu =>
                        <option
                            key={menu.id}
                            value={menu.id}
                        >
                            {menu.name}
                        </option>
                    )}
                </select>
                {menusIncluded.map(menu =>
                    <div className='menu-tag' key={menu.id}>
                        <Link
                            to={"/Menu/Edit/" + menu.id}
                            title={menu.name}
                        >
                            {menu.name.length > 20 ? menu.name.substr(0, 19) + '…' : menu.name}
                        </Link>
                        <div
                            className="tag-x"
                            title="Remove cocktail from menu"
                            onClick={() => this.props.handleRemoveCocktailFromMenu(menu.id, this.props.cocktailId)}
                        >
                            ×
                        </div>
                    </div>
                )}
            </div>
        );
    }
}

export default Cocktail;