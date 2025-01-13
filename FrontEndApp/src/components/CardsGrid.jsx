import Card from "./Card";
import pictureFromAssetsFolder from "../assets/pictureFromAssetsFolder.jpg";

export default function CardGrid() {
  return (
    <div className="grid grid-cols-4 gap-x-6 gap-y-24 mt-20 px-10 md:px-24 justify-items-center">
      <div className="justify-self-start col-span-2 self-end text-black text-5xl font-semibold font-sans">
        Най-популярни
      </div>
      <div />
      <button className="w-44 h-[71px] bg-secondary2 rounded-full justify-self-center -mb-3 p-6 flex gap-2 text-center text-black text-2xl  font-semibold font-sans leading-none shadow-black/30 shadow-md hover:bg-secondary hover:text-white transition-all ease-out duration-100">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          strokeWidth="1.5"
          stroke="currentColor"
          className="size-6"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M10.5 6h9.75M10.5 6a1.5 1.5 0 1 1-3 0m3 0a1.5 1.5 0 1 0-3 0M3.75 6H7.5m3 12h9.75m-9.75 0a1.5 1.5 0 0 1-3 0m3 0a1.5 1.5 0 0 0-3 0m-3.75 0H7.5m9-6h3.75m-3.75 0a1.5 1.5 0 0 1-3 0m3 0a1.5 1.5 0 0 0-3 0m-9.75 0h9.75"
          />
        </svg>
        Филтри
      </button>
      <Card
        picture={pictureFromAssetsFolder}
        title="Заглавие 1"
        description="Кратко описание 1"
      />
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
      <button className="w-full max-w-96 h-20 pb-1 mb-44 mt-16 col-start-2 col-span-2 bg-secondary rounded-full text-white font-sans text-xl font-semibold shadow-black/30 shadow-md">
        Покажи Още
      </button>
    </div>
  );
}
