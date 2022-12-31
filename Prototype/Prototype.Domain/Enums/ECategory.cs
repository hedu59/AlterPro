using System.ComponentModel;

namespace Prototype.Domain.Enums
{
    public enum  ECategory
    {
        [Description("Painters")]
        painters = 1,

        [Description("Interior Painters")]
        interiorPainters = 2,

        [Description("Demolition")]
        demolition = 3,

        [Description("Building")]
        building = 4,

    }
}
