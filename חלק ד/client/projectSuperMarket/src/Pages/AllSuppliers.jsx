import { useEffect, useState } from "react";
import { getAllSuppliers } from "../servieses/api";
import { useNavigate } from "react-router-dom";

const AllSuppliers = () => {
    const [suppliers, setSuppliers] = useState([]);
    const navigate = useNavigate();
    useEffect(() => {
        const fetchSuppliers = async () => {
            try {
                const data = await getAllSuppliers();
                setSuppliers(data);
                console.log("ספקים:", data);
            } catch (error) {
                console.error("שגיאה בשליפת ספקים:", error);
            }
        };
        fetchSuppliers();
    }, []);
    return (
        <>
            <button onClick={() => navigate(-1)} className="text-blue-600 underline">⬅ חזור</button>
            <div className="p-6" >
                <h1 className="text-3xl font-bold mb-6 text-center">רשימת הספקים</h1>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6" id="suppliers-list">
                    {suppliers.map((supplier) => (
                        <div
                            key={supplier.id}
                            className="bg-white rounded-2xl shadow-md p-4 border hover:shadow-lg transition-all duration-300" id="supplier-card"
                        >
                            <h2 className="text-xl font-semibold text-purple-700 mb-2">
                                {supplier.id}
                            </h2>
                            <p> {supplier.representativeName}<strong>:נציג</strong></p>
                            <p> {supplier.phoneNumber}<strong>:טלפון</strong></p>
                            <div className="mt-3">
                                <strong>:סחורות</strong>
                                {supplier.goods && supplier.goods.length > 0 ? (
                                    <ul className="list-disc list-inside text-sm mt-1">
                                        {supplier.goods.map((good, idx) => (
                                            <>
                                                <li key={idx}>{good.id}</li>
                                                <li key={idx}>{good.productName}</li>
                                                <li key={idx}>{good.pricePerItem}</li>
                                                <li key={idx}>{good.minQuantity}</li>
                                            </>
                                        ))}
                                    </ul>
                                ) : (
                                    <p className="text-gray-500 text-sm">אין סחורות</p>
                                )}
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </>
    );
};

export default AllSuppliers;
