import React from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';

import CocktailResultList from './CocktailResultList';
import Cocktail from './Cocktail';
import Header from './Header';
import HeaderMenu from './HeaderMenu';
import Menu from './Menu';
import MenuHandlers from '../MenuHandlers';
import MenuList from './MenuList';
import NotFound from './NotFound';

const defaultMenus = [
    {
        id: 'e3d9b3e9-c9a9-4cc6-9978-488e426cb52a',
        name: 'Classics',
        cocktailIds: [1, 2, 4, 7, 8, 15, 27]
    }
];

class App extends React.Component {
    constructor(props) {
        super(props);

        const menusJson = localStorage.getItem('menus');
        this.state = {
            showIngredientSearch: false,
            menus: menusJson ? JSON.parse(menusJson) : defaultMenus
        };

        this.menuHandlers = new MenuHandlers(this);
        this.toggleIngredientSearch = this.toggleIngredientSearch.bind(this);
        this.handleIngredientSearch = this.handleIngredientSearch.bind(this);
    }

    toggleIngredientSearch() {
        this.setState({ showIngredientSearch: !this.state.showIngredientSearch });
    }

    handleIngredientSearch(selectedIngredients) {
        this.selectedIngredients = null;
        this.props.history.push('/?' + selectedIngredients.map(i => 'i=' + i).join('&'));
    }

    renderPage(title, ingredients) {
        const modalOpen = this.state.showIngredientSearch;

        return (
            <div className="app-container">
                <div className="app">
                    <Route render={(props) =>
                        <Header {...props}
                            showIngredientSearch={this.state.showIngredientSearch}
                            toggleIngredientSearch={this.toggleIngredientSearch}
                            selectedIngredients={ingredients}
                            handleIngredientSearch={this.handleIngredientSearch}
                        />
                    } />
                    <HeaderMenu />

                    <div id="content">
                        <Switch>
                            <Route exact path='/' render={(props) =>
                                <CocktailResultList {...props}
                                    title={title}
                                    query={window.location.search}
                                    selectedIngredients={ingredients}
                                    handleIngredientSearch={this.handleIngredientSearch}
                                />
                            } />
                            <Route path='/Cocktail/:id' render={(props) =>
                                <Cocktail {...props}
                                    menus={this.state.menus}
                                    handleAddCocktailToMenu={this.menuHandlers.handleAddCocktailToMenu}
                                    handleRemoveCocktailFromMenu={this.menuHandlers.handleRemoveCocktailFromMenu}
                                />
                            } />
                            <Route path='/Menu/Edit/:id' render={(props) =>
                                <Menu {...props}
                                    {...this.state.menus.find(m => m.id === props.match.params.id) }
                                    handleRenameMenu={this.menuHandlers.handleRenameMenu}
                                    handleAddCocktailToMenu={this.menuHandlers.handleAddCocktailToMenu}
                                    handleRemoveCocktailFromMenu={this.menuHandlers.handleRemoveCocktailFromMenu}
                                />
                            } />
                            <Route path='/Menu/' render={(props) =>
                                <MenuList {...props}
                                    menus={this.state.menus}
                                    handleAddMenu={this.menuHandlers.handleAddMenu}
                                    handleRemoveMenu={this.menuHandlers.handleRemoveMenu}
                                />
                            } />
                            <Route component={NotFound} />
                        </Switch>
                    </div>
                </div>
                {modalOpen && <div className="modal-overlay"></div>}
            </div>
        );
    }

    render() {
        const query = getQueryString();
        this.selectedIngredients = query['i'] || this.selectedIngredients || [];

        let title = (query['title'] || [])[0];
        title = (title ? decodeURIComponent(title.replace(/\+/g, ' ')) : 'Cocktails');
        document.title = title + ' - Miksd';

        return (
            <Switch>
                <Route path='/Menu/View/:name;:cocktailIds' render={(props) =>
                    <Menu {...props}
                        readOnly={true}
                        name={decodeURIComponent(props.match.params.name)}
                        cocktailIds={props.match.params.cocktailIds.split(',')}
                    />
                } />
                <Route render={() => this.renderPage(title, this.selectedIngredients)} />
            </Switch>
        );
    }
}

const getQueryString = () => {
    var result = {};
    var vars = window.location.search.substring(1).split('&');

    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split('=');
        var key = pair[0];
        var val = pair[1];

        result[key] = result[key] || [];
        result[key].push(val);
    }
    return result;
};

export default withRouter(App);