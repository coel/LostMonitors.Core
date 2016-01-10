using System;

namespace LostMonitors.Core
{
    public interface IPlayer
    {
        void Play(BoardState currentState, Turn theirMove, Func<Turn, Card> draw);
        string GetFriendlyName();
    }
}
