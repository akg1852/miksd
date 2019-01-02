import React from 'react';

const IngredientSelect = ({ id, selection, handleSelect }) => {
    const include = selection === true;
    const exclude = selection === false;

    return (
        <span className="ingredient-select">
            <input type="checkbox" name="i" id={"ingredient+" + id}
                checked={include} readOnly value={id} />
            <label className={include ? 'include' : ''} htmlFor={"ingredient+" + id}
                onClick={() => handleSelect(id, !include ? true : undefined)}>✓</label>
            <input type="checkbox" name="i" id={"ingredient-" + id}
                checked={exclude} readOnly value={-id} />
            <label className={exclude ? 'exclude' : ''} htmlFor={"ingredient-" + id}
                onClick={() => handleSelect(id, !exclude ? false : undefined)}>✗</label>
        </span>
    );
}

export default IngredientSelect;
