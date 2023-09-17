using ProyectoMaquina.Control;
using ProyectoMaquina.Modelo;
using System;


namespace ProyectoMaquina.View
{
    internal class View
    {
        static void Main(string[] args)
        {
           
           Controller controller =Controller.GetInstance();
        

            string text_bienvenida = "Bienvenido a la maquina espendedora";
            Console.WriteLine(text_bienvenida);
            string input_cliente = "";

            while (true)
            {
                do
                {
                    Console.WriteLine("Escoja el tipo de cliente: [C] o [P]");
                    input_cliente = Console.ReadLine();
                    try
                    {
                        if (input_cliente != "C" && input_cliente != "P")
                        {
                            throw new ArgumentException("Ingrese solo 'C' o 'P'");
                        }
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }

                } while (input_cliente != "C" && input_cliente != "P");


                Console.WriteLine("La lista de productos es: ");
                                                                
                Console.WriteLine(controller.DisplayProductList());

                if (input_cliente == "C")
                {
                    Console.WriteLine("Escoja un producto de la lista ....");
                    bool valid_product = false;
                    IProduct selectedProduct = null;

                    do
                    {
                        string input_producto = Console.ReadLine();
                        try
                        {
                            valid_product = controller.ProductExists(input_producto) && controller.ProductHasInventory(input_producto);

                            if (!valid_product)
                            {
                                throw new Exception("Escoja un producto válido.");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error: {e.Message}");
                        }

                        if (valid_product)
                        {
                            selectedProduct = controller.ListaProductos.Find(p => p.Name == input_producto);
                            if (selectedProduct != null)
                            {
                                Console.WriteLine($"Ingrese {selectedProduct.Price} para comprar {selectedProduct.Name}:");
                                int montoIngresado = 0;

                                while (montoIngresado < selectedProduct.Price)
                                {
                                    Console.WriteLine("Ingrese dinero:");
                                    try
                                    {
                                        int billete = Convert.ToInt32(Console.ReadLine());
                                        montoIngresado += billete;
                                    }
                                    catch (FormatException e)
                                    {
                                        Console.WriteLine($"Ingrese solo billetes: {e.Message}");
                                    }
                                }

                                if (montoIngresado >= selectedProduct.Price)
                                {
                                    
                                    Console.WriteLine($"Compra exitosa. Se ha entregado un {selectedProduct.Name}.");
                                    if (montoIngresado > selectedProduct.Price)
                                    {
                                       
                                        int cambio = montoIngresado - selectedProduct.Price;
                                        Console.WriteLine($"Cambio: {cambio}");
                                    }
                                    selectedProduct.Quantity--; 
                                    break; 
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Escoja un producto válido.");
                        }
                    } while (!valid_product);


                    //codigo   
                   
                }
                else if (input_cliente == "P")
                {
                    Console.WriteLine("Bienvenido, proveedor.");
                    Console.WriteLine("¿Desea agregar (A) o quitar (Q) productos?");
                    string input_proveedor = Console.ReadLine();

                    try
                    {
                        if (input_proveedor == "A")
                        {
                            Console.WriteLine("Ingrese el nombre del producto a agregar:");
                            string nombreProducto = Console.ReadLine();
                            Console.WriteLine("Ingrese la cantidad a agregar:");
                            int cantidad = Convert.ToInt32(Console.ReadLine());
                            controller.AgregarProducto(nombreProducto, cantidad);
                        }
                        else if (input_proveedor == "Q")
                        {
                            Console.WriteLine("Ingrese el nombre del producto a quitar:");
                            string nombreProducto = Console.ReadLine();
                            Console.WriteLine("Ingrese la cantidad a quitar:");
                            int cantidad = Convert.ToInt32(Console.ReadLine());
                            controller.QuitarProducto(nombreProducto, cantidad);
                        }
                        else
                        {
                            throw new ArgumentException("Debe ingresar 'A' para agregar o 'Q' para quitar productos.");
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Error: {e.Message}. Ingrese un número válido para la cantidad.");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error desconocido: {e.Message}");
                    }

                }
            }


        }
    }
}