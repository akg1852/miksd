import React from 'react';

class Menu extends React.Component {
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
            <ul className="menu">
                {this.state.categories.map(c =>
                    <li key={c.name} className={c.url === url ? 'current' : ''}>
                        <a href={c.url}>{c.name}</a>
                    </li>
                )}
            </ul>
        );
    }
}

export default Menu;