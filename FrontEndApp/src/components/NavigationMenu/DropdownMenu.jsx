
import PropTypes from "prop-types";

const DropdownMenuRight = ({ isOpen, items, onItemClick }) => {
  return (
    isOpen && (
      <div className="absolute left-0 mt-5 box-content h-[70vh] w-[180vh] rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 z-10">
        <ul className="py-1 text-xl text-gray-500 w-[50vh]">
          {items.map((item, index) => (
            <li
              key={index}
              className="px-12 py-6 hover:text-black font-bold font-['GT Eesti Pro Display'] hover:underline underline-offset-8 decoration-4 decoration-teal-400 cursor-pointer"
              onClick={() => onItemClick(item)}
            >
              {item}
            </li>
          ))}
        </ul>
      </div>
    )
  );
};

const DropdownMenuLeft = ({ isOpen,  items, onItemClick }) => {
  return (
    isOpen && (
      <div className="absolute  mt-5 right-0 h-[70vh] w-[60vw] bg-white  z-20 border-l-2">
    
        <div className="grid grid-cols-1 gap-4 p-6">
          {items.map((item, index) => (
            <div
              key={index}
              className="flex items-start space-x-4 hover:bg-gray-100 rounded-lg p-4 cursor-pointer"
              onClick={() => onItemClick(item)}
            >
              <div className="w-8 h-8 bg-gray-300 rounded-full"></div>
              <div>
                <h4 className="font-bold text-gray-800">{item}</h4>
                
              </div>
              <div className="flex-1 gap-3">
                <span className="text-gray-500">Допълнителна информация</span>
              </div>
            </div>
          ))}
        </div>
      </div>
    )
  );
};

DropdownMenuRight.propTypes = {
  isOpen: PropTypes.bool.isRequired,
  items: PropTypes.arrayOf(PropTypes.string).isRequired,
  onItemClick: PropTypes.func.isRequired,
};

DropdownMenuLeft.propTypes = {
  isOpen: PropTypes.bool.isRequired,
  title: PropTypes.string.isRequired,
  items: PropTypes.arrayOf(PropTypes.string).isRequired,
  onItemClick: PropTypes.func.isRequired,
};

export { DropdownMenuRight, DropdownMenuLeft };
