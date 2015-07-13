using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.mongo;
using com.etak.core.repository.mongo.extensions;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace com.etak.core.repository.crm.mongo
{
    /// <summary>
    /// Implementation of IOperationLogRepository with mongo
    /// </summary>
    /// <typeparam name="TOperationLog">The managed entity, is or extends OperationLog</typeparam>
    public class OperationLogRepositoryMongo<TOperationLog> :  MongoRepository<TOperationLog, Int64>, IOperationLogRepository<TOperationLog>
        where TOperationLog : OperationLog
    {
        #region Property mapping
        static OperationLogRepositoryMongo()
        {
            BsonClassMap.RegisterClassMap<TOperationLog>(cm =>
            {
                cm.MapIdProperty(x => x.Code).SetIdGenerator(MongoDB.Bson.Serialization.IdGenerators.NullIdChecker.Instance);
                cm.MapPropertyIgnoreDefault(x => x.Channel, "chn");
                cm.MapPropertyIgnoreDefault(x => x.DealerID, "di");
                cm.MapPropertyIgnoreDefault(x => x.Description,"dsc").SetIgnoreIfDefault(true);
                cm.MapPropertyIgnoreDefault(x => x.ExternalCode,"eid");
                cm.MapPropertyIgnoreDefault(x => x.ForeignCode,"fid");
                cm.MapPropertyIgnoreDefault(x => x.InvokeParams,"par");
                cm.MapPropertyIgnoreDefault(x => x.Invoker,"inv");
                cm.MapPropertyIgnoreDefault(x => x.Messages,"msg");
                cm.MapPropertyIgnoreDefault(x => x.OperationCode,"cod");
                cm.MapPropertyIgnoreDefault(x => x.OperationDate,"ts");
                cm.MapPropertyIgnoreDefault(x => x.OperationInfo, "opinfo");
                cm.MapPropertyIgnoreDefault(x => x.OrderCode,"seqid");
                cm.MapPropertyIgnoreDefault(x => x.Remark,"rmk");
                cm.MapPropertyIgnoreDefault(x => x.Result,"res");
                cm.MapPropertyIgnoreDefault(x => x.Status,"status");
                cm.MapPropertyIgnoreDefault(x => x.SystemMessages,"smesg");
                cm.MapPropertyIgnoreDefault(x => x.TopupHistoryID, "tid");
                cm.MapPropertyIgnoreDefault(x => x.TraceOrder, "torder");
                cm.MapPropertyIgnoreDefault(x => x.UserID, "uid");
                cm.MapPropertyIgnoreDefault(x => x.Vmo, "vmo");                
            });            
        }
        #endregion

        #region Durability and consistency
        static readonly MongoDatabaseSettings _dbSettings = new MongoDatabaseSettings
        {
            WriteConcern = new WriteConcern { FSync = false, Journal = false, W = 1, },
            ReadPreference = new ReadPreference { ReadPreferenceMode = ReadPreferenceMode.PrimaryPreferred },
            
        };
        protected override MongoDatabaseSettings dbSettings { get { return _dbSettings; } }


        static readonly MongoCollectionSettings _collSettings = new MongoCollectionSettings
        {
            WriteConcern = new WriteConcern { FSync = false, Journal = false, W = 1, },
            ReadPreference = new ReadPreference { ReadPreferenceMode = ReadPreferenceMode.PrimaryPreferred },
        };
        #endregion

        #region Name space mapping
        protected override MongoCollectionSettings collSettings { get { return (_collSettings); } }

        static readonly MongoDBNameSpace _ns = new MongoDBNameSpace("CRM", "OperationLog");
        #endregion

        protected override MongoDBNameSpace nsMap  { get { return (_ns); }}

        public IEnumerable<TOperationLog> GetByOrderCodeAndDealerId(string referenceCode, int vmnoId)
        {
            return GetQuery().Where(x => x.ExternalCode == referenceCode && x.DealerID == vmnoId);
        }

        public IEnumerable<TOperationLog> GetByOrderCodeColumnAndDealerId(int orderCode, int vmnoId)
        {
            return (GetQuery().
                    Where(x => x.OrderCode == orderCode && x.DealerID == vmnoId));
        }
    }
}
