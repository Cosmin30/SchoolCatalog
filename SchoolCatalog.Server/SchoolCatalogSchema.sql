IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Clase] (
    [IdClasa] int NOT NULL IDENTITY,
    [NumeClasa] nvarchar(20) NOT NULL,
    [ProfilClasa] nvarchar(50) NOT NULL,
    [IdOrar] int NULL,
    CONSTRAINT [PK_Clase] PRIMARY KEY ([IdClasa])
);

CREATE TABLE [Profesori] (
    [IdProfesor] int NOT NULL IDENTITY,
    [NumeProfesor] nvarchar(100) NOT NULL,
    [PrenumeProfesor] nvarchar(100) NOT NULL,
    [EmailProfesor] nvarchar(max) NOT NULL,
    [DataNasterii] datetime2 NOT NULL,
    CONSTRAINT [PK_Profesori] PRIMARY KEY ([IdProfesor])
);

CREATE TABLE [Elevi] (
    [IdElev] int NOT NULL IDENTITY,
    [NumeElev] nvarchar(50) NOT NULL,
    [PrenumeElev] nvarchar(50) NOT NULL,
    [DataNasterii] datetime2 NOT NULL,
    [ClasaId] int NULL,
    CONSTRAINT [PK_Elevi] PRIMARY KEY ([IdElev]),
    CONSTRAINT [FK_Elevi_Clase_ClasaId] FOREIGN KEY ([ClasaId]) REFERENCES [Clase] ([IdClasa]) ON DELETE CASCADE
);

CREATE TABLE [Orare] (
    [IdOrar] int NOT NULL IDENTITY,
    [DescriereOrar] nvarchar(max) NULL,
    [AnScolar] nvarchar(max) NULL,
    [IdClasa] int NOT NULL,
    CONSTRAINT [PK_Orare] PRIMARY KEY ([IdOrar]),
    CONSTRAINT [FK_Orare_Clase_IdClasa] FOREIGN KEY ([IdClasa]) REFERENCES [Clase] ([IdClasa]) ON DELETE CASCADE
);

CREATE TABLE [ClasaProfesor] (
    [ClaseIdClasa] int NOT NULL,
    [ProfesoriIdProfesor] int NOT NULL,
    CONSTRAINT [PK_ClasaProfesor] PRIMARY KEY ([ClaseIdClasa], [ProfesoriIdProfesor]),
    CONSTRAINT [FK_ClasaProfesor_Clase_ClaseIdClasa] FOREIGN KEY ([ClaseIdClasa]) REFERENCES [Clase] ([IdClasa]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClasaProfesor_Profesori_ProfesoriIdProfesor] FOREIGN KEY ([ProfesoriIdProfesor]) REFERENCES [Profesori] ([IdProfesor]) ON DELETE CASCADE
);

CREATE TABLE [Materii] (
    [IdMaterie] int NOT NULL IDENTITY,
    [NumeMaterie] nvarchar(100) NOT NULL,
    [ProfesorId] int NULL,
    CONSTRAINT [PK_Materii] PRIMARY KEY ([IdMaterie]),
    CONSTRAINT [FK_Materii_Profesori_ProfesorId] FOREIGN KEY ([ProfesorId]) REFERENCES [Profesori] ([IdProfesor]) ON DELETE NO ACTION
);

CREATE TABLE [Users] (
    [IdUser] int NOT NULL IDENTITY,
    [Email] nvarchar(max) NOT NULL,
    [Parola] nvarchar(100) NOT NULL,
    [Rol] nvarchar(max) NOT NULL,
    [IdElev] int NULL,
    [IdProfesor] int NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([IdUser]),
    CONSTRAINT [FK_Users_Elevi_IdElev] FOREIGN KEY ([IdElev]) REFERENCES [Elevi] ([IdElev]),
    CONSTRAINT [FK_Users_Profesori_IdProfesor] FOREIGN KEY ([IdProfesor]) REFERENCES [Profesori] ([IdProfesor])
);

CREATE TABLE [ClasaMaterie] (
    [ClaseIdClasa] int NOT NULL,
    [MateriiIdMaterie] int NOT NULL,
    CONSTRAINT [PK_ClasaMaterie] PRIMARY KEY ([ClaseIdClasa], [MateriiIdMaterie]),
    CONSTRAINT [FK_ClasaMaterie_Clase_ClaseIdClasa] FOREIGN KEY ([ClaseIdClasa]) REFERENCES [Clase] ([IdClasa]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClasaMaterie_Materii_MateriiIdMaterie] FOREIGN KEY ([MateriiIdMaterie]) REFERENCES [Materii] ([IdMaterie]) ON DELETE CASCADE
);

CREATE TABLE [Medii] (
    [IdMedie] int NOT NULL IDENTITY,
    [IdElev] int NOT NULL,
    [IdMaterie] int NOT NULL,
    [Valoare] float NOT NULL,
    CONSTRAINT [PK_Medii] PRIMARY KEY ([IdMedie]),
    CONSTRAINT [FK_Medii_Elevi_IdElev] FOREIGN KEY ([IdElev]) REFERENCES [Elevi] ([IdElev]) ON DELETE CASCADE,
    CONSTRAINT [FK_Medii_Materii_IdMaterie] FOREIGN KEY ([IdMaterie]) REFERENCES [Materii] ([IdMaterie]) ON DELETE CASCADE
);

CREATE TABLE [Note] (
    [IdNota] int NOT NULL IDENTITY,
    [Valoare] int NOT NULL,
    [DataNotei] datetime2 NOT NULL,
    [EsteAnulata] bit NOT NULL,
    [IdElev] int NOT NULL,
    [IdMaterie] int NOT NULL,
    CONSTRAINT [PK_Note] PRIMARY KEY ([IdNota]),
    CONSTRAINT [FK_Note_Elevi_IdElev] FOREIGN KEY ([IdElev]) REFERENCES [Elevi] ([IdElev]) ON DELETE CASCADE,
    CONSTRAINT [FK_Note_Materii_IdMaterie] FOREIGN KEY ([IdMaterie]) REFERENCES [Materii] ([IdMaterie]) ON DELETE CASCADE
);

CREATE TABLE [OrarItems] (
    [IdOrarItem] int NOT NULL IDENTITY,
    [ZiSaptamana] nvarchar(10) NOT NULL,
    [OraInceput] nvarchar(5) NOT NULL,
    [OraSfarsit] nvarchar(5) NOT NULL,
    [IdMaterie] int NOT NULL,
    [IdProfesor] int NOT NULL,
    [IdOrar] int NOT NULL,
    CONSTRAINT [PK_OrarItems] PRIMARY KEY ([IdOrarItem]),
    CONSTRAINT [FK_OrarItems_Materii_IdMaterie] FOREIGN KEY ([IdMaterie]) REFERENCES [Materii] ([IdMaterie]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrarItems_Orare_IdOrar] FOREIGN KEY ([IdOrar]) REFERENCES [Orare] ([IdOrar]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrarItems_Profesori_IdProfesor] FOREIGN KEY ([IdProfesor]) REFERENCES [Profesori] ([IdProfesor]) ON DELETE CASCADE
);

CREATE TABLE [Teme] (
    [IdTema] int NOT NULL IDENTITY,
    [Descriere] nvarchar(max) NULL,
    [TermenLimita] datetime2 NOT NULL,
    [IdMaterie] int NOT NULL,
    [IdClasa] int NOT NULL,
    CONSTRAINT [PK_Teme] PRIMARY KEY ([IdTema]),
    CONSTRAINT [FK_Teme_Clase_IdClasa] FOREIGN KEY ([IdClasa]) REFERENCES [Clase] ([IdClasa]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Teme_Materii_IdMaterie] FOREIGN KEY ([IdMaterie]) REFERENCES [Materii] ([IdMaterie]) ON DELETE CASCADE
);

CREATE TABLE [FisiereTema] (
    [IdFisier] int NOT NULL IDENTITY,
    [NumeFisier] nvarchar(255) NOT NULL,
    [UrlFisier] nvarchar(max) NOT NULL,
    [IdElev] int NOT NULL,
    [TemaId] int NOT NULL,
    [DataIncarcare] datetime2 NOT NULL,
    CONSTRAINT [PK_FisiereTema] PRIMARY KEY ([IdFisier]),
    CONSTRAINT [FK_FisiereTema_Elevi_IdElev] FOREIGN KEY ([IdElev]) REFERENCES [Elevi] ([IdElev]) ON DELETE CASCADE,
    CONSTRAINT [FK_FisiereTema_Teme_TemaId] FOREIGN KEY ([TemaId]) REFERENCES [Teme] ([IdTema]) ON DELETE CASCADE
);

CREATE INDEX [IX_ClasaMaterie_MateriiIdMaterie] ON [ClasaMaterie] ([MateriiIdMaterie]);

CREATE INDEX [IX_ClasaProfesor_ProfesoriIdProfesor] ON [ClasaProfesor] ([ProfesoriIdProfesor]);

CREATE INDEX [IX_Elevi_ClasaId] ON [Elevi] ([ClasaId]);

CREATE INDEX [IX_FisiereTema_IdElev] ON [FisiereTema] ([IdElev]);

CREATE INDEX [IX_FisiereTema_TemaId] ON [FisiereTema] ([TemaId]);

CREATE INDEX [IX_Materii_ProfesorId] ON [Materii] ([ProfesorId]);

CREATE INDEX [IX_Medii_IdElev] ON [Medii] ([IdElev]);

CREATE INDEX [IX_Medii_IdMaterie] ON [Medii] ([IdMaterie]);

CREATE INDEX [IX_Note_IdElev] ON [Note] ([IdElev]);

CREATE INDEX [IX_Note_IdMaterie] ON [Note] ([IdMaterie]);

CREATE UNIQUE INDEX [IX_Orare_IdClasa] ON [Orare] ([IdClasa]);

CREATE INDEX [IX_OrarItems_IdMaterie] ON [OrarItems] ([IdMaterie]);

CREATE INDEX [IX_OrarItems_IdOrar] ON [OrarItems] ([IdOrar]);

CREATE INDEX [IX_OrarItems_IdProfesor] ON [OrarItems] ([IdProfesor]);

CREATE INDEX [IX_Teme_IdClasa] ON [Teme] ([IdClasa]);

CREATE INDEX [IX_Teme_IdMaterie] ON [Teme] ([IdMaterie]);

CREATE INDEX [IX_Users_IdElev] ON [Users] ([IdElev]);

CREATE INDEX [IX_Users_IdProfesor] ON [Users] ([IdProfesor]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250717110927_InitialCreate', N'9.0.7');

COMMIT;
GO

