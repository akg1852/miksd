import React from 'react';
import { Link } from 'react-router-dom'

class HeaderMenu extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        const featured = { name: 'Featured', url: '/' };
        fetch('/Cocktail/Categories')
            .then(response => {
                if (response.status == 200) {
                    response.json().then(categories => {
                        this.setState({ categories: [featured].concat(categories) });
                    });
                }
            });
    }

    render() {
        if (!this.state.categories) {
            return null;
        }

        const url = window.location.pathname + window.location.search;
        return (
            <ul className="header-menu">
                {this.state.categories.map(c =>
                    <li key={c.name} className={c.url === url ? 'current' : ''}>
                        <Link to={c.url}>{c.name}</Link>
                    </li>
                )}
            </ul>
        );
    }
}

export default HeaderMenu;