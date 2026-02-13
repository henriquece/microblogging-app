import { Link } from "react-router";

export default function Component() {
  return (
    <div className="flex-1 flex flex-col items-center gap-16 min-h-0">
      <p>Indexx</p>
      <Link to="/home">Home</Link>
    </div>
  );
}
