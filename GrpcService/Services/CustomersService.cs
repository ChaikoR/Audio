using Grpc.Core;

namespace GrpcService.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _loger;
        public CustomersService(ILogger<CustomersService> loger)
        {
            _loger = loger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomeerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1) {
                output.FirstName = "Гриша";
                output.LastName = "Чайко";
            }
            else if (request.UserId == 2)
                {
                    output.FirstName = "Вася";
                    output.LastName = "пупкин";
                }
            else 
            {
                output.FirstName = "Иван";
                output.LastName = "Иванов";
            }
            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request, 
            IServerStreamWriter<CustomerModel> responseStream, 
            ServerCallContext context)
        {
            List<CustomerModel> customer = new List<CustomerModel>
            { 
                new CustomerModel()
                {
                    FirstName =   "Гриша2",
                    LastName = "Чайко2",
                    Age = 42
                },
                 new CustomerModel()
                {
                     FirstName =   "Гриша3",
                    LastName = "Чайко3",
                    Age = 42
                },
                  new CustomerModel()
                {
                      FirstName =   "Гриша4",
                    LastName = "Чайко4",
                    Age = 42
                },
                   new CustomerModel()
                {
                       FirstName =   "Гриша5",
                    LastName = "Чайко5",
                    Age = 42
                }

            };
            foreach (var cust in customer)
            {
                await Task.Delay(1000);
               await responseStream.WriteAsync(cust);
            }
        }
    }
}
