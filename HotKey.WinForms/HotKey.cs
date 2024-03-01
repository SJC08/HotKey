namespace Asjc.HotKey.WinForms
{
    public class HotKey : HotKeyBase
    {
        static HotKey() => Application.AddMessageFilter(new HotKeyMessageFilter());

        public HotKey() { }

        public HotKey(Keys key, Modifiers modifier, bool register = true)
        {
            Key = key;
            Modifier = modifier;
            if (register) Register();
        }

        public HotKey(Keys key, Modifiers modifier, EventHandler callback, bool register = true) : this(key, modifier, register)
        {
            Pressed += callback;
        }

        public new Keys Key
        {
            get => (Keys)base.Key;
            init => base.Key = (uint)value;
        }

        private class HotKeyMessageFilter : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                    return Handle((int)m.WParam);
                else
                    return false;
            }
        }
    }
}
