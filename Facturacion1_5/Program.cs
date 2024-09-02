using Facturacion1_5.Dominio;
using Facturacion1_5.Sevicios;

GestorFacturas GFacturas = new GestorFacturas();
GestorArticulos GArticulos = new GestorArticulos();

Console.WriteLine("Todas Las facturas");

var ListFacturas = GFacturas.DevolverFacturas();

foreach (var f in ListFacturas)
{
    Console.WriteLine("\nFactura: " + f.ToString());
    int DCant = 1;
    foreach (DetallesFactura d in f.TraerDetalles())
    {
        Console.WriteLine($"Detalle {DCant}: " + d.ToString());
        DCant++;
    }
}

//insertantdo una factura y sus detalles

//obteniendo los articulos
var Articulos = GArticulos.DevolverArticulos();

//creando los detalles
DetallesFactura Detalle1 = new DetallesFactura(){Cantidad = 3,Articulo = Articulos[0]};
DetallesFactura Detalle2 = new DetallesFactura(){Cantidad = 8,Articulo = Articulos[1]};
DetallesFactura Detalle3 = new DetallesFactura(){Cantidad = 10,Articulo = Articulos[2]};
//detalle con mismo aticulo
DetallesFactura Detalle4 = new DetallesFactura(){Cantidad = 12,Articulo = Articulos[1]};

//Creando la factura
Factura oFactura = new Factura()
{
    FormaPago = new FormaPago(){Codigo = 2},
    Cliente = "José"
};
//Agregando los detalles

oFactura.AgregarDetalle(Detalle1);
oFactura.AgregarDetalle(Detalle2);
oFactura.AgregarDetalle(Detalle3);
oFactura.AgregarDetalle(Detalle4);

if (GFacturas.GuardarFactura(oFactura))
{
    Console.WriteLine("\nFactura Insertada con exito");
}
else
{
    Console.WriteLine("Factura no insertada\n");
}

//Mostrando todas las facturas nuevas

ListFacturas = GFacturas.DevolverFacturas();

foreach (var f in ListFacturas)
{
    Console.WriteLine("\nFactura: " + f.ToString());
    int DCant = 1;
    foreach (DetallesFactura d in f.TraerDetalles())
    {
        Console.WriteLine($"Detalle {DCant}: " + d.ToString());
        DCant++;
    }
}

Console.Read();