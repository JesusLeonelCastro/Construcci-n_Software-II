-- Crear base de datos EFTIC
CREATE DATABASE EFTIC;
GO

-- Usar la base de datos EFTIC
USE EFTIC;
GO

-- Crear tabla Estados
CREATE TABLE Estados (
    EstadoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Estado VARCHAR(100),
    Descripcion_Estado VARCHAR(255)
);

-- Crear tabla Sede
CREATE TABLE Sede (
    SedeID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Sede VARCHAR(100),
    Descripcion VARCHAR(255)
);

-- Crear tabla Area
CREATE TABLE Area (
    AreaID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Area VARCHAR(100),
    Descripcion_Area VARCHAR(255),
    SedeID INT,
    FOREIGN KEY (SedeID) REFERENCES Sede(SedeID)
);

-- Crear tabla Tipo_Equipo
CREATE TABLE Tipo_Equipo (
    Tipo_EquipoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Tipo_Equipo VARCHAR(100),
    Descripcion_Tipo_Equipo VARCHAR(255)
);

-- Crear tabla Falla
CREATE TABLE Falla (
    FallaID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Falla VARCHAR(100),
    Descripcion_Falla VARCHAR(255)
);

-- Crear tabla O_Actividades
CREATE TABLE O_Actividades (
    O_ActividadesID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_O_Actividad VARCHAR(100),
    Descripcion_O_Actividad VARCHAR(255)
);

-- Crear tabla Usuario
CREATE TABLE Usuario (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    DNI VARCHAR(8),
    Nombre_Usuario VARCHAR(100),
    Apellido_Usuario VARCHAR(100),
    Correo VARCHAR(100),
    Password VARCHAR(100),
    TipoUsuario VARCHAR(100),
    SedeID INT,
    AreaID INT,
    FOREIGN KEY (SedeID) REFERENCES Sede(SedeID),
    FOREIGN KEY (AreaID) REFERENCES Area(AreaID)
);

-- Crear tabla Equipo_Retirado
CREATE TABLE Equipo_Retirado (
    Equipo_RetiradosID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Equipo VARCHAR(100),
    Tipo VARCHAR(100),
    Color VARCHAR(50),
    Serie VARCHAR(50),
    Cod_Patrimonial VARCHAR(100),
    Sub_Tipo VARCHAR(100),
    Modelo VARCHAR(50),
    Marca VARCHAR(50),
    Codigo_Interno VARCHAR(50),
    Observaciones VARCHAR(50),
    Ingreso DATE
);

-- Crear tabla Informes
CREATE TABLE Informes (
    InformeID INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(255),
    Fecha_Solicitud DATE,
    Fecha_Informe DATE,
    Diagnostico VARCHAR(100),
    Solucion_Primaria TEXT,
    Nombre_Equipos VARCHAR(100),
    Tipo VARCHAR(100),
    Color VARCHAR(50),
    Serie VARCHAR(50),
    Cod_Patrimonial VARCHAR(100),
    Sub_Tipo VARCHAR(100),
    Modelo VARCHAR(50),
    Marca VARCHAR(50),
    Codigo_Interno VARCHAR(50),
    Observaciones VARCHAR(100),
    Ingreso DATE,
    EstadoID INT,
    UsuarioID INT,
    Tipo_EquipoID INT,
    AreaID INT,
    SedeID INT,
    FallaID INT,
    O_ActividadesID INT,
    Equipo_RetiradosID INT,
    FOREIGN KEY (EstadoID) REFERENCES Estados(EstadoID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuario(UsuarioID),
    FOREIGN KEY (Tipo_EquipoID) REFERENCES Tipo_Equipo(Tipo_EquipoID),
    FOREIGN KEY (AreaID) REFERENCES Area(AreaID),
    FOREIGN KEY (SedeID) REFERENCES Sede(SedeID),
    FOREIGN KEY (FallaID) REFERENCES Falla(FallaID),
    FOREIGN KEY (O_ActividadesID) REFERENCES O_Actividades(O_ActividadesID),
    FOREIGN KEY (Equipo_RetiradosID) REFERENCES Equipo_Retirado(Equipo_RetiradosID)
);

-- Crear tabla Inventario
CREATE TABLE Inventario (
    InventarioID INT IDENTITY(1,1) PRIMARY KEY,
    Cod_Patrimonial VARCHAR(100),
    Color VARCHAR(50),
    Serie VARCHAR(50),
    Modelo VARCHAR(50),
    Marca VARCHAR(50),
    Codigo_Interno VARCHAR(50),
    direccion_MAC VARCHAR(50),
    Capacidad_Disco VARCHAR(50),
    Capacidad_Ram VARCHAR(50),
    Marca_Procesador VARCHAR(50),
    Direccion_IP VARCHAR(50),
    Sistema_operativo VARCHAR(50),
    Ingreso DATE,
    Tipo_EquipoID INT,
    SedeID INT,
    AreaID INT,
    FOREIGN KEY (Tipo_EquipoID) REFERENCES Tipo_Equipo(Tipo_EquipoID),
    FOREIGN KEY (SedeID) REFERENCES Sede(SedeID),
    FOREIGN KEY (AreaID) REFERENCES Area(AreaID)
);

-- Crear tabla Asignar
CREATE TABLE Asignar (
    AsignarID INT IDENTITY(1,1) PRIMARY KEY,
    AreaID INT,
    UsuarioID INT,
    InventarioID INT,
    FOREIGN KEY (AreaID) REFERENCES Area(AreaID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuario(UsuarioID),
    FOREIGN KEY (InventarioID) REFERENCES Inventario(InventarioID)
);
