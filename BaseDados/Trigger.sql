Create trigger atualizar on Classificacao
after Insert
AS BEGIN
	declare @media float;
	declare @id int;

	select @id = prato from inserted;

	select @media = round(avg(Cast (classificacao as float)),1) from Classificacao
	where Classificacao.prato= @id;
	
	update Prato
	set classificacao = @media
	where idPrato = @id;

END

drop trigger atualizar;

select * from Prato;

INSERT INTO [dbo].[Classificacao]
           ([cliente]
           ,[prato]
           ,[classificacao]
           ,[comentario])
     VALUES
           ('Bruno'
           ,39
           ,1
           ,'Nao aconselho')

