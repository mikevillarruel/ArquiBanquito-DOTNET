/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     09/01/2021 1:03:48                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CUENTA') and o.name = 'FK_CUENTA_CLI_CUEN_CLIENTE')
alter table CUENTA
   drop constraint FK_CUENTA_CLI_CUEN_CLIENTE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MOVIMIENTO') and o.name = 'FK_MOVIMIEN_CUEN_MOV_CUENTA')
alter table MOVIMIENTO
   drop constraint FK_MOVIMIEN_CUEN_MOV_CUENTA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLIENTE')
            and   type = 'U')
   drop table CLIENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CUENTA')
            and   type = 'U')
   drop table CUENTA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MOVIMIENTO')
            and   type = 'U')
   drop table MOVIMIENTO
go

/*==============================================================*/
/* Table: CLIENTE                                               */
/*==============================================================*/
create table CLIENTE (
   ID_CLIENTE           int                  identity(1,1)	not null,
   NOMBRES_CLIENTE      varchar(40)             null,
   APELLIDOS_CLIENTE    varchar(40)             null,
   CEDULA_CLIENTE       char(10)             null,
   DIRECCION_CLIENTE    varchar(120)            null,
   TELEFONO_CLIENTE     varchar(10)             null,
   CORREO_CLIENTE       varchar(128)            null,
   constraint PK_CLIENTE primary key (ID_CLIENTE)
)
go

/*==============================================================*/
/* Table: CUENTA                                                */
/*==============================================================*/
create table CUENTA (
   ID_CUENTA            int                  identity(1,1)	not null,
   ID_CLIENTE           int                  not null,
   NRO_CUENTA           varchar(8)              null,
   TIPO_CUENTA          varchar(10)             null,
   SALDO_CUENTA         decimal(12,2)        null,
   constraint PK_CUENTA primary key (ID_CUENTA)
)
go

/*==============================================================*/
/* Table: MOVIMIENTO                                            */
/*==============================================================*/
create table MOVIMIENTO (
   ID_MOVIMIENTO        int                  identity(1,1)	not null,
   ID_CUENTA            int                  not null,
   FECHA_MOVIMIENTO     datetime             null,
   TIPO_MOVIMIENTO      varchar(10)             null,
   IMPORTE_MOVIMIENTO   decimal(12,2)        null,
   DESTINO_MOVIMIENTO   varchar(8)              null,
   SALDO_MOVIMIENTO     decimal(12,2)        null,
   constraint PK_MOVIMIENTO primary key (ID_MOVIMIENTO)
)
go

alter table CUENTA
   add constraint FK_CUENTA_CLI_CUEN_CLIENTE foreign key (ID_CLIENTE)
      references CLIENTE (ID_CLIENTE)
go

alter table MOVIMIENTO
   add constraint FK_MOVIMIEN_CUEN_MOV_CUENTA foreign key (ID_CUENTA)
      references CUENTA (ID_CUENTA)
go

