using System;

class Program
{
    static void Main(string[] args)
    {
        int[] arr = { 12, 11, 13, 5, 6, 7 };
        HeapSort(arr);
        Console.WriteLine("Отсортированный массив:");
        PrintArray(arr);
    }

    static void HeapSort(int[] arr)
    {
        int n = arr.Length;

        // Построение кучи (перегруппируем массив)
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(arr, n, i);

        // Один за другим извлекаем элементы из кучи
        for (int i = n - 1; i > 0; i--)
        {
            // Перемещаем текущий корень в конец
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            // Вызываем процедуру heapify на уменьшенной куче
            Heapify(arr, i, 0);
        }
    }

    static void Heapify(int[] arr, int n, int i)
    {
        int largest = i; // Инициализируем наибольший элемент как корень
        int l = 2 * i + 1; // Левый потомок
        int r = 2 * i + 2; // Правый потомок

        // Если левый потомок больше корня
        if (l < n && arr[l] > arr[largest])
            largest = l;

        // Если правый потомок больше, чем самый большой элемент на данный момент
        if (r < n && arr[r] > arr[largest])
            largest = r;

        // Если самый большой элемент не корень
        if (largest != i)
        {
            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;

            // Рекурсивно обрабатываем поддерево
            Heapify(arr, n, largest);
        }
    }

    static void PrintArray(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n; ++i)
            Console.Write(arr[i] + " ");
        Console.WriteLine();
    }
}