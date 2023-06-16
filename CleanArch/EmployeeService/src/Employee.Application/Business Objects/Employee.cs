namespace Employee.Application.Domain;

//public readonly record struct Employee(int Id, string Name, string Address)
//{
//    public readonly string? EmailId { get; init; }
//};


public readonly record struct Employee
{
    public readonly int Id { get; init; }
    public readonly string Name { get; init; }
    public readonly string Address { get; init; }
    public readonly string? EmailId { get; init; }

    public Department Department { get; init; }

    //public Employee()
    //{
    //    throw new InvalidOperationException("Using the default Constructor is not allowed");
    //}
    public Employee(int id, string name, string address, Department department)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "Name cannot be null or empty");
        if (string.IsNullOrEmpty(address))
            throw new ArgumentNullException(nameof(address), "Address cannot be null or empty");

        Id = id;
        Name = name;
        Address = address;
        Department = department;
    }
};