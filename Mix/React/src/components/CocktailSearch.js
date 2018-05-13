import React from 'react';
import { Link } from 'react-router-dom'

class CocktailSearch extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            query: '',
            cocktails: [],
            selection: 0
        };

        this.handleInput = this.handleInput.bind(this);
        this.handleSearch = this.handleSearch.bind(this);
        this.goToCocktail = this.goToCocktail.bind(this);
        this.handleKey = this.handleKey.bind(this);
        this.handleSelection = this.handleSelection.bind(this);
    }

    static getDerivedStateFromProps(nextProps, prevState) {
        return {
            query: '',
        }
    }

    handleInput(query) {
        this.setState({
            query: query,
            selection: 0
        }, () => this.handleSearch(query));
    }

    handleSearch(query) {
        if (!query) return;

        fetch('/Cocktail/Search/?q=' + encodeURIComponent(this.state.query))
            .then(response => {
                if (response.status == 200) {
                    response.json().then(data => {
                        this.setState({ cocktails: data });
                    });
                }
            });
    }

    goToCocktail() {
        const cocktails = this.state.cocktails;
        if (cocktails && cocktails.length) {
            this.props.history.push('/Cocktail/' + this.state.cocktails[this.state.selection].id);
        }
    }

    handleKey(e) {
        const key = e.keyCode;
        if (key == '38' || key == '40') {
            e.preventDefault();

            const cocktails = this.state.cocktails;
            const shift = (key == '38') ? -1 : 1;
            const selection = (cocktails.length + this.state.selection + shift) % cocktails.length;
            this.handleSelection(selection);
        }
    }

    handleSelection(selection) {
        if (this.state.selection !== selection) {
            this.setState({ selection });
        }
    }

    render() {
        return (
            <form id="cocktail-search-form"
                onSubmit={(e) => { e.preventDefault(); this.goToCocktail() }}>
                <input id="search-field" type="search" autoComplete="off" spellCheck="false"
                    placeholder="Cocktail Search"
                    value={this.state.query}
                    onChange={(e) => this.handleInput(e.target.value)}
                    onKeyDown={this.handleKey} />
                {this.state.query && <CocktailSearchResults handleSelection={this.handleSelection} {...this.state} />}
            </form>
        );
    }
}

const CocktailSearchResults = ({ cocktails, selection, handleSelection }) => (
    <div id="search-results">
        {(cocktails.length === 0) ? 'No results' :
            cocktails.map((c, i) => (
                <Link key={c.id} to={"/Cocktail/" + c.id}
                    className={"search-result " + (i === selection ? ' search-result-selected' : '')}
                    onMouseMove={() => handleSelection(i)}>
                    {c.name}
                </Link>
            ))}
    </div>
);

export default CocktailSearch;