namespace HKB.DataClass;

public class AnonymousTransactionsBan
{
    public string id { get; set; }
    public int level { get; set; }
    public int lastUpgradeAt { get; set; }
    public int snapshotReferralsCount { get; set; }
}