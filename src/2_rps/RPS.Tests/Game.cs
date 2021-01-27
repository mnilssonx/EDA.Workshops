using System.Collections.Generic;
using System.Linq;

namespace RPS.Tests
{
    public static class Game
    {
        public static IEnumerable<IEvent> Handle(CreateGame command, GameState state)
            => new IEvent[] { new GameCreated() };

        public static IEnumerable<IEvent> Handle(JoinGame command, GameState state)
            => command.PlayerId != state.CreatorId ? new IEvent[] { new GameStarted(), new RoundStarted() } : Enumerable.Empty<IEvent>();

        public static IEnumerable<IEvent> Handle(PlayGame command, GameState state)
            => new IEvent[] { new HandShown() };
    }
}