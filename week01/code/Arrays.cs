public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // 1) Create a double array of size 'length' to hold the multiples.
        // 2) Loop from i = 0 to i = length - 1.
        // 3) The value at index i should be the (i+1)th multiple of 'number':
        //       result[i] = number * (i + 1)
        // 4) Return the filled array.

        var result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // 1) Rotating right by 'amount' means the last 'amount' items move to the front.
        // 2) Find the index where the last 'amount' items start:
        //       tailStart = data.Count - amount
        // 3) Split the list into two slices:
        //       tail = data.GetRange(tailStart, amount)
        //       head = data.GetRange(0, tailStart)
        // 4) Clear the original list (we must modify it in place).
        // 5) Add the tail first, then the head, to rebuild the list in rotated order.

        int tailStart = data.Count - amount;

        var tail = data.GetRange(tailStart, amount);
        var head = data.GetRange(0, tailStart);

        data.Clear();
        data.AddRange(tail);
        data.AddRange(head);
    }
}
