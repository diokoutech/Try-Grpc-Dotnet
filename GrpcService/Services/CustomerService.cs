
using Grpc.Core;

namespace GrpcService.Services;

public class CustomerService : Customer.CustomerBase
{
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ILogger<CustomerService> logger)
    {
        _logger = logger;
    }

    public override Task<CustomerResponse> GetCustomer(CustomerRequest request, ServerCallContext context)
    {
        var model = new CustomerResponse();
        if (request.CustomerId == 1)
        {
            model.CustomerEmail = "zoola@yopmail.com";
            model.CustomerName = "Zola Tech";
        }
        else if (request.CustomerId == 2)
        {
            model.CustomerEmail = "cheikhou@yopmail.com";
            model.CustomerName = "Cheikhou Tech";
        }
        else if (request.CustomerId == 3)
        {
            model.CustomerEmail = "diokou@yopmail.com";
            model.CustomerName = "Diokou Tech";
        }
        model.CustomerId = request.CustomerId;

        return Task.FromResult(model);
    }

    public override Task<CustomerResponse> AddCustomer(CustomeAddRequest request, ServerCallContext context)
    {
        // return base.AddCustomer(request, context);

        return Task.FromResult(new CustomerResponse
        {
            CustomerId = 1,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail
        });
    }

    public override Task<CustomerList> GetAllCustomers(Empty empty, ServerCallContext context)
    {

        var customerList = new CustomerList();
        // item 1
        customerList.Customers.Add(new CustomerResponse
        {
            CustomerId = 1,
            CustomerName = "Zola Tech",
            CustomerEmail = "zola@yopmail.com"
        });
        // item 2
        customerList.Customers.Add(new CustomerResponse
        {
            CustomerId = 2,
            CustomerName = "Cheikhou DIOKOU",
            CustomerEmail = "cheikhou@yopmail.com"
        });

        return Task.FromResult(customerList);
    }
}