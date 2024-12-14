import Card from "./Card";
import pictureFromAssetsFolder from "../assets/pictureFromAssetsFolder.jpg";

export default function CardGrid() {
  return (
    <div className="grid grid-cols-4 gap-x-6 gap-y-24 mt-20 px-10 md:px-24 justify-items-center">
      <div className="justify-self-start col-span-2 self-end text-black text-4xl font-semibold font-sans">
        Най-популярни
      </div>
      <div />
      <button></button>
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
      <button className="w-full max-w-96 h-20 pb-1 mb-44 mt-16 col-start-2 col-span-2 bg-secondary rounded-full text-white font-sans text-xl font-semibold">
        Покажи Още
      </button>
    </div>
  );
}
