import { createBrowserRouter, RouterProvider } from "react-router";
import HomePage from "./pages/HomePage";
import EventsPage from "./pages/EventsPage";
import EventPage from "./pages/EventPage";
import NotFoundPage from "./pages/NotFoundPage";
import ProfilePage from "./pages/ProfilePage";
import CartPage from "./pages/CartPage";
import CreateActivity from "./pages/CreateActivityPage";


export default function App() {
  return (
    <div>
      <RouterProvider router={router} />
    </div>
  );
}

const router = createBrowserRouter([
  {
    path: "/",
    element: <HomePage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/events",
    element: <EventsPage />,
  },
  {
    path: "/events/:eventId",
    element: <EventPage />,
  },
  {
    path: "/events/:eventId",
    element: <EventPage />,
  },
  {
    path: "/profile/:profileId",
    element: <ProfilePage />,
  },
  {
    path: "/cart/:cartId",
    element: <CartPage />,
  },
  {
    path: "/create",
    element: <CreateActivity />,
  },
]);
