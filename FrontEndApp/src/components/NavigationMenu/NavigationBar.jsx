
import { FaShoppingCart, FaUserAlt } from "react-icons/fa";
import { IoIosArrowUp, IoIosArrowDown } from "react-icons/io";
import { useState } from "react";
import { DropdownMenuRight, DropdownMenuLeft } from "./DropdownMenu"; 

const NavigationBar = () => {
  const [isEventsOpen, setIsEventsOpen] = useState(false);
  const [isActivitiesOpen, setIsActivitiesOpen] = useState(false);
  const [activeButton, setActiveButton] = useState(null);

  // open "Всички събития" and close "Всички активности"
  const toggleEventsDropdown = () => {
    setIsEventsOpen(!isEventsOpen);
    setIsActivitiesOpen(false);
    setActiveButton(isEventsOpen ? null : "events");
  };

  // open "Всички активности" and close "Всички събития"
  const toggleActivitiesDropdown = () => {
    setIsActivitiesOpen(!isActivitiesOpen);
    setIsEventsOpen(false);
    setActiveButton(isActivitiesOpen ? null : "activities");
  };

  const handleItemClick = (item) => {
    console.log(item); 
  };

  const handleSearch = () => {
    console.log("Search button clicked");
  };

  return (
    <nav className="navbar w-full h-[140px] border-b shadow border border-[#d9d9d9] flex items-center justify-between px-4 bg-white">
      <div className="container w-70">
        {/* Logo Section */}
        <div className="navbar-logo w-48 mb-5 mt-7 ml-5 ">
          <div className="text-center text-white pr-70 text-xl py-3 bg-[#40ddc7]">
            Лого
          </div>
        </div>

        {/* Buttons Section */}
        <div className="flex items-center space-x-4 ml-5">
          <button
            onClick={toggleEventsDropdown}
            className={`text-gray-600 font-medium flex items-center ${activeButton === "events" ? "text-teal-400" : "text-gray-600"}`}
          >
            Всички събития
            {isEventsOpen ? (
              <IoIosArrowDown className="text-2xl hover:text-teal-600 ml-2" />
            ) : (
              <IoIosArrowUp className="text-2xl hover:text-teal-600 ml-2" />
            )}
          </button>

          <button
            onClick={toggleActivitiesDropdown}
            className={`text-gray-600 font-medium flex items-center ${activeButton === "activities" ? "text-teal-400" : "text-gray-600"}`}
          >
            Всички активности
            {isActivitiesOpen ? (
              <IoIosArrowDown className="text-2xl hover:text-teal-600 ml-2" />
            ) : (
              <IoIosArrowUp className="text-2xl hover:text-teal-600 ml-2" />
            )}
          </button>
        </div>

        
        <DropdownMenuRight
          isOpen={isEventsOpen}
          items={["Концерти", "Култура", "Спорт", "Благотворителни", "Други"]}
          onItemClick={handleItemClick}
        />

        <DropdownMenuRight
          isOpen={isActivitiesOpen}
          title="Категории"
          items={["Боулинг", "Гребане", "Катерене", "Рисуване", "Други"]}
          onItemClick={handleItemClick}
        />

        <DropdownMenuLeft
          isOpen={isEventsOpen || isActivitiesOpen}
          title="Примери"
          items={["Пример 1", "Пример 2", "Пример 3", "Пример 4", "Пример 5"]}
          onItemClick={handleItemClick}
        />
      </div>

      {/* Search bar */}
      <div className="navbar-search pl-4 flex items-center space-x-2 w-96 h-12 left-[422px] top-[50px] absolute bg-white rounded-3xl shadow border border-[#d9d9d9]">
        <input
          type="text"
          className="search-input p-1 rounded-3xl outline-transparent w-200"
          placeholder="Търсене..."
        />
        <button
          className="search-button w-28 h-8 left-[255px] top-[8px] text-white absolute bg-[#40ddc7] rounded-2xl hover:bg-teal-700"
          onClick={handleSearch}
        >
          Търсене
        </button>
      </div>

      {/* Create Button */}
      <div className="navbar-button">
        <button className="create main-button w-40 h-12 left-[850px] top-[50px] text-white absolute bg-[#40ddc7] rounded-3xl hover:bg-teal-700 shadow border border-[#d9d9d9]">
          Създай
        </button>
      </div>

     
      <div className="navbar-icons flex space-x-8 mr-8 text-gray-600">
    
        <div className=" shoppingCart flex flex-col items-center">
          <FaShoppingCart className="text-xl hover:text-teal-600" />
          <span className="text-sm mt-1">Количка</span>
        </div>

     
        <div className="profile flex flex-col items-center">
          <FaUserAlt className="text-xl hover:text-teal-600" />
          <span className="text-sm mt-1">Профил</span>
        </div>
      </div>
    </nav>
  );
};

export default NavigationBar;
