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
                    onClick={this.props.handleAddMenu}>
                    +
                </div>
                <h2>Menus</h2>
                <ul>
                    {this.props.menus.map(m => <li key={m.id}>
                        <Link to={"/Menu/Edit/" + m.id}>{m.name}</Link>
                        <span className="remove-menu-button"
                            onClick={() => this.props.handleRemoveMenu(m.id)}>
                            −
                        </span>
                    </li>)}
                </ul>
            </div>
        );
    }
}

export default MenuList;