USE master;
GO

-- Crear base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SteamClone')
    CREATE DATABASE SteamClone;
GO


-- Usar base de datos
USE SteamClone;
GO 

-- =========================
-- Tabla: Developers
-- =========================
CREATE TABLE Developers (
    developer_id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    country NVARCHAR(100),
    website NVARCHAR(255)
);
GO 

-- =========================
-- Tabla: Developers
-- =========================
CREATE TABLE Editores (
    editorID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

-- =========================
-- Tabla: Users
-- =========================
CREATE TABLE Users (
    user_id INT IDENTITY PRIMARY KEY,
    username NVARCHAR(50) UNIQUE NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    password_hash NVARCHAR(255) NOT NULL,
    country NVARCHAR(100),
    created_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    last_login DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
GO 

-- =========================
-- Tabla: Games
-- =========================
CREATE TABLE Games (
    game_id INT IDENTITY PRIMARY KEY,
    title NVARCHAR(150) NOT NULL,
    description TEXT,
    price DECIMAL(10,2) DEFAULT 0,
    release_date DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    developer_id INT,
    publisher_id INT,

    FOREIGN KEY (developer_id) REFERENCES Developers(developer_id)
);
GO 

-- =========================
-- Tabla: Games Sessions
-- =========================
CREATE TABLE Game_Sessions (
    sessionID INT IDENTITY(1,1) PRIMARY KEY,
    usuarioID INT,
    gameID INT,
    start_time DATETIME2,
    end_time DATETIME2,

    FOREIGN KEY (UsuarioID) REFERENCES Users(user_id),
    FOREIGN KEY (GameID) REFERENCES Games(game_id)
);
GO

-- =========================
-- Tabla: Geners
-- =========================
CREATE TABLE Geners (
    gener_id INT IDENTITY(1,1) PRIMARY KEY,
    descripcion NVARCHAR(50) NOT NULL
);
GO


-- =========================
-- Tabla: GenersGames
-- =========================
CREATE TABLE GenerGame (
    game_id INT,
    gener_id INT,

    PRIMARY KEY (game_id, gener_id),

    FOREIGN KEY (game_id) REFERENCES Games(game_id),
    FOREIGN KEY (gener_id) REFERENCES Geners(gener_id)
);
GO


-- =========================
-- Tabla: User_Games 
-- =========================
CREATE TABLE User_Games(
    user_id INT,
    game_id INT,
    purchase_date DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    playtime_hours INT DEFAULT 0,

	PRIMARY KEY (user_id, game_id),

    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (game_id) REFERENCES Games(game_id)
);
GO 

-- =========================
-- Tabla: Friends
-- =========================
CREATE TABLE Friends (
    user_id INT,
    friend_id INT,
    status NVARCHAR(20),
    created_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    PRIMARY KEY (user_id, friend_id),

    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (friend_id) REFERENCES Users(user_id)
);
GO 

-- =========================
-- Tabla: Achievements
-- =========================
CREATE TABLE Achievements (
    achievement_id INT IDENTITY PRIMARY KEY,
    game_id INT,
    title NVARCHAR(150),
    description TEXT,

    FOREIGN KEY (game_id) REFERENCES Games(game_id)
);
GO 

-- =========================
-- Tabla: User Achievements
-- =========================
CREATE TABLE User_Achievements (
    user_id INT,
    achievement_id INT,
    unlocked_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    PRIMARY KEY (user_id, achievement_id),

    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (achievement_id) REFERENCES Achievements(achievement_id)
);
GO 

-- =========================
-- Tabla: Reviews
-- =========================
CREATE TABLE Reviews (
    review_id INT IDENTITY PRIMARY KEY,
    user_id INT,
    game_id INT,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comment TEXT,
    created_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

	CONSTRAINT UQ_User_Game UNIQUE (user_id, game_id),

    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (game_id) REFERENCES Games(game_id)
);
GO 

-- =========================================
-- Tabla: Review_Comments
-- =========================================
CREATE TABLE Review_Comments (
    comment_id INT IDENTITY(1,1) PRIMARY KEY,
    review_id INT,
    user_id INT,
    comment TEXT,
    created_at DATETIME2 DEFAULT SYSDATETIME(),

    FOREIGN KEY (review_id) REFERENCES Reviews(review_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);
GO


-- =========================================
-- WISHLIST
-- =========================================
CREATE TABLE Wishlist (
    user_id INT,
    game_id INT,

    PRIMARY KEY (user_id, game_id),

    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (game_id) REFERENCES Games(game_id)
);
GO

-- =========================================
-- OFFERS
-- =========================================
CREATE TABLE Offers (
    offer_id INT IDENTITY(1,1) PRIMARY KEY,
    game_id INT,
    discount_pct DECIMAL(5,2),
    start_date DATETIME2,
    end_date DATETIME2,

    FOREIGN KEY (game_id) REFERENCES Games(game_id)
);
GO

-- =========================
-- Inserts
-- =========================

-- 1️ Developers
INSERT INTO Developers (name, country, website)
VALUES ('Valve', 'USA', 'https://www.valvesoftware.com');
-- 2️ Otro Developer
INSERT INTO Developers (name, country, website)
VALUES ('Rockstar Games', 'USA', 'https://www.rockstargames.com');
GO 

-- 3️ Users
INSERT INTO Users (username, email, password_hash, country)
VALUES ('player1', 'player1@email.com', 'hash123', 'Ecuador');
-- 4️ Otro User
INSERT INTO Users (username, email, password_hash, country)
VALUES ('player2', 'player2@email.com', 'hash456', 'Colombia');
GO

-- 5️ Game
INSERT INTO Games (title, description, price, release_date, developer_id)
VALUES ('Counter Strike Clone', 'Shooter competitivo', 0.00, '2023-09-01', 1);
-- 6️ Otro Game
INSERT INTO Games (title, description, price, release_date, developer_id)
VALUES ('Open World Crime', 'Juego de mundo abierto', 59.99, '2020-05-10', 2);
GO

-- 7️ User Library (player1 compra juego 1)
INSERT INTO User_Game (user_id, game_id, playtime_hours)
VALUES (1, 1, 120);
GO

-- 8️ Friends
INSERT INTO Friends (user_id, friend_id, status)
VALUES (1, 2, 'accepted');
GO

-- 9️ Achievement
INSERT INTO Achievements (game_id, title, description)
VALUES (1, 'First Win', 'Ganar tu primera partida');
GO

-- 10 Review
INSERT INTO Reviews (user_id, game_id, rating, comment)
VALUES (1, 1, 5, 'Excelente juego!');
GO