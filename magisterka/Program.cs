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
        private static Container _container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(_container.GetInstance<Form>());
        }

        private static void Bootstrap()
        {
            _container = new Container();

            // Wrappers
            _container.Register<IMyStreamReader, MyStreamReader>();
            _container.Register<IMyMessageBox, MyMessageBox>();
            _container.Register<IMyOpenFileDialog, MyOpenFileDialog>();
            _container.Register<IMySaveFileDialog, MySaveFileDialog>();
            _container.Register<IMyStreamWriter, MyStreamWriter>();

            // Services
            _container.Register<Interfaces.IFileReaderService, FileReaderService>();
            _container.Register<IGranuleService, GranuleService>();
            _container.Register<IGranuleSetPresenter, GranuleSetPresenter>();
            _container.Register<IActionService, ActionsService>();
            _container.Register<ICoverageDataConverter, CoverageDataConverter>();
            _container.Register<IFileService, FileService>();
            _container.Register<IFormData, FormData>(Lifestyle.Singleton);
            _container.Register<IGranuleSetDtoConverter, GranuleSetDtoConverter>();

            // Other
            _container.Register<ICoverageFileValidator, CoverageFileValidator>();
            _container.Register<IGranuleComparerForBuildTree, NumberOfOnesForGranuleComparer>();

            _container.Verify();
        }
    }
}
