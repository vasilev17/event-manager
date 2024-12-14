import defaultImage from "../assets/default.jpg";

export default function Card({
  picture = defaultImage,
  title = "Event",
  description = "Description",
}) {
  const trimmedTitle =
    title.length > 25 ? title.substring(0, 30) + "..." : title;

  const trimmedDescription =
    description.length > 50
      ? description.substring(0, 50) + "..."
      : description;

  return (
    <div className="relative max-w-96">
      <div className="rounded-t-3xl bg-primary2 shadow-md overflow-hidden">
        <img
          src={picture}
          alt="picture"
          className="h-32 sm:h-48 w-full object-cover"
        ></img>
      </div>
      <div className="p-4 pb-5 -mt-6 rounded-3xl bg-primary absolute w-full">
        <span className="font-bold text-xl text-white">{trimmedTitle}</span>
        <span className="block text-xl font-semibold text-white">
          {trimmedDescription}
        </span>
      </div>
    </div>
  );
}
