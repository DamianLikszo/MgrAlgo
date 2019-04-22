using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Services;
using magisterka.Wrappers;
using Moq;
using Xunit;

namespace Test
{
    public class FileServiceTests
    {
        private readonly Mock<IMyStreamReader> _myStreamReaderMock;
        private readonly Mock<IMyMessageBox> _myMessageBoxMock;
        private readonly Mock<IMyOpenFileDialog> _myOpenFileDialogMock;
        private readonly IFileService _fileService;

        public FileServiceTests()
        {
            _myStreamReaderMock = new Mock<IMyStreamReader>();
            _myMessageBoxMock = new Mock<IMyMessageBox>();
            _myOpenFileDialogMock = new Mock<IMyOpenFileDialog>();
            _fileService = new FileService(_myStreamReaderMock.Object, _myMessageBoxMock.Object,
                _myOpenFileDialogMock.Object);
        }

        [Fact]
        public void ReadFile_WhenPutRightFile_ThenShouldReturnContent()
        {
            //Arrange
            var path = "path";
            var content = $"1;2;3;{Environment.NewLine}1;2;3;{Environment.NewLine}1;2;3;{Environment.NewLine}";
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(content));
            _myStreamReaderMock.Setup(x => x.GetReader(path)).Returns(new StreamReader(ms));

            //Act
            var result = _fileService.ReadFile(path);

            //Assert
            var expect = new List<string> { "1;2;3;", "1;2;3;", "1;2;3;" };
            Assert.Equal(expect, result);
        }

        [Fact]
        public void ReadFile_WhenStreamReaderThrowException_ThenReturnNullAndShowMessage()
        {
            //Arrange
            var path = "path";
            _myStreamReaderMock.Setup(x => x.GetReader(path)).Throws(new Exception());

            //Act
            var result = _fileService.ReadFile(path);

            //Assert
            Assert.Null(result);
            _myMessageBoxMock.Verify(x => x.Show(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void SelectFile_WhenSelectFile_ThenShouldReturnFilePath()
        {
            //Arrange
            var path = "path";
            _myOpenFileDialogMock.Setup(x => x.ShowDialog()).Returns(DialogResult.OK);
            _myOpenFileDialogMock.Setup(x => x.FileName).Returns(path);

            //Act
            var result = _fileService.SelectFile();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(path, result);
        }

        [Fact]
        public void SelectFile_WhenNotSelectFile_ThenShouldReturnNull()
        {
            //Arrange
            _myOpenFileDialogMock.Setup(x => x.ShowDialog()).Returns(DialogResult.Cancel);

            //Act
            var result = _fileService.SelectFile();

            //Assert
            Assert.Null(result);
        }
    }
}
