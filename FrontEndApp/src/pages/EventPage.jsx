import { useParams } from "react-router";
import Footer from "../components/Footer";
import MailAbonament from "../components/MailAbonament";

export default function EventPage() {
  const params = useParams();
  return (
    <>
      <div className="page-placeholder">
        Event {params.eventId}
      </div>
      <MailAbonament />
      <Footer />
    </>
  );
}
