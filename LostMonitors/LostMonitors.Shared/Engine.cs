using System;
using System.Collections.Generic;
using System.Text;

namespace LostMonitors
{
    public interface IEngine
    {
        void Init(IPlayer player1, IPlayer player2);
    }

    public class Engine
    {
    }
}
