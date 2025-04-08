import React, { useEffect, useState } from "react";
import axios from "axios";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";


export default function ApproveOrders() {
  const [orders, setOrders] = useState([]);
  const navigate = useNavigate();
  const { user } = useAuth();

  useEffect(() => {
    fetchPendingOrders();
  }, []);

  const fetchPendingOrders = async () => {
    try {
      let res;

      if (user) {
        const supplierRes = await axios.get(`https://localhost:7158/api/Orders/supplier/${user.id}`);
        const supplierPending = supplierRes.data.filter((o) => o.statusOrders === 0);
        setOrders(supplierPending);
      } else {
        const token = localStorage.getItem("adminToken");
        res = await axios.get("https://localhost:7158/api/Orders", {
          headers: { Authorization: `Bearer ${token}` },
        });
        const adminOrders = res.data.filter((o) => [1].includes(o.statusOrders));
        setOrders(adminOrders);
      }
    } catch (err) {
      toast.error("שגיאה בטעינת ההזמנות");
    }
  };

  const approveOrder = async (orderId, currentStatus) => {
    try {
      const newStatus = user ? 1 : currentStatus === 0 ? 1 : 2;

      const headers = user
        ? { "Content-Type": "application/json" }
        : {
          Authorization: `Bearer ${localStorage.getItem("adminToken")}`,
          "Content-Type": "application/json",
        };

      await axios.put(
        `https://localhost:7158/api/Orders/updateStatus/${orderId}`,
        JSON.stringify(newStatus),
        { headers }
      );

      toast.success("ההזמנה עודכנה בהצלחה!");
      setOrders((prev) => prev.filter((order) => order.id !== orderId));
    } catch (err) {
      toast.error("שגיאה בעדכון ההזמנה");
      console.error("Error:", err);
    }
  };

  return (
    <div className="max-w-4xl mx-auto mt-10 p-6 bg-white shadow-lg rounded-xl">
      <h2 className="text-2xl font-bold mb-6 text-blue-700">
        {user ? "אישור הזמנות כספק" : "אישור קבלת הזמנות"}
      </h2>

      <button onClick={() => navigate(-1)} className="mb-4 text-blue-600 underline">
        ⬅ חזור
      </button>

      {orders.length === 0 ? (
        <>
          < div id="giphy">
            <p className="text-gray-600">אין כרגע הזמנות לאישור.</p>
            <img src="https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExOGZxdnl1NXcwbmE0OThxeTV0bmJuNWNrcjR2NG8xazBvOGk1Zjc1byZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9ZQ/IzcFv6WJ4310bDeGjo/giphy.gif" alt="פרצוף בוכה" style={{ width: "150px" }} />

          </div></>
      ) : (
        <div id="orders">
          <ul className="space-y-4" id="order-card">
            {orders.map((order) => (
              <li
                key={order.id}
                className="flex justify-between items-center p-4 border rounded-xl shadow text-right"
              >
                <div>
                  <p className="font-semibold">מס' הזמנה: {order.id}</p>
                  {!user && <p>ספק: {order.supplierName}</p>}
                  <p>
                    סטטוס:{" "}
                    {order.statusOrders === 0
                      ? "ממתין"
                      : order.statusOrders === 1
                        ? "בתהליך"
                        : "הושלמה"}
                  </p>
                  <p>כמות: {order.quantityOrdered}</p>
                  <p>מוצרים: {order.goods?.map((g) => g.productName).join(", ")}</p>
                </div>
                <button
                  onClick={() => approveOrder(order.id, order.statusOrders)}
                  className="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-lg"
                >
                  ✅ אשר הזמנה
                </button>
              </li>
            ))}
          </ul>
        </div>
      )}

    </div>
  );
}
