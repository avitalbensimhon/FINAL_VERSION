import React from "react";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import { Toaster } from "react-hot-toast";
import Header from "./Componenta/Header";
import SupplierLogin from "./Pages/SupplierLogin";
import SupplierRegister from "./Pages/SupplierRegister";
import OrdersList from "./Pages/OrdersList";
import { AuthProvider, useAuth } from "./context/AuthContext";
import AdminLogin from "./Pages/AdminLogin"
import AdminOrders from "./Pages/AdminOrders";
import AdminHome from "./Pages/AdminHome";
import ApproveOrders from "./Pages/ApproveOrders";
import OrderGoods from "./Pages/OrderGoods";
import AllSuppliers from "./Pages/AllSuppliers";
import SupplierDashboard from "./Pages/SupplierDashboard";
import ApproveOrdersSuppliers from "./Pages/ApproveOrdersSuppliers";
import AdminBoxOffice from "./Pages/AdminBoxOffice";
function PrivateRoute({ children }) {
  const { user } = useAuth();
  return user ? children : <Navigate to="/login" />;
}

export default function App() {
  return (
    <>
      <AuthProvider>
        <Router>
          <Toaster position="top-center" reverseOrder={false} />
          <div
          >
            <Header />
            <Routes >
              <Route path="/admin/home" element={<AdminHome />} />
              <Route path="/admin-box-office" element={<AdminBoxOffice />} />
              <Route path="/supplier/dashboard" element={<SupplierDashboard />} />
              <Route path="/admin/order-goods" element={<OrderGoods />} />
              <Route path="/admin/order-status" element={<OrdersList />} />
              <Route path="/admin/approve-orders" element={<ApproveOrders />} />
              <Route path="/suppliers" element={<AllSuppliers />} />
              <Route path="/admin/all-orders" element={<AdminOrders />} />
              <Route path="/approve-orders" element={<ApproveOrdersSuppliers />} />
              <Route path="/login" element={<SupplierLogin />} />
              <Route path="/register" element={<SupplierRegister />} />
              <Route path="*" element={<Navigate to="/login" />} />
              <Route path="/admin/login" element={<AdminLogin />} />
            </Routes>
          </div >
          <Toaster position="top-right" />
        </Router>
      </AuthProvider>
    </>
  )
}
