import React from "react";
import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import { getAllProducts } from "../servieses/api";

export default function Header() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();
  const j = getAllProducts().then((response) => {
    console.log(response.data);
  })

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <header className="bg-blue-600 text-white py-4 px-6 flex justify-between items-center shadow">
      <Link to="/admin/login" className="text-blue-500">כניסת מנהל</Link>

      <h1 className="text-2xl font-bold">🛒מערכת ניהול מכולת</h1>
      {user ? (
        <div className="flex items-center gap-4">
          <span className="text-sm">שלום, {user.companyName}</span>
          <button
            onClick={handleLogout}
            className="bg-white text-blue-600 px-3 py-1 rounded hover:bg-gray-100 transition"
          >
            התנתק
          </button>
        </div>
      ) : null}
    </header>
  );
}
