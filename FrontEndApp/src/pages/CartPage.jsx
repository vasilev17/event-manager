import { useParams } from "react-router";
import Footer from "../components/Footer";

export default function CartPage() {
  const params = useParams();
  return (
    <>
      <div className="page-placeholder">
        Cart of profile {params.cartId}
      </div>
      <Footer />
    </>
  );
}