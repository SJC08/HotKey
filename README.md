Register hotkeys easily!

**Use `Asjc.*` instead of `AS.*` and `AsUtils.*`!**

## Getting Started

First, install `AS.HotKey.Wpf` or `AS.HotKey.WinForms`, and then import the corresponding namespace.

Quickly register hotkeys with just **one line** of code, such as:

```csharp
new HotKey(Key.Space, Modifiers.Ctrl, (_, _) => MessageBox.Show("Ctrl + Space"));
```

