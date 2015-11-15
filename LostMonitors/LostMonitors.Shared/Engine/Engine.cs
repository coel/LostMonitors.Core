﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LostMonitors.Engine
{
    public interface IEngine
    {
        GlobalBoardState Init(IPlayer player1, IPlayer player2);
        GlobalBoardTurn Play();
    }

    public class Engine : IEngine
    {
        private EnginePlayer _player1;
        private EnginePlayer _player2;
        private EnginePlayer _nextPlayer;
        private Turn _lastTurn;
        private Deck _deck;

        internal const int InitialCards = 8;
        private Dictionary<Destination, Stack<Card>> _discards; 

        public GlobalBoardState Init(IPlayer player1, IPlayer player2)
        {
            _player1 = new EnginePlayer(player1);
            _player2 = new EnginePlayer(player2);
            _nextPlayer = _player1;
            _lastTurn = null;
            _deck = new Deck();

            for (var i = 0; i < InitialCards; i++)
            {
                _player1.Cards.Add(_deck.Draw());
                _player2.Cards.Add(_deck.Draw());
            }

            _discards = new Dictionary<Destination, Stack<Card>>();
            var destinations = Enum.GetValues(typeof(Destination)).Cast<Destination>().ToList();

            foreach (var destination in destinations)
            {
                _discards.Add(destination, new Stack<Card>());
            }

            return new GlobalBoardState(_player1, _player2, _deck, _discards);
        }

        public GlobalBoardTurn Play()
        {
            Turn playerTurn = null;
            Card drawn = null;

            Func<Turn, Destination, Card> draw = (turn, destination) =>
            {
                if (playerTurn != null)
                {
                    throw new Exception("You can have more than one turn!");
                }
                playerTurn = turn;

                if (turn.Draw.HasValue)
                {
                    drawn = _discards[turn.Draw.Value].Pop();
                }
                else
                {
                    drawn = _deck.Draw();
                }
                return drawn;
            };

            // TODO: if I pass this lambda in, can information be extracted from it?
            _nextPlayer.Player.Play(new BoardState(_deck, _discards, _nextPlayer, OtherPlayer(_nextPlayer)), _lastTurn, draw);

            if (playerTurn == null)
            {
                throw new Exception("Player returned without making a turn");
            }

            if (_nextPlayer.Cards.Contains(playerTurn.Card))
            {
                _nextPlayer.Cards.Remove(playerTurn.Card);
            }
            else
            {
                throw new Exception(string.Format("Player tried to play card they are not holding: {0}, {1}", playerTurn.Card.Destination, playerTurn.Card.Destination));
            }

            if (playerTurn.Discard)
            {
                _discards[playerTurn.Card.Destination].Push(playerTurn.Card);
            }
            else
            {
                var result = _nextPlayer.AddExpedition(playerTurn.Card);
                if (!result)
                {
                    throw new Exception(string.Format("Player tried to play card ({0}, {1}) lower than last expedition card", playerTurn.Card.Destination, playerTurn.Card.Value));
                }
            }

            _nextPlayer.Cards.Add(drawn);

            _lastTurn = playerTurn;

            _nextPlayer = OtherPlayer(_nextPlayer);

            return new GlobalBoardTurn(playerTurn, drawn);
        }

        private EnginePlayer OtherPlayer(EnginePlayer player)
        {
            if (player == _player1)
                return _player2;

            return _player1;
        }
    }
}
