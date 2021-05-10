using TeamBuilder.App.Core;

namespace TeamBuilder.App
{
    class Application
    {
        static void Main(string[] args)
        {
            AuthenticationManager authenticationManager = new AuthenticationManager();
            CommandDispatcher commandDispatcher = new CommandDispatcher(authenticationManager);
            Engine engine = new Engine(commandDispatcher);
            engine.Run();
        }
    }
}