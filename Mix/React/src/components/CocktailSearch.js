import React from 'react';

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
            window.location = '/Cocktail/' + this.state.cocktails[this.state.selection].Id;
        }
    }

    handleKey(e) {
        const key = e.keyCode;
        if (key == '38' || key == '40') {
            e.preventDefault();

            const cocktails = this.state.cocktails;
            const shift = (key == '38') ? -1 : 1;
            const selection = (cocktails.length + this.state.selection + shift) % cocktails.length;
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
                    onInput={(e) => this.handleInput(e.target.value)}
                    onKeyDown={this.handleKey} />
                {this.state.query && <CocktailSearchResults {...this.state} />}
            </form>
        );
    }
}

const CocktailSearchResults = ({ cocktails, selection }) => (
    <div id="search-results">
        {(cocktails.length === 0) ? 'No results' :
            cocktails.map((c, i) => (<a key={c.Id} href={"/Cocktail/" + c.Id}
                className={"search-result " + (i === selection ? ' search-result-selected' : '')}>
                {c.Name}</a>
            ))}
    </div>
);

export default CocktailSearch;