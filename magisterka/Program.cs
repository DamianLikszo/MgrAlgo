using magisterka.Interfaces;
using magisterka.Services;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Windows.Forms;

namespace magisterka
{
    static class Program
    {
        private static Container container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(container.GetInstance<Form>());
        }

        private static void Bootstrap()
        {
            container = new Container();

            container.Register<Interfaces.IFileReaderService, FileReaderService>();
            container.Register<IGranuleService, GranuleService>();
            container.Register<IZbGranService, ZbGranService>();
            container.Register<IDevService, DevService>();

            container.Verify();
        }
    }
}
