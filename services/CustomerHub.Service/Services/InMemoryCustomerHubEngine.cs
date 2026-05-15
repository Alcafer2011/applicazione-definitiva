using AIEnterpriseOS.CustomerHub.Service.Models;

namespace AIEnterpriseOS.CustomerHub.Service.Services;

public interface ICustomerHubEngine
{
    Customer CreateCustomer(Customer customer);
    Customer? GetCustomer(string id);
    IEnumerable<Customer> GetAllCustomers();

    CustomerMessage AddMessage(CustomerMessage msg);
    IEnumerable<CustomerMessage> GetMessages(string customerId);

    TimelineEvent AddEvent(TimelineEvent evt);
    IEnumerable<TimelineEvent> GetTimeline(string customerId);
}

public class InMemoryCustomerHubEngine : ICustomerHubEngine
{
    private readonly List<Customer> _customers = new();
    private readonly List<CustomerMessage> _messages = new();
    private readonly List<TimelineEvent> _timeline = new();

    public Customer CreateCustomer(Customer customer)
    {
        _customers.Add(customer);
        return customer;
    }

    public Customer? GetCustomer(string id)
        => _customers.FirstOrDefault(x => x.Id == id);

    public IEnumerable<Customer> GetAllCustomers()
        => _customers;

    public CustomerMessage AddMessage(CustomerMessage msg)
    {
        _messages.Add(msg);

        _timeline.Add(new TimelineEvent
        {
            CustomerId = msg.CustomerId,
            Type = "message",
            Description = msg.Content
        });

        return msg;
    }

    public IEnumerable<CustomerMessage> GetMessages(string customerId)
        => _messages.Where(x => x.CustomerId == customerId);

    public TimelineEvent AddEvent(TimelineEvent evt)
    {
        _timeline.Add(evt);
        return evt;
    }

    public IEnumerable<TimelineEvent> GetTimeline(string customerId)
        => _timeline.Where(x => x.CustomerId == customerId)
                    .OrderByDescending(x => x.CreatedAt);
}
