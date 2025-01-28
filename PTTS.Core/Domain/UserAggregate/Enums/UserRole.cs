using System.Text.Json.Serialization;

namespace PTTS.Core.Domain.UserAggregate.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<UserRole>))]
public enum UserRole
{
	Admin,
	SuperAdmin,
}
