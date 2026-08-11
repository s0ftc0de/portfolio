using ATM.web.Models;
using System.Net.Http.Json;

namespace ATM.web.Services
{
    public class AccountApiService
    {
        private readonly HttpClient _httpClient;

        public AccountApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AccountViewModel>?> GetAccountsAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<AccountViewModel>>("api/accounts");
        }

        public async Task<WithdrawResponseViewModel?> WithdrawAsync(
        WithdrawViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/withdraw",
                model
            );

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content
                .ReadFromJsonAsync<WithdrawResponseViewModel>();
        }

        public async Task<List<WithdrawalHistoryViewModel>?> GetHistoryAsync(
                string accountNumber)
        {
            return await _httpClient
                .GetFromJsonAsync<List<WithdrawalHistoryViewModel>>(
                    $"api/withdraw/history/{accountNumber}"
                );
        }
    }
}