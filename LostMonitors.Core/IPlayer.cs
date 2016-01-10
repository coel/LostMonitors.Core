using System;

namespace LostMonitors.Core
{
    public interface IPlayer
    {
        void Play(BoardState currentState, Turn theirMove, Func<Turn, Destination, Card> draw);
        string GetFriendlyName();
    }
}
