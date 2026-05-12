using FCG.Payments.Domain.Documents;
using MongoDB.Driver;

namespace FCG.Payments.Application.UseCases.Service
{
    public class MongoAuditService
    {
        private readonly IMongoCollection<AuditDocument> _collection;

        public MongoAuditService(IMongoClient client)
        {
            var database = client.GetDatabase("audit_db");

            _collection =
                database.GetCollection<AuditDocument>("audit_logs");
        }

        public async Task SaveAsync(AuditDocument audit)
        {
            await _collection.InsertOneAsync(audit);
        }
    }
}
