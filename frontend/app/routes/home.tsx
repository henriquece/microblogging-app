import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router";

export default function Component() {
  const { data } = useQuery({
    queryKey: ["users"],
    queryFn: async () => {
      const response = await fetch(`${import.meta.env.VITE_API_URL}/users`);

      return await response.json();
    },
  });

  return (
    <div className="flex-1 flex flex-col items-center gap-16 min-h-0">
      <p>Home</p>
      <Link to="/">Indexx</Link>
      <ul>
        {data?.map((user: { name: string }) => (
          <li key={user.name}>{user.name}</li>
        ))}
      </ul>
    </div>
  );
}
