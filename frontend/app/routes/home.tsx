import { useQuery } from "@tanstack/react-query";

export default function Component() {
  const { data } = useQuery({
    queryKey: ["users"],
    queryFn: async () => {
      const response = await fetch("http://localhost:5100/users");

      return await response.json();
    },
  });

  return (
    <div className="flex-1 flex flex-col items-center gap-16 min-h-0">
      <p>Whatnext?</p>
      <ul>
        {data?.map((user: { name: string }) => (
          <li key={user.name}>{user.name}</li>
        ))}
      </ul>
    </div>
  );
}
