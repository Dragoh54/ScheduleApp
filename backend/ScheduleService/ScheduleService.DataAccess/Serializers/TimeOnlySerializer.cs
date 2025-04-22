using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ScheduleService.DataAccess.Serializers;

public class TimeOnlySerializer : StructSerializerBase<TimeOnly>
{
    private const string Format = "HH:mm";

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TimeOnly value)
    {
        context.Writer.WriteString(value.ToString(Format));
    }

    public override TimeOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var type = context.Reader.GetCurrentBsonType();
        return type switch
        {
            BsonType.String => TimeOnly.Parse(context.Reader.ReadString()),
            BsonType.DateTime => TimeOnly.FromDateTime(new DateTime(context.Reader.ReadDateTime(), DateTimeKind.Utc)),
            _ => throw new NotSupportedException($"Cannot convert a {type} to a TimeOnly")
        };
    }
}