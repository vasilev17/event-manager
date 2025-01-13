import { Link } from "react-router";

export default function NotFoundPage() {
  return (
    <div className="p-12">
      <h1 className="text-5xl mb-16 pl-2">404 Page Not Found</h1>

      <Link
        to={"/"}
        className="text-5xl border border-5 rounded-full px-8 pt-4 pb-6 bg-primary text-white "
      >
        Home
      </Link>
    </div>
  );
}
