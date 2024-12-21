import { BrowserRouter as Router, Routes, Route } from 'react-router';
import HomePage from './pages/HomePage';
import EventsPage from './pages/EventsPage';
import EventPage from './pages/EventPage';
import NotFoundPage from './pages/NotFoundPage';
import ProfilePage from './pages/ProfilePage';
import CartPage from './pages/CartPage';
import CreateActivity from './pages/CreateActivityPage';
import NavigationBar from './components/NavigationMenu/NavigationBar';
import LoginSigninPage from './pages/LoginSinginPage';

export default function App() {
  return (
    <Router>
      <div>
        <NavigationBar />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LoginSigninPage />} />
          <Route path="/events" element={<EventsPage />} />
          <Route path="/events/:eventId" element={<EventPage />} />
          <Route path="/profile/:profileId" element={<ProfilePage />} />
          <Route path="/cart/:cartId" element={<CartPage />} />
          <Route path="/create" element={<CreateActivity />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </div>
    </Router>
  );
}
