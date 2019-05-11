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
        private readonly Mock<IMyStreamWriter> _myStreamWriterMock;
        private readonly Mock<IMyMessageBox> _myMessageBoxMock;
        private readonly Mock<IMyOpenFileDialog> _myOpenFileDialogMock;
        private readonly Mock<IMySaveFileDialog> _mySaveFileDialogMock;
        private readonly IFileService _fileService;

        public FileServiceTests()
        {
            _myStreamReaderMock = new Mock<IMyStreamReader>();
            _myStreamWriterMock = new Mock<IMyStreamWriter>();
            _myMessageBoxMock = new Mock<IMyMessageBox>();
            _myOpenFileDialogMock = new Mock<IMyOpenFileDialog>();
            _mySaveFileDialogMock = new Mock<IMySaveFileDialog>();
            _fileService = new FileService(_myStreamReaderMock.Object, _myMessageBoxMock.Object,
                _myOpenFileDialogMock.Object, _mySaveFileDialogMock.Object, _myStreamWriterMock.Object);
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
            var expected = new List<string> { "1;2;3;", "1;2;3;", "1;2;3;" };
            Assert.Equal(expected, result);
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
        public void GetPathFromOpenFileDialog_WhenSelectFile_ThenShouldReturnFilePath()
        {
            //Arrange
            var path = "path";
            _myOpenFileDialogMock.Setup(x => x.ShowDialog()).Returns(DialogResult.OK);
            _myOpenFileDialogMock.Setup(x => x.FileName).Returns(path);

            //Act
            var result = _fileService.GetPathFromOpenFileDialog();

            //Assert
            Assert.Equal(path, result);
        }

        [Fact]
        public void GetPathFromOpenFileDialog_WhenDoNotSelectFile_ThenShouldReturnNull()
        {
            //Arrange
            _myOpenFileDialogMock.Setup(x => x.ShowDialog()).Returns(DialogResult.Cancel);

            //Act
            var result = _fileService.GetPathFromOpenFileDialog();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetPathFromSaveFileDialog_WhenSelectFile_ThenShouldReturnFilePath()
        {
            //Arrange
            var path = "path";
            _mySaveFileDialogMock.Setup(x => x.ShowDialog()).Returns(DialogResult.OK);
            _mySaveFileDialogMock.Setup(x => x.FileName).Returns(path);

            //Act
            var result = _fileService.GetPathFromSaveFileDialog();

            //Assert
            Assert.Equal(path, result);
        }

        [Fact]
        public void GetPathFromSaveFileDialog_WhenDoNotSelectFile_ThenShouldReturnNull()
        {
            //Arrange
            _mySaveFileDialogMock.Setup(x => x.ShowDialog()).Returns(DialogResult.Cancel);

            //Act
            var result = _fileService.GetPathFromSaveFileDialog();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void SaveFile_WhenPathIsEmpty_ThenShouldReturnFalseAndShowMessage()
        {
            //Arrange
            var content = new List<string> {"some line"};

            //Act
            var result = _fileService.SaveFile(null, content);

            //Assert
            Assert.False(result);
            _myMessageBoxMock.Verify(x => x.Show(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void SaveFile_WhenStreamWriterThrowException_ThenReturnNullAndShowMessage()
        {
            //Arrange
            var path = "path";
            var content = new List<string>{"some line", "second line"};
            _myStreamWriterMock.Setup(x => x.GetStreamWriter(path)).Throws(new Exception());

            //Act
            var result = _fileService.SaveFile(path, content);

            //Assert
            Assert.False(result);
            _myMessageBoxMock.Verify(x => x.Show(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void SaveFile_WhenEverythingIsFine_ThenShouldReturnTrue()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "some line", "second line" };
            var ms = new MemoryStream();
            _myStreamWriterMock.Setup(x => x.GetStreamWriter(path)).Returns(new StreamWriter(ms));
            
            //Act
            var result = _fileService.SaveFile(path, content);

            //Assert
            var actual = Encoding.UTF8.GetString(ms.ToArray());
            var expected = string.Join(Environment.NewLine, content) + Environment.NewLine;
            Assert.True(result);
            Assert.Equal(expected, actual);
        }
    }
}
