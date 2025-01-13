import CardInShppingCart from "../components/CardInShoppingCart";
import TicketSection from "../components/TicketSection";
import CartSummary from "../components/CartSummary";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { isTokenExpired } from "../api/authUtils";
import LoginSignup1 from "../components/login_signup1";

const CartPage = () => {
  const [step, setStep] = useState(1);
  const [isLoggedIn, setIsLoggedIn] = useState(true);

  const navigate = useNavigate();

  useEffect(() => {
    const checkToken = () => {
      if (isTokenExpired()) {
        setIsLoggedIn(false);
        navigate("/login");
      }
    };
    checkToken(); // Check on initial render
  }, [navigate]);

  if (!isLoggedIn) {
    return <LoginSignup1 />;
  }

  const nextStep = () => {
    setStep(step + 1);
  };

  const renderStep = () => {
    switch (step) {
      case 1:
        return (
          <div className="bg-gray-50 min-h-screen p-6 font-sans">
            {/* CART PAGE */}
            <section className="max-w-6xl mx-auto bg-white rounded-lg shadow-md p-6 grid grid-cols-1 md:grid-cols-3 gap-8">
              <div className="md:col-span-2">
                <h1 className="text-2xl font-bold mb-6">КОЛИЧКА</h1>
                {/* Event Cards */}
                <div className="grid grid-cols-1 gap-4">
                  <CardInShppingCart />
                  <CardInShppingCart />
                </div>
              </div>

              {/* Cart Summary */}
              <div className="bg-white p-6 rounded-lg shadow">
                <CartSummary nextStep={nextStep} />
              </div>
            </section>
          </div>
        );

      case 2:
        return (
          <div className="bg-gray-50 min-h-screen w-full max-w-screen-lg p-6 font-sans">
            {/* Personal Info */}
            <section className="max-w-6xl mx-auto bg-white rounded-lg shadow-md p-10 pl-32 pr-32 mt-8">
              <h2 className="text-xl font-bold mb-4">Преглед на поръчка</h2>
              <TicketSection />
              <label className="block text-gray-700 font-medium mt-5 mb-2">
                Име и фамилия
              </label>
              <input
                type="text"
                placeholder="Въведете две имена"
                className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
              />
              <label className="block text-gray-700 font-medium mt-5 mb-2">
                Имейл адрес
              </label>
              <input
                type="text"
                placeholder="Въведете валиден имейл адрес"
                className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
              />
              <button
                onClick={nextStep}
                className="bg-teal-500 text-white mt-5 px-6 py-2 rounded-lg shadow"
              >
                Продължи
              </button>
            </section>
          </div>
        );

      case 3:
        return (
          <div className="bg-gray-50 min-h-screen w-full max-w-screen-lg p-6 font-sans">
            {/* ORDER CONFIRMATION PAGE */}
            <section className="max-w-6xl mx-auto bg-white rounded-lg shadow-md p-10 pl-32 pr-32 mt-8">
              <h2 className="text-xl font-bold mb-4">Преглед на поръчка</h2>
              <TicketSection />

              {/* Payment Method */}
              <div className="payment-method mb-6">
                <h3 className="text-lg font-bold mb-2 mt-4">
                  Начин на плащане
                </h3>
                <div className="border rounded-lg p-4">
                  <label className="flex items-center">
                    <input
                      type="radio"
                      name="payment"
                      defaultChecked
                      className="form-radio text-teal-500"
                    />
                    <span className="ml-2 text-gray-600">
                      Плащане с банкова карта
                    </span>
                  </label>
                </div>
                <button
                  onClick={() => setStep(1)}
                  className="bg-teal-500 text-white w-full max-w-56 py-2 mt-8 rounded-lg shadow"
                >
                  Завърши покупката
                </button>
              </div>
            </section>
          </div>
        );

      default:
        return null;
    }
  };

  return (
    <div className="min-h-screen w-full max-w-screen-2xl bg-gray-50 flex items-center justify-center">
      {renderStep()}
    </div>
  );
};

export default CartPage;
