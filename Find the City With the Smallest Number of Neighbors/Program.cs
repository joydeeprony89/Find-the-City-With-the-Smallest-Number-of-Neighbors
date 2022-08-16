using System;

namespace Find_the_City_With_the_Smallest_Number_of_Neighbors
{
  class Program
  {
    static void Main(string[] args)
    {
      int distanceThreshold = 2;
      int n = 5;
      var edges = new int[6][] { new int[] { 0, 1, 2 }, new int[] { 0, 4, 8 }, new int[] { 1, 2, 3 }, new int[] { 1, 4, 2 }, new int[] { 2, 3, 1 }, new int[] { 3, 4, 1 } };
      Solution s = new Solution();
      var answer = s.FindTheCity(n, edges, distanceThreshold);
      Console.WriteLine(answer);
    }
  }

  public class Solution
  {
    public int FindTheCity(int n, int[][] edges, int distanceThreshold)
    {
      var dp = new long[n][];
      for (int i = 0; i < n; i++)
      {
        dp[i] = new long[n];
        Array.Fill(dp[i], int.MaxValue);
        // self loops not possible so distance 0, e.g - 0,0 - 1,1
        // rest all the distance will be infinite
        dp[i][i] = 0;
      }
      foreach (var edge in edges)
      {
        int s = edge[0];
        int d = edge[1];
        int c = edge[2];

        // its an undirected graph
        dp[s][d] = c;
        dp[d][s] = c;
      }

      // now we have to process wach node one by one to find out the min distance bw them

      // Floyd Warshall Algorithm
      // K loop is used as making each node as intermidiate node while calculating distance bw i, j
      for (int k = 0; k < n; k++)
      {
        // i and j loops are used as distance between i to j via k as middle node
        for (int i = 0; i < n; i++)
        {
          for (int j = 0; j < n; j++)
          {
            dp[i][j] = Math.Min(dp[i][j], dp[i][k] + dp[k][j]);
          }
        }
      }

      // Algorithm complted

      int res = -1;
      int minimum = n;
      for (int i = 0; i < n; i++)
      {
        int count = 0;
        for (int j = 0; j < n; j++)
        {
          if (dp[i][j] <= distanceThreshold) count++;
        }

        if (count <= minimum)
        {
          res = i;
          minimum = Math.Min(count, minimum);
        }
      }

      return res;
    }
  }
}
