import React, { useState } from "react";

const Dropdown = ({ options, value, onChange }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [selectedOptions, setSelectedOptions] = useState(value || []);

  const toggleDropdown = () => {
    setIsOpen(!isOpen);
  };

  const handleOptionSelect = (option) => {
    let newSelectedOptions;
    if (selectedOptions.includes(option)) {
      newSelectedOptions = selectedOptions.filter(
        (selected) => selected !== option
      );
    } else {
      newSelectedOptions = [...selectedOptions, option];
    }
    setSelectedOptions(newSelectedOptions);
    if (onChange) {
      onChange(newSelectedOptions);
    }
  };

  const selectedOptionsDisplay =
    selectedOptions.length > 0 ? selectedOptions.join(", ") : "Изберете опции";

  return (
    <div className="relative inline-block w-full">
      <button
        type="button" // Important: Add type="button" to prevent form submission
        onClick={toggleDropdown}
        className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500 bg-white text-left"
      >
        {selectedOptionsDisplay}
      </button>
      <ul
        className={`absolute z-10 w-full mt-2 bg-white border border-gray-300 rounded-lg shadow-lg max-h-48 overflow-y-auto ${
          isOpen ? "block" : "hidden" // Use block/hidden for show/hide
        }`}
      >
        {options.map((option) => (
          <li
            key={option}
            className="px-4 py-2 hover:bg-gray-100 cursor-pointer"
          >
            <label className="inline-flex items-center w-full">
              <input
                type="checkbox"
                className="hidden peer" // Hide the default checkbox
                checked={selectedOptions.includes(option)}
                onChange={() => handleOptionSelect(option)}
              />
              <div className="w-5 h-5 border rounded-full border-gray-300 mr-2 peer-checked:bg-teal-500 peer-checked:border-teal-500"></div>{" "}
              {/* Custom checkbox */}
              <span>{option}</span>
            </label>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Dropdown;
