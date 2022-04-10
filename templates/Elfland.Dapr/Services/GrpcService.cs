using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
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

    public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
    {
        var response = new InvokeResponse();
        switch (request.Method)
        {
            case "sayhi":
                // This GetWeatherForecastRequest is a defined in .proto file,
                // not in Application.Queries.WeatherForecastQueries.GetWeatherForecastRequest.

                // response.Data = Any.Pack(
                //     await _mediator.Send(
                //         new GetWeatherForecastGrpcRequest
                //         {
                //             Request = request.Data.Unpack<GetWeatherForecastRequest>()
                //         }
                //     )
                // );
                break;
            default:
                break;
        }
        return response;
    }
}
