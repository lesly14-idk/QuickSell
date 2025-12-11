import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginRegister from "./App.jsx";
import Carrito from "./Carrito.jsx";

ReactDOM.createRoot(document.getElementById("root")).render(
  <BrowserRouter>
      <Routes>
          <Route path="/" element={<LoginRegister />} />
          <Route path="/Carrito" element={<Carrito />} />
      </Routes>
  </BrowserRouter>
);
