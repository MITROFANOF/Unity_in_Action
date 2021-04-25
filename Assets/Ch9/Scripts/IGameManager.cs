namespace Ch9.Scripts
{
    public interface IGameManager
    {
        ManagerStatus Status { get; }

        void Startup();
    }
}