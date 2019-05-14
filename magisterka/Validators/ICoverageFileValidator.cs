using magisterka.Models;

namespace magisterka.Validators
{
    public interface ICoverageFileValidator
    {
        bool Valid(CoverageFile coverageFile, out string errorMessage);
    }
}
