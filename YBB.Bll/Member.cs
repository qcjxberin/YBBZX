using Ant.Model;
using System.Data;
using YBB.Common;

namespace YBB.Bll
{
    public class Member
    {
        public static DataTable MemberSeleteByUserID(int int_0)
        {
            return Ant.DAL.Member.MemberSeleteByUserID(int_0);
        }

        public static UserPowers GetUserPower()
        {
            AntCache cacheService = AntCache.GetCacheService();
            object userPower = cacheService.RetrieveObject("/Ant/UserPowers");
            if (userPower == null)
            {
                userPower = Ant.DAL.Member.GetUserPower();
                cacheService.AddObject("/Ant/UserPowers", userPower);
            }
            return (UserPowers)userPower;
        }

 

 


    }
}
