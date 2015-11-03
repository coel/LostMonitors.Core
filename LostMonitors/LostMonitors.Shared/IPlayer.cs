using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMonitors
{
    public interface IPlayer
    {
        Turn Play(BoardState currentState, List<Change> whatChanged);
    }
}
