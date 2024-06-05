using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Asjc.HotKey
{
    public abstract class HotKeyBase
    {
        protected const int WM_HOTKEY = 0x0312;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, Modifiers fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static readonly Dictionary<int, HotKeyBase> map = [];

        [JsonIgnore]
        public int Id => (int)Modifier << 16 | (int)Key;

        public uint Key { get; init; }

        public Modifiers Modifier { get; init; }

        public event Action<HotKeyBase>? Pressed;

        public virtual bool Register()
        {
            bool result = RegisterHotKey(IntPtr.Zero, Id, Modifier, Key);
            if (result) map.Add(Id, this);
            return result;
        }

        public virtual bool Unregister()
        {
            bool result = UnregisterHotKey(IntPtr.Zero, Id);
            if (result) map.Remove(Id);
            return result;
        }

        protected static bool Handle(int id)
        {
            if (map.TryGetValue(id, out var hotKey))
            {
                hotKey.Pressed?.Invoke(hotKey);
                return true;
            }
            return false;
        }
    }
}
