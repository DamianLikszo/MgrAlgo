using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Validators;

namespace magisterka.Services
{
    public class FileReaderService : IFileReaderService
    {
        private readonly ICoverageFileValidator _coverageFileValidator;
        private readonly IFileService _fileService;
        private readonly ICoverageDataConverter _coverageDataConverter;
        private readonly char _separator = ';';

        public FileReaderService(ICoverageFileValidator coverageFileValidator, IFileService fileService,
            ICoverageDataConverter coverageDataConverter)
        {
            _coverageFileValidator = coverageFileValidator;
            _fileService = fileService;
            _coverageDataConverter = coverageDataConverter;
        }

        public CoverageFile OpenAndReadFile()
        {
            var path = _fileService.GetPathFromOpenFileDialog(FileService.CsvFilter);
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            var content = _fileService.ReadFile(path);
            if (content == null)
            {
                return null;
            }

            var coverageData = _coverageDataConverter.Convert(content);
            if (coverageData == null)
            {
                return null;
            }

            var result = new CoverageFile(path, coverageData);
            return _coverageFileValidator.ValidAndShow(result) ? result : null;
        }

        public List<string> PreparePrint(GranuleSet granuleSet)
        {
            var content = new List<string> { _printHeader(granuleSet) };

            if (granuleSet.Count > 0)
            {
                content.AddRange(_printContent(granuleSet));
            }

            return content;
        }

        //TODO: other File, Rename, maybe t
        public bool SaveFile(GranuleSet granuleSet)
        {
            var path = _fileService.GetPathFromSaveFileDialog(FileService.CsvFilter);
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            var content = PreparePrint(granuleSet);
            var result = _fileService.SaveFile(path, content);
            return result;
        }

        private List<string> _printContent(GranuleSet granuleSet)
        {
            var content = new List<string>();

            var length = granuleSet[0].Count();
            for (var i = 0; i < length; i++)
            {
                var line = $"u{i + 1}";

                foreach (var granule in granuleSet)
                {
                    line += _separator + granule[i].ToString();
                }

                content.Add(line);
            }

            return content;
        }

        private string _printHeader(GranuleSet granuleSet)
        {
            var header = "";

            for (var i = 0; i < granuleSet.Count; i++)
            {
                if (i > 0)
                {
                    header += _separator;
                }

                header += $"g(u{i + 1})";
            }

            return header;
        }
    }
}
