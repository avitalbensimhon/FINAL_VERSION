import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import axios from "axios";

export default function AdminLogin() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    console.log({ username, password });
    try {
      const res = await axios.post("https://localhost:7158/api/Auth/login", { Name: username, Password: password });
      localStorage.setItem("adminToken", res.data.token);
      console.log("Token:", res.data.token);

      toast.success("ברוך הבא מנהל!");
      navigate("/admin/home");
    } catch (err) {
      toast.error("פרטי התחברות שגויים");
    }
  };

  return (
    <>
      <button onClick={() => navigate(-1)} className="text-blue-600 underline">⬅ חזור</button>
      <div className="max-w-md mx-auto mt-10 p-4 bg-white shadow rounded">
        <h2 className="text-2xl mb-4">כניסת מנהל</h2>
        <form onSubmit={handleLogin} className="space-y-4">
          <input
            type="text"
            placeholder="שם משתמש"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            className="w-full p-2 border rounded"
          />
          <input
            type="password"
            placeholder="סיסמה"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full p-2 border rounded"
          />
          <button className="w-full bg-blue-500 text-white p-2 rounded">
            התחבר
          </button>
        </form>
      </div>
    </>
  );
}
