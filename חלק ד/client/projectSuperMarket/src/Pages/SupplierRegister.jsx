import React, { useState } from "react";
import { registerSupplier } from "../servieses/api";
import { useNavigate } from "react-router-dom";
import toast from 'react-hot-toast';

const SupplierRegister = () => {
  const [formData, setFormData] = useState({
    companyName: "",
    phoneNumber: "",
    representativeName: "",
    goodsList: [],
  });

  const [newGood, setNewGood] = useState({ productName: "", pricePerItem: "", minQuantity: "" });

  const navigate = useNavigate();

  const handleAddGood = () => {
    if (newGood.productName && newGood.pricePerItem && newGood.minQuantity) {
      setFormData({
        ...formData,
        goodsList: [...formData.goodsList, newGood],
      });
      setNewGood({ productName: "", pricePerItem: "", minQuantity: "" });
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await registerSupplier(formData);
      toast.success("הרישום הצליח!");
      navigate("/supplier-login");
    } catch (error) {
      console.error("Registration failed:", error);
      toast.error("קרתה שגיאה בהרשמה. נסה שוב.");
    }
  };

  return (
    <>
      <button onClick={() => navigate(-1)} className="text-blue-600 underline">⬅ חזור</button>

      <div className="container mx-auto p-6">
        <h1 className="text-2xl font-bold mb-4">רישום ספק חדש</h1>
        <form onSubmit={handleSubmit} className="space-y-4" id="formRegister">
          <input
            type="text"
            placeholder="שם חברה"
            value={formData.companyName}
            onChange={(e) => setFormData({ ...formData, companyName: e.target.value })}
            className="w-full p-2 border rounded-md"
          />
          <input
            type="text"
            placeholder="מספר טלפון"
            value={formData.phoneNumber}
            onChange={(e) => setFormData({ ...formData, phoneNumber: e.target.value })}
            className="w-full p-2 border rounded-md"
          />
          <input
            type="text"
            placeholder="שם נציג"
            value={formData.representativeName}
            onChange={(e) => setFormData({ ...formData, representativeName: e.target.value })}
            className="w-full p-2 border rounded-md"
          />

          <div className="space-y-2 border p-4 rounded-md">
            <h2 className="font-semibold">הוספת סחורה</h2>
            <input
              type="text"
              placeholder="שם מוצר"
              value={newGood.productName}
              onChange={(e) => setNewGood({ ...newGood, productName: e.target.value })}
              className="w-full p-2 border rounded-md"
            />
            <input
              type="number"
              placeholder="מחיר ליחידה"
              value={newGood.pricePerItem}
              onChange={(e) => setNewGood({ ...newGood, pricePerItem: parseFloat(e.target.value) })}
              className="w-full p-2 border rounded-md"
            />
            <input
              type="number"
              placeholder="כמות מינימלית"
              value={newGood.minQuantity}
              onChange={(e) => setNewGood({ ...newGood, minQuantity: parseInt(e.target.value) })}
              className="w-full p-2 border rounded-md"
            />
            <button
              type="button"
              onClick={handleAddGood}
              className="bg-green-500 text-white px-4 py-2 rounded-md"
            >
              הוסף סחורה
            </button>
            {formData.goodsList.length > 0 && (
              <ul className="list-disc list-inside mt-2">
                {formData.goodsList.map((item, index) => (
                  <li key={index}>
                    {item.productName} - ₪{item.pricePerItem} (מינימום: {item.minQuantity})
                  </li>
                ))}
              </ul>
            )}
          </div>

          <button type="submit" className="w-full bg-blue-500 text-white p-2 rounded-md">
            רישום
          </button>
        </form>
      </div>
    </>
  );
};

export default SupplierRegister;
