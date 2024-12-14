import Card from "./Card";

export default function CardsRow({
  mainTitle,
  card1Picture,
  card1Title,
  card1Description,
  card2Picture,
  card2Title,
  card2Description,
  card3Picture,
  card3Title,
  card3Description,
}) {
  return (
    <div className="grid grid-cols-3 px-10 md:px-24 gap-20 justify-items-center self-end">
      <div className="justify-self-start self-end  text-black text-4xl font-semibold font-sans">
        {mainTitle}
      </div>
      <div />
      <div />
      <Card
        picture={card1Picture}
        title={card1Title}
        description={card1Description}
      />
      <Card
        picture={card2Picture}
        title={card2Title}
        description={card2Description}
      />
      <Card
        picture={card3Picture}
        title={card3Title}
        description={card3Description}
      />
      <button className="w-full max-w-96 h-20 pb-1 mb-44 mt-16 col-start-2 bg-secondary rounded-full text-white font-sans text-xl font-semibold">
        Покажи Още
      </button>
    </div>
  );
}
