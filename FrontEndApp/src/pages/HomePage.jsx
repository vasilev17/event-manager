import Footer from "../components/Footer";
import MailAbonament from "../components/MailAbonament";
import CardsRow from "../components/CardsRow";
import pictureFromAssetsFolder from "../assets/pictureFromAssetsFolder.jpg";
import PlaceAndDateSearch from "../components/PlaceAndDateSearch";

export default function HomePage() {
  const hardCodedPictureLink =
    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%3Fid%3DOIP.rbVMAX7i5iH4MrkykUiy9gHaEK%26pid%3DApi&f=1&ipt=58261e1cbfc98b5409839b963a42f3f62be6efc4851a24b84d4ab3730c5f3a97&ipo=images";

  return (
    <div>
      <PlaceAndDateSearch />

      <CardsRow
        mainTitle="Концерти"
        card1Picture={hardCodedPictureLink}
        card1Title="Заглавие 1"
        card1Description="Кратко описание 1 което не e толкова кратко свъшност, за това ще се отреже края му"
        card2Picture={hardCodedPictureLink}
        card2Title="Заглавие 2"
        card2Description="Кратко описание 2"
        card3Picture={pictureFromAssetsFolder}
        card3Title="Заглавие 3"
        card3Description="Кратко описание 3"
      />
      <CardsRow
        mainTitle="Туризъм"
        card1Picture={hardCodedPictureLink}
        card1Title="Заглавие 1"
        card1Description="Кратко описание 2 което не e толкова кратко свъшност, за това ще се отреже края му"
        card2Picture={hardCodedPictureLink}
        card2Title="Заглавие 2"
        card2Description="Кратко описание 2"
        card3Picture={pictureFromAssetsFolder}
        card3Title="Заглавие 3"
        card3Description="Кратко описание 3"
      />
      <MailAbonament />
      <Footer />
    </div>
  );
}
