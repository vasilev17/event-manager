import { useEffect, useState } from "react";
import TicketSection from "../components/TicketSection";
import { isTokenExpired } from "../api/authUtils";
import LoginSignup1 from "../components/login_signup1";
import { useNavigate } from "react-router";

function ProfilePage() {
  const [isEditingPhone, setIsEditingPhone] = useState(false); // Edit state
  const [isEditingEmail, setIsEditingEmail] = useState(false);
  const [isEditingUsername, setIsEditingUsername] = useState(false);
  const [isEventsVisible, setIsEventsVisible] = useState(true); // Visibility state for events section
  const [phoneNumber, setPhoneNumber] = useState("0888888888"); // Phone number state
  const [emailAddress, setEmailAddress] = useState("example123@gmail.com");
  const [username, setUsername] = useState("потребителско име");
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

  // Toggle editing mode
  const toggleEditUsername = () => {
    setIsEditingUsername(!isEditingUsername);
  };

  const toggleEditEmail = () => {
    setIsEditingEmail(!isEditingEmail);
  };

  const toggleEditPhone = () => {
    setIsEditingPhone(!isEditingPhone);
  };
  // Update the username
  const handleUsernameChange = (e) => {
    setUsername(e.target.value);
  };

  // Update the email address
  const handleEmailChange = (e) => {
    setEmailAddress(e.target.value);
  };

  // Update the phone number
  const handlePhoneChange = (e) => {
    setPhoneNumber(e.target.value);
  };

  // Delete the events section
  const handleDeleteEvents = () => {
    setIsEventsVisible(false);
  };

  return (
    <div className="bg-gray-50 min-h-screen p-8">
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Profile Section */}
        <div className="lg:col-span-1 bg-white rounded-lg shadow-lg p-6">
          <div className="flex flex-col items-center mb-4">
            <div className="bg-teal-400 rounded-full h-24 w-24 flex items-center justify-center">
              {/* Placeholder for Image */}
              <span className="text-white text-5xl mb-2">+</span>
            </div>
            <button className="text-teal-500 mt-2 hover:underline">Edit</button>
          </div>
          <div>
            <h2 className="text-lg font-bold mb-2">Профил</h2>
            <div className="space-y-4 text-sm">
              <div>
                <p className="username text-gray-600">Име</p>
                {isEditingUsername ? (
                  <input
                    type="text"
                    value={username}
                    onChange={handleUsernameChange}
                    className="border rounded p-2 w-full focus:outline-none focus:ring focus:ring-teal-300"
                  />
                ) : (
                  <p className="text-black">{username}</p>
                )}
                <button
                  onClick={toggleEditUsername}
                  className="text-teal-500 hover:underline mt-1"
                >
                  {isEditingUsername ? "Запази" : "Редактиране"}
                </button>
              </div>
              <div>
                <p className="text-gray-600">Email address</p>
                {isEditingEmail ? (
                  <input
                    type="text"
                    value={emailAddress}
                    onChange={handleEmailChange}
                    className="border rounded p-2 w-full focus:outline-none focus:ring focus:ring-teal-300"
                  />
                ) : (
                  <p className="text-black">{emailAddress}</p>
                )}
                <button
                  onClick={toggleEditEmail}
                  className="text-teal-500 hover:underline mt-1"
                >
                  {isEditingEmail ? "Запази" : "Редактиране"}
                </button>
              </div>
              <div>
                <p className="text-gray-600">Телефонен номер</p>
                {isEditingPhone ? (
                  <input
                    type="text"
                    value={phoneNumber}
                    onChange={handlePhoneChange}
                    className="border rounded p-2 w-full focus:outline-none focus:ring focus:ring-teal-300"
                  />
                ) : (
                  <p className="text-black">{phoneNumber}</p>
                )}
                <button
                  onClick={toggleEditPhone}
                  className="text-teal-500 hover:underline mt-1"
                >
                  {isEditingPhone ? "Запази" : "Редактиране"}
                </button>
              </div>
            </div>
            <div className="flex flex-col mt-4 space-y-2 text-sm">
              <a href="#" className="text-teal-500 hover:underline">
                Смяна на парола
              </a>
              <a href="#" className="text-red-500 hover:underline">
                Изход
              </a>
              <a href="#" className="text-red-500 hover:underline">
                Изтриване на профил
              </a>
            </div>
          </div>
        </div>

        {/* Tickets Section */}

        <div className="lg:col-span-2">
          <h2 className="text-xl font-bold mb-4">Моите билети</h2>
          <TicketSection />

          {/* Events Section */}
          {isEventsVisible && (
            <div>
              <h2 className="text-xl font-bold mt-8 mb-4">Моите събития</h2>
              <div className="bg-white rounded-lg shadow-lg p-6">
                <div className="flex justify-between">
                  <div>
                    <p className="font-semibold">Име на събитие</p>
                    <p className="text-sm text-gray-500">Дата</p>
                  </div>
                  <button
                    onClick={handleDeleteEvents}
                    className="text-red-500 hover:underline mt-2"
                  >
                    Изтриване
                  </button>
                </div>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default ProfilePage;
