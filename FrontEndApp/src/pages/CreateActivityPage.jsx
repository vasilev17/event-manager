import { useState } from "react";
import Popup from "../components/PopUp";
import { api, getToken } from "../api/authUtils.js";
import Dropdown from "../components/Dropdown.jsx";
import Toggle from "../components/Toggle.jsx";

const CreateActivity = () => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [address, setAddress] = useState("");
  // const [timeStart, setTimeStart] = useState("");
  // const [timeEnd, setTimeEnd] = useState("");
  const [webpage, setWebpage] = useState("");
  const [isActivity, setIsActivity] = useState(false);
  const [isThirdParty, setIsThirdParty] = useState(false);
  const [types, setTypes] = useState([]);
  const [errorMessage, setErrorMessage] = useState(null);
  const [step, setStep] = useState(1);

  const timeStart = "2025-01-05T13:56:47.095Z";
  const timeEnd = "2025-01-07T13:56:47.095Z";
  const picture = null;

  const eventTypes = [
    "Convention",
    "Conference",
    "Corporate",
    "Seminar",
    "Presentation",
    "GalaDinner",
    "Entertainment",
    "Other",
  ];

  const handleCreateEvent = async () => {
    const formData = new FormData();

    formData.append("Name", name);
    formData.append("Description", description);
    formData.append("StartDateTime", timeStart); // Make sure timeStart is a string in ISO 8601 format
    formData.append("EndDateTime", timeEnd); // Make sure timeEnd is a string in ISO 8601 format

    types.forEach((type) => {
      formData.append("Types", type); // Append each type individually
    });

    formData.append("Webpage", webpage);
    formData.append("Address", address);
    formData.append("IsActivity", isActivity); // Use boolean values
    formData.append("IsThirdParty", isThirdParty); // Use boolean values

    if (picture) {
      formData.append("Picture", picture); // Append the file if available
    }

    try {
      const response = await api.post("Event/CreateEvent", formData, {
        headers: {
          "Content-Type": "multipart/form-data", // Important: Set correct content type
          Authorization: `Bearer ${getToken()}`, // if you need authorization
        },
      });
      console.log("Event created:", response.data);
      // Handle success
    } catch (error) {
      console.error("Error creating event:", error);
      if (error.response) {
        console.error("Response data:", error.response.data);
        console.error("Response status:", error.response.status);
        setErrorMessage("Something went wrong trying to create event!");
      }
      // Handle error, display message
    }

    setName("");
    setDescription("");
    setAddress("");
    // setTimeStart("");
    // setTimeEnd("");
    setWebpage("");
    setIsActivity(false);
    setIsThirdParty(false);
    setTypes([]);

    nextStep();
  };

  const nextStep = () => {
    setStep(step + 1);
  };

  const renderStep = () => {
    switch (step) {
      case 1:
        return (
          <div className="w-full max-w-2xl mx-auto p-6 bg-white shadow rounded-lg">
            <h2 className="text-xl font-semibold text-center mb-4">
              Добавяне на активност
            </h2>
            <div className="w-full h-1 bg-gray-200 rounded mb-6">
              <div className="w-1/5 h-full bg-teal-500 rounded"></div>
            </div>

            <label className="block text-gray-700 font-medium mb-2">
              Заглавие на новата активност
            </label>
            <input
              type="text"
              onChange={(e) => setName(e.target.value)}
              placeholder="Добавете заглавие"
              className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
            />
            <label className="block text-gray-700 font-medium mb-2">
              Описание
            </label>
            <input
              type="text"
              onChange={(e) => setDescription(e.target.value)}
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
          <div className="w-full max-w-2xl mx-auto p-6 bg-white shadow rounded-lg">
            <h2 className="text-xl font-semibold text-center mb-4">
              Добавяне на активност
            </h2>
            <div className="w-full h-1 bg-gray-200 rounded mb-6">
              <div className="w-2/5 h-full bg-teal-500 rounded"></div>
            </div>
            <label className="block text-gray-700 font-medium mb-2">
              Адрес
            </label>
            <input
              type="text"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
              placeholder="Въведете адрес"
              className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
            />
            <label className="block text-gray-700 font-medium mb-2">
              Време на започване{" "}
            </label>
            <input
              type="text"
              value={timeStart}
              onChange={(e) => setTimeStart(e.target.value)}
              placeholder="2025-01-01T12:00:00Z"
              className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
            />
            <label className="block text-gray-700 font-medium mb-2">
              Време на приключване
            </label>
            <input
              type="text"
              value={timeEnd}
              onChange={(e) => setTimeEnd(e.target.value)}
              placeholder="2025-01-01T14:00:00Z"
              className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
            />
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
          <div className="w-full max-w-2xl mx-auto p-6 bg-white shadow rounded-lg">
            <h2 className="text-xl font-semibold text-center mb-4">
              Добавяне на активност
            </h2>
            <div className="w-full h-1 bg-gray-200 rounded mb-6">
              <div className="w-3/5 h-full bg-teal-500 rounded"></div>
            </div>
            <label className="block text-gray-700 font-medium mb-2">
              Уеб страница (не е задължително)
            </label>
            <input
              type="text"
              value={webpage}
              onChange={(e) => setWebpage(e.target.value)}
              placeholder="Въведете адрес"
              className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
            />
            <label className="block text-gray-700 font-medium mb-2">
              Тип на събитието{" "}
            </label>
            <Dropdown options={eventTypes} value={types} onChange={setTypes} />
            <label className="block text-gray-700 font-medium mb-2">
              Повтаряшо ли се е събитието
            </label>
            <Toggle value={isActivity} onChange={setIsActivity} />
            <label className="block text-gray-700 font-medium mb-2">
              Third party ли е събитието
            </label>
            <Toggle value={isThirdParty} onChange={setIsThirdParty} />
            <button
              onClick={nextStep}
              className="mt-6 w-full max-w-48 ml-[35%] bg-teal-500 text-white py-2 rounded-2xl hover:bg-teal-600"
            >
              Продължи
            </button>
          </div>
        );
      case 4:
        return (
          <div className="w-full max-w-2xl mx-auto mt-10 p-6 bg-white shadow rounded-lg">
            <h2 className="text-xl font-semibold text-center mb-4">
              Добавяне на активност
            </h2>
            <div className="w-full h-1 bg-gray-200 rounded mb-6">
              <div className="w-4/5 h-full bg-teal-500 rounded"></div>
            </div>
            <div className="w-full h-60 flex flex-col items-center justify-center border-2 border-dashed border-gray-300 bg-teal-50 rounded-lg">
              <span className="text-teal-500 text-lg mb-2">
                Качване на снимки
              </span>
              <img
                className="picture w-20 h-20"
                src="./src/assets/picture-icon.png"
              />
              <p className="text-sm text-gray-500">
                Поставете снимките тук или кликнете
              </p>
              <button className="mt-4 bg-teal-500 text-white px-4 py-2 rounded-lg hover:bg-teal-600">
                Избери Снимка
              </button>
            </div>
            <button
              onClick={handleCreateEvent}
              className="mt-6 w-full max-w-48 ml-[35%] bg-teal-500 text-white py-2 rounded-2xl hover:bg-teal-600"
            >
              Продължи
            </button>
          </div>
        );
      case 5:
        return (
          <div className="w-full max-w-2xl mx-auto mt-10 p-6 bg-white shadow rounded-lg">
            <h2 className="text-xl font-semibold text-center mb-4">
              Добавяне на активност
            </h2>
            <div className="w-full h-1 bg-gray-200 rounded mb-6">
              <div className="w-full h-full bg-teal-500 rounded"></div>
            </div>
            <img
              className="picture w-20 h-20 mt-6 mr-auto ml-auto"
              src="./src/assets/like.png"
            />
            <p className="text-center mt-6 text-gray-700">
              Вашата активност беше успешно добавена!
            </p>
            <button
              onClick={() => setStep(1)}
              className="mt-6 w-full max-w-48 ml-[35%] bg-teal-500 text-white py-2 rounded-2xl hover:bg-teal-600"
            >
              Нова активност
            </button>
            {errorMessage && (
              <Popup
                message={errorMessage}
                onClose={() => setErrorMessage(null)}
              />
            )}
          </div>
        );
      default:
        return null;
    }
  };

  return (
    <div className="min-h-screen bg-teal-50 flex items-center justify-center">
      {renderStep()}
    </div>
  );
};

export default CreateActivity;
