import cartImage from "../assets/cart.png"
import Footer from "../components/Footer";
import Card from "../components/Card"

const EmptyCartPage = () => {
  return (
    <div className="min-h-screen bg-white flex flex-col items-center justify-start py-10 px-4">
      {/* Cart Section */}
      <div className="flex flex-col items-center justify-center mt-10">
        {/* Cart Image */}
        <div className="w-48 h-48 flex justify-center items-center">
          <img
           src={cartImage}
            alt="Cart Icon"
            className="w-full h-full object-contain"
          />
        </div>

        {/* Empty Cart Text */}
        <h1 className="text-2xl font-bold text-gray-700 mt-4">
          Количката Ви е празна
        </h1>

        {/* Button */}
        <button className="mt-6 px-6 py-2 border-2 border-teal-500 text-teal-500 rounded-full hover:bg-teal-500 hover:text-white transition">
            <a></a>
          Намерете неща за правене
        </button>
      </div>

      {/* Horizontal Line */}
      <div className="w-full max-w-4xl border-t border-gray-300 mt-12 mb-8"></div>

      {/* Suggestions Section */}
      <div className="w-full max-w-4xl">
        <h2 className="text-xl font-semibold text-gray-700 mb-6">Още предложения</h2>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-16">
          {/* Placeholder Boxes */}
          <div className="w-full h-full bg-gray-200 rounded-3xl">
            <Card />
          </div>
          <div className="w-full h-full bg-gray-200 rounded-3xl">
            <Card />
          </div>
          <div className="w-full h-full bg-gray-200 rounded-3xl">
            <Card />
          </div>
        </div>
      </div>
      <Footer />
    </div>
    
  );
};

export default EmptyCartPage;
