create table articulo (
	id bigint auto_increment primary key,
	nombre varchar (50) not null unique,
	categoria bigint,
	precio decimal(10,2)
);

create table categoria (
	id bigint auto_increment primary key,
	nombre varchar (50) not null unique
);

create table cliente (
	id bigint auto_increment primary key,
	nombre varchar (50) not null unique
);

create table pedido (
	id bigint auto_increment primary key,
	cliente bigint not null,
	fecha datetime not null,
	importe decimal(10,2)
);

create table pedidolinea (
	id bigint auto_increment primary key,
	pedido bigint not null,
	articulo bigint not null,
	precio decimal(10,2),
	unidades decimal(10,2),
	importe decimal(10,2)
);

alter table articulo add foreing key (categoria) references categoria (id);
alter table pedido add foreing key (cliente) references cliente (id);
alter table pedidolinea add foreing key (pedido) references pedido (id) on delete cascade;
alter table pedidolinea add foreing key (articulo) references pedido (id);