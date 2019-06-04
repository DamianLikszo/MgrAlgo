using App.Models;

namespace App.Validators
{
    public interface ICoverageFileValidator
    {
        bool Valid(CoverageFile coverageFile, out string error);
    }
}
