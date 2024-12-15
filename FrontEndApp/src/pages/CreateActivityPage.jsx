 import { useState } from "react";


  const CreateActivity = () => {
    const [step, setStep] = useState(1);
  
    const nextStep = () => {
      setStep(step + 1);
    };
  
    const renderStep = () => {
      switch (step) {
        case 1:
          return (
            <div className="w-full max-w-2xl mx-auto p-6 bg-white shadow rounded-lg">
              <h2 className="text-xl font-semibold text-center mb-4">Добавяне на активност</h2>
              <div className="w-full h-1 bg-gray-200 rounded mb-6">
                <div className="w-1/3 h-full bg-teal-500 rounded"></div>
              </div>
              <label className="block text-gray-700 font-medium mb-2">Имена</label>
              <input
                type="text"
                placeholder="Въведете две имена"
                className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
              />
               <label className="block text-gray-700 font-medium mb-2">Заглавие на добавената активност</label>
              <input
                type="text"
                placeholder="Добавете заглавие"
                className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
              />
              <label className="block text-gray-700 font-medium mb-2">Описание</label>
              <input
                type="text"
                placeholder="Въведете подробно описание"
                className="w-full px-4 py-10 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
              />
              <button
                onClick={nextStep}
                className="mt-6 w-full max-w-48 ml-[35%] bg-teal-500 text-white py-2 rounded-2xl hover:bg-teal-600"
              >
                Продължи
              </button>
            </div>
          );
        case 2:
          return (
            <div className="w-full max-w-2xl mx-auto mt-10 p-6 bg-white shadow rounded-lg">
              <h2 className="text-xl font-semibold text-center mb-4">Добавяне на активност</h2>
              <div className="w-full h-1 bg-gray-200 rounded mb-6">
                <div className="w-2/3 h-full bg-teal-500 rounded"></div>
              </div>
              <div className="w-full h-60 flex flex-col items-center justify-center border-2 border-dashed border-gray-300 bg-teal-50 rounded-lg">
                <span className="text-teal-500 text-lg mb-2">Качване на снимки</span>
                <img className="picture w-20 h-20" src="./src/assets/picture-icon.png"/>
                <p className="text-sm text-gray-500">Поставете снимките тук или кликнете</p>
                <button className="mt-4 bg-teal-500 text-white px-4 py-2 rounded-lg hover:bg-teal-600">
                  Избери Снимка
                </button>
              </div>
              <button
                onClick={nextStep}
                className="mt-6 w-full max-w-48 ml-[35%] bg-teal-500 text-white py-2 rounded-2xl hover:bg-teal-600"
              >
                Продължи
              </button>
            </div>
          );
        case 3:
          return (
            <div className="w-full max-w-2xl mx-auto mt-10 p-6 bg-white shadow rounded-lg">
              <h2 className="text-xl font-semibold text-center mb-4">Добавяне на активност</h2>
              <div className="w-full h-1 bg-gray-200 rounded mb-6">
                <div className="w-full h-full bg-teal-500 rounded"></div>
              </div>
              <img className="picture w-20 h-20 mt-6 mr-auto ml-auto" src="./src/assets/like.png"/>
              <p className="text-center mt-6 text-gray-700">Вашата активност беше успешно добавена!</p>
              <button
                onClick={() => setStep(1)}
                className="mt-6 w-full max-w-48 ml-[35%] bg-teal-500 text-white py-2 rounded-2xl hover:bg-teal-600"
              >
                Нова активност
              </button>
            </div>
          );
        default:
          return null;
      }
    };
  
    return <div className="min-h-screen bg-teal-50 flex items-center justify-center">{renderStep()}</div>;
  };
  
  export default CreateActivity;
  
