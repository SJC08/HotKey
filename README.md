# HotKey

[![NuGet](https://img.shields.io/nuget/v/Asjc.HotKey?logo=nuget&label=Asjc.HotKey)](https://www.nuget.org/packages/Asjc.HotKey/)
[![NuGet](https://img.shields.io/nuget/v/Asjc.HotKey.Wpf?logo=nuget&label=Asjc.HotKey.Wpf)](https://www.nuget.org/packages/Asjc.HotKey.Wpf/)
[![NuGet](https://img.shields.io/nuget/v/Asjc.HotKey.WinForms?logo=nuget&label=Asjc.HotKey.WinForms)](https://www.nuget.org/packages/Asjc.HotKey.WinForms/)

Register hotkeys easily!

## Getting Started

First, install `AS.HotKey.Wpf` or `AS.HotKey.WinForms`, and then import the corresponding namespace.

Quickly register hotkeys with just **one line** of code, such as:

```csharp
new HotKey(Key.Space, Modifiers.Ctrl, _ => MessageBox.Show("Ctrl + Space"));
```

