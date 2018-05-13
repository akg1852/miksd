import React from 'react';

class Menu extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const categories = this.props.categories;
        categories.unshift({ name: 'Featured', url: '/' });
        const url = window.location.pathname + window.location.search;

        return (
            <ul className="menu">
                {categories.map(c =>
                    <li key={c.name} className={c.url === url ? 'current' : ''}>
                        <a href={c.url}>{c.name}</a>
                    </li>
                )}
            </ul>
        );
    }
}

export default Menu;