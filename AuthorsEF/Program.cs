namespace AuthorsEF
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var mainView = new Form1();
            var model = new Model();
            var mainPresenter = new MainPresenter(mainView, model);
            Application.Run(mainView);
        }
    }
}