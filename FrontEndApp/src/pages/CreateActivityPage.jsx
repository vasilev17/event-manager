import { useEffect, useState } from "react";
import Popup from "../components/PopUp";
import { api, getToken, isTokenExpired } from "../api/authUtils.js";
import Dropdown from "../components/Dropdown.jsx";
import Toggle from "../components/Toggle.jsx";
import { useNavigate } from "react-router";
import LoginSignup1 from "../components/login_signup1.jsx";

const CreateActivity = () => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [address, setAddress] = useState("");
  const [timeStart, setTimeStart] = useState("");
  const [timeEnd, setTimeEnd] = useState("");
  const [webpage, setWebpage] = useState("");
  const [isActivity, setIsActivity] = useState(false);
  const [isThirdParty, setIsThirdParty] = useState(false);
  const [types, setTypes] = useState([]);
  const [picture, setPicture] = useState(null);
  const [errorMessage, setErrorMessage] = useState(null);
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

  function emptyForm() {
    setName("");
    setDescription("");
    setAddress("");
    setTimeStart("");
    setTimeEnd("");
    setWebpage("");
    setIsActivity(false);
    setIsThirdParty(false);
    setTypes([]);
    setPicture(null);
  }

  const handleCreateEvent = async () => {
    const formData = new FormData();

    // Validate and format timeStart and timeEnd
    const formattedTimeStart = formatDateTime(timeStart);
    const formattedTimeEnd = formatDateTime(timeEnd);

    if (!formattedTimeStart || !formattedTimeEnd) {
      setErrorMessage(
        "Invalid date and time format. Please use DD.MM.YYYY HH:MM."
      );
      return;
    }

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
          Authorization: "Bearer ${getToken()}", // if you need authorization
        },
      });
      console.log("Event created:", response.data);
      emptyForm();
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

    nextStep();
  };

  const nextStep = () => {
    setTimeStart("20.01.2025 12:00");
    setTimeEnd("20.01.2025 14:00");
    setAddress("sofia");
    setTypes(["Convention, Conference"]);

    setStep(step + 1);
  };

  const formatDateTime = (dateTimeString) => {
    // Validate input format (DD.MM.YYYY HH:mm)
    const dateRegex = /^\d{2}\.\d{2}\.\d{4}$/;
    const timeRegex = /^\d{2}:\d{2}$/;
    const parts = dateTimeString.split(" ");

    if (
      parts.length !== 2 ||
      !dateRegex.test(parts[0]) ||
      !timeRegex.test(parts[1])
    ) {
      return null; // Invalid format
    }

    const [day, month, year] = parts[0].split(".").map(Number);
    const [hours, minutes] = parts[1].split(":").map(Number);

    // Create a Date object
    const date = new Date(year, month - 1, day, hours, minutes, 0, 0); // Months are 0-indexed

    // Convert to ISO 8601 format (YYYY-MM-DDTHH:mm:ssZ)
    return date.toISOString();
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
              placeholder="20.01.2025 12:00"
              className="w-full px-4 py-2 mb-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-teal-500"
            />
            <label className="block text-gray-700 font-medium mb-2">
              Време на приключване
            </label>
            <input
              type="text"
              value={timeEnd}
              onChange={(e) => setTimeEnd(e.target.value)}
              placeholder="20.01.2025 14:00"
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
            <div>
              <button
                onClick={nextStep}
                className=" w-full max-w-48 ml-[35%] bg-teal-500 text-white py-2 rounded-2xl hover:bg-teal-600"
              >
                Продължи
              </button>
            </div>
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
            <div
              className=" m-auto w-96 h-60 flex flex-col justify-center border-2 border-dashed border-gray-300 bg-teal-50 rounded-lg"
              onDragOver={(e) => {
                e.preventDefault();
                e.stopPropagation();
                e.dataTransfer.dropEffect = "copy"; // Optional: Indicate to the browser that the drop operation is a copy
              }}
              onDrop={(event) => {
                event.preventDefault();
                event.stopPropagation();
                const file = event.dataTransfer.files[0];

                if (!file.type.startsWith("image/")) {
                  alert("Please drop an image file.");
                  return;
                }

                const reader = new FileReader();
                reader.onload = (e) => {
                  setPicture(e.target.result);
                };
                reader.readAsDataURL(file);
              }}
            >
              {picture ? (
                <img
                  className="object-cover w-full h-full rounded-lg" // Use object-cover for image scaling
                  src={picture}
                  alt="Uploaded Picture"
                />
              ) : (
                <div className="flex flex-col items-center justify-center">
                  {" "}
                  <span className="text-teal-500 text-lg mb-2">
                    Качване на снимка
                  </span>
                  <p className="text-sm text-gray-500">Поставете снимка тук</p>
                </div>
              )}
            </div>
            <button
              onClick={handleCreateEvent}
              className="mt-6 w-full max-w-48 ml-[35%] bg-teal-500 text-white py-2 rounded-2xl hover:bg-teal-600"
            >
              Създай събитие
            </button>
          </div>
        );
      case 5:
        if (errorMessage) {
          return (
            <Popup
              message={errorMessage}
              onClose={() => {
                setErrorMessage(null), setStep(1);
              }}
            />
          );
        } else {
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
        }

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
