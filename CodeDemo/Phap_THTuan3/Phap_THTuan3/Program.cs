using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phap_THTuan3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choose = 1;
            Console.OutputEncoding = Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1. Tính tổng mảng từ 1..n, theo phương pháp chia để trị dùng đệ qui");
            Console.WriteLine("2. Tính tổng mảng từ 1..n, theo phương pháp chia để trị KHÔNG dùng đệ qui");
            Console.WriteLine("3. Thực hiện sắp xếp quicksort 6 phần tử dùng đệ quy");
            Console.WriteLine("4. Tìm kiếm phần tử bất kỳ bằng tìm kiếm nhị phân dùng đệ quy");
            Console.WriteLine("5. Tìm kiếm phần tử bất kỳ bằng tìm kiếm nhị phân KHÔNG dùng đệ quy");
            do
            {
                Console.WriteLine("Mời thầy nhập bài toán cần giải quyết trong các bài trên và bấm 0 để kết thúc");

                choose = Convert.ToInt32(Console.ReadLine());
                luachon(choose);
            } while (choose != 0);

            //9. Thuật toán QuickSort

            Console.ReadKey();
        }
        public static void luachon(int choose)
        {
            int[] arr = { 21, 12, 33, 44, 22, 66 };
            Console.WriteLine("Tất cả phần tử ban đầu của mảng là: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($" {arr[i]}");
            }
            Console.WriteLine();
            switch (choose)
            {
                case 1:
                    int sum = SumList(arr);
                    /*1. Tính tổng mảng từ 1..n, theo phương pháp chia để trị dùng đệ qui
           */
                    Console.WriteLine($"Tổng mảng từ 1..n, theo phương pháp chia để trị dùng đệ qui là:\r\n {sum}");
                    break;
                case 2:
                    Console.WriteLine("Tổng mảng từ 1..n, theo phương pháp chia để trị KHÔNG dùng đệ qui là: " + SumArrayDivideAndConquer(arr));
                    break;
                case 3:
                    Console.WriteLine("Sử dụng  giải thuật QuickSort để sắp xếp");
                    Console.WriteLine("Mảng ban đầu:");
                    PrintArray(arr);
                    QuickSort(arr, 0, arr.Length - 1);
                    Console.WriteLine("Mảng sau khi sắp xếp:");
                    PrintArray(arr);
                    break;
                case 4:
                    int n = 6;// so luong phan tu cua mang
                    Console.WriteLine("Nhập giá trị X muốn tìm:");
                    int x = Convert.ToInt32(Console.ReadLine());
                    int result = BinarySearch(arr, 0, n - 1, x);

                    if (result == -1)
                        Console.WriteLine("Element not present");
                    else
                        Console.WriteLine("Element found at index "
                                          + result);
                    Console.ReadKey();
                    break;
                case 5:

                    Console.WriteLine("Nhập giá trị X muốn tìm:");
                    int x5 = Convert.ToInt32(Console.ReadLine());
                    int result5 = BinarySearchNoRecursive(arr, x5);

                    if (result5 == -1)
                        Console.WriteLine("Element not present");
                    else
                        Console.WriteLine("Element found at index "
                                          + result5);
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }
        #region
        /*
         Giải thuật tính tổng mảng theo pp chia để trị và dùng đệ quy 
         */
        public static int SumList(int[] arr)    //Dung de quy thay vong for
        {
            return SumList(arr, 0, arr.Length - 1);
        }

        public static int SumList(int[] arr, int left, int right)//Dung de quy de tinh left va right sau do cong leftsum va rightsum 
        {
            if (left == right)
                return arr[left];

            int mid = (left + right) / 2;
            int leftSum = SumList(arr, left, mid);
            int rightSum = SumList(arr, mid + 1, right);

            return leftSum + rightSum;
        }

        #endregion

        #region
        /*
          Giải thuật tính tổng mảng theo pp chia để trị và KHÔNG dùng đệ quy 
          Phương pháp này sử dụng ngăn xếp để mô phỏng quá trình chia để trị mà không sử dụng đệ quy.
         */
        static int SumArrayDivideAndConquer(int[] arr)
        {
            if (arr.Length == 0)
            {
                return 0;
            }

            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((0, arr.Length - 1));
            int totalSum = 0;

            while (stack.Count > 0)
            {
                var (left, right) = stack.Pop();

                if (left == right)
                {
                    totalSum += arr[left];
                }
                else
                {
                    int mid = (left + right) / 2;
                    stack.Push((left, mid));
                    stack.Push((mid + 1, right));
                }
            }

            return totalSum;
            #endregion
        }
        // Hàm in mảng
        public static void PrintArray(int[] arr)
        {
            foreach (int item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        #region
        /*
         * Thuật toán quicksort Đệ Quy
         */
        // Hàm Quick Sort
        public static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                // Phân hoạch mảng và lấy vị trí pivot
                int pivotIndex = Partition(arr, left, right);

                // Sắp xếp đệ quy mảng con bên trái pivot
                QuickSort(arr, left, pivotIndex - 1);

                // Sắp xếp đệ quy mảng con bên phải pivot
                QuickSort(arr, pivotIndex + 1, right);
            }
        }

        // Hàm phân hoạch mảng
        public static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right]; // Chọn phần tử cuối làm pivot
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, right);
            return i + 1;
        }

        // Hàm hoán đổi hai phần tử
        public static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        #endregion

        #region
        /*
         * BINARY SEARCH Dùng dệ quy
         */
        static int BinarySearch(int[] arr, int l, int r, int x)//Binary Search Chi Nen Ap Dung cho Mang Tang Hoac Giam, Hon loan se ko hieu qua!
        {
            if (r >= l)
            {
                int mid = l + (r - l) / 2;//Khoi tao gia tri phan doi Mang bang cach lay 0 + n-1(so luong phan tu tru di 1) /2 (Mang co 5 phan tu nen gia tri mid la 2)

                // If the element is present at the 
                // middle itself 
                if (arr[mid] == x)//Day la vi tri giua mang-> neu nam o day la qua may man
                    return mid;

                // If element is smaller than mid, then 
                // it can only be present in left subarray 
                if (arr[mid] > x)// Nam o phia truoc mang( gia tri giua>gia tri can tim)
                    return BinarySearch(arr, l, mid - 1, x);

                // Else the element can only be present 
                // in right subarray 
                //Nam o phia sau mang(gia tri giua < gia tri can tim)
                return BinarySearch(arr, mid + 1, r, x);
            }

            // We reach here when element is not present 
            // in array 
            return -1;
        }
        #endregion


        #region
        /*
        *BinarySearch KHÔNG đệ quy 
        *
        */
        static int BinarySearchNoRecursive(int[] arr, int x)
        {
            int left = 0, right = arr.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                // Check if x is present at mid
                if (arr[mid] == x)
                    return mid;

                // If x greater, ignore left half
                if (arr[mid] < x)
                    left = mid + 1;

                // If x is smaller, ignore right half
                else
                    right = mid - 1;
            }

            // if we reach here, then element was not present
            return -1;
        }
        #endregion
    }
}

