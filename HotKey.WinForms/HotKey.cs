namespace AS.HotKey.WinForms
{
    public class HotKey : HotKeyBase
    {
        static HotKey() => Application.AddMessageFilter(new HotkeyMessageFilter());

        private class HotkeyMessageFilter : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                throw new NotImplementedException();
            }
        }
    }
}
