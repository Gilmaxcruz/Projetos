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

CREATE TABLE [Categorias] (
    [Id] uniqueidentifier NOT NULL,
    [CategoriaNome] varchar(200) NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Marcas] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(100) NOT NULL,
    CONSTRAINT [PK_Marcas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Movimentos] (
    [Id] uniqueidentifier NOT NULL,
    [MantimentoId] uniqueidentifier NOT NULL,
    [Quantidade] float NOT NULL,
    [TipoMovimento] int NOT NULL,
    CONSTRAINT [PK_Movimentos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TpMantimentos] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(100) NOT NULL,
    [Obrigatorio] bit NOT NULL,
    CONSTRAINT [PK_TpMantimentos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UnidadeMedidas] (
    [Sigla] varchar(100) NOT NULL,
    [Unidade] varchar(60) NOT NULL,
    CONSTRAINT [PK_UnidadeMedidas] PRIMARY KEY ([Sigla])
);
GO

CREATE TABLE [TpMantimentoCategorias] (
    [Id] uniqueidentifier NOT NULL,
    [MantimentoTpId] uniqueidentifier NOT NULL,
    [CategoriaId] uniqueidentifier NOT NULL,
    [CategoriaId1] uniqueidentifier NULL,
    CONSTRAINT [PK_TpMantimentoCategorias] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TpMantimentoCategorias_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TpMantimentoCategorias_Categorias_CategoriaId1] FOREIGN KEY ([CategoriaId1]) REFERENCES [Categorias] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TpMantimentoCategorias_TpMantimentos_MantimentoTpId] FOREIGN KEY ([MantimentoTpId]) REFERENCES [TpMantimentos] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Mantimentos] (
    [Id] uniqueidentifier NOT NULL,
    [TipoMantimentoId] uniqueidentifier NOT NULL,
    [MarcaId] uniqueidentifier NOT NULL,
    [UnidadeSigla] varchar(100) NULL,
    [Validade] datetime2 NOT NULL,
    [Capacidade] varchar(200) NOT NULL,
    [Imagem] varchar(100) NOT NULL,
    [ConteudoAtual] varchar(200) NOT NULL,
    [Estoque] float NOT NULL,
    [EstoqueMin] float NOT NULL,
    CONSTRAINT [PK_Mantimentos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Mantimentos_Marcas_MarcaId] FOREIGN KEY ([MarcaId]) REFERENCES [Marcas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Mantimentos_TpMantimentos_TipoMantimentoId] FOREIGN KEY ([TipoMantimentoId]) REFERENCES [TpMantimentos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Mantimentos_UnidadeMedidas_UnidadeSigla] FOREIGN KEY ([UnidadeSigla]) REFERENCES [UnidadeMedidas] ([Sigla]) ON DELETE NO ACTION
);
GO

CREATE TABLE [MantimentoMovimento] (
    [MantimentosId] uniqueidentifier NOT NULL,
    [MovimentosId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_MantimentoMovimento] PRIMARY KEY ([MantimentosId], [MovimentosId]),
    CONSTRAINT [FK_MantimentoMovimento_Mantimentos_MantimentosId] FOREIGN KEY ([MantimentosId]) REFERENCES [Mantimentos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_MantimentoMovimento_Movimentos_MovimentosId] FOREIGN KEY ([MovimentosId]) REFERENCES [Movimentos] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_MantimentoMovimento_MovimentosId] ON [MantimentoMovimento] ([MovimentosId]);
GO

CREATE INDEX [IX_Mantimentos_MarcaId] ON [Mantimentos] ([MarcaId]);
GO

CREATE INDEX [IX_Mantimentos_TipoMantimentoId] ON [Mantimentos] ([TipoMantimentoId]);
GO

CREATE INDEX [IX_Mantimentos_UnidadeSigla] ON [Mantimentos] ([UnidadeSigla]);
GO

CREATE INDEX [IX_TpMantimentoCategorias_CategoriaId] ON [TpMantimentoCategorias] ([CategoriaId]);
GO

CREATE INDEX [IX_TpMantimentoCategorias_CategoriaId1] ON [TpMantimentoCategorias] ([CategoriaId1]);
GO

CREATE INDEX [IX_TpMantimentoCategorias_MantimentoTpId] ON [TpMantimentoCategorias] ([MantimentoTpId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210809180506_Initialcreate', N'5.0.8');
GO

COMMIT;
GO

