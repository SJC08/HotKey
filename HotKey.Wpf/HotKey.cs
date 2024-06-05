using System.Windows.Input;
using System.Windows.Interop;

namespace Asjc.HotKey.Wpf
{
    public class HotKey : HotKeyBase
    {
        static HotKey() => ComponentDispatcher.ThreadFilterMessage += HotKeyHandler;

        public HotKey() { }

        public HotKey(Key key, Modifiers modifier, bool register = true)
        {
            Key = key;
            Modifier = modifier;
            if (register) Register();
        }

        public HotKey(Key key, Modifiers modifier, Action<HotKeyBase> callback, bool register = true) : this(key, modifier, register)
        {
            Pressed += callback;
        }

        public new Key Key
        {
            get => KeyInterop.KeyFromVirtualKey((int)base.Key);
            init => base.Key = (uint)KeyInterop.VirtualKeyFromKey(value);
        }

        private static void HotKeyHandler(ref MSG msg, ref bool handled)
        {
            if (msg.message == WM_HOTKEY)
                handled = Handle((int)msg.wParam);
        }
    }
}
