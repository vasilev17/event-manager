import { FaShoppingCart, FaUserAlt } from "react-icons/fa";
import { IoIosArrowUp, IoIosArrowDown } from "react-icons/io";
import { useState } from "react";
import { DropdownMenuRight, DropdownMenuLeft } from "./DropdownMenu";
import { Link } from "react-router";
import { getUsername } from "../../api/authUtils";
import logo from "../../assets/logo.png";

const NavigationBar = () => {
  const [isEventsOpen, setIsEventsOpen] = useState(false);
  const [isActivitiesOpen, setIsActivitiesOpen] = useState(false);
  const [activeButton, setActiveButton] = useState(null);
  const [search, setSearch] = useState("");

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
    <nav className="navbar w-full border-b shadow border border-[#d9d9d9] flex items-center justify-between px-4 bg-white">
      <div className="container w-70">
        {/* Logo Section */}
        <Link to="/">  
              <img className="logo w-48 mb-3 mt-4" src={logo} />       
        </Link>

        {/* Buttons Section */}
        <div className="flex items-center space-x-4 ml-5 mb-3">
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
<div className="navbar-search px-4 py-2 sm:px-4 sm:py-1 max-w-sm sm:w-auto mr-10 flex items-center space-x-2 bg-white rounded-3xl shadow border border-[#d9d9d9] absolute sm:relative left-0 sm:left-auto">
  <input
    onChange={(e) => setSearch(e.target.value)}
    type="text"
    className="search-input p-2 sm:p-1 rounded-3xl outline-transparent w-full sm:w-96 focus:outline-none"
    placeholder="Търсене..."
  />
  <Link to={"events/" + search}>
    <button
      className="search-button px-4 py-2 sm:px-8 ml-10 text-white bg-teal-400 rounded-3xl hover:bg-teal-700 shadow border border-[#d9d9d9]"
      onClick={handleSearch}
    >
      Търсене
    </button>
  </Link>
</div>


      {/* Create Button */}
      <Link to="/create">
        <div className="navbar-button">
          <button className="create main-button px-12 py-3 mr-16 text-white bg-teal-400 rounded-3xl hover:bg-teal-700 shadow border border-[#d9d9d9] sm:ml-4">
          
            Създай
          </button>
        </div>
      </Link>

      <div className="navbar-icons flex space-x-8 mr-8 text-gray-600">
        <Link to="cart/whoever-this-user-is">
          <div className=" shoppingCart flex flex-col items-center">
            <FaShoppingCart className="text-xl hover:text-teal-600" />
            <span className="text-sm mt-1">Количка</span>
          </div>
        </Link>
        <Link to={getUsername() ? "profile/"+getUsername() : "/login"}>
          <div className="profile flex flex-col items-center">
            <FaUserAlt className="text-xl hover:text-teal-600" />
            <span className="text-sm mt-1">Профил</span>
          </div>
        </Link>
      </div>
    </nav>
  );
};

export default NavigationBar;
