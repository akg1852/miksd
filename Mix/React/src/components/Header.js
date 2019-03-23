import React from 'react';
import CocktailSearch from './CocktailSearch';

class Header extends React.Component {
    render() {
        return (
            <div id="header">
                <img className="glass" src="/Content/glass.png" />
                <div id="header-main">
                    <div id="header-top">
                        <h1 id="title">Miksd</h1>
                        <CocktailSearch history={this.props.history} />
                    </div>

                    <div id="header-bottom">
                        <p id="subtitle">Your guide to cocktails / mixed drinks</p>
                    </div>
                </div>
            </div>
        );
    }
}

export default Header;