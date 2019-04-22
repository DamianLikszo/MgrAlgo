using magisterka.Models;
using magisterka.Wrappers;

namespace magisterka.Validators
{
    public interface ICoverageFileValidator
    {
        bool Valid(CoverageFile coverageFile, out string errorMessage);
        bool ValidAndShow(CoverageFile coverageFile, IMyMessageBox myMessageBox);
    }
}
