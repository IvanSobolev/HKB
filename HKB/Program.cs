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
                    if (lastClickerUser.balanceCoins >= topUpgrade[i].price)
                    {
                        lastClickerUser = await HttpReqest.BuyUpgradeRequest(topUpgrade[i].id);
                        break;
                    }
                }
                
                //output 
                
                Console.WriteLine("available taps: " + lastClickerUser.availableTaps);
                Console.WriteLine("balance: " + lastClickerUser.balanceCoins);
                Console.WriteLine("earn passive per pour: " + lastClickerUser.earnPassivePerHour);
                Console.WriteLine("update price: " + topUpgrade[0].price + "   " + topUpgrade[1].price + "   " + topUpgrade[2].price);
                Console.WriteLine("last update: " + DateTimeOffset.FromUnixTimeSeconds(lastClickerUser.lastSyncUpdate).Minute);
                Console.WriteLine("________________________________________");
            }
        }
    }
}