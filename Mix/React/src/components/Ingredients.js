import React from 'react';

const Ingredients = ({ ingredients, selectedCategory, handleCategoryChange, handleIngredientSelect }) => (
    <div id="ingredients" className="ingredients">
        <div className="ingredient-category-headers">
            {ingredients.map(c => <IngredientCategoryHeader key={c.Category}
                name={c.Category}
                isCurrent={c.Category === selectedCategory}
                handleCategoryChange={handleCategoryChange} />)}
        </div>
        {ingredients.map(c => <IngredientCategory key={c.Category}
            ingredients={c.Ingredients}
            isCurrent={c.Category === selectedCategory}
            handleIngredientSelect={handleIngredientSelect} />)}
    </div>
);

const IngredientCategoryHeader = ({ name, isCurrent, handleCategoryChange }) => (
    <div className={"ingredient-category-header " + (isCurrent ? 'current' : '')}
        onClick={() => handleCategoryChange(name)}>{name}</div>
);

const IngredientCategory = ({ ingredients, isCurrent, handleIngredientSelect }) => (
    <div className={"ingredient-category " + (isCurrent ? 'current' : '')}>
        {ingredients.map(i => <Ingredient key={i.Id} id={i.Id}
            name={i.Name}
            isSelected={i.isSelected}
            handleSelect={handleIngredientSelect} />)}
    </div>
);

const Ingredient = ({ id, name, isSelected, handleSelect }) => (
    <span className="ingredient-option">
        <input className="ingredient-checkbox" type="checkbox" name="i"
            checked={!!isSelected}
            onChange={(e) => handleSelect(id, e.target.checked)}
            id={"ingredient-" + id}
            value={id} />
        <label htmlFor={"ingredient-" + id}>{name}</label>
    </span>
);

export default Ingredients;
