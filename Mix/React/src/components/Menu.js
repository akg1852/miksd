import React from 'react';
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
        if (this.props !== prevProps) {
            this.getCocktails();
        }
    }

    getViewLink() {
        const cocktails = this.props.cocktailIds.join(',');
        return this.props.name.replace(';', '') + ';' + cocktails;
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
            this.props.readOnly ? (
                <p>Sorry, no cocktails have been added to this menu.</p>
            ) : (
                <p>Go <Link to="/" className="link">find some cocktails</Link> to add to this menu!</p>
            )
        ): (
            <dl>
                {this.state.cocktails.map(c =>
                    <div key={c.id}>
                        <dt>
                            <Link
                                to={'/Cocktail/' + c.id}
                                target={this.props.readOnly ? '_blank' : null}
                            >
                                {c.name}
                            </Link>
                            {this.props.readOnly ? null :
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

        this.isNew = !!this.isNew || this.props.name === 'Untitled Menu';
        return (
            <div>
                <div className="menu">
                    {this.props.readOnly || !this.props.cocktailIds.length ? null :
                        <Link
                            to={'/Menu/View/' + this.getViewLink()}
                            className="share-link"
                        >
                            Share
                        </Link>
                    }
                    <ContentEditable
                        content={this.props.name}
                        editable={!this.props.readOnly}
                        onChange={(e, value) => this.props.handleRenameMenu(this.props.id, value)}
                        tagName="h2"
                        onBlur={() => this.blur = true}
                        caretPosition={!this.blur && this.isNew ? 'end' : null}
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
        <div>Menu generated by <Link to="/" target="_blank" className="link">Miksd</Link></div>
    </div>
);

export default Menu;