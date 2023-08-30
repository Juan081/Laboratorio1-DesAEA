class CuentaBancaria
{
    public int NumeroCuenta { get; set; }
    public string TitularCuenta { get; set; }
    protected decimal Saldo { get; set; }
    public int Pin { get; set; }

    public CuentaBancaria(int numeroCuenta, string titularCuenta, decimal saldoInicial, int pin)
    {
        NumeroCuenta = numeroCuenta;
        TitularCuenta = titularCuenta;
        Saldo = saldoInicial;
        Pin = pin;
    }

    public decimal ConsultarSaldo()
    {
        return Saldo;
    }

    public void Depositar(decimal cantidad)
    {
        Saldo += cantidad;
    }

    public virtual void Retirar(decimal cantidad)
    {
        if (cantidad > Saldo)
        {
            throw new InvalidOperationException("Saldo insuficiente");
        }
        Saldo -= cantidad;
    }

    public void CambiarPIN(int nuevoPin)
    {
        if (nuevoPin == Pin)
        {
            throw new InvalidOperationException("El nuevo PIN no puede ser igual al PIN actual.");
        }
        Pin = nuevoPin;
    }
}

class CajeroAutomatico : CuentaBancaria
{
    private decimal LimiteRetiroDiario { get; set; }

    public CajeroAutomatico(int numeroCuenta, string titularCuenta, decimal saldoInicial, int pin, decimal limiteRetiroDiario)
        : base(numeroCuenta, titularCuenta, saldoInicial, pin)
    {
        LimiteRetiroDiario = limiteRetiroDiario;
    }

    public override void Retirar(decimal cantidad)
    {
        if (cantidad > LimiteRetiroDiario)
        {
            throw new InvalidOperationException("La cantidad de retiro excede el límite diario.");
        }

        base.Retirar(cantidad);
    }
}

class Program
{
    static void Main(string[] args)
    {
        CajeroAutomatico cajero = new CajeroAutomatico(123456, "Usuario Ejemplo", 1000, 1234, 2207);
        Console.WriteLine("Ingresar PIN ");
        int pin = Convert.ToInt32(Console.ReadLine());
        if (pin == cajero.Pin)
        {
            while (true)
            {
                Console.WriteLine("1. Consultar saldo");
                Console.WriteLine("2. Depositar fondos");
                Console.WriteLine("3. Retirar efectivo");
                Console.WriteLine("4. Cambiar PIN");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine($"Saldo actual: {cajero.ConsultarSaldo()}");
                        break;
                    case 2:
                        Console.Write("Ingrese la cantidad a depositar: ");
                        decimal deposito = decimal.Parse(Console.ReadLine());
                        cajero.Depositar(deposito);
                        Console.WriteLine("Depósito exitoso.");
                        break;
                    case 3:
                        Console.Write("Ingrese la cantidad a retirar: ");
                        decimal retiro = decimal.Parse(Console.ReadLine());
                        try
                        {
                            cajero.Retirar(retiro);
                            Console.WriteLine("Retiro exitoso.");
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine($"Error: {e.Message}");
                        }
                        break;
                    case 4:
                        Console.Write("Ingrese el nuevo PIN: ");
                        int nuevoPin = int.Parse(Console.ReadLine());
                        cajero.CambiarPIN(nuevoPin);
                        Console.WriteLine("PIN cambiado exitosamente.");
                        break;
                    case 5:
                        Console.WriteLine("Gracias por usar nuestro cajero automático.");
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                Console.WriteLine();
            }

        }
        else 
        {
            Console.WriteLine("Contraseña Incorrecta.....");
        }
    }
}