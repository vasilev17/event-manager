import React, { useState } from "react";

export default function UserOrganiserToggle({ isOrganizer, setisOrganizer }) {
  const [isActive, setIsActive] = useState(isOrganizer); // Initialize isActive based on the initial isOrganizer prop

  const handleToggle = () => {
    setIsActive(!isActive); 
    setisOrganizer(!isOrganizer); 
  };

  return (
    <div className="absolute w-[350px] h-[61px] top-[230px] left-9 inline-block rounded-full bg-gray-300 cursor-pointer">
      {isOrganizer ? (
        <div
          onClick={handleToggle}
          className="absolute w-1/2 h-full rounded-full shadow-md transition-transform duration-200 ease-in-out transform translate-x-full bg-teal-500 text-white"
        >
          <div className="flex items-center justify-center h-full">
            <span>Организатор</span>     
          </div>
        </div>
      ) : (
        <div
          onClick={handleToggle}
          className="absolute w-1/2 h-full rounded-full text-white bg-primary shadow-md transition-transform duration-200 ease-in-out transform translate-x-0"
        >
          <div className="flex items-center justify-center h-full rounded-full">
            <span>Потребител</span>
          </div>
        </div>
      )}
    </div>
  );
}