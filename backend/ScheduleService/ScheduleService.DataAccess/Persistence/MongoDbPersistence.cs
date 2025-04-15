using System.Diagnostics;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

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
            
            //RegisterAllSerializersFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true),
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register(
                "ApplicationConventions",
                pack,
                t => true);
        }

        private static void RegisterAllSerializersFromAssembly(Assembly assembly)
        {
            var serializerTypes = assembly.GetTypes()
                .Where(t => typeof(IBsonSerializer).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var type in serializerTypes)
            {
                if (Activator.CreateInstance(type) is not IBsonSerializer serializer) continue;
                
                var supportedType = type.BaseType?.GenericTypeArguments.FirstOrDefault();
                if (supportedType != null)
                {
                    BsonSerializer.RegisterSerializer(supportedType, serializer);
                }
            }
        }
    }
}
