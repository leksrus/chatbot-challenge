namespace StockBot.Domain.Entities;

public class Bot
{
    private readonly IDictionary<string, string> _commands;


    public Bot()
    {
        _commands = new Dictionary<string, string>
        {
            {"/stock", "Information about stock"}
        };
    }

    public bool IsValidCommand(string key)
    {
        return _commands.TryGetValue(key, out _);
    }
}