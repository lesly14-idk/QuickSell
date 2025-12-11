import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./App.css"; 
//import "./Carrito.jsx"
import 'boxicons/css/boxicons.min.css';

export default function LoginRegister() {
   const navigate = useNavigate();
    const [isActive, setIsActive] = useState(false);
   
    // Constantes para el LOGIN (DEJADAS INTACTAS)
    const [email, setEmail] = useState("");
    const [contraseña, setContraseña] = useState("");
    const [errorMsg, setErrorMsg] = useState("");

    // NUEVAS Constantes para el REGISTRO
    const [nombreRegister, setNombreRegister] = useState("");
    const [telefonoRegister, setTelefonoRegister] = useState("");
    const [emailRegister, setEmailRegister] = useState("");
    const [direccionRegister, setDireccionRegister] = useState("");
    const [contraseñaRegister, setContraseñaRegister] = useState("");
    const [errorMsgRegister, setErrorMsgRegister] = useState("");


    const handleLogin = async (e) => {
        e.preventDefault();
        setErrorMsg("");
        try {
            const response = await fetch("http://localhost:5038/api/Auth/login", {
                //http://localhost:7224/api/Auth/login

                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    email: email,
                    contraseña: contraseña
                })
            });

            if (!response.ok) {
                setErrorMsg("Credenciales incorrectas");
                return;
            }

            const data = await response.json();
            console.log("LOGIN OK:", data);

            // Guardar token
            localStorage.setItem("token", data.token);

            alert("El login funcionaaa");
           // window.location.href = "./Carrito.jsx";
           navigate("/Carrito");
        } 
        catch (error) {
            console.error("Error:", error);
            setErrorMsg("No se pudo conectar al servidor");
        }
    };
    // --- FIN FUNCIÓN DE LOGIN ---


    // --- NUEVA FUNCIÓN DE REGISTRO ---
    const handleRegister = async (e) => {
        e.preventDefault();
        setErrorMsgRegister(""); 

        // ⚠️ ATENCIÓN: La URL de registro debe apuntar al endpoint de tu controlador de Usuarios
        // Si tu API usa un endpoint como 'api/Usuarios' para el POST, úsalo.
        try {
            const response = await fetch("http://localhost:5038/api/Usuarios", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    nombre_usuario: nombreRegister,
                    telefono_usuario: telefonoRegister,
                    direccion_usuario: direccionRegister,
                    email_usuario: emailRegister,
                    contraseña_usuario: contraseñaRegister,
                    // VALORES FIJOS SOLICITADOS:
                    tipo_usuario: "Cliente", 
                    estado_usuario: "Activo"
                    // NOTA: id_tipoUsuario, id_usuario y fecha_registro NO se envían, los genera la BD/API.
                })
            });

            if (response.status === 201 || response.ok) {
                alert("¡Registro exitoso! Ya puedes iniciar sesión.");
                // Limpiar formulario y cambiar a Login
                setNombreRegister('');
                setTelefonoRegister('');
                setEmailRegister('');
                setDireccionRegister('');
                setContraseñaRegister('');
                setIsActive(false); // Vuelve a la vista de Login
            } else {
                // Intentar leer el mensaje de error del backend
                const errorData = await response.json();
                console.error("Error de Registro:", errorData);
                setErrorMsgRegister(errorData.message || "Error al registrar usuario. Intenta más tarde.");
            }
        } 
        catch (error) {
            console.error("Error de conexión:", error);
            setErrorMsgRegister("No se pudo conectar con la API.");
        }
    };
    // --- FIN FUNCIÓN DE REGISTRO ---


    return (
        <div className={`container ${isActive ? "active" : ""}`}>
            {/* LOGIN (INCLUYE handleLogin) */}
            <div className="form-box login">
                <form onSubmit={handleLogin}>
                    <h1>Login</h1>

                    <div className="input-box">
                        <input
                            type="email"
                            placeholder="Correo"
                            required
                            value={email}
                            onChange={(e) => setEmail(e.target.value)} />
                        <i className="bx bxs-user"></i>
                    </div>

                    <div className="input-box">
                        <input
                            type="password"
                            placeholder="Contraseña"
                            required
                            value={contraseña}
                            onChange = {(e) => setContraseña(e.target.value)} />
                        <i className="bx bxs-lock-alt"></i>
                    </div>
                    {errorMsg && <p style={{ color: "red" }}>{errorMsg}</p>}

                    {/* ... Resto del contenido del Login ... */}
                     <div className="forgot-link">
                         <a href="#">¿Olvidaste tu Contraseña?</a>
                     </div>

                     <button type="submit" className="btn">
                         Iniciar Sesión
                     </button>

                     <p>Siguenos en Nuestras Redes Sociales: </p>

                     <div className="social-icons">
                         <a href="#"><i className="bx bxl-facebook"></i></a>
                         <a href="#"><i className="bx bxl-google"></i></a>
                         <a href="#"><i className="bx bxl-twitter"></i></a>
                         <a href="#"><i className="bx bxl-github"></i></a>
                         <a href="#"><i className="bx bxl-linkedin"></i></a>
                     </div>

                     <br />

                     <a href="">¿Quieres volver a la página principal?</a>
                </form>
            </div>

            {/* REGISTRARSE (INCLUYE handleRegister) */}
            <div className="form-box register">
                <form onSubmit={handleRegister}>
                    <h1>Registrarse</h1>

                    <div className="input-box">
                        <input type="text" 
                               placeholder="Nombre" 
                               required 
                               value={nombreRegister}
                               onChange={(e) => setNombreRegister(e.target.value)} />
                        <i className="bx bxs-user"></i>
                    </div>

                    <div className="input-box">
                        <input type="text" 
                               placeholder="Telefono" 
                               required 
                               value={telefonoRegister}
                               onChange={(e) => setTelefonoRegister(e.target.value)} />
                        <i className="bx bxs-phone"></i>
                    </div>

                    <div className="input-box">
                        <input type="email" 
                               placeholder="Email" 
                               required 
                               value={emailRegister}
                               onChange={(e) => setEmailRegister(e.target.value)} />
                        <i className="bx bxs-envelope"></i>
                    </div>

                    <div className="input-box">
                        <input type="text" 
                               placeholder="Direccion" 
                               required 
                               value={direccionRegister}
                               onChange={(e) => setDireccionRegister(e.target.value)} />
                        <i className="bx bxs-pin"></i>
                    </div>

                    <div className="input-box">
                        <input type="password" 
                               placeholder="Contraseña" 
                               required 
                               value={contraseñaRegister}
                               onChange={(e) => setContraseñaRegister(e.target.value)} />
                        <i className="bx bxs-lock-alt"></i>
                    </div>

                    {errorMsgRegister && <p style={{ color: "red" }}>{errorMsgRegister}</p>}

                    <button type="submit" className="btn">Registrarse</button>
                
                </form>
            </div>

            {/* TOGGLE BOX (INTACTO) */}
            <div className="toggle-box">
                <div className="toggle-panel toggle-left">
                    <h1>Hola, Bienvenid@.</h1>
                    <p>¿No tienes cuenta?</p>

                    <button
                        className="btn register-btn"
                        type="button"
                        onClick={() => setIsActive(true)}
                    >
                        Registrate
                    </button>
                </div>

                <div className="toggle-panel toggle-right">
                    <h1>¡Bienvenid@!</h1>
                    <p>Siguenos en Nuestras Redes Sociales</p>

                    <button
                        className="btn login-btn"
                        type="button"
                        onClick={() => setIsActive(false)}
                    >
                        Iniciar Sesión
                    </button>
                </div>
            </div>
        </div>
    );
}
