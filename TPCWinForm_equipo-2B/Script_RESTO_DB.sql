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
    ('Papas fritas', 350.00, 100, 'https://example.com/images/papas-fritas.jpg'),
    ('Hamburguesa', 700.00, 50, 'https://example.com/images/hamburguesa.jpg'),
    ('Pizza Margarita', 1000.00, 30, 'https://example.com/images/pizza-margarita.jpg'),
    ('Ensalada César', 600.00, 20, 'https://example.com/images/ensalada-cesar.jpg'),
    ('Refresco', 200.00, 150, 'https://example.com/images/refresco.jpg'),
    ('Carne de Res', 1200.00, 40, 'https://example.com/images/carne-res.jpg'),
    ('Pechuga de Pollo', 900.00, 60, 'https://example.com/images/pechuga-pollo.jpg'),
    ('Tarta de Jamón y Queso', 800.00, 25, 'https://example.com/images/tarta-jamon-queso.jpg'),
    ('Cerveza Artesanal', 350.00, 70, 'https://example.com/images/cerveza-artesanal.jpg'),
    ('Vino Malbec', 1200.00, 35, 'https://example.com/images/vino-malbec.jpg');
GO

INSERT INTO Usuarios (Nombre, Apellido, Imagen, Contraseña, IdRol, Correo, DNI)
VALUES 
('Luis', 'Ramírez', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58495.jpg', '1234abcd', 2, 'luis@mail.com', '67890123'),
('Sofía', 'García', 'https://img.freepik.com/vector-premium/perfil-mujer-dibujos-animados_18591-58520.jpg', 'abcd1234', 2, 'sofia@mail.com', '78901234'),
('Martín', 'Vega', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58493.jpg', 'abcd1234', 2, 'martin@mail.com', '89012345'),
('Carla', 'Sánchez', 'https://img.freepik.com/vector-premium/perfil-mujer-dibujos-animados_18591-58513.jpg', '1234abcd', 2, 'carla@mail.com', '90123456'),
('Javier', 'Torres', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58498.jpg', '1234abcd', 2, 'javier@mail.com', '23456789');
GO


ALTER TABLE Mesas
ADD Estado BIT NOT NULL DEFAULT 0;
GO


ALTER TABLE DetallePedidos
ADD PrecioUnitario DECIMAL(10, 2) NOT NULL,
    PrecioTotal DECIMAL(10, 2) NOT NULL;
GO

