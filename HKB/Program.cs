using HKB.DataClass;
using HKB.DataUpdateClass;

namespace HKB;

class Program
{
    static async Task Main(string[] args)
    {
        ClickerUser lastClickerUser = await HttpReqest.SyncUserRequest();;
        List<UpgradesForBuy> lastUpgrades = await HttpReqest.SyncUpdateRequest();
        
        
        
        bool isRun = true;
        while (isRun)
        {
            if (lastClickerUser == null)
            {
                lastClickerUser = await HttpReqest.SyncUserRequest();
            }
            else if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - lastClickerUser.lastSyncUpdate >= 60)
            {
                lastClickerUser = await HttpReqest.SyncUserRequest();
                lastUpgrades = await HttpReqest.SyncUpdateRequest();
                
                //click
                
                if (lastClickerUser.availableTaps >= 2500)
                {
                    lastClickerUser = await HttpReqest.AllClickRequest();
                }
                
                //updates
                float maxprofits = 0;
                int[] price = new int[3];
                UpgradesForBuy[] topUpgrade = new UpgradesForBuy[3];
                foreach (var upgrade in lastUpgrades)
                {
                    if (upgrade.isAvailable && !upgrade.isExpired)
                    {
                        float profit = upgrade.profitPerHourDelta / (float)upgrade.price;
                        if (profit >= maxprofits)
                        {
                            topUpgrade[2] = topUpgrade[1];
                            topUpgrade[1] = topUpgrade[0];
                            topUpgrade[0] = upgrade;
                        }
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (lastClickerUser.balanceCoins >= price[i])
                    {
                        lastClickerUser = await HttpReqest.BuyUpgradeRequest(topUpgrade[i].id);
                        break;
                    }
                }
                
                //output 
                
                Console.WriteLine("available taps: " + lastClickerUser.availableTaps);
                Console.WriteLine("balance: " + lastClickerUser.balanceCoins);
                Console.WriteLine("last update: " + DateTimeOffset.FromUnixTimeSeconds(lastClickerUser.lastSyncUpdate));
            }
        }
    }
}