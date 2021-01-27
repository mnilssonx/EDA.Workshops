namespace RPS.Tests
{
    public class GameState
    {
        public GameState When(IEvent @event) => this;

        public GameState When(GameCreated gameCreated)
        {
            CreatorId = gameCreated.PlayerId;
            Status = GameStatus.ReadyToStart;
            return this;
        }

        public string CreatorId { get; set; }

        public GameStatus Status { get; set; }

        public enum GameStatus
        {
            None = 0,
            ReadyToStart = 10,
            Started = 20,
            Ended = 50
        }
    }
}