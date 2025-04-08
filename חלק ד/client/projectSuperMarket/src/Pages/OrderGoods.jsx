import React, { useState } from "react";
import axios from "axios";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

export default function OrderGoods() {
  const [form, setForm] = useState({
    supplierId: "",
    quantityOrdered: "",
    statusOrders: 0,
    goods: [
      {
        productName: "",
        pricePerItem: "",
        minQuantity: "",
      },
    ],
  });
  const navigate = useNavigate();
  const handleFormChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: value,
    }));
  };
  const handleGoodsChange = (index, e) => {
    const { name, value } = e.target;
    const newGoods = [...form.goods];
    newGoods[index][name] = value;
    setForm((prev) => ({
      ...prev,
      goods: newGoods,
    }));
  };
  const addGoodsRow = () => {
    setForm((prev) => ({
      ...prev,
      goods: [...prev.goods, { productName: "", pricePerItem: "", minQuantity: "" }],
    }));
  };
  const removeGoodsRow = (index) => {
    const newGoods = [...form.goods];
    newGoods.splice(index, 1);
    setForm((prev) => ({
      ...prev,
      goods: newGoods,
    }));
  };
  const handleSubmit = async (e) => {
    e.preventDefault();
    const token = localStorage.getItem("adminToken");
    const payload = {
      supplierId: parseInt(form.supplierId),
      quantityOrdered: parseInt(form.quantityOrdered),
      statusOrders: 0,
      goods: form.goods.map((g) => ({
        productName: g.productName,
        pricePerItem: parseFloat(g.pricePerItem),
        minQuantity: parseInt(g.minQuantity),
      })),
    };
    try {
      await axios.post("https://localhost:7158/api/Orders", payload, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });
      toast.success("!ההזמנה נשלחה בהצלחה");
      setForm({
        supplierId: "",
        quantityOrdered: "",
        statusOrders: 0,
        goods: [{ productName: "", pricePerItem: "", minQuantity: "" }],
      });
      navigate(-1);
    } catch (err) {
      toast.error("שגיאה בשליחת ההזמנה");
      console.error(err);
    }
  };
  return (
    <div className="max-w-2xl mx-auto mt-10 p-6 bg-white shadow rounded-lg">
      <h2 className="text-2xl font-bold mb-4 text-center">🧾 הזמנת סחורה</h2>
      <button onClick={() => navigate(-1)} className="text-blue-600 underline mb-4" id="b_form">⬅ חזור</button>
      <form onSubmit={handleSubmit} className="space-y-4" id="order-form">
        <div id="supplier-info">
          <div >
            <label className="block font-medium mb-1">ת"ז ספק</label>
            <input
              type="number"
              name="supplierId"
              value={form.supplierId}
              onChange={handleFormChange}
              required
              className="w-full border rounded p-2"
            />
          </div>
          <div>
            <label className="block font-medium mb-1">כמות כללית להזמנה</label>
            <input
              type="number"
              name="quantityOrdered"
              value={form.quantityOrdered}
              onChange={handleFormChange}
              required
              className="w-full border rounded p-2"
            />
          </div>
        </div>
        <div id="order-status">
          <h3 className="text-xl font-semibold mt-6 mb-2">מוצרים להזמנה:</h3>
          {form.goods.map((item, index) => (
            <div key={index} className="border rounded p-4 mb-4 space-y-2 bg-gray-50">
              <div>
                <label className="block mb-1">שם מוצר</label>
                <input
                  type="text"
                  name="productName"
                  value={item.productName}
                  onChange={(e) => handleGoodsChange(index, e)}
                  required
                  className="w-full border rounded p-2"
                />
              </div>
              <div>
                <label className="block mb-1">מחיר ליחידה</label>
                <input
                  type="number"
                  name="pricePerItem"
                  value={item.pricePerItem}
                  onChange={(e) => handleGoodsChange(index, e)}
                  required
                  className="w-full border rounded p-2"
                  step="0.01"
                  min="0"
                />
              </div>
              <div>
                <label className="block mb-1">כמות מינימלית</label>
                <input
                  type="number"
                  name="minQuantity"
                  value={item.minQuantity}
                  onChange={(e) => handleGoodsChange(index, e)}
                  required
                  className="w-full border rounded p-2"
                  min="1"
                />
              </div>
            </div>
          ))}
          <button
            type="button"
            onClick={addGoodsRow}
            className="w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600 transition"
          >
            ➕ הוסף מוצר נוסף
          </button>

          {form.goods.length > 1 && (
            <button
              type="button"
              onClick={() => removeGoodsRow(index)}
              className="text-red-500 underline text-sm mt-1"
            >
              הסר מוצר
            </button>
          )}
        </div>
        <button
          type="submit"
          className="w-full bg-green-600 text-white py-2 rounded hover:bg-green-700 transition mt-4"
        >
          שלח הזמנה
        </button>
      </form>
    </div>
  );
}
