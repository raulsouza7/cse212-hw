using System;
using System.Collections.Generic;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  
    /// For example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: Let's create an array of doubles with the size of 'length'.
        // Step 2: We'll start at 'number' and fill the array with multiples of that number.
        // Step 3: The first value will be 'number', then 'number * 2', then 'number * 3', and so on.
        // Step 4: After we fill the array with 'length' multiples, we'll return it.
        // And, honestly, I think that's it! Just keep multiplying by 'number' as we go.

        double[] result = new double[length]; // create an array of the given size
        for (int i = 0; i < length; i++) // loop through each index
        {
            result[i] = number * (i + 1); // assign multiples of 'number' to the array
        }
        return result; // return the array full of multiples
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  
    /// For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because the list is dynamic, this function will modify the existing data list rather than returning a new list. It's like, it'll just change what already exists.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Okay, so for that one, here's what I've thought:
        // Step 1: We need to rotate the list to the right by 'amount'.
        // Step 2: To do that, we can take the last 'amount' elements and move them to the front.
        // Step 3: If the list has 9 elements and we rotate by 3, we want the last 3 elements to come first.
        // Step 4: First, we'll figure out the part we need to slice (which is the last part).
        // Step 5: After that, we will just add the part from the front of the list to the back.
        // Step 6: And, we've rotated the list. Done!

        // Handle cases where 'amount' is greater than the data size
        amount = amount % data.Count; // ensures no out-of-bounds errors

        // Get the last 'amount' elements and add them to the front.
        List<int> partToMove = data.GetRange(data.Count - amount, amount); // get the last 'amount' elements
        data.RemoveRange(data.Count - amount, amount); // remove those elements from the original list
        data.InsertRange(0, partToMove); // insert those elements at the front
    }
}
