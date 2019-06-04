namespace App.Models
{
    public class GranuleDto 
    {
        public GranuleDto(int[] inside, int objectNumber)
        {
            Inside = inside;
            ObjectNumber = objectNumber;
        }

        public int[] Inside { get; }
        public int[][] Children { get; set; }
        public int ObjectNumber { get; }
    }
}
