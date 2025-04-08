import React from "react";
import { useNavigate } from "react-router-dom";
import '../assets/Styles/global.css'
export default function AdminHome() {
  const navigate = useNavigate();

  return (
    <>
      <button onClick={() => navigate(-1)} className="text-blue-600 underline">⬅ חזור</button>

      <div className="flex justify-center items-center min-h-screen bg-gray-100">
        <div className="w-full max-w-3xl p-8 bg-white rounded-2xl shadow-xl text-center space-y-6">
          <h1 className="text-3xl font-bold mb-6 text-blue-700">ברוך הבא למערכת הניהול</h1>


          <div className="space-y-4">
            <button
              onClick={() => navigate("/admin/order-goods")}
              className="w-full p-3 bg-green-500 hover:bg-green-600 text-white text-lg font-semibold rounded-xl transition"
            >
              📦 הזמנת סחורה מספק
            </button>

            <button
              onClick={() => navigate("/admin/order-status")}
              className="w-full p-3 bg-yellow-500 hover:bg-yellow-600 text-white text-lg font-semibold rounded-xl transition"
            >
              📊 צפייה בסטטוס הזמנות קיימות
            </button>

            <button
              onClick={() => navigate("/admin/approve-orders")}
              className="w-full p-3 bg-blue-500 hover:bg-blue-600 text-white text-lg font-semibold rounded-xl transition"
            >
              ✅ אישור קבלת הזמנה
            </button>

            <button
              onClick={() => navigate("/admin/all-orders")}
              className="w-full p-3 bg-purple-500 hover:bg-purple-600 text-white text-lg font-semibold rounded-xl transition"
            >
              📚 מאגר כל ההזמנות
            </button>
            <button
              onClick={() => navigate("/suppliers")}
              className="w-full p-3 bg-purple-500 hover:bg-purple-600 text-white text-lg font-semibold rounded-xl transition"
            >
              📚 מאגר כל הספקים
            </button>
            <button
              onClick={() => navigate("/admin-box-office")}
              className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 mb-4"
            >
              ניהול קופות
            </button>
          </div>
        </div>
      </div>
    </>
  );
}
