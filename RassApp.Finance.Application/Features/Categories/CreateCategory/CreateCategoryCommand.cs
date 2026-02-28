using MediatR;
using RassApp.SharedKernel.Common.Results;

namespace RassApp.Finance.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name)
    : IRequest<Result<Guid>>;