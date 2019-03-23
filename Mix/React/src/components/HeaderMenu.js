import React from 'react';
import { Link } from 'react-router-dom'
import IngredientSearch from './IngredientSearch';

class HeaderMenu extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        this.mounted = true;
        const featured = { name: 'Featured Cocktails', url: '/' };
        fetch('/Cocktail/Categories')
            .then(response => {
                if (response.status == 200) {
                    response.json().then(categories => {
                        if (this.mounted) {
                            this.setState({ categories: [featured].concat(categories) });
                        }
                    });
                }
            });
    }

    componentWillUnmount() {
        this.mounted = false;
    }

    showCategories(show) {
        this.setState({ showCategories: show });
    }

    showIngredientSearch(show) {
        this.setState({ showIngredientSearch: show });
    }

    render() {
        if (!this.state.categories) {
            return null;
        }

        return (
            <div className="header-menu-wrapper" onClick={() => this.showCategories(false)}>
                <ul className="header-menu">
                    <li onClick={e => {
                        e.stopPropagation();
                        this.showCategories(!this.state.showCategories);
                    }}>
                        Browse Cocktails ▽
                    </li>
                    <li onClick={e => {
                        this.showIngredientSearch(!this.state.showIngredientSearch);
                    }}>
                        Search by Ingredients ▽
                    </li>
                    <li><Link to="/Menu">Menus</Link></li>
                </ul>
                {this.state.showCategories &&
                    <ul className="header-menu-categories">
                        {this.state.categories.map(c =>
                            <li key={c.name}><Link to={c.url}>{c.name}</Link></li>
                        )}
                    </ul>
                }
                <IngredientSearch
                    selectedIngredients={this.props.selectedIngredients}
                    handleIngredientSearch={this.props.handleIngredientSearch}
                    showIngredientSearch={this.state.showIngredientSearch}
                    hideIngredientSearch={() => this.showIngredientSearch(false)}
                />
            </div>
        );
    }
}

export default HeaderMenu;