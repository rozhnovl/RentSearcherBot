using System;

namespace TelegramMessageListener;

public class SearchSubscriberInfo
{
    public Guid? Id { get; internal set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string EmailTemplate { get; set; }
    public string Name { get; set; }
    public int? MinRoomNumber { get; set; }
    public int? MaxPrice{ get; set; }
}