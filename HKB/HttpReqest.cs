using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using HKB.DataClass;
using HKB.DataUpdateClass;

namespace HKB;

public class HttpReqest
{
    private static string mainUrl = "https://api.hamsterkombatgame.io/clicker/";

    private static string token = "1721491154575ZqgNWdqksH3FbOVGaJUXi0qB3qaLLF8K3wLgQZj424ymOuVYiEUp4oiRrpJOCjiV6846679980";
    
    public static async Task<ClickerUser> AllClickRequest()
    {
        using (var client = new HttpClient())
        {
            var url = mainUrl + "tap";

            // Настройка авторизации по токену
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Создание контента запроса
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var json = $"{{\"count\":2500,\"availableTaps\":0,\"timestamp\":{timestamp}}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Отправка POST-запроса
                var response = await client.PostAsync(url, content);

                // Проверка результата
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserRoot>(responseContent).clickerUser;
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка содержимого: {errorContent}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                return null;
            }
        }
    }
    
    public static async Task<ClickerUser> SyncUserRequest()
    {
        using (var client = new HttpClient())
        {
            var url = mainUrl + "sync";

            // Настройка авторизации по токену
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // // Создание контента запроса
            // var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            // var json = $"{{\"count\":2500,\"availableTaps\":1000,\"timestamp\":{timestamp}}}";
            var content = new StringContent("", Encoding.UTF8, "application/json");

            try
            {
                // Отправка POST-запроса
                var response = await client.PostAsync(url, content);

                // Проверка результата
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserRoot>(responseContent).clickerUser;
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка содержимого: {errorContent}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                return null;
            }
        }
    }
    
    public static async Task<List<UpgradesForBuy>> SyncUpdateRequest()
    {
        using (var client = new HttpClient())
        {
            var url = mainUrl + "upgrades-for-buy";

            // Настройка авторизации по токену
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Создание контента запроса
            var content = new StringContent("", Encoding.UTF8, "application/json");

            try
            {
                // Отправка POST-запроса
                var response = await client.PostAsync(url, content);

                // Проверка результата
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UpgradeRoot>(responseContent).upgradesForBuy;
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка содержимого: {errorContent}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                return null;
            }
        }
    }
    
    public static async Task<ClickerUser> BuyUpgradeRequest(string idUpgrade)
    {
        using (var client = new HttpClient())
        {
            var url = mainUrl + "buy-upgrade";

            // Настройка авторизации по токену
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Создание контента запроса
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var json = $"{{\"upgradeId\":\"{idUpgrade}\",\"timestamp\":{timestamp}}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Отправка POST-запроса
                var response = await client.PostAsync(url, content);

                // Проверка результата
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserRoot>(responseContent).clickerUser;
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка содержимого: {errorContent}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                return null;
            }
        }
    }
}