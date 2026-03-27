

CREATE DATABASE SteamcloneBD;
GO

USE SteamcloneBD;
GO

--Tabla Desarrolladores
CREATE TABLE Desarrolladores (
    DesarrolladorID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

--Tabla de Editores
CREATE TABLE Editores (
    EditorID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

--Tabla de Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    UusuarioNombre NVARCHAR(50) UNIQUE NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Pass NVARCHAR(255) NOT NULL,
    Estado NVARCHAR(20) DEFAULT 'offline'
        CHECK (Estado IN ('online','playing','offline')),
    created_at DATETIME2 DEFAULT SYSDATETIME()
);
GO

--Tabla Juegos
CREATE TABLE Games (
    GameID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Description NVARCHAR(255),
    Precio DECIMAL(10,2) NOT NULL,
    FechaCompra DATE,
    DesarrolladorID INT NOT NULL,
    EditorID INT,
    created_at DATETIME2 DEFAULT SYSDATETIME()

    FOREIGN KEY (DesarrolladorID) REFERENCES Desarrolladores(DesarrolladorID),
    FOREIGN KEY (EditorID) REFERENCES Editores(EditorID),

);
GO

--Tabla compras 
CREATE TABLE UsuarioGames (
    UsuarioID INT,
    GameID INT,
    purchase_price DECIMAL(10,2),
    purchase_date DATETIME2 DEFAULT SYSDATETIME(),

    PRIMARY KEY (UsuarioID, GameID),

    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (GameID) REFERENCES Games(GameID)
);
GO

--Tabla de logins
CREATE TABLE Game_Sessions (
    SessionID INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioID INT,
    GameID INT,
    start_time DATETIME2,
    end_time DATETIME2,

    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (GameID) REFERENCES Games(GameID)
);
GO

--Tabla Generos de los juegos 
CREATE TABLE Generos (
    GeneroID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE GenerosGames (
    GameID INT,
    GeneroID INT,

    PRIMARY KEY (GameID, GeneroID),

    FOREIGN KEY (GameID) REFERENCES Games(GameID),
    FOREIGN KEY (GeneroID) REFERENCES Generos(GeneroID)
);
GO

--Tabla de librerias
CREATE TABLE Libreria (
    LibreriaID INT IDENTITY(1,1) PRIMARY KEY,
    GameID INT,
    Name NVARCHAR(100),
    Description NVARCHAR,

    FOREIGN KEY (GameId) REFERENCES Games(GameID)
);
GO

--Tabla de Usiarioslibrerias
CREATE TABLE UsuarioLibreria (
    UsuarioID INT,
    LibreriaID INT,
    CreateDate DATETIME2 DEFAULT SYSDATETIME(),

    PRIMARY KEY (UsuarioID, LibreriaID),

    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (LibreriaID) REFERENCES Libreria(LibreriaID)
);
GO

--Tabla Amigos
CREATE TABLE Friends (
    UsuarioID INT,
    FriendID INT,
    created_at DATETIME2 DEFAULT SYSDATETIME(),

    PRIMARY KEY (UsuarioID, FriendID),

    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (FriendID) REFERENCES UsuarioS(UsuarioID)
);
GO