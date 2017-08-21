using System;

// Задание №7 практики 2017г. 
// Задание 7.8, стр. 2: 8. Выписать все булевы функции от 3 аргументов, которые не линейны. Выписать их вектора в лексикографическом порядке.
// Задания по учебной практике

namespace Practice_2017_7
{
    class Program
    {
        static bool IsVectorLinear(bool[] vector)
        {                          // Вектор, который требуется проверить
            bool[,] pasc = new bool[3, vector.Length];                           // Таблица из метода Паскаля (со второй строки, тк 1-ая - это вектор)

            for (int i = 0; i < vector.Length; i++)                              // Заполнение 2-го ряда таблицы (снизу показан код без цикла)
                pasc[0, i] = vector[i/2*2] ^ (i % 2 != 0 & vector[i]);
            
            //pasc[0, 0] = vector[0];
            //pasc[0, 1] = vector[0] ^ vector[1];
            //pasc[0, 2] = vector[2];
            //pasc[0, 3] = vector[2] ^ vector[3];
            //pasc[0, 4] = vector[4];
            //pasc[0, 5] = vector[4] ^ vector[5];
            //pasc[0, 6] = vector[6];
            //pasc[0, 7] = vector[6] ^ vector[7];

            for (int i = 0; i < vector.Length; i++)                              // Заполнение 3-го ряда таблицы (снизу показан код без цикла), двойным комментарием помечены ненужные ячейки (не нужны, тк важно лишь есть ли конъюнкция)
                pasc[1, i] = pasc[0, i/2%2 != 0 ? i-2 : i] ^ (i/2%2 != 0 & pasc[0, i]);
            
            ////pasc[1, 0] = pasc[0, 0];
            //pasc[1, 1] = pasc[0, 1];
            //pasc[1, 2] = pasc[0, 0] ^ pasc[0, 2];
            //pasc[1, 3] = pasc[0, 1] ^ pasc[0, 3];
            ////pasc[1, 4] = pasc[0, 4];
            //pasc[1, 5] = pasc[0, 5];
            //pasc[1, 6] = pasc[0, 4] ^ pasc[0, 6];
            //pasc[1, 7] = pasc[0, 5] ^ pasc[0, 7];

            for (int i = 0; i < vector.Length; i++)                              // Заполнение 4-го ряда таблицы (снизу показан код без цикла), двойным комментарием помечены ненужные ячейки (не нужны, тк важно лишь есть ли конъюнкция)
                pasc[2, i] = pasc[1, i/4%2 != 0 ? i - 4 : i] ^ (i/4%2 != 0 & pasc[1, i]);

            ////pasc[2, 0] = pasc[1, 0];
            ////pasc[2, 1] = pasc[1, 1];
            ////pasc[2, 2] = pasc[1, 2];
            //pasc[2, 3] = pasc[1, 3];
            ////pasc[2, 4]= pasc[1, 0] ^ pasc[1, 4];
            //pasc[2, 5]= pasc[1, 1] ^ pasc[1, 5];
            //pasc[2, 6] = pasc[1, 2] ^ pasc[1, 6];
            //pasc[2, 7] = pasc[1, 3] ^ pasc[1, 7];

            if (pasc[2, 3] | pasc[2, 5] | pasc[2, 6] | pasc[2, 7]) return false; // Эти ячейки отвечают за элементы с конъюнкцией в полиноме Жегалкина по таблицы по методу Паскаля
            return true;
        }            // Линеен ли вектор

        static void WriteVector(bool[] vector)
        {                      // Вектор для вывода
            foreach (var var in vector)
                Console.Write(var ? "1" : "0");
            
            Console.WriteLine();
        }               // Выводит вектор на экран

        static void WriteAllNonLinearVectorsFrom3Arguments()
        {
            bool[] vector = new bool[8];
            Console.WriteLine("Вектора нелинейных булевых функций от 3-х аргументов:");
            for (int i = 0; i < Math.Pow(2, 8); i++)
            {
                for (int j = 0, k = (int)Math.Pow(2, 7); j < vector.Length; j++, k/=2)
                    vector[j]= i / k % 2 != 0;

                if (((vector[0] ? 1 : 0) + (vector[1] ? 1 : 0) + (vector[2] ? 1 : 0) +
                    (vector[3] ? 1 : 0) + (vector[4] ? 1 : 0) + (vector[5] ? 1 : 0) +
                    (vector[6] ? 1 : 0) + (vector[7] ? 1 : 0)) % 2!=0) WriteVector(vector); // У линейных ф-ий среди значений нулей и единиц всегда четное число
                else if (!IsVectorLinear(vector)) WriteVector(vector);                      // Иначе проверяем на линейность и выписываем, если оказался нелинейным
            }
            Console.ReadKey();
        } // Выводит все нелинейные вектора от 3-х аргументов

        static void Main(string[] args)
        {
            WriteAllNonLinearVectorsFrom3Arguments();
        }
    }
}
