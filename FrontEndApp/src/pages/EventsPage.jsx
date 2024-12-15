import CardsGrid from "../components/CardsGrid";
import CategotyPlaceDateSearch from "../components/CategoryPlaceDateSearch";
import Footer from "../components/Footer";
import MailAbonament from "../components/MailAbonament";

export default function EventsPage() {
  return (
    <>
      <CategotyPlaceDateSearch />
      <CardsGrid />
      <MailAbonament />
      <Footer />
    </>
  );
}
