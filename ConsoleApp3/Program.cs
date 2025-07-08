

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Pixel[,] testData = new Pixel[,]
            {
                { new Pixel(1,1,1), new Pixel(2,2,2),new Pixel(34,4,4) },
                { new Pixel(15,15,15), new Pixel(25,25,25),new Pixel(347,47,47) },
                { new Pixel(188,188,188), new Pixel(288,288,288),new Pixel(348,489,489) }
            };
            MedianFiltration(testData);
        }

        public class Pixel
        {
            public int red;
            public int green;
            public int blue;

            public Pixel(int red, int green, int blue)
            {
                this.red = red;
                this.green = green;
                this.blue = blue;
            }
        }

        public static Pixel[,] MedianFiltration(Pixel[,] data)
        {
            return MedianFiltrationAlgorythm(data);
        }

        public static Pixel[,] MedianFiltrationAlgorythm(Pixel[,] pixeles)
        {
            //Количество строк и столбцов в результирующем массиве(res)
            int resX, resY;
            //Логика подсчета количества строк и столбцов в результирующем массиве
            //в res должно быть строк/столбцов втрое меньше, чем в изначальном, но в случае если строки/столбцы изначального массива
            //не делятся нацело на 3, то увеличиваем на 1 количество столбцов/строк в res
            resX = pixeles.GetLength(0) % 3 == 0 ? pixeles.GetLength(0) / 3 : pixeles.GetLength(0) / 3 + 1;
            resY = pixeles.GetLength(1) % 3 == 0 ? pixeles.GetLength(1) / 3 : pixeles.GetLength(1) / 3 + 1;
            //Результирующий массив
            Pixel[,] res = new Pixel[resY, resX];


            //Массивы для вычисления медиан в скользящих окнах(SliceWindow)
            int[] redPartOfPixelesFromSliceWindow = new int[9];
            int[] greenPartOfPixelesFromSliceWindow = new int[9];
            int[] bluePartOfPixelesFromSliceWindow = new int[9];

            //Переменные для хранения медиан
            int redMedian;
            int greenMedian;
            int blueMedian;

            //Первые 2 цикла двигают скользящее окно(SliceWindow),
            //Следующие 2 цикла проходят по скользящему окну и вычисляют медиану для каждого цвета
            for (int y = 0; y < pixeles.GetLength(1); y += 3)
            {
                for (int x = 0; x < pixeles.GetLength(0); x += 3)
                {
                    //Проходим по скользящему окну и наполняем наши массивы для вычисления медиан
                    for (int ySliceWindow = 0; ySliceWindow < 3; ySliceWindow++)
                    {
                        for (int xSliceWindow = 0; xSliceWindow < 3; xSliceWindow++)
                        {
                            redPartOfPixelesFromSliceWindow[xSliceWindow + ySliceWindow*3] = pixeles[y, x].red;
                            greenPartOfPixelesFromSliceWindow[xSliceWindow + ySliceWindow*3] = pixeles[y, x].green;
                            bluePartOfPixelesFromSliceWindow[xSliceWindow + ySliceWindow*3] = pixeles[y, x].blue;
                        }
                    }
                    //Сортируем
                    Array.Sort(redPartOfPixelesFromSliceWindow);
                    Array.Sort(greenPartOfPixelesFromSliceWindow);
                    Array.Sort(bluePartOfPixelesFromSliceWindow);
                    //Вычисляем медиану
                    redMedian = redPartOfPixelesFromSliceWindow[4];
                    greenMedian = greenPartOfPixelesFromSliceWindow[4];
                    blueMedian = bluePartOfPixelesFromSliceWindow[4];
                    //Формируем новый пиксель
                    Pixel pixel = new Pixel(redMedian, greenMedian, blueMedian);
                    //Добавляем пиксель в результирующий массив
                    res[y/3, x/3] = pixel;
                }
            }

            return res;
        }
    }
}
