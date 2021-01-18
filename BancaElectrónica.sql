/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     10/01/2021 22:05:42                          */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO')
            and   type = 'U')
   drop table USUARIO
go

/*==============================================================*/
/* Table: USUARIO                                               */
/*==============================================================*/
create table USUARIO (
   ID_USUARIO           int                  identity(1,1)	not null,
   ID_CLIENTE           int                  null,
   CEDULA_CLIENTE       char(10)             null,
   NOMBRE_USUARIO       varchar(32)             null,
   PASSWORD_USUARIO     varchar(128)             null,
   CAMBIO_USUARIO       int                  null,
   constraint PK_USUARIO primary key (ID_USUARIO)
)
go

