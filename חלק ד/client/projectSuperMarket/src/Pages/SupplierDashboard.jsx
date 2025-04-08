import React from "react";
import { useNavigate } from "react-router-dom";

const SupplierDashboard = () => {
    const navigate = useNavigate();

    return (
        <div className="container mx-auto p-6">
            <h1 className="text-3xl font-bold mb-6 text-center">איזור ספק</h1>
            <button onClick={() => navigate(-1)} className="mb-4 text-blue-600 underline">
                ⬅ חזור
            </button>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <button
                    onClick={() => navigate("/admin/order-status")}
                    className="bg-blue-500 text-white p-4 rounded shadow hover:bg-blue-600 transition"
                >
                    📋 צפייה בהזמנות שממתינות
                </button>

                <button
                    onClick={() => navigate("/approve-orders")}
                    className="bg-green-500 text-white p-4 rounded shadow hover:bg-green-600 transition"
                >
                    ✅ אישור הזמנות
                </button>
            </div>
        </div>
    );
};

export default SupplierDashboard;
