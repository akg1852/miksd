import React from 'react';
import { Link } from 'react-router-dom'

class HeaderMenu extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        this.mounted = true;
        const featured = { name: 'Featured Cocktails', url: '/' };
        const menus = { name: 'Menus', url: '/Menu' };
        fetch('/Cocktail/Categories')
            .then(response => {
                if (response.status == 200) {
                    response.json().then(categories => {
                        if (this.mounted) {
                            this.setState({ categories: [featured, menus].concat(categories) });
                        }
                    });
                }
            });
    }

    componentWillUnmount() {
        this.mounted = false;
    }

    render() {
        if (!this.state.categories) {
            return null;
        }

        const url = window.location.pathname + window.location.search;
        const isMenu = window.location.pathname.indexOf('/Menu/Edit') === 0;
        const isCurrent = c => c.url === url || c.url === '/Menu' & isMenu;

        return (
            <ul className="header-menu">
                {this.state.categories.map(c =>
                    <li key={c.name} className={isCurrent(c) ? 'current' : ''}>
                        <Link to={c.url}>{c.name}</Link>
                    </li>
                )}
            </ul>
        );
    }
}

export default HeaderMenu;