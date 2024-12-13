import React, { useState } from 'react';

export default function Checkbox() {
  const [isChecked, setIsChecked] = useState(false);

  const handleCheckboxChange = () => {
    setIsChecked(!isChecked);
  };

  return (
    <button
      type="checkbox"
      className={"h-6 m-2 aspect-square bg-white border border-gray-500" }
      onClick={handleCheckboxChange}
    >
      {isChecked && (
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6">
        <path stroke-linecap="round" stroke-linejoin="round" d="M6 18 18 6M6 6l12 12" />
      </svg>
      
      )}
    </button>
  );
}