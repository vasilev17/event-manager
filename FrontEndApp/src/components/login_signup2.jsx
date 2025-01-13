import { useState } from "react";
import ellipse31 from "../assets/Ellipse 31.svg";
import ellipse32 from "../assets/Ellipse 32.png";
import ellipse33 from "../assets/Ellipse 33.png";
import rectangle74 from "../assets/Rectangle 74.png";
import { api, storeUserData } from "../api/authUtils.js";
import { useNavigate } from "react-router";
import Popup from "./PopUp.jsx";

export const LoginSignup2 = ({ isOrganizer }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [errorMessage, setErrorMessage] = useState(null);
  const navigate = useNavigate();

  function handleLogin() {
    const requestBody = {
      userName: username,
      password: password,
    };
    api
      .post("User/Login", requestBody)
      .then((response) => {
        const token = response.data.token;
        storeUserData(token, username, email); // Store the token
        navigate("/profile/" + username);
      })
      .catch((error) => {
        console.log(error);
        setErrorMessage("Wrong username or password");
      });
  }

  function handleSingin() {
    const requestBody = {
      userName: username,
      password: password,
      firstName: "First",
      lastName: "Last",
      email: email,
      role: isOrganizer ? "Organizer" : "User",
    };
    api
      .post("User/Register", requestBody)
      .then((response) => {
        const token = response.data.token;
        storeUserData(token, username, email); // Store the token
        navigate("/profile/" + username);
      })
      .catch((error) => {
        console.log(error);
        setErrorMessage("Something went wrong with registration attempt");
      });
  }

  return (
    <div className="relative flex place-content-center w-[519px] h-[450px] m-auto bg-white rounded-[42px] shadow-[10px_10px_8.1px_-1px_#00000040]">
      <div
        className="absolute w-px h-[298px] top-[60px] left-[253px] object-cover bg-black"
        alt="Line"
      />

      <div className="w-[126px] top-11 left-[58px] [font-family:'Kode_Mono-Bold',Helvetica] text-black text-xl text-center absolute font-bold tracking-[0] leading-[normal]">
        Вход
      </div>

      <div className="w-[138px] top-11 left-[310px] [font-family:'Kode_Mono-Bold',Helvetica] text-black text-xl text-center absolute font-bold tracking-[0] leading-[normal]">
        Регистрация
      </div>

      <input
        onChange={(e) => setUsername(e.target.value)}
        type="text"
        placeholder="Въведете потребителско име"
        className="top-[110px] left-[31px] input-login"
      />

      <input
        onChange={(e) => setUsername(e.target.value)}
        type="text"
        placeholder="Въведете потребителско име"
        className="top-[110px] left-72 input-login "
      />

      <input
        onChange={(e) => setPassword(e.target.value)}
        type="text"
        placeholder="Въведете парола"
        className=" top-44 left-[31px] input-login "
      />

      <input
        onChange={(e) => setEmail(e.target.value)}
        type="text"
        placeholder="Въведете email адрес"
        className=" top-44 left-72 input-login "
      />

      <input
        onChange={(e) => setPassword(e.target.value)}
        type="text"
        placeholder="Въведете парола"
        className=" top-[243px] left-72 input-login "
      />

      <div className="w-[131px] top-[87px] left-[31px] [font-family:'Segoe_UI-Bold',Helvetica] font-bold text-black text-xs text-center whitespace-nowrap absolute tracking-[0] leading-[normal]">
        Потребителско име
      </div>

      <div className="w-[68px] top-[86px] left-[291px] [font-family:'Segoe_UI-Bold',Helvetica] text-black text-xs whitespace-nowrap absolute font-bold tracking-[0] leading-[normal]">
        Имена
      </div>

      <div className="w-16 top-[157px] left-[31px] [font-family:'Segoe_UI-Bold',Helvetica] text-black text-xs text-center whitespace-nowrap absolute font-bold tracking-[0] leading-[normal]">
        Парола
      </div>

      <button
        onClick={handleLogin}
        className="absolute w-[106px] h-[37px] top-[254px] left-[66px] bg-[#5abab7]  hover:bg-teal-600 rounded-[41px]"
      >
        <div className="w-[62px] top-2.5 left-[22px] [font-family:'Segoe_UI-Bold',Helvetica] text-white text-sm text-center whitespace-nowrap absolute font-bold tracking-[0] leading-[normal]">
          Влизане
        </div>
      </button>

      <button
        onClick={handleSingin}
        className="absolute w-[116px] h-[37px] top-[341px] left-[327px] bg-[#5abab7] hover:bg-teal-600 rounded-[41px]"
      >
        <div className="w-[85px] top-[10px] left-[15px]  [font-family:'Segoe_UI-Bold',Helvetica] text-white text-[13px] text-center absolute font-bold tracking-[0] leading-[normal]">
          Регистрация
        </div>
      </button>

      <img
        className="absolute w-[22px] h-[23px] top-[400px] left-[169px] object-cover"
        alt="Ellipse"
        src={ellipse31}
      />

      <img
        className="absolute w-[22px] h-[23px] top-[400px] left-[242px] object-cover"
        alt="Ellipse"
        src={ellipse32}
      />

      <img
        className="absolute w-[22px] h-[23px] top-[401px] left-[316px]"
        alt="Ellipse"
        src={ellipse33}
      />

      <div className="w-[129px] top-[302px] left-[57px] [font-family:'Segoe_UI-Bold',Helvetica] font-bold text-black text-xs text-center absolute tracking-[0] leading-[normal]">
        Забравена парола?
      </div>

      <div className="absolute w-[110px] top-[154px] left-[291px] [font-family:'Segoe_UI-Bold',Helvetica] font-bold text-black text-xs tracking-[0] leading-[normal] whitespace-nowrap">
        Email адрес
      </div>

      <div className="w-[68px] top-[220px] left-[294px] [font-family:'Segoe_UI-Bold',Helvetica] text-black text-xs whitespace-nowrap absolute font-bold tracking-[0] leading-[normal]">
        Парола
      </div>

      <p className="absolute w-[174px] top-[290px] left-[297px] [font-family:'Segoe_UI-Regular',Helvetica] font-normal text-[#929292] text-[8px] tracking-[0] leading-[normal]">
        * Паролата трябва да съдържа поне 8 символа и да съдържа поне една
        цифра, поне една малка буква и поне една главна буква
      </p>

      <img
        className="absolute w-[11px] h-[11px] top-6 left-[471px] object-cover"
        alt="Rectangle"
        src={rectangle74}
      />

      {errorMessage && (
        <Popup message={errorMessage} onClose={() => setErrorMessage(null)} />
      )}
    </div>
  );
};

export default LoginSignup2;
