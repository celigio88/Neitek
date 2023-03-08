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
GO

CREATE TABLE [dsMetas] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(80) NOT NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [TotalTareas] int NOT NULL,
    [PorcentajeTarea] float NOT NULL,
    CONSTRAINT [PK_dsMetas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dsTareas] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(80) NOT NULL,
    [FechaCreada] datetime2 NOT NULL,
    [Abierta] bit NOT NULL,
    [MetaId] int NOT NULL,
    CONSTRAINT [PK_dsTareas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_dsTareas_dsMetas_MetaId] FOREIGN KEY ([MetaId]) REFERENCES [dsMetas] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_dsTareas_MetaId] ON [dsTareas] ([MetaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230307071622_PrimeraMigracion', N'7.0.3');
GO

COMMIT;
GO

