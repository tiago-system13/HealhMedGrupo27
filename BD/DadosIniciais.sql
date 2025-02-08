USE [HealthMedDB]
GO

INSERT INTO [dbo].[Medicos]
           ([Nome]
           ,[Telefone]
           ,[Email]
           ,[CPF]
           ,[CRM]
           ,[ValorConsulta]
           ,[Especialidade])
     VALUES
           ('Gregory House',
           '79985854687',
           'house.md@heathmed.com.br',
           '16270839017',
           '184666',
           1000,
		   'Neurocirurgia')
GO

INSERT INTO [dbo].[Pacientes]
           ([Nome]
           ,[Telefone]
           ,[Email]
           ,[CPF])
     VALUES
           ('James Hunt',
            '81988666986',
            'jim.hunt@hesketh.com',
            '75626987088')
GO


INSERT INTO [dbo].[Usuarios]
           ([Senha])
     VALUES
           ('8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92')
GO


INSERT INTO [dbo].[Usuarios]
           ([Senha])
     VALUES
           ('8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92')
GO

INSERT INTO [dbo].[UsuarioMedico]
           ([MedicosIdMedico]
           ,[UsuariosIdUsuario])
     VALUES
           ((SELECT TOP 1 IdMedico FROM Medicos ORDER BY 1 DESC),
           (SELECT TOP 1 IdUsuario FROM Usuarios ORDER BY 1 ASC))
GO

INSERT INTO [dbo].[UsuarioPaciente]
           ([PacientesIdPaciente]
           ,[UsuariosIdUsuario])
     VALUES
           ((SELECT TOP 1 IdPaciente FROM Pacientes ORDER BY 1 DESC),
           (SELECT TOP 1 IdUsuario FROM Usuarios ORDER BY 1 DESC))
GO