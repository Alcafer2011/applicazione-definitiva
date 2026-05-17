using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace HR.Service.Models
{
    [BsonIgnoreExtraElements]
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("department")]
        public string Department { get; set; }

        [BsonElement("position")]
        public string Position { get; set; }

        [BsonElement("salary")]
        public decimal Salary { get; set; }

        [BsonElement("hireDate")]
        public DateTime HireDate { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
