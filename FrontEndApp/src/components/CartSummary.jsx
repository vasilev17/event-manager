import cartImage from "../assets/cart.png"

const CartSummary = ({ nextStep }) => {
    return (
      <div className=" mt-6 pt-3 flex flex-col items-start">
        {/* Cart Icon */}
        
        <img className="cartImage w-40 h-40 ml-3 mb-4" src={cartImage} alt="Cart Icon" />
          
  
        {/* Cart Summary + Button */}
        <div>
          <p className="text-gray-500">Обща сума (2 продукта)</p>
          <p className="text-2xl font-bold mb-4">123 лв.</p>
          <button
            onClick={nextStep}
            className="bg-teal-500 text-white px-6 py-2 rounded-lg shadow"
          >
            КЪМ ЗАВЪРШВАНЕ
          </button>
        </div>
      </div>
    );
  };
  
  export default CartSummary;
  
