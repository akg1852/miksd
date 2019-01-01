export default class MenuHandlers {
    constructor(app) {
        this.handleAddMenu = this.handleAddMenu.bind(app);
        this.handleRemoveMenu = this.handleRemoveMenu.bind(app);
        this.handleRenameMenu = this.handleRenameMenu.bind(app);
        this.handleAddCocktailToMenu = this.handleAddCocktailToMenu.bind(app);
        this.handleRemoveCocktailFromMenu = this.handleRemoveCocktailFromMenu.bind(app);
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

                        saveMenus(menus)
                        this.setState({ menus }, () => {
                            this.props.history.push('/Menu/Edit/' + menu.id);
                        });
                    });
                }
            });
    }

    handleRemoveMenu(id) {
        const menus = this.state.menus.filter(menu => menu.id !== id);
        saveMenus(menus)
        this.setState({ menus });
    }

    handleRenameMenu(id, name) {
        const menus = this.state.menus
        const menu = menus.find(m => m.id === id);

        menu.name = name || 'Untitled Menu';
        saveMenus(menus);
        this.setState({ menus });
    }

    handleAddCocktailToMenu(menuId, cocktailId, index) {
        const menus = this.state.menus;
        const cocktailIds = menus.find(m => m.id === menuId).cocktailIds;

        if (!cocktailIds.includes(cocktailId)) {
            if (index == null) {
                index = Infinity;
            }
            cocktailIds.splice(index, 0, cocktailId);
            saveMenus(menus);
            this.setState({ menus });
        }
    }

    handleRemoveCocktailFromMenu(menuId, cocktailId, callback) {
        const menus = this.state.menus;
        const cocktailIds = menus.find(m => m.id === menuId).cocktailIds;
        const cocktailIndex = cocktailIds.indexOf(cocktailId);

        if (cocktailIndex !== -1) {
            cocktailIds.splice(cocktailIndex, 1);
            saveMenus(menus);
            this.setState({ menus },
                () => { callback && callback(cocktailIndex) });
        }
    }
}

function saveMenus(menus) {
    localStorage.setItem('menus', JSON.stringify(menus));
}