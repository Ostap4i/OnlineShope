namespace WebApplication3.Model
{
    public class ApplicationLogService : ILogService
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
