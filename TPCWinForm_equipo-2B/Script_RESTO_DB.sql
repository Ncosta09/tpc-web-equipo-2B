-- Crear la base de datos
CREATE DATABASE RESTO_DB;
GO

-- Usar la base de datos
USE RESTO_DB;
GO

-- Crear tabla Roles
CREATE TABLE Roles (
    IdRol SMALLINT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(50) NOT NULL
);
GO

-- Crear tabla Usuarios
CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
	Imagen VARCHAR(150) NULL,
    Contraseña VARCHAR(100) NOT NULL,
    IdRol SMALLINT NOT NULL,
    Correo VARCHAR(100) NOT NULL,
    DNI VARCHAR(25) NOT NULL,
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol)
);
GO

-- Crear tabla Mesas
CREATE TABLE Mesas (
    IdMesa SMALLINT PRIMARY KEY IDENTITY(1,1),
    NumeroMesa INT NOT NULL
);
GO

-- Crear tabla UsuariosxMesa (tabla intermedia)
CREATE TABLE UsuariosxMesa (
    ID INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,
    IdMesa SMALLINT NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdMesa) REFERENCES Mesas(IdMesa)
);
GO

-- Crear tabla Pedidos
CREATE TABLE Pedidos (
    IDPedido INT PRIMARY KEY IDENTITY(1,1),
    IdMesa SMALLINT NOT NULL,
    IdUsuario INT NOT NULL,
    Estado TINYINT NOT NULL, 
    FechaInicio DATETIME NOT NULL,
    FechaCierre DATETIME NULL,
    FOREIGN KEY (IdMesa) REFERENCES Mesas(IdMesa),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);
GO

-- Crear tabla Insumos
CREATE TABLE Insumos (
    IdInsumo INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL
);
GO
-- MODI
ALTER TABLE Insumos
ADD ImagenURL VARCHAR(255) NULL;
GO

-- Crear tabla DetallePedidos
CREATE TABLE DetallePedidos (
    IdDetalle INT PRIMARY KEY IDENTITY(1,1),
    IdPedido INT NOT NULL,
    IdInsumo INT NOT NULL,
    Cantidad INT NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(IDPedido),
    FOREIGN KEY (IdInsumo) REFERENCES Insumos(IdInsumo)
);
GO

CREATE TABLE Ventas (
    IdVenta INT PRIMARY KEY IDENTITY(1,1),
    IdMesa SMALLINT NOT NULL,
    Fecha DATETIME NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IdMesa) REFERENCES Mesas(IdMesa)
);

INSERT INTO Roles (Descripcion) 
VALUES 
('Cliente'),
('Mesero'),
('Manager');
GO

INSERT INTO Usuarios (Nombre, Apellido, Imagen, Contraseña, IdRol, Correo, DNI)
VALUES 
('Juan', 'Pérez', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58482.jpg', '1234', 3, 'test@mail.com', '12345678');
GO


INSERT INTO Usuarios (Nombre, Apellido, Imagen, Contraseña, IdRol, Correo, DNI)
VALUES 
('Ana', 'Gómez', 'https://img.freepik.com/vector-premium/perfil-mujer-dibujos-animados_18591-58530.jpg', 'abcd1234', 2, 'ana@mail.com', '23456789'),
('Carlos', 'Martínez', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58490.jpg', '1234abcd', 2, 'carlos@mail.com', '34567890'),
('Marta', 'Fernández', 'https://img.freepik.com/vector-premium/perfil-mujer-dibujos-animados_18591-58531.jpg', 'abcd1234', 1, 'marta@mail.com', '45678901'),
('Pedro', 'López', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58512.jpg', '1234abcd', 3, 'pedro@mail.com', '56789012');
GO


INSERT INTO Mesas (NumeroMesa)
VALUES 
(1),
(2),
(3),
(4),
(5),
(6),
(7),
(8),
(9),
(10),
(11),
(12);
GO

INSERT INTO Insumos (Nombre, Precio, Stock, ImagenURL)
VALUES 
    ('Papas fritas', 350.00, 100, 'https://media.istockphoto.com/photos/bunch-of-fried-french-fries-on-a-white-background-closeup-picture-id1130991737?k=6&m=1130991737&s=170667a&w=0&h=sVUZHEplnr2Lzt1P409awJ6cUqKsHJPwodobhOF6kiM='),
    ('Hamburguesa', 700.00, 50, 'https://th.bing.com/th/id/OIP.RRrZKc6uX4sKK7ofbFabhQHaEf?rs=1&pid=ImgDetMain'),
    ('Pizza Margarita', 1000.00, 30, 'https://zenideen.com/wp-content/uploads/2020/06/pizza-mozzarella-tomaten-scaled.jpeg'),
    ('Ensalada César', 600.00, 20, 'https://th.bing.com/th/id/OIP.xKBJXVYTf7YE9vU-jIHZPwHaE8?rs=1&pid=ImgDetMain'),
    ('Refresco', 200.00, 150, 'https://res.cloudinary.com/walmart-labs/image/upload/w_960,dpr_auto,f_auto,q_auto:best/gr/images/product-images/img_large/00750103131164L.jpg'),
    ('Carne de Res', 1200.00, 40, 'https://th.bing.com/th/id/OIP.FW_1Z5ZQ3Su0KU82BZD4iwHaER?rs=1&pid=ImgDetMain'),
    ('Pechuga de Pollo', 900.00, 60, 'https://th.bing.com/th/id/OIP.sa9AuTTEMGvan5I0XVFuwQHaE8?rs=1&pid=ImgDetMain'),
    ('Tarta de Jamón y Queso', 800.00, 25, 'https://th.bing.com/th/id/OIP.xIHcCZ0nWxITbZhX63700wHaD8?rs=1&pid=ImgDetMain'),
    ('Cerveza Artesanal', 350.00, 70, 'https://th.bing.com/th/id/OIP.cm7k8KVwQDuFE8l_t-N7PQHaE8?rs=1&pid=ImgDetMain'),
    ('Vino Malbec', 1200.00, 35, 'https://th.bing.com/th/id/OIP.qr01NA164aiZzfnOh8pXHQHaIa?rs=1&pid=ImgDetMain');
GO

INSERT INTO Usuarios (Nombre, Apellido, Imagen, Contraseña, IdRol, Correo, DNI)
VALUES 
('Luis', 'Ramírez', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58495.jpg', '1234abcd', 2, 'luis@mail.com', '67890123'),
('Sofía', 'García', 'https://img.freepik.com/vector-premium/perfil-mujer-dibujos-animados_18591-58520.jpg', 'abcd1234', 2, 'sofia@mail.com', '78901234'),
('Martín', 'Vega', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58493.jpg', 'abcd1234', 2, 'martin@mail.com', '89012345'),
('Carla', 'Sánchez', 'https://img.freepik.com/vector-premium/perfil-mujer-dibujos-animados_18591-58513.jpg', '1234abcd', 2, 'carla@mail.com', '90123456'),
('Javier', 'Torres', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58498.jpg', '1234abcd', 2, 'javier@mail.com', '23456789');
GO

-- Insertar datos en la tabla Ventas
INSERT INTO Ventas (IdMesa, Fecha, Total)
VALUES 
    (1, '2024-11-01', 1500.00),
    (2, '2024-11-01', 1200.00),
    (3, '2024-11-02', 900.00),
    (4, '2024-11-02', 1100.00),
    (5, '2024-11-03', 1300.00),
    (6, '2024-11-03', 1800.00);
GO

-- Insertar datos en la tabla Pedidos
INSERT INTO Pedidos (IdMesa, IdUsuario, Estado, FechaInicio, FechaCierre, PrecioTotalMesa)
VALUES 
    (1, 2, 1, '2024-11-01 12:00', '2024-11-01 12:45', 1500.00),
    (2, 3, 1, '2024-11-01 13:00', '2024-11-01 13:40', 1200.00),
    (3, 2, 1, '2024-11-02 14:00', '2024-11-02 14:50', 900.00),
    (4, 4, 1, '2024-11-02 15:00', '2024-11-02 15:30', 1100.00),
    (5, 5, 1, '2024-11-03 16:00', '2024-11-03 16:45', 1300.00),
    (6, 2, 1, '2024-11-03 17:00', '2024-11-03 17:50', 1800.00);
GO

-- Insertar datos en la tabla DetallePedidos
INSERT INTO DetallePedidos (IdPedido, IdInsumo, Cantidad, PrecioUnitario, PrecioTotal)
VALUES 
    (1, 1, 2, 350.00, 700.00), -- Pedido 1: 2 Papas Fritas
    (1, 2, 1, 700.00, 700.00), -- Pedido 1: 1 Hamburguesa
    (2, 3, 1, 1000.00, 1000.00), -- Pedido 2: 1 Pizza Margarita
    (2, 5, 1, 200.00, 200.00), -- Pedido 2: 1 Refresco
    (3, 4, 1, 600.00, 600.00), -- Pedido 3: 1 Ensalada César
    (3, 9, 2, 350.00, 700.00), -- Pedido 3: 2 Cervezas Artesanales
    (4, 6, 1, 1200.00, 1200.00), -- Pedido 4: 1 Carne de Res
    (5, 8, 2, 400.00, 800.00), -- Pedido 5: 2 Tartas de Jamón y Queso
    (6, 10, 1, 1200.00, 1200.00); -- Pedido 6: 1 Vino Malbec
GO

ALTER TABLE Mesas
ADD Estado BIT NOT NULL DEFAULT 0;
GO


ALTER TABLE DetallePedidos
ADD PrecioUnitario DECIMAL(10, 2) NOT NULL,
    PrecioTotal DECIMAL(10, 2) NOT NULL
GO

ALTER TABLE Pedidos
ADD 
	PrecioTotalMesa DECIMAL(10, 2) NOT NULL;
GO