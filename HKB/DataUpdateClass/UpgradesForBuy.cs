namespace HKB.DataUpdateClass;

public class UpgradesForBuy
{
    public string id { get; set; }
    public string name { get; set; }
    public int price { get; set; }
    public int profitPerHour { get; set; }
    public Condition condition { get; set; }
    public string section { get; set; }
    public int level { get; set; }
    public int currentProfitPerHour { get; set; }
    public int profitPerHourDelta { get; set; }
    public bool isAvailable { get; set; }
    public bool isExpired { get; set; }
    public DateTime? releaseAt { get; set; }
}