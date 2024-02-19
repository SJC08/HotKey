using System.Windows.Input;
using System.Windows.Interop;

namespace AS.HotKey.Wpf
{
    public class HotKey : HotKeyBase
    {
        static HotKey() => ComponentDispatcher.ThreadFilterMessage += HotKeyHandler;

        public HotKey() { }

        public HotKey(Key key, ModifierKeys modifier)
        {
            Key = key;
            Modifier = modifier;
        }

        public HotKey(Key key, ModifierKeys modifier, HotKeyCallback callback) : this(key, modifier)
        {
            Pressed += callback;
        }

        public Key Key
        {
            get => KeyInterop.KeyFromVirtualKey((int)KeyCode);
            init => KeyCode = (uint)KeyInterop.VirtualKeyFromKey(value);
        }

        public ModifierKeys Modifier
        {
            get => (ModifierKeys)ModifierCode;
            init => ModifierCode = (uint)value;
        }

        private static void HotKeyHandler(ref MSG msg, ref bool handled)
        {
            if (msg.message == WM_HOTKEY)
                handled = Handle((int)msg.wParam);
        }
    }
}
