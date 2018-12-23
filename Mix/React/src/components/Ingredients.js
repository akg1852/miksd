import React from 'react';

const Ingredients = ({ ingredients, selectedCategory, handleCategoryChange, handleIngredientSelect }) => (
    <div id="ingredients" className="ingredients">
        <div className="ingredient-category-headers">
            {ingredients.map(c => <IngredientCategoryHeader key={c.category}
                name={c.category}
                isCurrent={c.category === selectedCategory}
                count={c.ingredients.filter(i => i.selection !== undefined).length}
                handleCategoryChange={handleCategoryChange} />)}
        </div>
        {ingredients.map(c => <IngredientCategory key={c.category}
            ingredients={c.ingredients}
            isCurrent={c.category === selectedCategory}
            handleIngredientSelect={handleIngredientSelect} />)}
    </div>
);

const IngredientCategoryHeader = ({ name, isCurrent, count, handleCategoryChange }) => (
    <div className={"ingredient-category-header " + (isCurrent ? 'current' : '')}
        style={{ flexBasis: textWidth(name) }}
        onClick={() => handleCategoryChange(name)}>
        {name}
        {(count > 0) && <span className="ingredient-count"> ({count})</span>}
    </div>
);

const textWidth = (text) => {
    const el = document.createElement("span");
    el.textContent = text;
    el.style.visibility = 'hidden';
    document.body.appendChild(el);
    const textWidth = (el.offsetWidth) + "px";
    document.body.removeChild(el);
    return textWidth;
};

const IngredientCategory = ({ ingredients, isCurrent, handleIngredientSelect }) => (
    <div className={"ingredient-category " + (isCurrent ? 'current' : '')}>
        {ingredients.map(i => <Ingredient key={i.id} id={i.id}
            name={i.name}
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
                checked={include} readOnly value={id} />
            <label className="include" htmlFor={"ingredient+" + id}
                onClick={() => handleSelect(id, !include ? true : undefined)}>✓</label>
            <input type="checkbox" name="i" id={"ingredient-" + id}
                checked={exclude} readOnly value={-id} />
            <label className="exclude" htmlFor={"ingredient-" + id}
                onClick={() => handleSelect(id, !exclude ? false : undefined)}>✗</label>
            <span className="ingredient-name"
                onClick={() => handleSelect(id, noSelection ? true : undefined)}>{name}</span>
        </span>
    );
}

export default Ingredients;
