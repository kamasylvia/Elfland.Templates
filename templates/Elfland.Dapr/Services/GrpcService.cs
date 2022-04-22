using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;

namespace Elfland.Dapr.Services;

public class GrpcService : AppCallback.AppCallbackBase
{
    private readonly IMediator _mediator;

    public GrpcService(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public override async Task<InvokeResponse> OnInvoke(
        InvokeRequest request,
        ServerCallContext context
    )
    {
        var response = new InvokeResponse();
        switch (request.Method)
        {
            case "greet":
                // The code below require a MediatR.IRequestHandler which accepts a HelloRequest request, and return a HelloReply response.

                // response.Data = Any.Pack(
                //     await _mediator.Send(
                //         request.Data.Unpack<HelloRequest>()
                //     )
                // );
                break;
            default:
                break;
        }
        return response;
    }
}
