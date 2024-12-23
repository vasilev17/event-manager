import { useState } from "react";
import rectangle74 from "../assets/Rectangle 74.png";
import concertPicture from "../assets/concertBackgroung.jpg";
import LoginSignup2 from "./login_signup2";
import { isTokenExpired } from "../api/authUtils";
import { useNavigate } from "react-router";

export const LoginSigninPage = () => {
  const [clicked, setClicked] = useState(false);
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
        <LoginSignup2 />
      ) : (
        <div className="relative w-[418px] h-[391px] bg-white m-auto rounded-[42px] shadow-[10px_10px_8.1px_-1px_#00000040]">
          <img
            className="absolute w-[13px] h-[13px] top-[34px] left-[359px] object-cover"
            alt="Rectangle"
            src={rectangle74}
          />

          <p className="absolute w-80 top-24 left-[51px] [font-family:'Noto_Sans-Bold',Helvetica] font-bold text-black text-base text-center tracking-[0] leading-[normal]">
            Влез и открий най-интересните събития около теб!
          </p>

          <button
            onClick={() => setClicked(true)}
            className="absolute w-[350px] h-[61px] top-[230px] left-9 rounded-[85px] border border-solid border-[#5abab7]"
          >
            <div className="absolute w-[186px] h-[61px] top-0 left-[164px] bg-[#40ddc7] rounded-[85px] border border-solid border-[#5abab7]">
              <div className="left-9 text-white absolute w-[111px] top-5 [font-family:'Noto_Sans-Medium',Helvetica] font-medium text-base text-center tracking-[0] leading-[normal] whitespace-nowrap">
                Организатор
              </div>
            </div>

            <div className="left-7 text-[#28666e] absolute w-[111px] top-5 [font-family:'Noto_Sans-Medium',Helvetica] font-medium text-base text-center tracking-[0] leading-[normal] whitespace-nowrap">
              Потребител
            </div>
          </button>

          <div className="absolute w-[274px] top-44 left-[75px] [font-family:'Noto_Sans-Medium',Helvetica] font-medium text-black text-base text-center tracking-[0] leading-[normal]">
            Продължи като
          </div>
        </div>
      )}
    </div>
  );
};

export default LoginSigninPage;
