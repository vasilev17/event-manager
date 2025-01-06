import { useState } from "react";
import rectangle74 from "../assets/Rectangle 74.png";
import concertPicture from "../assets/concertBackgroung.jpg";
import LoginSignup2 from "../components/login_signup2";
import { isTokenExpired } from "../api/authUtils";
import { useNavigate } from "react-router";
import UserOrganiserToggle from "../components/UserOrganizerToggle";
import LoginSignup1 from "../components/login_signup1";

export const LoginSigninPage = () => {
  const [clicked, setClicked] = useState(false);
  const [isOrganizer, setisOrganizer] = useState(false);
  const navigate = useNavigate();

  if (!isTokenExpired()) {
    navigate("profile/" + getUserData().name);
  }
  return (
    <div
      className="w-screen  h-screen bg-cover bg-center bg-no-repeat bg-gray-200 -m-8 -mr-8 p-20 "
      style={{ backgroundImage: `url(${concertPicture})` }}
    >
      {clicked ? (
        <LoginSignup2 isOrganizer={isOrganizer} />
      ) : (
        <LoginSignup1
          setClicked={setClicked}
          isOrganizer={isOrganizer}
          setisOrganizer={setisOrganizer}
        />
      )}
    </div>
  );
};

export default LoginSigninPage;
