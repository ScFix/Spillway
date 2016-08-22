namespace Spillway.Interfaces
{
    public interface INotification
    {
        int IsUnread { get; set; }
        long Date { get; set; }
        string Type { get; set; }
        string Link { get; set; }
    }
}