CREATE DATABASE innovativo;
USE innovativo
CREATE TABLE cliente (
    ID int NOT NULL AUTO_INCREMENT,
    NomeFantasia varchar(255) NOT NULL,
    PRIMARY KEY (ID)
);	

CREATE TABLE usuario (
    ID int NOT NULL AUTO_INCREMENT,
    Email varchar(255) NOT NULL,
    Nome varchar(255) NOT NULL,    
    Senha blob not null,
    ClienteId int null,
    PRIMARY KEY (ID),
    foreign key (ClienteId) References cliente(ID)
);	

CREATE TABLE eficaciacanaisrelatorio (
    ID int NOT NULL,
    IdCliente int not null,    
    Descricao varchar(255) NOT NULL,    
    DataInicial DATETIME NOT NULL,
    DataFinal DATETIME NOT NULL,    
    PRIMARY KEY (ID),
    foreign key (IdCliente) References cliente(ID) 
);	

CREATE TABLE eficaciacanalbuscapaga (
    ID int NOT NULL,
    EficaciaCanalID int NOT NULL,
    Visitantes int NOT NULL,
    Leads int NOT NULL,    
    Oportunidades int NOT NULL,        
    Vendas int NOT NULL,            
    PRIMARY KEY (ID),
    foreign key (EficaciaCanalID) References eficaciacanaisrelatorio(ID)    
);	



CREATE TABLE eficaciacanaldireto (
    ID int NOT NULL,
    EficaciaCanalID int NOT NULL,
    Visitantes int NOT NULL,
    Leads int NOT NULL,    
    Oportunidades int NOT NULL,        
    Vendas int NOT NULL,            
    PRIMARY KEY (ID),
    foreign key (EficaciaCanalID) References eficaciacanaisrelatorio(ID)    
);	

CREATE TABLE eficaciacanalemail (
    ID int NOT NULL,
    EficaciaCanalID int NOT NULL,
    Visitantes int NOT NULL,
    Leads int NOT NULL,    
    Oportunidades int NOT NULL,        
    Vendas int NOT NULL,            
    PRIMARY KEY (ID),
    foreign key (EficaciaCanalID) References eficaciacanaisrelatorio(ID)    
);	

CREATE TABLE eficaciacanalorganico (
    ID int NOT NULL,
    EficaciaCanalID int NOT NULL,
    Visitantes int NOT NULL,
    Leads int NOT NULL,    
    Oportunidades int NOT NULL,        
    Vendas int NOT NULL,            
    PRIMARY KEY (ID),
    foreign key (EficaciaCanalID) References eficaciacanaisrelatorio(ID)    
);	

CREATE TABLE eficaciacanalreferencia (
    ID int NOT NULL,
    EficaciaCanalID int NOT NULL,
    Visitantes int NOT NULL,
    Leads int NOT NULL,    
    Oportunidades int NOT NULL,        
    Vendas int NOT NULL,            
    PRIMARY KEY (ID),
    foreign key (EficaciaCanalID) References eficaciacanaisrelatorio(ID)    
);	


drop table eficaciaCanalReferencia
drop table eficaciaCanalOrganico
drop table eficaciaCanalEmail
drop table eficaciaCanalDireto
drop table eficaciaCanalBuscaPaga
drop table eficaciaCanaisRelatorio
drop table usuario
drop table cliente

insert into cliente (NomeFantasia) values ('Customer 1')
insert into usuario (Email,Nome,Senha,ClienteId) values ('andrewsmaia@gmail.com','Andrew','123',null)


select * from usuario
select @senha:=Senha from usuario Where ID=2;
update usuario set Senha=@senha where ID=1;
