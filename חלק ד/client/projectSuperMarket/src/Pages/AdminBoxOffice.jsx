import React, { useState } from "react";
import axios from "axios";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

export default function AdminBoxOffice() {
    const navigate = useNavigate();

    const [formBoxOffice, setFormBoxOffice] = useState({
        goods: [
            {
                productName: "",
                minQuantity: "",
            },
        ],
    });


    const [orders, setOrders] = useState([]);
    const [totalPrice, setTotalPrice] = useState(0);

    const handleFormChange = (index, e) => {
        const { name, value } = e.target;
        const updatedGoods = [...formBoxOffice.goods];
        updatedGoods[index][name] = value;
        setFormBoxOffice({ goods: updatedGoods });
    };

    const addGoodsRow = () => {
        setFormBoxOffice((prev) => ({
            goods: [...prev.goods, { productName: "", minQuantity: "" }],
        }));
    };

    const removeGoodsRow = (index) => {
        const newGoods = [...formBoxOffice.goods];
        newGoods.splice(index, 1);
        setFormBoxOffice({ goods: newGoods });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const soldItems = {};
        formBoxOffice.goods.forEach((item) => {
            if (item.productName && item.minQuantity) {
                soldItems[item.productName] = parseInt(item.minQuantity);
            }
        });

        try {
            const response = await axios.post(
                "https://localhost:7158/api/HandlePurchaseFromCashier",
                soldItems
            );

            const data = response.data;

            if (data.messages && data.messages.length > 0) {
                data.messages.forEach((msg) => toast(msg));
            }

            if (data.orders && data.orders.length > 0) {
                setOrders(data.orders);
                const total = data.orders.reduce((sum, order) => {
                    const item = order.goods[0];
                    return sum + item.pricePerItem * order.quantityOrdered;
                }, 0);
                setTotalPrice(total);
            }
        } catch (err) {
            if (err.response?.data?.messages) {
                err.response.data.messages.forEach((msg) => toast.error(msg));
            } else {
                toast.error("שגיאה כללית בבדיקה");
            }
        }
    };

    return (
        <div className="p-6 max-w-2xl mx-auto">
            <h1 className="text-2xl font-bold mb-2">ברוך הבא למערכת ניהול קופות</h1>
            <p className="mb-4">כאן תוכל לנהל את כל ההזמנות והקופות שלך.</p>

            <button onClick={() => navigate(-1)} className="mb-4 underline text-blue-500">
                ⬅ חזור
            </button>

            <form onSubmit={handleSubmit} className="space-y-4">
                {formBoxOffice.goods.map((item, index) => (
                    <div key={index} className="border p-3 rounded space-y-2">
                        <div>
                            <label>שם מוצר:</label>
                            <input
                                type="text"
                                name="productName"
                                value={item.productName}
                                onChange={(e) => handleFormChange(index, e)}
                                required
                                className="border p-1 rounded w-full"
                            />
                        </div>
                        <div>
                            <label>כמות שנמכרה:</label>
                            <input
                                type="number"
                                name="minQuantity"
                                value={item.minQuantity}
                                onChange={(e) => handleFormChange(index, e)}
                                required
                                min="1"
                                className="border p-1 rounded w-full"
                            />
                        </div>
                        {formBoxOffice.goods.length > 1 && (
                            <button
                                type="button"
                                onClick={() => removeGoodsRow(index)}
                                className="text-red-500 underline text-sm"
                            >
                                הסר מוצר
                            </button>
                        )}
                    </div>
                ))}

                <button type="button" onClick={addGoodsRow} className="text-green-600 underline">
                    ➕ הוסף מוצר נוסף
                </button>

                <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                    בדוק מלאי ושלח
                </button>
            </form>


            {orders.length > 0 && (
                <div className="mt-6 p-4 border rounded shadow">
                    <h2 className="text-lg font-semibold">💳 סיכום הזמנה</h2>

                    {orders.map((order, index) => (
                        <div key={index} className="border-b py-4">
                            <h3 className="text-md font-semibold">הזמנה {order.id}</h3>

                            <div className="mt-2">

                                {order.goods[0]?.supplier?.companyName ? (
                                    <div>
                                        {order.goods[0].supplier.companyName} <strong>:ספק</strong>
                                    </div>
                                ) : (
                                    <div>ספק לא נמצא</div>
                                )}
                            </div>

                            {order.goods.map((good, goodIndex) => (
                                <div key={goodIndex} className="mt-2">
                                    <div> {good.productName}<strong>:מוצר</strong></div>
                                    <div> {order.quantityOrdered}<strong>:כמות להזמנה</strong></div>
                                    <div>{good.pricePerItem}  <strong> ₪</strong><strong>:מחיר למוצר</strong></div>
                                </div>
                            ))}
                        </div>
                    ))}

                    <p className="mt-4">{totalPrice}<strong> ₪</strong><strong>:סה"כ לתשלום </strong></p>

                    <form className="mt-4 space-y-2">
                        <input
                            type="text"
                            placeholder="מספר כרטיס"
                            className="border p-1 rounded w-full"
                            required
                        />
                        <input
                            type="text"
                            placeholder="תוקף (MM/YY)"
                            className="border p-1 rounded w-full"
                            required
                        />
                        <input
                            type="text"
                            placeholder="CVV"
                            className="border p-1 rounded w-full"
                            required
                        />
                        <button
                            onClick={(e) => {
                                e.preventDefault();
                               
                                toast.success("💳 תשלום אושר! 🎉", {
                                    icon: "🎉",
                                    duration: 3000,
                                });

                                
                                setOrders([]);  
                                setFormBoxOffice({
                                    goods: [
                                        {
                                            productName: "",
                                            minQuantity: "",
                                        },
                                    ],  
                                });
                            }}
                            className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
                        >
                            אשר תשלום
                        </button>
                    </form>
                </div>
            )}
        </div>
    );
}
