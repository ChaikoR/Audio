using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;


class Program
{
    static async Task Main(string[] args)
    {
        //var input = new HelloRequest { Name = "RyhoR" };

        //var channel = GrpcChannel.ForAddress("https://localhost:7298");
        //var client = new Greeter.GreeterClient(channel);

        //var reply = await client.SayHelloAsync(input);

        //Console.WriteLine(reply.Message);

        var channel = GrpcChannel.ForAddress("https://localhost:7298");
        var client = new Customer.CustomerClient(channel);

        //var reply = await client.GetCustomerInfoAsync(new CustomeerLookupModel {UserId=2 });
        
        //Console.WriteLine(reply.FirstName);
        //Console.WriteLine(reply.LastName);


        

        //var reply = await client.GetCustomerInfoAsync(new CustomeerLookupModel {UserId=2 });

        using (var call= client.GetNewCustomers(new NewCustomerRequest()))
        {
            while (await call.ResponseStream.MoveNext())
            {
                var currentCustomer = call.ResponseStream.Current;
                Console.WriteLine($"{currentCustomer.FirstName} - {currentCustomer.LastName} - {currentCustomer.Age}");
            }
        }
        Console.ReadLine();
    }
}