import paintPicture from "../assets/paintEventBackground.jpg";

export default function CategotyPlaceDateSearch() {
  return (
    <div>
      <div
        className="bg-cover bg-center bg-no-repeat w-full h-96 flex flex-col items-center justify-center"
        style={{
          backgroundImage: `url(${paintPicture})`,
        }}
      >
        <div className="flex flex-col items-center justify-center w-full h-48 mt-4 bg-white/60 shadow-black/30 shadow-md ">
          <div className=" text-center text-secondary text-5xl font-bold tracking-widest mb-2 ">
            Всички събития
          </div>
        </div>
      </div>
      <div className="w-full h-36 bg-secondary px-36 gap-24 shadow-black/30 shadow-md flex items-center justify-center">
        <button className="event-category-button">
          КАТЕГОРИЯ
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
              d="m19.5 8.25-7.5 7.5-7.5-7.5"
            />
          </svg>
        </button>
        <button className="event-category-button">
          МЯСТО
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
              d="m19.5 8.25-7.5 7.5-7.5-7.5"
            />
          </svg>
        </button>
        <button className="event-category-button">
            ДАТА
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
              d="m19.5 8.25-7.5 7.5-7.5-7.5"
            />
          </svg>
        </button>
      </div>
    </div>
  );
}
