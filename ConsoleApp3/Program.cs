

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Pixel[,] testData = new Pixel[,]
            {
                { new Pixel(1,2,3), new Pixel(11,22,33),new Pixel(111,222, 333), new Pixel(1111,2222,3333), new Pixel(11111,22222,33333),new Pixel(111111,222222,333333) },
                { new Pixel(7,8,9), new Pixel(77,88,99),new Pixel(777,888,999) , new Pixel(7777,8888,9999), new Pixel(77777,88888,99999),new Pixel(777777,888888,999999) },
                { new Pixel(4,5,6), new Pixel(44,55,66),new Pixel(444,555,666),  new Pixel(4444,5555,6666), new Pixel(44444,55555,66666),new Pixel(444444,555555,666666) }
            };
            Pixel[,] testData1 = new Pixel[,]
            {
                { new Pixel(1,2,3), new Pixel(4,22,33),new Pixel(7,222, 333) }, 
                { new Pixel(2,2222,3333), new Pixel(5,22222,33333),new Pixel(8,222222,333333) },
                { new Pixel(3,8,9), new Pixel(6,88,99),new Pixel(9,888,999) }, 
                { new Pixel(3,8888,9999), new Pixel(4,88888,99999),new Pixel(7,888888,999999) },
                { new Pixel(2,5,6), new Pixel(5,55,66),new Pixel(8,555,666) }, 
                { new Pixel(1,5555,6666), new Pixel(6,55555,66666),new Pixel(9,555555,666666) }
            };
            MedianFiltration(testData);
            MedianFiltration(testData1);
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
            resY = pixeles.GetLength(0) % 3 == 0 ? pixeles.GetLength(0) / 3 : pixeles.GetLength(0) / 3 + 1;
            resX = pixeles.GetLength(1) % 3 == 0 ? pixeles.GetLength(1) / 3 : pixeles.GetLength(1) / 3 + 1;
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


            //Объявление переменных для циклов, чтобы не путаться
            int dataX = pixeles.GetLength(1);
            int dataY = pixeles.GetLength(0);
            //Первые 2 цикла двигают скользящее окно(SliceWindow),
            //Следующие 2 цикла проходят по скользящему окну и вычисляют медиану для каждого цвета
            for (int y = 0; y < dataY; y += 3)
            {
                for (int x = 0; x < dataX; x += 3)
                {
                    //Проходим по скользящему окну и наполняем наши массивы для вычисления медиан
                    for (int ySliceWindow = 0; ySliceWindow < 3; ySliceWindow++)
                    {
                        for (int xSliceWindow = 0; xSliceWindow < 3; xSliceWindow++)
                        {
                            redPartOfPixelesFromSliceWindow[xSliceWindow + ySliceWindow*3] = pixeles[y + ySliceWindow, x + xSliceWindow].red;
                            greenPartOfPixelesFromSliceWindow[xSliceWindow + ySliceWindow*3] = pixeles[y + ySliceWindow, x + xSliceWindow].green;
                            bluePartOfPixelesFromSliceWindow[xSliceWindow + ySliceWindow*3] = pixeles[y + ySliceWindow, x + xSliceWindow].blue;
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
