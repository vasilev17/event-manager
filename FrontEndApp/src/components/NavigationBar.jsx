import { FaShoppingCart, FaUserAlt } from "react-icons/fa";
import { IoIosArrowUp } from "react-icons/io";

const NavigationBar = () => {
  const handleSearch = () => {
    console.log('Search button clicked');
  };

  return (
    <nav className="navbar w-full h-[140px] border-b flex items-center justify-between px-4 bg-white">

      <div className="container w-70">
        {/* Logo Section */}
        <div className="navbar-logo w-48 mb-9 ">
          <div className="text-center text-white pr-70 text-xl py-3 bg-[#40ddc7]">
            Лого
          </div>
        </div>

        {/* Buttons Section */}
        <div className="flex items-center space-x-4">
          <button className="text-gray-600 hover:text-gray-900 font-medium flex items-center">
            Всички събития
            <IoIosArrowUp className="text-2xl hover:text-teal-600 ml-2" />
          </button>
          <button className="text-gray-600 hover:text-gray-900 font-medium flex items-center">
            Всички активности
            <IoIosArrowUp className="text-2xl hover:text-teal-600 ml-2" />
          </button>
        </div>
      </div>


      {/* Search bar */}
      <div className="navbar-search flex items-center space-x-2 w-96 h-12 left-[422px] top-[50px] absolute bg-white rounded-3xl shadow border border-[#d9d9d9]">
        <input
          type="text"
          className="search-input p-1 pl-4 border-0 border-color: transparent rounded-3xl w-200"
          placeholder="Търсене..."
        />
        <button
          className="search-button w-28 h-8 left-[255px] top-[8px] text-white absolute bg-[#40ddc7] rounded-2xl hover:bg-teal-700"
          onClick={handleSearch}
        >
          Търсене
        </button>
      </div>

      {/* Button */}
      <div className="navbar-button">
        <button className="main-button w-40 h-12 left-[850px] top-[50px] text-white absolute bg-[#40ddc7] rounded-3xl hover:bg-teal-700 shadow border border-[#d9d9d9]">
          Създай
        </button>
      </div>

      {/* Icons */}
      <div className="navbar-icons flex space-x-8 mr-8 mb-8 text-gray-600">
        {/* Количка Icon and Text */}
        <div className="flex flex-col items-center">
          <FaShoppingCart className="text-xl hover:text-teal-600" />
          <span className="text-sm mt-1">Количка</span>
        </div>

        {/* Профил Icon and Text */}
        <div className="flex flex-col items-center">
          <FaUserAlt className="text-xl hover:text-teal-600" />
          <span className="text-sm mt-1">Профил</span>
        </div>
      </div>

    </nav>
  );
};

export default NavigationBar;
