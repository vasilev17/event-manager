import { useParams } from "react-router";
import Footer from "../components/Footer";
import MailAbonament from "../components/MailAbonament";
import Card from "../components/Card";
import { useRef } from "react";
import concert from "../assets/concert.jpg"

export default function EventPage() {
  const params = useParams();

  const scrollContainerRef = useRef(null);

  const scrollLeft = () => {
    if (scrollContainerRef.current) {
      scrollContainerRef.current.scrollBy({
        left: -300, 
        behavior: "smooth",
      });
    }
  };

  const scrollRight = () => {
    if (scrollContainerRef.current) {
      scrollContainerRef.current.scrollBy({
        left: 300, 
        behavior: "smooth",
      });
    }
  };

  return (
    <>
      <div className="bg-white">
        {/* Event Header Section */}
        <div className="relative">
          <img
            src={concert} 
            alt="Event"
            className="w-full h-[300px] object-cover"
          />
        </div>

        {/* Content Section */}
        <div className="container mx-auto px-16 py-8">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
            {/* Left Column */}
            <div>
              <h1 className="text-3xl font-bold mb-4">
                Заглавие на събитието
              </h1>
              <p className="text-gray-600 leading-relaxed">
                Дълго описание на събитието, състоящо се от това къде се намира, на кой ден е, от колко часа е. 
                Tincidunt massa lorem mattis bibendum sed lacus lacus. Scelerisque ornare lorem
                et varius. Eu in arcu eget massa arcu. Proin eget facilisis et
                elit et luctus. Rhoncus eget nam tempus suscipit mattis.
              </p>
            </div>

            {/* Right Column - Map */}
            <div>
              <h2 className="text-2xl font-bold mb-4">Локация на събитието</h2>
              <div className="aspect-w-16 aspect-h-9">
                <iframe
                  title="Event Location"
                  className="w-full h-full border-none"
                  allowFullScreen=""
                  loading="lazy"
                ></iframe>
              </div>
            </div>
          </div>

          {/* Info Sections */}
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-8 mt-12">
            <div className="bg-gray-100 p-6 rounded-md">
              <h3 className="text-xl font-semibold mb-2">
                Lorem ipsum odor amet
              </h3>
              <p className="text-gray-600">
                Lorem ipsum odor amet, consectetur adipiscing elit.
                 Ornare et dignissim ornare pellentesque id egestas.
                  Tincidunt massa lorem mattis bibendum sed lacus lacus.
                   Scelerisque ornare lorem et varius. Eu in arcu eget massa arcu.
                    Proin eget facilisis et elit et luctus. Rhoncus eget nam tempus suscipit mattis.
              </p>
            </div>
            <div className="bg-gray-100 p-6 rounded-md">
              <h3 className="text-xl font-semibold mb-2">
                Lorem ipsum odor amet
              </h3>
              <p className="text-gray-600">
                Lorem ipsum odor amet, consectetur adipiscing.
              </p>
            </div>
          </div>
        </div>

        {/* Recent Searches */}
        <div className="bg-gray-100 py-8 mt-12 px-12 relative">
          <div className="container mx-auto px-4">
            <h2 className="text-2xl font-bold mb-6">Последно търсени</h2>

            {/* Arrows */}
            <button
              onClick={scrollLeft}
              className="absolute left-4 top-1/2 -translate-y-1/2 bg-white p-2 rounded-full shadow-md hover:bg-gray-200 focus:outline-none z-10"
            >
              ◀
            </button>
            <button
              onClick={scrollRight}
              className="absolute right-4 top-1/2 -translate-y-1/2 bg-white p-2 rounded-full shadow-md hover:bg-gray-200 focus:outline-none z-10"
            >
              ▶
            </button>

            {/* Sliding Content */}
            <div
              ref={scrollContainerRef}
              className="flex flex-shrink-0 space-x-4 overflow-x-auto no-scrollbar"
            >
              <Card />
              <Card />
              <Card />
              <Card />
              <Card />
              
            </div>
          </div>
        </div>
      </div>

      <MailAbonament />
      <Footer />
    </>
  );
}


