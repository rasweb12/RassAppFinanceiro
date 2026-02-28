namespace RassApp.SharedKernel.Abstractions;

public interface IMultiTenantEntity
{
    string TenantId { get; set; }
}