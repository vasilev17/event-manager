import { useState } from "react";
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
      className="w-screen min-h-screen bg-cover bg-center flex items-center justify-center"
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
