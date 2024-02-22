using System.Runtime.InteropServices;

namespace AS.HotKey
{
    public abstract class HotKeyBase
    {
        protected const int WM_HOTKEY = 0x0312;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, Modifiers fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static readonly Dictionary<int, HotKeyBase> map = [];

        public int Id => (int)Modifier << 16 | (int)Key;

        public uint Key { get; init; }

        public Modifiers Modifier { get; init; }

        public event EventHandler? Pressed;

        public bool Register()
        {
            bool result = RegisterHotKey(IntPtr.Zero, Id, Modifier, Key);
            if (result) map.Add(Id, this);
            return result;
        }

        public bool Unregister()
        {
            bool result = UnregisterHotKey(IntPtr.Zero, Id);
            if (result) map.Remove(Id);
            return result;
        }

        protected static bool Handle(int id)
        {
            if (map.TryGetValue(id, out var hotKey))
            {
                hotKey.Pressed?.Invoke(hotKey,EventArgs.Empty);
                return true;
            }
            return false;
        }
    }

    [Flags]
    public enum Modifiers : uint
    {
        None = 0x0000,
        Alt = 0x0001,
        Ctrl = 0x0002,
        Shift = 0x0004,
        Win = 0x0008,
        NoRepeat = 0x4000
    }
}
