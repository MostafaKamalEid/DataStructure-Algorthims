internal static class SortAlg
{
    public static void Merge<T>(ref T[] A, int start, int medium, int End, bool descending = false)
    {
        // 1 , 9 
        // A[start..medium] A[medium+1..End]
        // create temp array  i -->  start  && j --> medium

        int i = start, j = medium + 1, k = 0;
        T[] temp = new T[End - start + 1];

        //  Have we reached the end of any of the arrays?
        //  No:
        //       Compare current elements of both arrays
        //       Copy Bigger element into sorted array
        //       Move pointer of element containing smaller element
        while (i <= medium && j <= End)
        {
            var Ipointer = A[i] as IComparable;
            var Jpointer = A[j] as IComparable;
            var Compare = Ipointer.CompareTo(Jpointer);
            if (descending)
            {
  
                if (Compare > 0 || Compare == 0)
                {
                    temp[k] = A[i];
                    i++;
                }
                else
                {
                    temp[k] = A[j];
                    j++;

                }
            }
            else
            {
                if (Compare < 0 || Compare == 0)
                {
                    temp[k] = A[i];
                    i++;
                }
                else
                {
                    temp[k] = A[j];
                    j++;

                }
            }

            k++;
        }

        //Yes:
        //    Copy all remaining elements of non-empty array
        for (; i <= medium; i++)
        {
            temp[k] = A[i];
            k++;
        }
        for (; j <= End; j++)
        {
            temp[k] = A[j];
            k++;

        }
        // Copy from temp to Array
        for (k = 0; k < temp.Length; k++)
        {
            A[start++] = temp[k];
        }




    }

    public static void MergeSort<T>(ref T[] A, int start, int End)
    {
       

        var medium = (start + End) / 2;
        if (start >= End)
        {
            return;
        }
        MergeSort(ref A, start, medium);
        MergeSort(ref A, medium + 1, End);
        Merge(ref A, start, medium, End);

    }
}