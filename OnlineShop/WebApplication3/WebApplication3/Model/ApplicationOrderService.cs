namespace WebApplication3.Model
{
    public class ApplicationOrderService : IOrderService
    {
        private readonly ILogService _logService;

        private readonly ILogger<ApplicationOrderService> _logger;

        public ApplicationOrderService(ILogService logService, ILogger<ApplicationOrderService> _logger)
        {
            _logService = logService;
            this._logger = _logger;
        }

        public void processOrder(string orderId)
        {
            _logger.LogInformation("Hello from processOrder");
            _logService.Log(orderId);
        }
    }
}
