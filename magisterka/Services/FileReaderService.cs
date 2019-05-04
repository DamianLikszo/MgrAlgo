using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Validators;

namespace magisterka.Services
{
    public class FileReaderService : Interfaces.IFileReaderService
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
            CoverageFile result = null;

            var path = _fileService.SelectFile();
            
            if(path != null)
            {
                var content = _fileService.ReadFile(path);
                if (content == null)
                    return null;

                var data = _coverageDataConverter.Convert(content);
                if (data == null)
                    return null;

                result = new CoverageFile(path, data);
                if (!_coverageFileValidator.ValidAndShow(result))
                {
                    return null;
                }
            }

            return result;
        }

        //TODO: other File
        public bool SaveFile(List<Granule> data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Plik tekstowe|*.csv";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
                {
                    _printHeader(writer, data);

                    if (data.Count != 0)
                    {
                        var length = data[0].Count();

                        for (int i = 0; i < length; i++)
                        {
                            writer.Write($"u{i + 1}");

                            foreach (var granula in data)
                            {
                                writer.Write(_separator + granula[i].ToString());
                            }

                            writer.WriteLine();
                        }
                    }

                    writer.Close();
                }
            }

            return true;
        }

        private void _printHeader(StreamWriter writer, List<Granule> data)
        {
            writer.Write("obiekt/g(obiekt)");

            for (int i = 0; i < data.Count; i++)
            {
                writer.Write(_separator + $"g(u{i+1})");
            }

            writer.WriteLine();
        }
    }
}
