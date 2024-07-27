using System.Windows;
using System.Windows.Input;

namespace Asjc.HotKey.Wpf.Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new HotKey(Key.Space, Modifiers.Ctrl, _ => MessageBox.Show("Ctrl + Space"));
        }
    }

}
