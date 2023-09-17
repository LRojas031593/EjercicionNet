using ProyectoMaquina.Modelo;
using System.Security.Cryptography.X509Certificates;

namespace ProyectoMaquina.Control
{
    public sealed class Controller
    {   
        private static Controller _instance;

        public List<IProduct> ListaProductos {  get; set; }

        private Controller() {

            ListaProductos = new List<IProduct>();

            ListaProductos.Add(new Consumable("Cocacola",3000,15));
            ListaProductos.Add(new Consumable("Yumbo jet",5000,3));
            ListaProductos.Add(new Consumable("Wafer",700,20));

        }

        public static Controller GetInstance() {

            if (_instance == null) { 
                _instance = new Controller();
            
            }
            return _instance;
        
        }
        public string DisplayProductList()
        {
            return string.Join("\n", ListaProductos.Select(product => product.DisplayProduct()));
        }

        public bool ProductExists(string product_name)
        {
            return ListaProductos.Any(product => product.Name == product_name);
        }

        public bool ProductHasInventory(string product_name)
        {
            return ListaProductos.Any(product => product.Name == product_name && product.Quantity > 0);
        }
        //Codigo agregado desde aqui 
        public void AgregarProducto(string nombreProducto, int cantidad)
        {
            foreach (IProduct producto in ListaProductos)
            {
                if (producto.Name == nombreProducto)
                {
                    producto.Quantity += cantidad;
                    Console.WriteLine($"Se agregaron {cantidad} unidades de {nombreProducto}.");
                    return;
                }
            }

            
        }

        public void QuitarProducto(string nombreProducto, int cantidad)
        {
            foreach (IProduct producto in ListaProductos)
            {
                if (producto.Name == nombreProducto)
                {
                    if (producto.Quantity >= cantidad)
                    {
                        producto.Quantity -= cantidad;
                        Console.WriteLine($"Se quitaron {cantidad} unidades de {nombreProducto}.");
                    }
                    else
                    {
                        Console.WriteLine($"No hay suficientes unidades de {nombreProducto} para quitar.");
                    }
                    return;
                }
            }

            Console.WriteLine($"El producto {nombreProducto} no existe en la lista.");
        }


    }
}
