using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace GrpcService.Services;

public class CustomerService : Customer.CustomerBase
{
    private readonly ILogger<CustomerService> _logger;

    private IEnumerable<CustomerResponse> customers = new List<CustomerResponse>
    {
        new CustomerResponse { CustomerId = 1, CustomerName = "Zola Tech", CustomerEmail = "zola@gmail.com" },
        new CustomerResponse { CustomerId = 2, CustomerName = "Cheikhou Tech", CustomerEmail = "cheikhou@gmail.com" },
        new CustomerResponse { CustomerId = 3, CustomerName = "Fatou Tech", CustomerEmail = "fatou@gmail.com" },
        new CustomerResponse { CustomerId = 4, CustomerName = "Baba Top", CustomerEmail = "baba@gmail.com" },
        new CustomerResponse { CustomerId = 5, CustomerName = "Assane Niang", CustomerEmail = "assane@gmail.com" },
        new CustomerResponse { CustomerId = 6, CustomerName = "Mouhamed Kane", CustomerEmail = "kane@gmail.com" },
        new CustomerResponse { CustomerId = 7, CustomerName = "Cheikh Gassa", CustomerEmail = "cheikh@gmail.com" },
        new CustomerResponse { CustomerId = 8, CustomerName = "Habsatou THIAM", CustomerEmail = "habsatou@gmail.com" },
        new CustomerResponse { CustomerId = 9, CustomerName = "Mariama KESSOU", CustomerEmail = "mariama@gmail.com" },
        new CustomerResponse { CustomerId = 10, CustomerName = "Admama MENDY", CustomerEmail = "adama@gmail.com" },
    };

    public CustomerService(ILogger<CustomerService> logger)
    {
        _logger = logger;
    }

    public override Task<CustomerResponse?> GetCustomer(CustomerRequest request, ServerCallContext context)
    {
        var itemSearch = customers.FirstOrDefault(c => c.CustomerId == request.CustomerId);
        return Task.FromResult(itemSearch);
    }

    public override Task<CustomerResponse?> GetCustomerByEmail(CustomerRequestEmail request, ServerCallContext context)
    {
        var itemSearch = customers.FirstOrDefault(c => c.CustomerEmail.ToLower() == request.Email.ToLower());
        if(itemSearch == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Customer with Email {request.Email} not found"));
        }
        return Task.FromResult(itemSearch);
    }

    public override Task<CustomerResponse?> GetCustomerByName(CustomerRequestName request, ServerCallContext context)
    {
        var itemSearch = customers.FirstOrDefault(c => c.CustomerName.ToLower() == request.Name.ToLower());
        if(itemSearch == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Customer with Name {request.Name} not found"));
        }
        return Task.FromResult(itemSearch);
    }

    public override Task<DeleteResponse> DeleteCustomer(CustomerRequest request, ServerCallContext context)
    {
        var itemSearch = customers.FirstOrDefault(c => c.CustomerId == request.CustomerId);
        // delete the customer
        if (itemSearch != null)
        {
            customers = customers.Where(c => c.CustomerId != request.CustomerId);
            return Task.FromResult(new DeleteResponse 
            { 
                IsSuccess = true, 
                Message = "Suppression avec succès du customer" 
            });
        }else
        {
                // Renvoyer un statut NOT_FOUND si le client n'est pas trouvé
                throw new RpcException(new Status(StatusCode.NotFound, $"Customer with ID {request.CustomerId} not found"));
        }
    }

    public override Task<CustomerResponse> AddCustomer(CustomeAddRequest request, ServerCallContext context)
    {
        this.customers.Append(new CustomerResponse
        {
            CustomerId = this.customers.Count() + 1,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail
        });

        return Task.FromResult(new CustomerResponse
        {
            CustomerId = this.customers.Count() + 1,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail
        });
    }

    public override Task<CustomerList> GetAllCustomers(Empty empty, ServerCallContext context)
    {
        var customerList = new CustomerList();

        customers.ToList().ForEach(c =>
        {
            customerList.Customers.Add(c);
        });
        return Task.FromResult(customerList);
    }
}