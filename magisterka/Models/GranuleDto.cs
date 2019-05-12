namespace magisterka.Models
{
    public class GranuleDto 
    {
        public GranuleDto(int[] inside)
        {
            Inside = inside;
        }

        public int[] Inside { get; }
        public int[][] Child { get; set; }

    }
}
