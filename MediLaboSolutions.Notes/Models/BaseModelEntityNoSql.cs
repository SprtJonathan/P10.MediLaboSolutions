using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics;

namespace MediLaboSolutions.Notes.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BaseModelEntityNoSql
    {
        public BaseModelEntityNoSql()
        {

        }

        public BaseModelEntityNoSql(string id)
        {
            Id = id;
        }

        protected virtual string DebuggerDisplay
            => $"Id = {Id}, HashCode = {GetHashCode()}";

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
    }
}
