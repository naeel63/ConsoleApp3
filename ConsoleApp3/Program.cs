

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public class Pixel
        {
            public int red;
            public int green;
            public int blue;
        }

        public Pixel[][] MedianFiltration(Pixel[][] data)
        {
            return MedianFiltrationAlgorythm(data);
        }

        public Pixel[][] MedianFiltrationAlgorythm(Pixel[][] pixeles)
        {
            Pixel[][] res = new Pixel[(int)Math.Round(pixeles.Length/3.0)][];

            int[] redPartOfPixelesFromSliceWindow = new int[9];
            int[] greenPartOfPixelesFromSliceWindow = new int[9];
            int[] bluePartOfPixelesFromSliceWindow = new int[9];

            for (int i = 0; i < pixeles.Length; i += 3)
            {
                for (int j = 0; j < pixeles[i].Length; j+= 3)
                {
                    for (int ySliceWindow = 0; ySliceWindow < 3; ySliceWindow++)
                    {
                        for (int xSliceWindow = 0; xSliceWindow < 3; xSliceWindow++)
                        {
                            
                        }
                    }
                }
            }

            return res;
        }
    }
}
