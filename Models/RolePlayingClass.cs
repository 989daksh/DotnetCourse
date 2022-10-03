using System.Text.Json.Serialization;

namespace DotnetCourse.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RolePlayingClass
    {
        Warrior = 4,
        Soldier = 3,
        King = 2
    }
}