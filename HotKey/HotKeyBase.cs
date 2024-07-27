using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Asjc.HotKey
{
    public abstract partial class HotKeyBase
    {
        protected const int WM_HOTKEY = 0x0312;
        protected static readonly Dictionary<int, HotKeyBase> map = [];

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        protected static partial bool RegisterHotKey(IntPtr hWnd, int id, Modifiers fsModifiers, uint vk);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        protected static partial bool UnregisterHotKey(IntPtr hWnd, int id);

        [JsonIgnore]
        public int Id { get; protected set; }

        [JsonIgnore]
        public bool Registered { get; protected set; }

        public uint Key { get; init; }

        public Modifiers Modifier { get; init; }

        public event Action<HotKeyBase>? Pressed;

        public virtual bool Register()
        {
            if (Registered) return true;
            int id = (int)Modifier << 16 | (int)Key;
            if (RegisterHotKey(IntPtr.Zero, id, Modifier, Key))
            {
                Id = id;
                Registered = true;
                map.Add(Id, this);
                return true;
            }
            return false;
        }

        public virtual bool Unregister()
        {
            if (!Registered) return false;
            if (UnregisterHotKey(IntPtr.Zero, Id))
            {
                Id = default;
                Registered = false;
                map.Remove(Id);
                return true;
            }
            return false;
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
