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
    Contrase�a VARCHAR(100) NOT NULL,
    IdRol SMALLINT NOT NULL,
    Correo VARCHAR(100) NOT NULL,
    DNI VARCHAR(25) NOT NULL,
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol)
);
GO

-- Crear tabla Mesas
CREATE TABLE Mesas (
    IdMesa SMALLINT PRIMARY KEY IDENTITY(1,1),
    NumeroMesa INT NOT NULL,
	Estado BIT NOT NULL DEFAULT 0
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
	PrecioTotalMesa DECIMAL(10, 2),
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
	ImagenURL VARCHAR(255) NULL,
    Stock INT NOT NULL
);
GO

-- Crear tabla DetallePedidos
CREATE TABLE DetallePedidos (
    IdDetalle INT PRIMARY KEY IDENTITY(1,1),
    IdPedido INT NOT NULL,
    IdInsumo INT NOT NULL,
    Cantidad INT NOT NULL,
	PrecioUnitario DECIMAL(10, 2) NOT NULL,
	PrecioTotal DECIMAL(10, 2) NOT NULL
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

INSERT INTO Usuarios (Nombre, Apellido, Imagen, Contrase�a, IdRol, Correo, DNI)
VALUES 
('Juan', 'P�rez', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58482.jpg', '1234', 3, 'test@mail.com', '12345678'),
('Ana', 'G�mez', 'https://img.freepik.com/vector-premium/perfil-mujer-dibujos-animados_18591-58530.jpg', 'abcd1234', 2, 'ana@mail.com', '23456789'),
('Carlos', 'Mart�nez', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58490.jpg', '1234abcd', 2, 'carlos@mail.com', '34567890'),
('Marta', 'Fern�ndez', 'https://img.freepik.com/vector-premium/perfil-mujer-dibujos-animados_18591-58531.jpg', 'abcd1234', 1, 'marta@mail.com', '45678901'),
('Pedro', 'L�pez', 'https://img.freepik.com/vector-premium/perfil-hombre-dibujos-animados_18591-58512.jpg', '1234abcd', 3, 'pedro@mail.com', '56789012');
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
    ('Ensalada C�sar', 600.00, 20, 'https://th.bing.com/th/id/OIP.xKBJXVYTf7YE9vU-jIHZPwHaE8?rs=1&pid=ImgDetMain'),
    ('Refresco', 200.00, 150, 'https://res.cloudinary.com/walmart-labs/image/upload/w_960,dpr_auto,f_auto,q_auto:best/gr/images/product-images/img_large/00750103131164L.jpg'),
    ('Carne de Res', 1200.00, 40, 'https://th.bing.com/th/id/OIP.FW_1Z5ZQ3Su0KU82BZD4iwHaER?rs=1&pid=ImgDetMain'),
    ('Pechuga de Pollo', 900.00, 60, 'https://th.bing.com/th/id/OIP.sa9AuTTEMGvan5I0XVFuwQHaE8?rs=1&pid=ImgDetMain'),
    ('Tarta de Jam�n y Queso', 800.00, 25, 'https://th.bing.com/th/id/OIP.xIHcCZ0nWxITbZhX63700wHaD8?rs=1&pid=ImgDetMain'),
    ('Cerveza Artesanal', 350.00, 70, 'https://th.bing.com/th/id/OIP.cm7k8KVwQDuFE8l_t-N7PQHaE8?rs=1&pid=ImgDetMain'),
    ('Vino Malbec', 1200.00, 35, 'https://th.bing.com/th/id/OIP.qr01NA164aiZzfnOh8pXHQHaIa?rs=1&pid=ImgDetMain');
GO