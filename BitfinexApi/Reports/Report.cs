using BitfinexApi.Models;
using BitfinexApi.Services;
using BitfinexApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitfinexApi.Reports
{
    class Report : IReport
    {
        private readonly IRequestService requestService;
        private readonly ICzkRateService czkRateService;
        private readonly IPriceCurrencyService priceCurrencyService;

        public Report(
            IRequestService requestService,
            ICzkRateService czkRateService,
            IPriceCurrencyService priceCurrencyService)
        {
            this.requestService = requestService;
            this.czkRateService = czkRateService;
            this.priceCurrencyService = priceCurrencyService;
        }

        public async Task<ReportViewModel> Execute()
        {
            var wallets = await requestService.Execute<Wallet>(new WalletRequest());
            var priceCurrencies = await priceCurrencyService.Get(wallets.Select(wallet => wallet.Currency));

            var orderItems = await requestService.Execute<OrderItem>(new OrderRequest());
            var processedOrders = ProcessOrderItems(orderItems);

            var walletViews = wallets
                .Select(wallet =>
                {
                    var actualPrice = priceCurrencies.First(priceCurrency => priceCurrency.Currency == wallet.Currency).Price;
                    var orderPrice = processedOrders.FirstOrDefault(p => p.Symbol.Replace("USD", "") == wallet.Currency);
                    var totalUsd = wallet.Balance * actualPrice;
                    float? percentage = orderPrice != null ? orderPrice.Amount * actualPrice - orderPrice.Total : null;

                    return new WalletViewModel
                    {
                        Type = wallet.Type,
                        Currency = wallet.Currency,
                        Balance = wallet.Balance.ToString("0." + new string('#', 6)),

                        UnsettledInterest = wallet.UnsettledInterest.ToString("0." + new string('#', 6)),
                        AvaliableBalance = wallet.AvaliableBalance.ToString("0." + new string('#', 6)),
                        ActualPrice = actualPrice,
                        TotalUsd = totalUsd,
                        Percentage = percentage.HasValue ? (float)Math.Round(percentage.Value, 2) : null
                    };
                });

            var czkRate = await czkRateService.Get();
            var grandTotalUsd = walletViews.Sum(walletView => walletView.TotalUsd);
            var grandTotalCzk = grandTotalUsd * czkRate;

            return new ReportViewModel
            {
                WalletViews = walletViews,
                TotalUsd = (float)Math.Round(grandTotalUsd, 2),
                TotalCzk = (float)Math.Round(grandTotalCzk, 2),
                ProfitCzk = (float)Math.Round(grandTotalCzk - 8000.0f, 2),
                Percentage = (float)Math.Round((grandTotalCzk - 8000.0f) / (8000.0f / 100), 2)
            };
        }

        private static IEnumerable<Order> ProcessOrderItems(IEnumerable<OrderItem> orderItems)
        {
            var depositMocks = new List<OrderItem>
            {
                new OrderItem { Price = 90, Symbol = "LTCUSD", Amount = 0.5147f, Status = "EXECUTED"  },
                new OrderItem { Price = 107.20f, Symbol = "LTCUSD", Amount = 2.1744f, Status = "EXECUTED"  },
                new OrderItem { Price = 135.29f, Symbol = "LTCUSD", Amount = 0.6891f, Status = "EXECUTED"  }
            };

            var processedOrders = orderItems
                .Where(orderItem => orderItem.Status == "EXECUTED")
                .Concat(depositMocks)
                .GroupBy(order => order.Symbol)
                .Select(group => new Order
                {
                    Symbol = group.Key,
                    Amount = group.Sum(orderItem => orderItem.Amount),
                    Total = group.Sum(orderItem => orderItem.Amount * orderItem.Price)
                });

            return processedOrders;
        }
    }
}
