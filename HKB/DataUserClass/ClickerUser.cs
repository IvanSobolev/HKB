namespace HKB.DataClass;

public class ClickerUser
{
    public string id { get; set; }
    public double totalCoins { get; set; }
    public double balanceCoins { get; set; }
    public int level { get; set; }
    public int availableTaps { get; set; }
    public long lastSyncUpdate { get; set; }
    public string exchangeId { get; set; }
    public Boosts boosts { get; set; }
    public Upgrades upgrades { get; set; }
    public Tasks tasks { get; set; }
    public AirdropTasks airdropTasks { get; set; }
    public double referralsCount { get; set; }
    public double maxTaps { get; set; }
    public double earnPerTap { get; set; }
    public double earnPassivePerSec { get; set; }
    public double earnPassivePerHour { get; set; }
    public double lastPassiveEarn { get; set; }
    public double tapsRecoverPerSec { get; set; }
    public DateTime createdAt { get; set; }
    public Skin skin { get; set; }
    public DateTime startKeysMiniGameAt { get; set; }
}