using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Asjc.HotKey
{
    public abstract partial class HotKeyBase
    {
        /// <summary>
        /// Posted when the user presses a hot key registered by <see cref="RegisterHotKey(nint, int, Modifiers, uint)"/>.
        /// </summary>
        protected const int WM_HOTKEY = 0x0312;


        protected static readonly Dictionary<int, HotKeyBase> map = [];

        /// <summary>
        /// Defines a system-wide hot key.
        /// </summary>
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        protected static partial bool RegisterHotKey(IntPtr hWnd, int id, Modifiers fsModifiers, uint vk);

        /// <summary>
        /// Frees a hot key previously registered by the calling thread.
        /// </summary>
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        protected static partial bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// An <see langword="int"/> indicating the id of the hot key.
        /// </summary>
        [JsonIgnore]
        public int Id { get; protected set; }

        /// <summary>
        /// A <see langword="bool"/> indicating whether the hot key has been registered.
        /// </summary>
        [JsonIgnore]
        public bool Registered { get; protected set; }

        /// <summary>
        /// The virtual-key code of the hot key.
        /// </summary>
        public uint Key { get; init; }

        /// <summary>
        /// The keys that must be pressed in combination with the key specified by <see cref="Key"/> in order to generate the <see cref="WM_HOTKEY"/> message.
        /// </summary>
        public Modifiers Modifier { get; init; }

        /// <summary>
        /// Occurs when the hot key is pressed.
        /// </summary>
        public event Action<HotKeyBase>? Pressed;

        /// <summary>
        /// Register the hot key.
        /// </summary>
        /// <returns>
        /// A <see langword="bool"/> indicating whether the operation was successful.
        /// </returns>
        public virtual bool Register()
        {
            if (Registered) return true;
            int id = (int)Modifier << 16 | (int)Key;
            if (RegisterHotKey(IntPtr.Zero, id, Modifier, Key))
            {
                // If successful.
                Id = id;
                Registered = true;
                map.Add(Id, this);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Unregister the hot key.
        /// </summary>
        /// <returns>
        /// A <see langword="bool"/> indicating whether the operation was successful.
        /// </returns>
        public virtual bool Unregister()
        {
            if (!Registered) return false;
            if (UnregisterHotKey(IntPtr.Zero, Id))
            {
                // If successful.
                Id = default;
                Registered = false;
                map.Remove(Id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Handle a hot key with the specified id by invoking <see cref="Pressed"/>.
        /// </summary>
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
