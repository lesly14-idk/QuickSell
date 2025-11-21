CREATE DATABASE Quicksell_2
USE Quicksell_2

--Creamos las tablas
CREATE TABLE TIPO_USUARIOS
(
	id_tipoUsuario INT IDENTITY (1,1) not null PRIMARY KEY,
	tipo_nombreUsuario VARCHAR(50) not null CHECK (tipo_nombreUsuario IN ('Cliente', 'Proveedor', 'Admin')), --sust. el ENUM
)

--SOLICITADO


--Agregas los valores
INSERT INTO TIPO_USUARIOS (tipo_nombreUsuario) 
VALUES
('Cliente'),
('Proveedor'),
('Admin')

--Comprobacion
SELECT * FROM TIPO_USUARIOS



--Creamos las tablas
CREATE TABLE USUARIO
(
	id_usuario INT IDENTITY (1,1) not null PRIMARY KEY, 
	id_tipoUsuario INT not null,
	nombre_usuario VARCHAR(100) not null,
	telefono_usuario VARCHAR(20) not null,
	direccion_usuario VARCHAR(150) not null,
	email_usuario VARCHAR(100) not null UNIQUE,
	contraseña_usuario VARCHAR(255) not null, --eso no estaba en el diagrama pero deberia xd
	estado_usuario VARCHAR(50) not null CHECK (estado_usuario IN ('Activo', 'Inactivo', 'Suspendido')),
	fecha_registro DATETIME DEFAULT GETDATE()

	FOREIGN KEY (id_tipoUsuario) REFERENCES TIPO_USUARIOS (id_tipoUsuario) --Se declara como llave foranea
)

--SOLICITADO

--agrega los valores 3 valores a agregar (el auto incrementado no se agrega)
INSERT INTO USUARIO (id_tipoUsuario, nombre_usuario, telefono_usuario, direccion_usuario, email_usuario, contraseña_usuario, estado_usuario, fecha_registro)
VALUES
(1, 'Luis Hernández', '639 230 7151', 'Las Puentes', 'luisfernandohdz117@gmail.com', '1357901', 'Activo', GETDATE()),
(2, 'Ana López', '639 456 1234', 'C.Morelos y Av. Independencia', 'anaalopez69@gmail.com', '0192834', 'Activo', GETDATE()),
(3, 'Lesly Alvarez', '639 234 6790', 'Av. Tecnologico', 'lessterrazas@gmail.com', '1234567', 'Activo', GETDATE()),
(2, 'Sara Nájera', '639 125 5321', 'C.Morelos y 1era Mayo', 'saraisabeln8@gmail.com', '246810', 'Activo', GETDATE()),
(2, 'Leonardo Soto', '639 134 3456', 'c.independencia y col. juárez', 'leosoto88@gmail.com', '1235679', 'Activo', GETDATE())

--Consultas para verificar la tabla
SELECT * FROM USUARIO




CREATE TABLE PROVEEDORES
(
	id_proveedor INT IDENTITY (1,1) not null PRIMARY KEY, 
	id_usuario INT not null, --llama la llave foranea
	nombre_empresa VARCHAR(100) not null, --nombre proveedor
	catalogo_activo char(5) DEFAULT 'FALSE' CHECK (catalogo_activo IN ('TRUE', 'FALSE')), --sust. el BOOLEAN
	validado_por_proveedor char(5) DEFAULT 'FALSE' CHECK (validado_por_proveedor IN ('TRUE', 'FALSE')),
	validado_por_admin char(5) DEFAULT 'FALSE' CHECK (validado_por_admin IN ('TRUE', 'FALSE'))

	FOREIGN KEY (id_usuario) REFERENCES USUARIO (id_usuario) --Se declara como llave foranea
)

--agrega los valores (el auto incrementado no se agrega) --> id_proveedor
INSERT INTO PROVEEDORES (id_usuario, nombre_empresa, catalogo_activo, validado_por_proveedor, validado_por_admin)
VALUES
(2, 'Electronic', 'TRUE', 'TRUE', 'TRUE'),
(4, 'Accessory Aesthethic', 'TRUE', 'TRUE', 'TRUE'),
(5, 'Mens Boutique', 'TRUE', 'TRUE', 'TRUE')

--Comprobar los datos
SELECT * FROM PROVEEDORES

--SOLICITADO


CREATE TABLE CATEGORIA
(
	id_categoria INT IDENTITY (1,1) not null PRIMARY KEY,
	nombre_categoria VARCHAR(50) not null CHECK (nombre_categoria IN ('Ropa', 'Electronicos', 'Juguetes'))
)

--agrega los valores (el auto incrementado no se agrega) --> id_categoria
INSERT INTO CATEGORIA (nombre_categoria) 
VALUES 
('Ropa'),
('Electronicos'),
('Juguetes')

--Comprobar la tabla
SELECT * FROM CATEGORIA

--SOLICITADO


CREATE TABLE PRODUCTOS
(
	id_producto INT IDENTITY (1,1) not null PRIMARY KEY, 
	id_proveedor INT not null, --Se le asigna llave foranea
	id_categoria INT not null, --Se le asigna llave foranea
	nombre_producto VARCHAR(100) not null,
	descripcion_producto VARCHAR(300) not null,
	precio_producto DECIMAL(10,2) not null,
	stock_producto INT not null,
	estado_creacion VARCHAR(50) not null CHECK (estado_creacion IN ('Disponible', 'Agotado', 'Inactivo')),
	fecha_creacion DATETIME DEFAULT GETDATE(),
	validado_por_admin  char(5) DEFAULT 'FALSE' CHECK (validado_por_admin  IN ('TRUE', 'FALSE'))

	FOREIGN KEY (id_proveedor) REFERENCES PROVEEDORES (id_proveedor), --Se declara como llave foranea
	FOREIGN KEY (id_categoria) REFERENCES CATEGORIA (id_categoria) --Se declara como llave foranea	
)

--Agregar los valores (el auto incrementado no se agrega) --> id_producto  || El de la fecha es opcional que lo agregues, pero como lo hizo predeterminado, asi que xddd
INSERT INTO PRODUCTOS (id_proveedor, id_categoria, nombre_producto, descripcion_producto, precio_producto, stock_producto, estado_creacion, validado_por_admin)
VALUES
(1, 2, 'Laptop Gamer', 'Laptop Gamer de alto rendimiento, que cuenta con una tarjeta gráfica y CPU intercambiable', 9599.99, 10, 'Disponible', 'TRUE'),
(2, 1, 'Vestido Blanco', 'Vestido Blanco de tela brillosa de buena calidad', 399.99, 20, 'Disponible', 'TRUE'),
(3, 1, 'Traje Vaquero', 'Juego de vestimenta completo de Traje Vaquero para hombre, unica talla y de colección', 2999.99, 5, 'Agotado', 'TRUE')

--Comprobar la tabla
SELECT * FROM PRODUCTOS




CREATE TABLE SOLICITUDES_DE_COMPRA
(
	id_solicitud INT IDENTITY (1,1) not null PRIMARY KEY, 
	id_usuario INT not null, --llama la llave foranea ()
	id_proveedor INT not null, --Se le asigna llave foranea
	fecha_solicitud DATETIME DEFAULT GETDATE(),
	total DECIMAL(10,2) not null,
	estado VARCHAR(50) not null CHECK (estado IN ('Pendiente', 'Procesando', 'rechazada', 'enviada', 'entregada', 'cancelada')),
	gestionado_por_admin  char(5) DEFAULT 'FALSE' CHECK (gestionado_por_admin  IN ('TRUE', 'FALSE')),
	nota_seguimiento VARCHAR(300) not null

	FOREIGN KEY (id_usuario) REFERENCES USUARIO (id_usuario), --Se declara como llave foranea
	FOREIGN KEY (id_proveedor) REFERENCES PROVEEDORES (id_proveedor) --Se declara como llave foranea
)


--agregar los valores (el auto incrementado no se agrega) --> id_solicitud  || Los DATE son Opcional
INSERT INTO SOLICITUDES_DE_COMPRA (id_usuario, id_proveedor, total, estado, gestionado_por_admin, nota_seguimiento)
VALUES
(1, 1, 105.98, 'Pendiente', 'FALSE', 'Esperando confirmación de pago'),
(1, 3, 200.50, 'Procesando', 'FALSE', 'Producto en preparación')

--Comprobar tabla
SELECT * FROM SOLICITUDES_DE_COMPRA




CREATE TABLE DETALLE_SOLICITUD
(
	id_detalle INT IDENTITY (1,1) not null PRIMARY KEY, 
	id_solicitud INT not null, --hace llave foranea
	id_producto INT not null, --llave foranea
	cantidad INT not null,
	subtotal DECIMAL(10,2) not null

	FOREIGN KEY (id_solicitud) REFERENCES SOLICITUDES_DE_COMPRA (id_solicitud), --Se declara como llave foranea
	FOREIGN KEY (id_producto) REFERENCES PRODUCTOS (id_producto) --Se declara como llave foranea
)

--agregar los valores (el auto incrementado no se agrega) --> id_detalle  || Los DATE son Opcional
INSERT INTO DETALLE_SOLICITUD (id_solicitud, id_producto, cantidad, subtotal)
VALUES
(2,3,2, 5999.99), --Traje vaquero
(1, 1, 3, 28800.00 ) --laptop

--Comprobar los datos en la tabla
SELECT * FROM DETALLE_SOLICITUD



CREATE TABLE PAGOS
(
	id_pagos INT IDENTITY (1,1) not null PRIMARY KEY, 
	id_solicitud INT not null, --hace llave foranea
	metodo_pago VARCHAR(50) not null CHECK (metodo_pago IN ('Efectivo', 'Paypal', 'mercadoPago', 'otro')), --Efectivovo	monto DECIMAL(10,2) not null,
	monto DECIMAL(10,2) not null,
	estado_pago VARCHAR(50) not null CHECK (estado_pago IN ('Pendiente', 'Validado', 'rechazado', 'liberado', 'congelado')),
	fecha_pago DATETIME DEFAULT GETDATE(),
	validado_admin  char(5) DEFAULT 'FALSE' CHECK (validado_admin  IN ('TRUE', 'FALSE'))

	FOREIGN KEY (id_solicitud) REFERENCES SOLICITUDES_DE_COMPRA (id_solicitud) --Se declara como llave foranea
)

--agregar valores (el auto incrementado no se agrega) --> id_pagos  || Los DATE son Opcional
INSERT INTO PAGOS (id_solicitud, metodo_pago, monto, estado_pago, validado_admin)
VALUES
(1, 'mercadoPago', 8500.00, 'pendiente', 'TRUE'),
(2, 'mercadoPago', 6000.00, 'Validado', 'TRUE')

--comprobar tablas
SELECT * FROM PAGOS




CREATE TABLE ENVIOS
(
	id_envios INT IDENTITY (1,1) not null PRIMARY KEY,
	id_solicitud INT not null, --hace llave foranea
	tipo_envio VARCHAR(50) not null CHECK (tipo_envio IN ('Local', 'Autogestionado')),
	direccion_entrega VARCHAR(150) not null,
	numero_guia VARCHAR(50) not null,
	estado_envio VARCHAR(50) not null CHECK (estado_envio IN ('Preparacion', 'En transito', 'entregado', 'cancelado')),
	fecha_actualizacion DATETIME DEFAULT GETDATE(),

	FOREIGN KEY (id_solicitud) REFERENCES SOLICITUDES_DE_COMPRA (id_solicitud) --Se declara como llave foranea
)


--agregar valores (el auto incrementado no se agrega) -->id_envios  || Los DATE son Opcional
INSERT INTO ENVIOS (id_solicitud, tipo_envio, direccion_entrega,numero_guia, estado_envio)
VALUES
(1, 'Local', 'Las Puentes', 'GUI001234', 'Preparacion'),
(2, 'Autogestionado', 'Las Puentes', 'TRK567890', 'En transito')

--Comprobar datos de la tabla
SELECT * FROM ENVIOS



CREATE TABLE HISTORIAL_DE_OPERACIONES
(
	id_operaciones INT IDENTITY (1,1) not null PRIMARY KEY,
	tipo_operacion VARCHAR(50) not null,
	tabla_afectada VARCHAR(50) not null,
	descripcion VARCHAR(500) not null,
	fecha_operacion DATETIME DEFAULT GETDATE(),
)

--agregar valores  (el auto incrementado no se agrega) -->id_operaciones  || Los DATE son Opcional
INSERT INTO HISTORIAL_DE_OPERACIONES (tipo_operacion, tabla_afectada, descripcion)
VALUES
('Creación', 'PROVEEDORES', 'Administrador validó el proveedor Electronic con ID 1'),
('Actualización', 'PRODUCTOS', 'Admin actualizó el stock del producto Laptop Gamer'),
('Consulta', 'SOLICITUDES_DE_COMPRA', 'Cliente Luis Hernández consultó el estado de su pedido'),
('Validación', 'PAGOS', 'Administrador validó el pago de la solicitud 2'),
('Registro', 'PRODUCTOS', 'Proveedor Ana López registró nuevo producto en el sistema')

--comprobar los datos de la tabla
SELECT* FROM HISTORIAL_DE_OPERACIONES




CREATE TABLE CONFIGURACIONES_SISTEMA
(
	id_config INT IDENTITY (1,1) not null PRIMARY KEY,
	nombre_config VARCHAR(100) not null,
	valor VARCHAR(255) not null,
	ultima_modificacion DATETIME DEFAULT GETDATE(),
	modificado_por VARCHAR(50) not null CHECK (modificado_por IN ('Admin')),

	id_usuario INT not null, --llama la llave foranea
	FOREIGN KEY (id_usuario) REFERENCES USUARIO (id_usuario) --Se declara como llave foranea
)

--agregar valores  (el auto incrementado no se agrega) --> id_config  || Los DATE son Opcional
INSERT INTO CONFIGURACIONES_SISTEMA (nombre_config, valor, modificado_por, id_usuario)
VALUES
('Tiempo_Expiracion_Sesion', '30', 'Admin', 3),
('Limite_Intentos_Login', '3', 'Admin', 3),
('Tipo_Cambio_Dolar', '20.50', 'Admin', 3),
('IVA_Porcentaje', '16', 'Admin', 3),
('Email_Notificaciones', 'notificaciones@quicksell.com', 'Admin', 3)

--Comprobar los datos
SELECT * FROM CONFIGURACIONES_SISTEMA