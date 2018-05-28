using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IZbGranService
    {
        Granula SearchMin(ZbGran zbGran);
        ZbGran BuildSortedTree(ZbGran zbGran);
        string ReadResult(ZbGran treeGran);
    }
}
