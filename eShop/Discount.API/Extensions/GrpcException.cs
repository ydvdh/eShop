using Google.Rpc;
using Grpc.Core;
using static Google.Rpc.BadRequest.Types;
using GrpcStatus = Grpc.Core.Status;
using GoogleStatus = Google.Rpc.Status;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;

namespace Discount.API.Extensions;

public static class GrpcException
{
    public static RpcException CreateValidationException(Dictionary<string, string> fieldErrors)
    {
        var fieldViolations = new List<FieldViolation>();

        foreach (var error in fieldErrors)
        {
            fieldViolations.Add(new FieldViolation
            {
                Field = error.Key,
                Description = error.Value
            });
        }
        //Now Add Bad Request
        var badRequest = new BadRequest();
        badRequest.FieldViolations.AddRange(fieldViolations);

        var status = new GoogleStatus
        {
            Code = (int)StatusCode.InvalidArgument,
            Message = "Validation Failed",
            Details = { Any.Pack(badRequest) }
        };

        var trailers = new Metadata
            {
                { "grpc-status-details-bin", status.ToByteArray() }
            };

        return new RpcException(new GrpcStatus(StatusCode.InvalidArgument, "Validation errors"), trailers);
    }
}
