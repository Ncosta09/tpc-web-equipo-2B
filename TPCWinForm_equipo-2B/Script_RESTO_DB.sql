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
