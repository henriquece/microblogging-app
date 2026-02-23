import { useState } from "react";
import { Link } from "react-router";

export default function Component() {
  const [userNameValue, setUserNameValue] = useState("");

  const [passwordValue, setPasswordValue] = useState("");

  return (
    <div>
      <p>Login</p>
      <p style={{ marginTop: 64}}>User</p>
      <input
        value={userNameValue}
        onChange={(e) => setUserNameValue(e.target.value)}
      />
      <p>Password</p>
      <input
        value={passwordValue}
        onChange={(e) => setPasswordValue(e.target.value)}
      />
       <button
          style={{ display: "block", marginTop: 32 }}
          // onClick={() => {
          //   postMutation.mutate({ newPostValue });
          // }}
        >
          Login
        </button>
      <p style={{ marginTop: 64}}>
        <Link to="/home">Home</Link>
      </p>
    </div>
  );
}
