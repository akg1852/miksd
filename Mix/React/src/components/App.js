import React from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';

import CocktailResultList from './CocktailResultList';
import Cocktail from './Cocktail';
import Header from './Header';
import HeaderMenu from './HeaderMenu';
import Menu from './Menu';
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
            menus: menusJson ? JSON.parse(menusJson) : defaultMenus
        };

        this.handleAddMenu = this.handleAddMenu.bind(this);
        this.handleRemoveMenu = this.handleRemoveMenu.bind(this);
        this.handleRenameMenu = this.handleRenameMenu.bind(this);
        this.handleAddCocktailToMenu = this.handleAddCocktailToMenu.bind(this);
        this.handleRemoveCocktailFromMenu = this.handleRemoveCocktailFromMenu.bind(this);
    }

    handleAddMenu() {
        fetch('/Menu/Create')
            .then(response => {
                if (response.status == 200) {
                    response.json().then(menu => {
                        const menus = this.state.menus.concat(
                        {
                            id: menu.id,
                            name: 'Untitled Menu',
                            cocktailIds: []
                        });

                        this.saveMenus(menus)
                        this.setState({ menus }, () => {
                            this.props.history.push('/Menu/Edit/' + menu.id);
                        });
                    });
                }
            });
    }

    handleRemoveMenu(id) {
        const menus = this.state.menus.filter(menu => menu.id !== id);
        this.saveMenus(menus)
        this.setState({ menus });
    }

    handleRenameMenu(id, name) {
        const menus = this.state.menus
        const menu = menus.find(m => m.id === id);

        menu.name = name || 'Untitled Menu';
        this.saveMenus(menus);
    }

    handleAddCocktailToMenu(menuId, cocktailId) {
        const menus = this.state.menus;
        const cocktailIds = menus.find(m => m.id === menuId).cocktailIds;

        if (!cocktailIds.includes(cocktailId)) {
            cocktailIds.push(cocktailId);
            this.saveMenus(menus);
        }
    }

    handleRemoveCocktailFromMenu(menuId, cocktailId) {
        const menus = this.state.menus;
        const cocktailIds = menus.find(m => m.id === menuId).cocktailIds;
        const cocktailIndex = cocktailIds.indexOf(cocktailId);

        if (cocktailIndex !== -1) {
            cocktailIds.splice(cocktailIndex, 1);
            this.setState({ menus });
            this.saveMenus(menus);
        }
    }

    saveMenus(menus) {
        localStorage.setItem('menus', JSON.stringify(menus));
    }

    render() {
        const query = getQueryString();
        const ingredients = query['i'] || [];

        let title = (query['title'] || [])[0];
        title = (title ? decodeURIComponent(title.replace(/\+/g, ' ')) : 'Cocktails');
        document.title = title + ' - Miksd';

        const page = () => (
            <div>
                <Route render={(props) => <Header {...props} selectedIngredients={ingredients} />} />
                <HeaderMenu />

                <div id="content">
                    <Switch>
                        <Route exact path='/' render={(props) =>
                            <CocktailResultList {...props}
                                title={title}
                                ingredients={ingredients} />
                        } />
                        <Route path='/Cocktail/:id' render={(props) =>
                            <Cocktail {...props}
                                menus={this.state.menus}
                                handleAddCocktailToMenu={this.handleAddCocktailToMenu}
                            />
                        } />
                        <Route path='/Menu/Edit/:id' render={(props) =>
                            <Menu {...props}
                                {...this.state.menus.find(m => m.id === props.match.params.id) }
                                handleRenameMenu={this.handleRenameMenu}
                                handleRemoveCocktailFromMenu={this.handleRemoveCocktailFromMenu}
                            />
                        } />
                        <Route path='/Menu/' render={(props) =>
                            <MenuList {...props}
                                menus={this.state.menus}
                                handleAddMenu={this.handleAddMenu}
                                handleRemoveMenu={this.handleRemoveMenu}
                            />
                        } />
                        <Route component={NotFound} />
                    </Switch>
                </div>
            </div>
        );

        return (
            <Switch>
                <Route path='/Menu/View/:id' render={(props) =>
                    <Menu {...props}
                        readOnly={true}
                        {...this.state.menus.find(m => m.id === props.match.params.id) }
                    />
                } />
                <Route component={page} />
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