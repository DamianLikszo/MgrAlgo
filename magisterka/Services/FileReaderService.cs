using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka.Services
{
    public class FileReaderService : IFileReaderService
    {
        private readonly IFileService _fileService;
        private readonly char _separator = ';';

        public FileReaderService(IFileService fileService)
        {
            _fileService = fileService;
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
        public bool SaveFile(GranuleSet granuleSet, out string error)
        {
            error = null;
            var path = _fileService.GetPathFromSaveFileDialog(FileService.CsvFilter);
            if (string.IsNullOrEmpty(path))
            {
                if (path == string.Empty)
                {
                    error = "Empty file path.";
                }

                return false;
            }

            var content = PreparePrint(granuleSet);
            return _fileService.SaveFile(path, content, out error);
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
