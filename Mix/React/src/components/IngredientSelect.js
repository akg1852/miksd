import React from 'react';

const IngredientSelect = ({ selection, handleSelect }) => {
    const include = selection === true;
    const exclude = selection === false;

    return (
        <span className="ingredient-select">
            <span className={'ingredient-select-option' + (include ? ' include' : '')}
                onClick={(e) => {
                    e.preventDefault();
                    handleSelect(!include ? true : undefined)
                }}
            >
                ✓
            </span>
            <span className={'ingredient-select-option' + (exclude ? ' exclude' : '')}
                onClick={(e) => {
                    e.preventDefault();
                    handleSelect(!exclude ? false : undefined)
                }}
            >
                ✗
            </span>
        </span>
    );
}

export default IngredientSelect;
