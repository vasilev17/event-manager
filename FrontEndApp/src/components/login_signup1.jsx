import { useState } from "react";
import rectangle74 from "../assets/Rectangle 74.png";
import concertPicture from "../assets/concertBackgroung.jpg";
import LoginSignup2 from "./login_signup2";
import UserOrganiserToggle from "./UserOrganizerToggle";

export const LoginSignup1 = ({ setClicked, isOrganizer, setisOrganizer }) => {
  return (
    <div
      className="w-screen  h-screen bg-cover bg-center bg-no-repeat bg-gray-200 -m-8 -mr-8 p-20 "
      style={{ backgroundImage: `url(${concertPicture})` }}
    >
      <div className="relative w-[418px] h-[391px] bg-white m-auto rounded-[42px] shadow-[10px_10px_8.1px_-1px_#00000040]">
        <img
          className="absolute w-[13px] h-[13px] top-[34px] left-[359px] object-cover"
          alt="Rectangle"
          src={rectangle74}
        />

        <p className="absolute w-80 top-24 left-[51px] [font-family:'Noto_Sans-Bold',Helvetica] font-bold text-black text-base text-center tracking-[0] leading-[normal]">
          Влез и открий най-интересните събития около теб!
        </p>

        <UserOrganiserToggle
          isOrganizer={isOrganizer}
          setisOrganizer={setisOrganizer}
        />

        <div className="absolute w-[274px] top-44 left-[75px] [font-family:'Noto_Sans-Medium',Helvetica] font-medium text-black text-base text-center tracking-[0] leading-[normal]">
          Продължи като
        </div>

        <button
          onClick={() => setClicked(true)}
          className="absolute w-[274px] top-80 -left-8 max-w-48 ml-[35%] bg-teal-500 text-white py-2 rounded-2xl hover:bg-teal-600"
        >
          Продължи
        </button>
      </div>
    </div>
  );
};

export default LoginSignup1;
