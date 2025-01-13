import { useState } from "react";
import group21 from "../assets/Group 21.svg";
import Checkbox from "./CheckBox";

export default function MailAbonament() {
  const [email, setEmail] = useState("");
  function handleClick() {
    setEmail("");
    console.log(email);
  }
  return (
    <div className="bg-secondary2 p-10 md:p-24 h-auto grid grid-cols-5 relative over">
      <div className="col-span-2">
        <div className="text-black text-4xl font-semibold font-sans mb-6">
          Абонирайте се за новини
          <br />
        </div>
        <div className="text-secondary text-xl font-normal font-sans w-full">
          Абонирайте се за нашия бюлетин и получавайте информация за предстоящи
          събития и специални оферти директно във вашата поща.
        </div>
      </div>

      <div className="col-span-2">
        <input
          onChange={(e) => setEmail(e.target.value)}
          type="text"
          placeholder="Въведете имейл адрес..."
          className="w-full h-16 p-6 pb-7 bg-white rounded-full border border-gray-400 focus:outline-none font-sans placeholder:text-black/30 text-xl font-semibold"
        />
        <div className="flex pl-4 mt-6">
          <Checkbox />
          <div className="">
            <span className="text-black font-sans mr-1">
              Желая да се регистрирам за бюлетин и съм съгласен предоставената
              от мен информация да се обработва съобразно
            </span>
            <span className="text-black font-sans underline">
              политиката за поверителност на данните
            </span>
            <span className="text-black font-sans">.</span>
          </div>
        </div>
      </div>

      <div className="pl-6 pt-1">
        <button
          onClick={handleClick}
          className="w-full h-14 pb-1 bg-secondary rounded-full text-white font-sans text-xl font-semibold hover:shadow-black/30 hover:shadow-md transition-all ease-out duration-100"
        >
          Регистрирам се
        </button>
      </div>
      <img
        className=" absolute w-48 aspect-square -bottom-16 right-0 "
        alt="group"
        src={group21}
      />
    </div>
  );
}
