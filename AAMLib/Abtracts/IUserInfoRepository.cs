using System.Collections.Generic;
using AAMLib.Entities;

namespace AAMLib.Abtracts
{
    public interface IUserInfoRepository
    {
        IEnumerable<UserInfo> UserInfos { get; }
    }
}
