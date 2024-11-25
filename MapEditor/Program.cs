using GameEditor.Misc;

namespace GameEditor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Util.DesignMode = false;
            ApplicationConfiguration.Initialize();
            Util.MainWindow = new MainEditor.MainWindow();
            Application.Run(Util.MainWindow);
        }
    }
}