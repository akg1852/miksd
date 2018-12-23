import React from 'react';
import { Link } from 'react-router-dom';

class MenuList extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        document.title = 'Menus - Miksd';
        return (
            <div className="menus">
                <div className="add-menu-button"
                    title="Create new menu"
                    onClick={this.props.handleAddMenu}>
                    +
                </div>
                <h2>Menus</h2>
                <p className="small-print">
                    Create your own cocktail menus. They're saved to your browser automatically.<br />
                    When a menu is ready, use the "share" link to share a clean read-only version of it.
                </p>
                {!this.props.menus.length ?
                    <div>Create a new menu by clicking on the plus button.</div> :
                    <ul>
                        {this.props.menus.map(m =>
                            <li key={m.id} className="menus-list-item">
                                <Link to={"/Menu/Edit/" + m.id}>{m.name}</Link>
                                <span className="remove-menu-button"
                                    title="Delete menu"
                                    onClick={() => this.props.handleRemoveMenu(m.id)}
                                >
                                    ×
                                </span>
                            </li>
                        )}
                    </ul>
                }
            </div>
        );
    }
}

export default MenuList;