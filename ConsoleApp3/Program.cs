

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            
            Pixel[,] testData0 = new Pixel[,]
            {
                { new Pixel(1,2,3), new Pixel(4,22,33),new Pixel(7,222, 168) },
                { new Pixel(2,2222,15), new Pixel(5,55,132),new Pixel(8,190,97) },
                { new Pixel(3,8,9), new Pixel(6,88,34),new Pixel(9,180,90) },
            };
            Pixel[,] testData = new Pixel[,]
            {
                { new Pixel(1,2,3), new Pixel(11,22,33),new Pixel(111,222, 70), new Pixel(10,60,240), new Pixel(19,31,180),new Pixel(22,204,30) },
                { new Pixel(7,8,9), new Pixel(77,88,99),new Pixel(56,57,80) , new Pixel(11,61,241), new Pixel(15,200,181),new Pixel(23,208,31) },
                { new Pixel(4,5,6), new Pixel(44,55,66),new Pixel(21,58,81),  new Pixel(12,30,242), new Pixel(18,201,182),new Pixel(24,209,32) }
            };
            Pixel[,] testData1 = new Pixel[,]
            {
                { new Pixel(1,2,3), new Pixel(11,22,33),new Pixel(111,222, 70) },
                { new Pixel(7,8,9), new Pixel(77,88,99),new Pixel(56,57,80) },
                { new Pixel(4,5,6), new Pixel(44,55,66),new Pixel(21,58,81) }, 
                { new Pixel(10,60,240), new Pixel(19,31,180),new Pixel(22,204,30) },
                { new Pixel(11,61,241), new Pixel(15,200,181),new Pixel(23,208,31) }, 
                { new Pixel(12,30,242), new Pixel(18,201,182),new Pixel(24,209,32) }
            };
            Pixel[,] testData2 = new Pixel[,]
            {
                { new Pixel(1,2,3), new Pixel(10,60,240), new Pixel(19,31,180),new Pixel(22,204,30) },
                { new Pixel(2,8,9), new Pixel(11,61,241), new Pixel(15,200,181),new Pixel(23,208,31) },
                { new Pixel(3,5,6), new Pixel(5,30,242), new Pixel(18,201,182),new Pixel(24,209,32) }
            };
            Pixel[,] testData3 = new Pixel[,]
            {
                { new Pixel(1,2,3), new Pixel(4,22,33),new Pixel(7,222, 333) },
                { new Pixel(2,60,240), new Pixel(19,31,180),new Pixel(22,204,30) },
                { new Pixel(3,5,6), new Pixel(5,30,242), new Pixel(18,201,182) },
                { new Pixel(1,2,3), new Pixel(10,60,240), new Pixel(19,31,180) }
            };
            MedianFiltration(testData0);
            MedianFiltration(testData);
            MedianFiltration(testData1);
            MedianFiltration(testData2);
            MedianFiltration(testData3);
        }

        public class Pixel
        {
            private int _red;
            private int _green;
            private int _blue;

            public int Red
            {
                get => _red;
                set => _red = value > 255 ? 255 : value;
            }
            public int Green
            {
                get => _green;
                set => _green = value > 255 ? 255 : value;
            }
            public int Blue
            {
                get => _blue;
                set => _blue = value > 255 ? 255 : value;
            }

            public Pixel(int red, int green, int blue)
            {
                this.Red = red;
                this.Green = green;
                this.Blue = blue;
            }
        }

        public static Pixel[,] MedianFiltration(Pixel[,] pixeles)
        {
            //Количество строк и столбцов в исходном массиве
            int dataX = pixeles.GetLength(1);
            int dataY = pixeles.GetLength(0);
            //Объявление количества строк в результирующем массиве
            //Логика подсчета количества строк и столбцов в результирующем массиве:
            //в res должно быть строк/столбцов втрое меньше, чем в изначальном, но в случае если строки/столбцы изначального массива
            //не делятся нацело на 3, то увеличиваем на 1 количество столбцов/строк в res
            int resY = dataY % 3 == 0 ? dataY / 3 : dataY / 3 + 1;
            int resX = dataX % 3 == 0 ? dataX / 3 : dataX / 3 + 1;
            //Результирующий массив
            Pixel[,] res = new Pixel[resY, resX];
            
            //Первые 2 цикла двигают скользящее окно(SliceWindow),
            for (int y = 0; y < dataY; y += 3)
            {
                for (int x = 0; x < dataX; x += 3)
                {
                    //Если количество столбцов/строк исходного массива не делится на 3, то двигаем последнее в столбце/строке скользящее окно
                    if (dataX - x < 3)
                    {
                        x -= 3 - dataX % x;
                    }
                    if (dataY - y < 3)
                    {
                        y -= 3 - dataY % y;
                    }
                    //Формируем скользящее окно
                    Pixel[] arr = new Pixel[9];
                    for (int ySliceWindow = 0; ySliceWindow < 3; ySliceWindow++)
                    {
                        for (int xSliceWindow = 0; xSliceWindow < 3; xSliceWindow++)
                        {
                            arr[ySliceWindow * 3 + xSliceWindow] = pixeles[y + ySliceWindow, x + xSliceWindow];
                        }
                    }
                    //Вычисляем медианный пиксель
                    Pixel pixel = MedianFiltrationAlgorythm(arr);
                    //Добавляем пиксель в результирующий массив; +2 берется для случая, если количество столбцов/строк исходного массива не делится на 3
                    res[(y + 2)/3, (x + 2)/3] = pixel;
                }
            }

            return res;
        }

        public static Pixel MedianFiltrationAlgorythm(Pixel[] pixeles)
        {

            //Массивы для вычисления медиан в скользящих окнах(SliceWindow)
            int[] redPartOfPixelesFromSliceWindow = new int[9];
            int[] greenPartOfPixelesFromSliceWindow = new int[9];
            int[] bluePartOfPixelesFromSliceWindow = new int[9];

            //Проходим по скользящему окну и наполняем наши массивы для вычисления медиан
            for (int ySliceWindow = 0; ySliceWindow < 3; ySliceWindow++)
            {
                for (int xSliceWindow = 0; xSliceWindow < 3; xSliceWindow++)
                {
                    redPartOfPixelesFromSliceWindow[xSliceWindow + ySliceWindow * 3] = pixeles[ySliceWindow * 3 + xSliceWindow].Red;
                    greenPartOfPixelesFromSliceWindow[xSliceWindow + ySliceWindow * 3] = pixeles[ySliceWindow * 3 + xSliceWindow].Green;
                    bluePartOfPixelesFromSliceWindow[xSliceWindow + ySliceWindow * 3] = pixeles[ySliceWindow * 3 + xSliceWindow].Blue;
                }
            }
            //Сортируем
            Array.Sort(redPartOfPixelesFromSliceWindow);
            Array.Sort(greenPartOfPixelesFromSliceWindow);
            Array.Sort(bluePartOfPixelesFromSliceWindow);
            //Формируем новый пиксель
            Pixel resPixel = new Pixel(redPartOfPixelesFromSliceWindow[4], greenPartOfPixelesFromSliceWindow[4], bluePartOfPixelesFromSliceWindow[4]);

            return resPixel;
        }
    }
}
