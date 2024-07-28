namespace Asjc.HotKey
{
    [Flags]
    public enum Modifiers : uint
    {
        None = 0x0000,

        /// <summary>
        /// Either ALT key must be held down.
        /// </summary>
        Alt = 0x0001,

        /// <summary>
        /// Either CTRL key must be held down.
        /// </summary>
        Ctrl = 0x0002,

        /// <summary>
        /// Either SHIFT key must be held down.
        /// </summary>
        Shift = 0x0004,

        /// <summary>
        /// Either WINDOWS key must be held down.
        /// </summary>
        Win = 0x0008,

        /// <summary>
        /// Changes the hotkey behavior so that the keyboard auto-repeat does not yield multiple hotkey notifications.
        /// </summary>
        NoRepeat = 0x4000
    }
}
