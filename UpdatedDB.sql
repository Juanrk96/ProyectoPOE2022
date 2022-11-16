create database sivarviajes
use sivarviajes

create table vuelo (
	idVuelo int Identity(1,1) Primary key,
	aerolinea varchar(60),
	origen varchar(60),
	destino varchar (60),
	fecha date,
	hora_salida datetime,
	hora_llegada datetime,
	asientos int,
	costoVuelo float
)

create table reservacion (
	idReservacion int Identity(1,1) Primary key,
	fecha date,
	idVuelo int FOREIGN KEY REFERENCES vuelo(idVuelo),
	cantReservada int	
)

create table pasajero (
	idPasajero int Identity(1,1) Primary key,
	nombre varchar(100),
	direccion varchar (100),
	telefono varchar (12),
	email varchar(100)
)

create table detalle_reservacion(
	idDetalle int Identity(1,1) Primary key,
	idReservacion int FOREIGN KEY REFERENCES reservacion(idReservacion),
	idPasajero int FOREIGN KEY REFERENCES pasajero(idPasajero)
)