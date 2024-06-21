// See https://aka.ms/new-console-template for more information
using System;
using Grpc.Net.Client;
using Grpc.Core;
using System.Threading.Tasks;

namespace GrpcClientApp;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello Grpc from Zola ! \n",Console.ForegroundColor = ConsoleColor.Green);

        // Create a gRPC channel to communicate with the service
        var channel = GrpcChannel.ForAddress("http://localhost:5053");
        var customerClient = new Customer.CustomerClient(channel);
        var greetClient =  new Greeter.GreeterClient(channel);

        // say hello
        var helloRequest = new HelloRequest{
            Name = "Diokou Tech"
        };
        var resultHello = await greetClient.SayHelloAsync(helloRequest);
        Console.WriteLine("Say hello \n",Console.ForegroundColor = ConsoleColor.Green);Console.ResetColor();
        Console.WriteLine($"Message : {resultHello.Message} \n ");

        // get one costumer 
        var item = new CustomerRequest{
            CustomerId = 3
        };
        var result = await customerClient.GetCustomerAsync(item);
        Console.WriteLine("get One customer \n",Console.ForegroundColor = ConsoleColor.Green);Console.ResetColor();
        Console.WriteLine($" Id :  {result.CustomerId} \n Name : {result.CustomerName} \n Email : {result.CustomerEmail}");

        //get all customers
        var allCustomers = await customerClient.GetAllCustomersAsync(new Empty());
        Console.WriteLine("\n All Customers : \n",Console.ForegroundColor = ConsoleColor.Green);
        Console.ResetColor();

        foreach (var customer in allCustomers.Customers)
        {
            Console.WriteLine($" Id :  {customer.CustomerId} \n Name : {customer.CustomerName} \n Email : {customer.CustomerEmail} \n");
        }
            Console.WriteLine("\n Press any key to exit...");
            Console.ReadKey();
    }
}