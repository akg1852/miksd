import React from 'react';

const Ingredients = ({ ingredients, selectedCategory, handleCategoryChange, handleIngredientSelect }) => (
    <div id="ingredients" className="ingredients">
        <div className="ingredient-category-headers">
            {ingredients.map(c => <IngredientCategoryHeader key={c.Category}
                name={c.Category}
                isCurrent={c.Category === selectedCategory}
                count={c.Ingredients.filter(i => typeof(i.selection) === 'boolean').length}
                handleCategoryChange={handleCategoryChange} />)}
        </div>
        {ingredients.map(c => <IngredientCategory key={c.Category}
            ingredients={c.Ingredients}
            isCurrent={c.Category === selectedCategory}
            handleIngredientSelect={handleIngredientSelect} />)}
    </div>
);

const IngredientCategoryHeader = ({ name, isCurrent, count, handleCategoryChange }) => (
    <div className={"ingredient-category-header " + (isCurrent ? 'current' : '')}
        onClick={() => handleCategoryChange(name)}>
        {name}
        {(count > 0) && <span className="ingredient-count"> ({count})</span>}
    </div>
);

const IngredientCategory = ({ ingredients, isCurrent, handleIngredientSelect }) => (
    <div className={"ingredient-category " + (isCurrent ? 'current' : '')}>
        {ingredients.map(i => <Ingredient key={i.Id} id={i.Id}
            name={i.Name}
            selection={i.selection}
            handleSelect={handleIngredientSelect} />)}
    </div>
);

const Ingredient = ({ id, name, selection, handleSelect }) => (
    <span className="ingredient-option">
        <input className="ingredient-checkbox include" type="checkbox" name="i"
            checked={selection === true}
            onChange={(e) => handleSelect(id, e.target.checked ? true : undefined)}
            id={"ingredient+" + id}
            value={id} />
        <label htmlFor={"ingredient+" + id}>✓</label>
        <input className="ingredient-checkbox exclude" type="checkbox" name="i"
            checked={selection === false}
            onChange={(e) => handleSelect(id, e.target.checked ? false : undefined )}
            id={"ingredient-" + id}
            value={-id} />
        <label htmlFor={"ingredient-" + id}>✗</label>
        <span className="ingredient-name">{name}</span>
    </span>
);

export default Ingredients;
