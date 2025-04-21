using System.Diagnostics;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Persistence
{
    public static class MongoDbPersistence
    {
        private static bool _initialized;
        
        public static void Configure()
        {
            if (_initialized)
                return;
            
            RegisterSerializers();
            RegisterConventions();

            _initialized = true;
        }
        
        private static void RegisterSerializers()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
            
            BsonSerializer.RegisterSerializer(new TimeOnlySerializer());
        }

        private static void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register(
                "ApplicationConventions",
                pack,
                t => true);
        }
    }
}
