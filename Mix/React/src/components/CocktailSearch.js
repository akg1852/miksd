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

        this.reset = this.reset.bind(this);
        this.handleInput = this.handleInput.bind(this);
        this.handleKey = this.handleKey.bind(this);
        this.handleSelection = this.handleSelection.bind(this);
    }

    reset() {
        this.setState({ query: '' });
    }

    handleInput(e) {
        const query = e.target.value;

        this.setState({
            query: query,
            selection: 0
        }, () => this.getSearchResults(query));
    }

    getSearchResults(query) {
        if (!query) return;

        fetch('/Cocktail/Search/?q=' + encodeURIComponent(this.state.query))
            .then(response => {
                if (response.status == 200) {
                    response.json().then(data => {
                        this.setState({ cocktails: data || [] });
                    });
                }
            });
    }

    goToCocktail() {
        const cocktails = this.state.cocktails;
        if (cocktails.length) {
            this.setState({ query: '' }, () => {
                this.props.history.push('/Cocktail/' + this.state.cocktails[this.state.selection].id);
            });
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
                    onChange={this.handleInput}
                    onKeyDown={this.handleKey}
                    style={{ zIndex: this.state.query ? '101' : null }}
                />
                {this.state.query &&
                    <React.Fragment>
                        <CocktailSearchResults {...this.state}
                            handleSelection={this.handleSelection}
                            reset={this.reset}
                        />
                        <div
                            className="modal-overlay"
                            onClick={this.reset}
                        ></div>
                    </React.Fragment>
                }
            </form>
        );
    }
}

const CocktailSearchResults = ({ cocktails, selection, handleSelection, reset }) => (
    <div id="search-results"
        onClick={reset}
    >
        {(cocktails.length === 0) ? 'No results' :
            cocktails.map((c, i) => (
                <Link key={c.id} to={"/Cocktail/" + c.id}
                    className={"search-result " + (i === selection ? ' search-result-selected' : '')}
                    onMouseMove={() => handleSelection(i)}
                >
                    {c.name}
                </Link>
            ))}
    </div>
);

export default CocktailSearch;