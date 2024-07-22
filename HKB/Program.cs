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
                float[] maxprofits = new float[3];
                string[] id = new string[3];
                string[] categorys = new string[3];
                int[] price = new int[3];
                foreach (var upgrade in lastUpgrades)
                {
                    if (upgrade.isAvailable)
                    {
                        float profit = upgrade.profitPerHourDelta / (float)upgrade.price;
                        if (profit >= maxprofits[0])
                        {
                            for (int i = 2; i > 0; i--)
                            {
                                price[i] = price[i - 1];
                                maxprofits[i] = maxprofits[i - 1];
                                id[i] = id[i-1];
                                categorys[i] = categorys[i-1];
                            }
                    
                            maxprofits[0] = profit;
                            id[0] = upgrade.id;
                            categorys[0] = upgrade.section;
                            price[0] = upgrade.price;
                        }
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (lastClickerUser.balanceCoins >= price[i])
                    {
                        lastClickerUser = await HttpReqest.BuyUpgradeRequest(id[i]);
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