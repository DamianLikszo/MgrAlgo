using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IZbGranService
    {
        Granula SearchMin(ZbGran zbGran);
        void SortZbGran(ZbGran zbGran);
        ZbGran BuildSortedTree(ZbGran zbGranOrg);
        string ReadResult(ZbGran treeGran);
    }
}
