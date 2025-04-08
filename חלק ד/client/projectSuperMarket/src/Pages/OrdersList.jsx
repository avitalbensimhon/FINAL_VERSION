import React, { useEffect, useState } from "react";
import axios from "axios";
import { getOrdersBySupplier } from "../servieses/api";
import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

const OrdersList = () => {
  const { user } = useAuth(); 
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        let data = [];
        if (user) {
          const res = await getOrdersBySupplier(user.id);
          data = res.data;
        } else {
          const token = localStorage.getItem("adminToken");
          if (token) {
            const res = await axios.get("https://localhost:7158/api/Orders", {
              headers: { Authorization: `Bearer ${token}` },
            });
            data = res.data;
          }
        }
        const filtered = data.filter(
          (order) => order.statusOrders === 0 || order.statusOrders === 1
        );
        setOrders(filtered);
      } catch (error) {
        console.error("שגיאה בטעינת ההזמנות:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchOrders();
  }, [user]);

  return (
    <>
      <button onClick={() => navigate(-1)} className="text-blue-600 underline">
        ⬅ חזור
      </button>
      <div className="container mx-auto p-6">
        <h1 className="text-2xl font-bold mb-6">הזמנות פעילות</h1>
        {loading ? (
          <p>טוען הזמנות...</p>
        ) : orders.length > 0 ? (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6" id="orders">
            {orders.map((order) => (
              <div key={order.id} className="bg-white shadow p-4 mb-4 rounded" id="order-card">
                <p><strong>מספר הזמנה:</strong> {order.id}</p>
                <p><strong>ספק (ID):</strong> {order.supplierId}</p>
                <p><strong>סטטוס:</strong> {
                  order.statusOrders === 0 ? "ממתין" :
                    order.statusOrders === 1 ? "בתהליך" :
                      "לא ידוע"
                }</p>
                <p><strong>כמות:</strong> {order.quantityOrdered}</p>
                <p><strong>מוצרים:</strong> {order.goods.map(g => g.productName).join(", ")}</p>
              </div>
            ))}
          </div>
        ) : (
          <p>אין הזמנות פעילות להצגה.</p>
        )}
      </div>
    </>
  );
};

export default OrdersList;
