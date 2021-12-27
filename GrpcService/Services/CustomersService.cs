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
    }
}
