﻿import React from 'react';
import { Route, Link } from 'react-router-dom';
import ContentEditable from 'react-sane-contenteditable';
import NotFound from './NotFound';

class Menu extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};

        this.getCocktails()
    }

    componentDidMount() {
        this.mounted = true;
    }

    componentWillUnmount() {
        this.mounted = false;
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevProps !== this.props) {
            this.getCocktails();
        }
    }

    getCocktails() {
        if (!this.props.cocktailIds) {
            return;
        }

        fetch('/Menu/Data?' + this.props.cocktailIds.map(c => 'c=' + c).join('&'))
            .then(response => {
                if (response.status == 200) {
                    response.json().then(cocktails => {
                        if (this.mounted) {
                            this.setState({ cocktails });
                        }
                    });
                }
            });
    }

    handleMoveCocktail(cocktailId, moveAmount) {
        const menuId = this.props.id;
        this.props.handleRemoveCocktailFromMenu(menuId, cocktailId, oldIndex => {
            this.props.handleAddCocktailToMenu(menuId, cocktailId, oldIndex + moveAmount);
        })
    }

    render() {
        if (!this.props.name) {
            return <NotFound />;
        }

        document.title = this.props.name + ' - Miksd';

        const cocktailList = !this.state.cocktails ? null : !this.state.cocktails.length ? (
            <p>
                Go <Link to='/' className='link'>find some cocktails</Link> to add to this menu!
            </p>
        ): (
            <dl>
                {this.state.cocktails.map(c =>
                    <div key={c.id}>
                        <dt>
                            <Link to={"/Cocktail/" + c.id}>{c.name}</Link>
                            {!this.props.handleRemoveCocktailFromMenu ? null :
                                <React.Fragment>
                                    <span className="remove-menu-button"
                                        title="Remove cocktail from menu"
                                        onClick={() => this.props.handleRemoveCocktailFromMenu(this.props.id, c.id)}
                                    >
                                        −
                                    </span>
                                    <span>
                                        <span
                                            className="menu-move-arrow"
                                            title="Move cocktail up"
                                            onClick={() => this.handleMoveCocktail(c.id, -1)}
                                        >
                                            ▲
                                        </span>
                                        <span
                                            className="menu-move-arrow"
                                            title="Move cocktail down"
                                            onClick={() => this.handleMoveCocktail(c.id, 1)}
                                        >
                                            ▼
                                        </span>
                                    </span>
                                </React.Fragment>
                            }
                        </dt>
                        <dd>{c.recipe.join(', ')}</dd>
                    </div>
                )}
            </dl>
        );

        return (
            <div>
                <div className="menu">
                    <ContentEditable
                        content={this.props.name}
                        editable={!!this.props.handleRenameMenu}
                        onChange={(e, value) => this.props.handleRenameMenu(this.props.id, value)}
                        tagName="h2"
                    />
                    {cocktailList}
                </div>
                <Route path='/Menu/View' component={Footer} />
            </div>
        );
    }
}

const Footer = () => (
    <div className='footer'>
        <hr />
        <div>Menu generated by <Link to='/' className='link'>Miksd</Link></div>
    </div>
);

export default Menu;