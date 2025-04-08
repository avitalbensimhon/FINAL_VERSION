import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { loginSupplier } from "../servieses/api";
import toast from "react-hot-toast";
import axios from "axios";
const SupplierLogin = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const res = await axios.post("https://localhost:7158/api/Auth/login", { Name: username, Password: password });
      toast.success("Welcome: " + username);
      navigate("/supplier/dashboard");
    } catch (error) {
      toast.error("פרטי התחברות שגויים");
      console.log("שגיאה בהתחברות:", error);
    }
  };

  return (
    <>
      <button onClick={() => navigate(-1)} className="text-blue-600 underline">⬅ חזור</button>
      <div className="container mx-auto p-6">
        <h1 className="text-2xl font-bold mb-4">התחברות ספק</h1>
        <form onSubmit={handleLogin} className="space-y-4">
          <input
            type="text"
            placeholder="שם משתמש"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            className="w-full p-2 border rounded-md"
          />
          <input
            type="password"
            placeholder="סיסמה"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full p-2 border rounded-md"
          />
          <button type="submit" className="w-full bg-blue-500 text-white p-2 rounded-md">
            התחבר
          </button>
        </form>

        <div className="mt-4">
          <p>
            עדיין לא רשום?{" "}
            <a href="/register" className="text-blue-500">
              רישום ספק
            </a>
          </p  >
        </div>
      </div>
    </>
  );
};

export default SupplierLogin;
