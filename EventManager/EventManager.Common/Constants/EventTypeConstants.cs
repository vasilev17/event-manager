
namespace EventManager.Common.Constants
{
    public enum EventTypes
    {
        Convention,
        Conference,
        Corporate,
        Seminar,
        Presentation,
        GalaDinner,
        Entertainment,
        Other
    }

    internal static class EventTypeConstants
    {
        public const EventTypes DefaultEventType = EventTypes.Other;
    }
}
