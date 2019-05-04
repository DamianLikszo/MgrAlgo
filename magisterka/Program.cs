using magisterka.Interfaces;
using magisterka.Services;
using SimpleInjector;
using System;
using System.Windows.Forms;
using magisterka.Validators;
using magisterka.Wrappers;
using ICoverageDataConverter = magisterka.Interfaces.ICoverageDataConverter;

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
            container.Register<IMyStreamReader, MyStreamReader>();
            container.Register<IMyMessageBox, MyMessageBox>();
            container.Register<ICoverageFileValidator, CoverageFileValidator>();
            container.Register<IMyOpenFileDialog, MyOpenFileDialog>();
            container.Register<IFileService, FileService>();
            container.Register<ICoverageDataConverter, CoverageDataConverter>();
            container.Register<IFormData, FormData>(Lifestyle.Singleton);

            container.Verify();
        }
    }
}
