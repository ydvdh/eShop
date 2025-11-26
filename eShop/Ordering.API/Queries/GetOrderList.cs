using MediatR;
using Ordering.API.DTOs;

namespace Ordering.API.Queries;

public record GetOrderList(string UserName) : IRequest<List<OrderDto>>;
