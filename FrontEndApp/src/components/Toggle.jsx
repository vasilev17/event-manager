import React, { useState } from "react";

const Toggle = ({ value, onChange }) => {
  const [internalValue, setInternalValue] = useState(value || false);

  const handleToggle = () => {
    const newValue = !internalValue;
    setInternalValue(newValue);
    if (onChange) {
      onChange(newValue);
    }
  };

  return (
    <div className="relative inline-block w-20 h-8 rounded-full bg-gray-300 cursor-pointer">
      {internalValue ? (
        <div
          onClick={handleToggle}
          className="absolute w-2/3 h-full rounded-full shadow-md transition-transform duration-200 ease-in-out transform translate-x-2/3 bg-primary text-white"
        >
          <div className="flex items-center justify-center h-full">
            <span>Да</span>     
          </div>
        </div>
      ) : (
        <div
          onClick={handleToggle}
          className="absolute w-2/3 h-full rounded-full bg-white shadow-md transition-transform duration-200 ease-in-out transform translate-x-0"
        >
          <div className="flex items-center justify-center h-full border-gray-300 border rounded-full">
            <span className="text-gray-700">Не</span>
          </div>
        </div>
      )}
    </div>
  );
};

export default Toggle;
