using System.Collections;
using System.Collections.Generic;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it. If n <= 0 return 0. No loops.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0) return 0;
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length 'size'
    /// from a list of 'letters' into the results list.
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // If we've built a word of the correct length, add it
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Choose each letter once, recurse with remaining letters
        for (int i = 0; i < letters.Length; i++)
        {
            char chosen = letters[i];
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + chosen);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Count ways to climb 's' stairs using recursion + memoization.
    /// Base cases are given by the template/tests.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases (given by template)
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Init memo
        remember ??= new Dictionary<int, decimal>();

        // Check memo
        if (remember.TryGetValue(s, out decimal cached))
            return cached;

        // Recurse (pass remember down!)
        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        // Store + return
        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Expand wildcard binary patterns into all possible strings.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int idx = pattern.IndexOf('*');

        // No wildcard left -> complete string
        if (idx == -1)
        {
            results.Add(pattern);
            return;
        }

        string left = pattern[..idx];
        string right = pattern[(idx + 1)..];

        WildcardBinary(left + "0" + right, results);
        WildcardBinary(left + "1" + right, results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Initialize currPath on first call
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // IMPORTANT: Maze.IsValidMove signature is (currPath, x, y)
        if (!maze.IsValidMove(currPath, x, y))
            return;

        // Choose
        currPath.Add((x, y));

        // End?
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1); // backtrack
            return;
        }

        // Explore neighbors
        SolveMaze(results, maze, x + 1, y, currPath); // right
        SolveMaze(results, maze, x - 1, y, currPath); // left
        SolveMaze(results, maze, x, y + 1, currPath); // down
        SolveMaze(results, maze, x, y - 1, currPath); // up

        // Backtrack
        currPath.RemoveAt(currPath.Count - 1);
    }
}
