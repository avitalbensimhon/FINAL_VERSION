// src/services/api.js
import axios from "axios";

export const registerSupplier = async (formData) => {
  const payload = {
    companyName: formData.companyName,
    phoneNumber: formData.phoneNumber,
    representativeName: formData.representativeName,
    goods: formData.goodsList.map((item) => {
      return {
        productName: item.productName,
        pricePerItem: item.pricePerItem,
        minQuantity: item.minQuantity,
      };
    }),
  };

  const response = await axios.post("https://localhost:7158/api/Auth/register", payload);
  return response.data;
};
const API_BASE = "https://localhost:7158/api";
export const getAllProducts = () =>
  axios.get(`${API_BASE}/Goods`);
export const getAllSuppliers = async () => {
  const res = await axios.get(`${API_BASE}/Supplers`);
  return res.data;
};
export const loginSupplier = (credentials) =>
  axios.post(`${API_BASE}/Auth/login`, credentials);

export const getOrdersBySupplier = (supplierId) =>
  axios.get(`${API_BASE}/orders/by-supplier/${supplierId}`);

export const updateOrderStatus = (orderId, newStatus) =>
  axios.post(`${API_BASE}/orders/${orderId}/update-status`, { status: newStatus });
