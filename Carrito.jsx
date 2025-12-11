import React, { useState, useEffect, useRef } from 'react';
import './Carrito.css'; // Asegúrate de que este archivo existe y tiene los estilos
import './App.css';
import 'boxicons/css/boxicons.min.css';

// src/QuickSell.jsx
export default function Carrito() {
    // ===============================
    // ⚡ Datos simulados
    // ===============================
    const userData = {
        name: "Alex",
        isLoggedIn: true,
    };

    const mockProducts = [
        {
            id: 1,
            name: "Chiwaka Collection Japanese",
            desc: "Amigos tiernos",
            price: 799,
            stock: 12,
            img: "../img/chikawa_pluches.webp",
        },
        {
            id: 2,
            name: "Kirby",
            desc: "Peluche suave oficial nintendo",
            price: 499,
            stock: 8,
            img: "../img/kirby_pluche.png",
        },
        {
            id: 3,
            name: "Tenis Converse Rojos",
            desc: "Comodidad con cada paso que das",
            price: 11499,
            stock: 5,
            img: "../img/tnis.jpg",
        },
    ];

    // Estado del carrito. Inicialmente vacío para simular que no se ha agregado nada.
    const [cartItems, setCartItems] = useState([]); 

    const [isCartOpen, setIsCartOpen] = useState(false);
    const [isProfileOpen, setIsProfileOpen] = useState(false);

    // ===============================
    // ⚡ Funciones Actualizadas
    // ===============================
    const handleLogout = () => alert("Sesión cerrada (simulado)");

    /**
     * Añade un producto al carrito. Si ya existe, incrementa la cantidad.
     * @param {object} product El objeto producto a añadir.
     */
    const handleAddToCart = (product) => {
        // 1. Verificar si el producto ya está en el carrito
        const existingItemIndex = cartItems.findIndex(item => item.id === product.id);

        if (existingItemIndex !== -1) {
            // 2. Si existe, crear una copia y aumentar la cantidad
            const updatedCartItems = cartItems.map((item, index) => {
                if (index === existingItemIndex) {
                    return { ...item, cantidad: item.cantidad + 1 };
                }
                return item;
            });
            setCartItems(updatedCartItems);
            // alert(`Añadido: +1 ${product.name}`);
        } else {
            // 3. Si no existe, agregarlo con cantidad = 1
            const newItem = {
                id: product.id,
                name: product.name,
                price: product.price,
                img: product.img, // Añadimos la imagen
                cantidad: 1,
            };
            setCartItems([...cartItems, newItem]);
            // alert(`Producto agregado: ${product.name}`);
        }
    };

    /**
     * Vacía completamente el carrito.
     */
    const handleEmptyCart = () => {
        setCartItems([]);
        alert("Carrito vaciado");
    };
    
    // Cálculo del total del carrito
    const totalCarrito = cartItems.reduce(
        (acc, item) => acc + item.price * item.cantidad,
        0
    );

    // Formateo de moneda (para que se vea mejor, ej: 799.00)
    const formatCurrency = (amount) => {
        return amount.toLocaleString('es-MX', { style: 'decimal', minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    return (
        <div className="carrito-page">

            {/* ===========================
              HEADER TIPO MERCADO LIBRE
            ============================ */}
            <header className="header">
                <div className="logo">QuickSell</div>

                <div className="search-bar">
                    <input type="text" placeholder="Buscar productos..." />
                    <i className="bx bx-search"></i>
                </div>

                <div className="header-actions">

                    {/* Saludo y Perfil */}
                    <div className="user-area">
                        <span>Hola, {userData.name}</span>

                        <i
                            className="bx bx-user-circle icon"
                            onClick={() => setIsProfileOpen(!isProfileOpen)}
                        ></i>

                        {isProfileOpen && (
                            <div className="dropdown">
                                <button onClick={handleLogout}>Cerrar Sesión</button>
                            </div>
                        )}
                    </div>

                    {/* Carrito */}
                    <div className="cart-area">
                        <i
                            className="bx bx-cart icon"
                            onClick={() => setIsCartOpen(!isCartOpen)}
                        ></i>

                        {/* bubble */}
                        <span className="cart-count">{cartItems.length}</span>

                        {/* Mini carrito (Dropdown) */}
                        {isCartOpen && (
                            <div className="cart-dropdown">
                                <h4>Mi Carrito</h4>

                                {cartItems.length === 0 ? (
                                    <p className="empty">Carrito vacío</p>
                                ) : (
                                    <div className="cart-list">
                                        {cartItems.map((item) => (
                                            <div className="cart-item" key={item.id}>
                                                <img src={item.img} alt={item.name} />
                                                <div>
                                                    <p className="item-name">{item.name}</p>
                                                    <span className="item-details">
                                                        ${formatCurrency(item.price)} x {item.cantidad}
                                                    </span>
                                                    <strong className="item-subtotal">
                                                        ${formatCurrency(item.price * item.cantidad)}
                                                    </strong>
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                )}

                                <div className="cart-total">
                                    Total: <strong>${formatCurrency(totalCarrito)}</strong>
                                </div>

                                <button
                                    className="btn buy"
                                    onClick={() => alert("Ir al checkout (simulado)")}
                                >
                                    Pagar
                                </button>
                                <button className="btn empty" onClick={handleEmptyCart}>
                                    Vaciar Carrito
                                </button>
                            </div>
                        )}
                    </div>
                </div>
            </header>

            {/* ===========================
                CARRUSEL
            ============================ */}
            <div className="carousel">
                <div className="carousel-track">
                    <img src="../img/banner.webp" alt="Banner 1" />
                    <img src="../img/hero.jpg" alt="Hero" />
                    <img src="../img/curso1.jpg" alt="Curso 1" />
                </div>
            </div>

            {/* ===========================
                LISTADO DE PRODUCTOS
            ============================ */}

            <h2 className="title">Artículos que te gustarían</h2>

            <div className="products-grid">
                {mockProducts.map((p) => (
                    <div className="product-card" key={p.id}>
                        <img src={p.img} alt={p.name} />

                        <h3>{p.name}</h3>
                        <p className="desc">{p.desc}</p>

                        <div className="price">${formatCurrency(p.price)}</div>
                        <div className="stock">Stock: {p.stock}</div>

                        <button
                            className="btn add"
                            // IMPORTANTE: Aquí pasamos el objeto completo (p) a la función
                            onClick={() => handleAddToCart(p)} 
                        >
                            Agregar al carrito
                        </button>
                    </div>
                ))}
            </div>

        </div>
    );
}