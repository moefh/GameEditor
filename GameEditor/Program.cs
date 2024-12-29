using GameEditor.MainEditor;
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
            Util.CreateProjectWindow().Show();
            Application.Run();
        }
    }
}