import { useParams } from "react-router";

export default function ProfilePage() {
  const params = useParams();
  return (
    <>
      <div className="page-placeholder">
        Profile {params.profileId}
      </div>
    </>
  );
}