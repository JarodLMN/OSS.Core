﻿#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：OSSCore仓储层 —— 授权用户仓储
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-10-24
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Core.Domains.Members;
using OSS.Core.Infrastructure.Enums;
using OSS.Plugs.OrmMysql;

namespace OSS.Core.RepDapper.Members
{
    public class OauthUserRep: BaseMysqlRep<OauthUserRep, OauthUserMo>
    {
        public OauthUserRep()
        {
            m_TableName = "oauth_user_info";
        }

        public async Task<ResultMo<OauthUserMo>> GetOauthUserByAppUserId(long tenantId, string appUId, SocialPaltforms plat)
        {
            return await Get(u => u.app_user_id == appUId
                                               && u.tenant_id == tenantId 
                                               && u.platform == plat);
        }

        public async Task<ResultMo> UpdateUserWithToken(OauthUserMo user)
        {
            return await Update(
                u => new
                {
                    u.app_union_id,
                    u.head_img,
                    u.nick_name,
                    u.sex,
                    u.access_token,
                    u.expire_date,
                    u.refresh_token
                },
                w => w.id == user.id, user);
        }

        public async Task<ResultMo> UpdateUserIdByOauthId(long oauthUserId, long userId)
        {
            return await Update(
                u => new
                {
                    user_id = userId
                },
                w => w.id == oauthUserId);
        }
    }
}
