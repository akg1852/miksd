import React from 'react';

import CocktailList from './CocktailList';
import Cocktail from './Cocktail';
import Header from './Header';
import Menu from './Menu';

class App extends React.Component {
    constructor(props) {
        super(props);

        const path = window.location.pathname.split('/').slice(1);
        const query = getQueryString();

        this.state = {
            page: path[0].toLowerCase() || 'home',
            customTitle: (query['title'] || [])[0],
            selectedIngredients: query['i'] || [],
            cocktailId: path[1],
        };

        this.Page = this.Page.bind(this);
    }

    componentDidMount() {
        if (this.state.page === 'home') {
            fetch('/Cocktail/List' + window.location.search)
                .then(response => {
                    if (response.status == 200) {
                        response.json().then(cocktails => {
                            this.setState({ cocktails });
                        });
                    }
                });
        }
    }

    Page() {
        const page = this.state.page;

        if (page === 'home') {
            let title = this.state.customTitle;
            title = (title ? decodeURIComponent(title.replace(/\+/g, ' ')) : 'Cocktails');
            document.title = title + ' - Miksd';

            return this.state.cocktails &&
                <CocktailList cocktails={this.state.cocktails} title={title} />
        }
        else if (page === 'cocktail') {
            return <Cocktail id={this.state.cocktailId} />
        }
        else {
            return <div>Page not found!</div>
        }
    }

    render() {
        return (
            <div>
                <Header selectedIngredients={this.state.selectedIngredients} />
                <Menu />

                <div id="content">
                    {this.Page()}
                </div>
            </div>
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

export default App;