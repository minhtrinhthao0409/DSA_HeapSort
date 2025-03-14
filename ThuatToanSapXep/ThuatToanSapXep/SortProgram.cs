using System;
using System.Text;
namespace ThuatToanSapXep
{

    class SortProgram
    {
        // ==== MERGE SORT IMPLEMENTATION ====
        public static int[] MergeSort(int[] arr, int left, int right)
        {
            int[] arr_ = (int[])arr.Clone();
            if (left < right)
            {
                int mid = (left + right) / 2;

                // Đệ quy chia nhỏ mảng
                MergeSort(arr_, left, mid);
                MergeSort(arr_, mid + 1, right);

                // Gộp mảng đã sắp xếp
                Merge(arr_, left, mid, right);
            }
            return arr_;
        }
        private static void Merge(int[] arr, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] L = new int[n1];
            int[] R = new int[n2];

            for (int i = 0; i < n1; i++) L[i] = arr[left + i];
            for (int j = 0; j < n2; j++) R[j] = arr[mid + 1 + j];

            int x = 0, y = 0, k = left;

            while (x < n1 && y < n2)
            {
                if (L[x] <= R[y]) arr[k++] = L[x++];
                else arr[k++] = R[y++];
            }

            while (x < n1) arr[k++] = L[x++];
            while (y < n2) arr[k++] = R[y++];
        }

        // ==== QUICK SORT IMPLEMENTATION ====
        public static int[] QuickSort(int[] arr, int low, int high)
        {
            int[] arr_ = (int[])arr.Clone();
            if (low < high)
            {
                int pivotIndex = Partition(arr_, low, high);

                // Đệ quy sắp xếp 2 phần
                QuickSort(arr_, low, pivotIndex - 1);
                QuickSort(arr_, pivotIndex + 1, high);
            }
            return arr_;
        }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    Swap(arr, arr[i], arr[j]);
                }
            }
            Swap(arr, arr[i + 1], arr[high]);
            return i + 1;
        }

        // ==== HEAP SORT IMPLEMENTATION ====
        public static int[] HeapSort(int[] arr)
        {
            int[] arr_ = (int[]) arr.Clone();
            int n = arr_.Length;

            // Bước 1: Xây dựng Max Heap từ mảng
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(arr_, n, i);
            }

            // Bước 2: Trích xuất từng phần tử từ heap và đặt vào cuối mảng
            for (int i = n - 1; i > 0; i--)
            {
                // Đưa phần tử lớn nhất (root) về cuối mảng
                Swap(arr_, 0, i);

                // Gọi Heapify trên heap giảm kích thước
                Heapify(arr_, i, 0);
            }
            return arr_;
        }

        // Hàm Heapify để duy trì tính chất của Max Heap
        private static void Heapify(int[] arr, int n, int i)
        {
            int largest = i;      // Gán nút hiện tại là lớn nhất
            int left = 2 * i + 1; // Chỉ số con trái
            int right = 2 * i + 2;// Chỉ số con phải

            // So sánh với con trái
            if (left < n && arr[left] > arr[largest])
                largest = left;

            // So sánh với con phải
            if (right < n && arr[right] > arr[largest])
                largest = right;

            // Nếu largest không phải là nút hiện tại, hoán đổi và tiếp tục Heapify
            if (largest != i)
            {
                Swap(arr, i, largest);
                Heapify(arr, n, largest);
            }
        }
        private static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        // ==== PRINT ARRAY ====
        public static void PrintArray(int[] arr)
        {
            Console.WriteLine(string.Join(", ", arr));
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // 2. Đo thời gian thực hiện với 1000 phần tử, chạy 10,000 lần
            const int SIZE = 1000;
            const int TRIALS = 10000;
            Timing timing = new Timing();
            double heapTime = 0, quickTime = 0, mergeTime = 0;

            Console.WriteLine($"\n=== Đo thời gian thực hiện ({SIZE} phần tử, {TRIALS} lần) ===");

            // Heap Sort
            for (int i = 0; i < TRIALS; i++)
            {
                int[] arr = GenerateRandomArray(SIZE);
                timing.StartTime();
                HeapSort(arr);
                timing.StopTime();
                heapTime += timing.Result().TotalMilliseconds;
            }
            Console.WriteLine($"Heap Sort: {heapTime / TRIALS:F4} ms (trung bình mỗi lần)");

            // Quick Sort
            for (int i = 0; i < TRIALS; i++)
            {
                int[] arr = GenerateRandomArray(SIZE);
                timing.StartTime();
                QuickSort(arr, 0, arr.Length - 1);
                timing.StopTime();
                quickTime += timing.Result().TotalMilliseconds;
            }
            Console.WriteLine($"Quick Sort: {quickTime / TRIALS:F4} ms (trung bình mỗi lần)");

            // Merge Sort
            for (int i = 0; i < TRIALS; i++)
            {
                int[] arr = GenerateRandomArray(SIZE);
                timing.StartTime();
                MergeSort(arr, 0, arr.Length - 1);
                timing.StopTime();
                mergeTime += timing.Result().TotalMilliseconds;
            }
            Console.WriteLine($"Merge Sort: {mergeTime / TRIALS:F4} ms (trung bình mỗi lần)");

            // 3. Test ví dụ nhỏ
            int[] arr = { 12, 11, 13, 5, 6, 7 };
            Console.WriteLine("\n=== Test ví dụ nhỏ ===");
            Console.WriteLine("Mảng ban đầu:");
            PrintArray(arr);

            Console.WriteLine("Heap Sort:");
            PrintArray(HeapSort(arr));
            Console.WriteLine("Quick Sort:");
            PrintArray(QuickSort(arr, 0, arr.Length - 1));
            Console.WriteLine("Merge Sort:");
            PrintArray(MergeSort(arr, 0, arr.Length - 1));
        }
    }
}
