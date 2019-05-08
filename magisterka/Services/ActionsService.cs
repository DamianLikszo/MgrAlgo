using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Validators;

namespace magisterka.Services
{
    public class ActionsService : IActionService
    {
        private readonly IFormData _formData;
        private readonly IFileService _fileService;
        private readonly ICoverageDataConverter _coverageDataConverter;
        private readonly ICoverageFileValidator _coverageFileValidator;
        private readonly IGranuleService _granuleService;

        public ActionsService(IFormData formData, IFileService fileService,
            ICoverageDataConverter coverageDataConverter, ICoverageFileValidator coverageFileValidator,
            IGranuleService granuleService)
        {
            _formData = formData;
            _fileService = fileService;
            _coverageDataConverter = coverageDataConverter;
            _coverageFileValidator = coverageFileValidator;
            _granuleService = granuleService;
        }

        //TODO: add tests
        public bool Load()
        {
            var path = _fileService.SelectFile();
            if (path == null)
                return false;

            var content = _fileService.ReadFile(path);
            if (content == null)
                return false;

            var data = _coverageDataConverter.Convert(content);
            if (data == null)
                return false;

            var coverageFile = new CoverageFile(path, data);
            if (!_coverageFileValidator.ValidAndShow(coverageFile))
            {
                return false;
            }

            var zbGran = _granuleService.GenerateGran(coverageFile.CoverageData);
            _formData.PathSource = path;
            _formData.GranuleSet = zbGran;
            
            return true;
        }
    }
}
