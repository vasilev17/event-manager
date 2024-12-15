import defaultImage from "../assets/default.jpg";

export default function Card({
  picture = defaultImage,
  title = "Event",
  description = "Description",
}) {
  const trimmedTitle =
    title.length > 30 ? title.substring(0, 30) + "..." : title;

  const trimmedDescription =
    description.length > 30
      ? description.substring(0, 30) + "..."
      : description;

  return (
    <div className="flex flex-col max-w-96 hover:shadow-black/50 shadow-md rounded-3xl ">
      <img
        src={picture}
        alt="picture"
        className="h-32 sm:h-48 w-full object-cover rounded-t-3xl"
      ></img>
      <div className="p-4 pb-5 -mt-6 max-h-[92px] rounded-3xl bg-primary w-full">
        <span className="font-bold text-xl text-white">{trimmedTitle}</span>
        <span className="block text-xl font-semibold text-white">
          {trimmedDescription}
        </span>
      </div>
    </div>
  );
}
