import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
export default function AdminOrders() {
  const [orders, setOrders] = useState([]);
  const navigate = useNavigate();
  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const token = localStorage.getItem("adminToken");
        const res = await axios.get("https://localhost:7158/api/Orders", {
          headers: { Authorization: `Bearer ${token}` },
        });
        console.log("הזמנות שהתקבלו", res.data);
        setOrders(res.data);
      } catch (err) {
        console.error("שגיאה בטעינת ההזמנות", err);
      }
    };
    fetchOrders();
  }, []);

  return (
    <>
      <button onClick={() => navigate(-1)} className="text-blue-600 underline">⬅ חזור</button>
      <div className="p-4">
        <h2 className="text-2xl font-semibold mb-4" >רשימת ההזמנות</h2>
        <div id="orders">
          {orders.map((order) => (
            <div key={order.id} className="bg-white shadow p-4 mb-4 rounded">
              <p><strong>מספר הזמנה:</strong> {order.id}</p>
              <p><strong>ספק (ID):</strong> {order.supplierId}</p>
              <p><strong>סטטוס:</strong> {order.statusOrders === 0 ? "ממתין" : order.statusOrders === 1 ? "בתהליך" : order.statusOrders === 2 ? "הושלמה" : "לא ידוע"}</p>
              <p><strong>כמות:</strong> {order.quantityOrdered}</p>
              <p><strong>מוצרים:</strong> {order.goods.map(g => g.productName).join(", ")}</p>
            </div>
          ))}
        </div>
      </div>
    </>
  );
}
