using HKB.DataUpdateClass;

namespace HKB;

public class UpgradeRoot
{
    public List<UpgradesForBuy> upgradesForBuy { get; set; }
    public List<Section> sections { get; set; }
    public DailyCombo dailyCombo { get; set; }
}