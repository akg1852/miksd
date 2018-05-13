import React from 'react';
import { Route, Switch } from 'react-router-dom'

import CocktailResultList from './CocktailResultList';
import Cocktail from './Cocktail';
import Header from './Header';
import Menu from './Menu';
import NotFound from './NotFound';

class App extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const query = getQueryString();
        const ingredients = query['i'] || [];

        let title = (query['title'] || [])[0];
        title = (title ? decodeURIComponent(title.replace(/\+/g, ' ')) : 'Cocktails');
        document.title = title + ' - Miksd';

        return (
            <div>
                <Route render={(props) => <Header {...props} selectedIngredients={ingredients} />} />
                <Menu />

                <div id="content">
                    <Switch>
                        <Route exact path='/' render={(props) => <CocktailResultList {...props}
                            title={title}
                            ingredients={ingredients} />} />
                        <Route path='/Cocktail/:id' component={Cocktail} />} />
                        <Route component={NotFound} />
                    </Switch>
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