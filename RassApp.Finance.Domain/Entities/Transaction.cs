using RassApp.Finance.Domain.Common;
using RassApp.SharedKernel.Abstractions;

public class Transaction : BaseEntity
{
    public Guid TenantId { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }

    protected Transaction() { }

    public Transaction(Guid tenantId, decimal amount, string description)
    {
        TenantId = tenantId;
        Amount = amount;
        Description = description;
        SetCreated();
    }

    public void Update(decimal amount, string description)
    {
        Amount = amount;
        Description = description;
        SetUpdated();
    }
}