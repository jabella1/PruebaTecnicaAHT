--se debe crear la bd primero y cambiar la cadena de conexion en el .env

CREATE TABLE TipoDocumento (
    TipoDocumentoId INT IDENTITY(1,1) PRIMARY KEY,
    Tipo NVARCHAR(50) NOT NULL
);


CREATE TABLE Genero (
    GeneroId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL
);


CREATE TABLE Paciente (
    PacienteId INT IDENTITY(1,1) PRIMARY KEY,
    TipoDocumentoId INT NOT NULL,
    NumeroDocumento NVARCHAR(20) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    CorreoElectronico NVARCHAR(100) NULL,
    GeneroId INT NOT NULL,
    Direccion NVARCHAR(200) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Paciente_TipoDocumento 
        FOREIGN KEY (TipoDocumentoId) REFERENCES TipoDocumento(TipoDocumentoId),

    CONSTRAINT FK_Paciente_Genero 
        FOREIGN KEY (GeneroId) REFERENCES Genero(GeneroId)
);


INSERT INTO TipoDocumento (Tipo)
VALUES ('Cedula');

INSERT INTO Genero (Nombre)
VALUES ('Masculino');


ALTER TABLE Paciente
ADD CONSTRAINT UQ_Paciente_Documento UNIQUE (NumeroDocumento);