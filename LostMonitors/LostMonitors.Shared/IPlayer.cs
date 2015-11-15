using System;

namespace LostMonitors
{
    public interface IPlayer
    {
        void Play(BoardState currentState, Turn theirMove, Func<Turn, Destination, Card> draw);
    }
}
