using System.Collections.Generic;

namespace Bludk
{
    public interface IScreenResolver
    {
        IEnumerator<TUI> Load<TUI>() where TUI : ScreenUI;
    }
}