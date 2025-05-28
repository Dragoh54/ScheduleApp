using UserService.DataAccess.Interfaces.UnitOfWork;
using Grpc.Core;
using Mapster;

namespace UserService.Grpc.Services;

public class UserService : Grpc.UserService.UserServiceBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork userRepository)
    {
        _unitOfWork = userRepository;
    }

    public override async Task<GetUserInfoReply> GetUserInfo(GetUserInfoRequest request, ServerCallContext context)
    {
        Guid userGuid;
        var isUserIdValid = Guid.TryParse(request.UserId, out userGuid);

        if (!isUserIdValid) 
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "User id is not valid"));
        }
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userGuid, context.CancellationToken)
            ?? throw new RpcException(new Status(StatusCode.NotFound, "User does not exist"));

        return user.Adapt<GetUserInfoReply>();
    }
}