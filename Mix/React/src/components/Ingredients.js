import React from 'react';

const Ingredients = ({ ingredients, selectedCategory, handleCategoryChange, handleIngredientSelect }) => (
    <div id="ingredients" className="ingredients">
        <div className="ingredient-category-headers">
            {ingredients.map(c => <IngredientCategoryHeader key={c.Category}
                name={c.Category}
                isCurrent={c.Category === selectedCategory}
                count={c.Ingredients.filter(i => i.selection !== undefined).length}
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

const Ingredient = ({ id, name, selection, handleSelect }) => {
    const include = selection === true;
    const exclude = selection === false;
    const noSelection = selection === undefined;

    return (
        <span className={"ingredient-option " + (include ? 'include' : exclude ? 'exclude' : '')}>
            <input type="checkbox" name="i" id={"ingredient+" + id}
                checked={include} value={id} />
            <label className="include" htmlFor={"ingredient+" + id}
                onClick={() => handleSelect(id, !include ? true : undefined)}>✓</label>
            <input type="checkbox" name="i" id={"ingredient-" + id}
                checked={exclude} value={-id} />
            <label className="exclude" htmlFor={"ingredient-" + id}
                onClick={() => handleSelect(id, !exclude ? false : undefined)}>✗</label>
            <span className="ingredient-name"
                onClick={() => handleSelect(id, noSelection ? true : undefined)}>{name}</span>
        </span>
    );
}

export default Ingredients;
