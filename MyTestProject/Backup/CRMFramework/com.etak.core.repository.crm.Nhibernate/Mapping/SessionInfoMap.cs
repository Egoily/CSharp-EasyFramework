using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.Mapping
{
    class SessionInfoMap : ClassMap<SessionInfo>
    {
        private SessionInfoMap()
        {
            Schema("dbo");
            Table("SYS25_USER_SESSIONS");
            DynamicInsert();
            DynamicUpdate();
            
            Id(x => x.SessionID, "SYS25_USER_SESSIONID").GeneratedBy.Custom<SessionIdGenerator>();
            References(x => x.LoginInfo, "USERID").Cascade.None();//.ForeignKey("FK_DEALERS_SESSIONID_DEALERID");
            Map(x => x.IdleTimoutMinutes).Column("IDLE_TIMEOUT_MINUTES");
            Map(x => x.IdleTimeoutDate).Column("IDLE_TIMEOUT_DATE");
           
        }
    }
}
