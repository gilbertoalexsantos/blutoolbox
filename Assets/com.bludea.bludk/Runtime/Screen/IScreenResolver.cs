using System.Collections;
using System.Collections.Generic;

namespace Bludk
{
    public interface IScreenResolver
    {
        IEnumerator<TUI> Load<TUI>() where TUI : ScreenUI;
        IEnumerator Unload<TUI>(TUI ui) where TUI : ScreenUI;
    }
}