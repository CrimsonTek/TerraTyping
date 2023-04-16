using System.Text;

namespace TerraTyping.TypeLoaders;

public abstract partial class TypeLoader
{
    protected static class ColumnToIndex
    {
        public const int A = 0;
        public const int B = 1;
        public const int C = 2;
        public const int D = 3;
        public const int E = 4;
        public const int F = 5;
        public const int G = 6;
        public const int H = 7;
        public const int I = 8;
        public const int J = 9;
        public const int K = 10;
        public const int L = 11;
        public const int M = 12;
        public const int N = 13;
        public const int O = 14;
        public const int P = 15;
        public const int Q = 16;
        public const int R = 17;
        public const int S = 18;
        public const int T = 19;
        public const int U = 20;
        public const int V = 21;
        public const int W = 22;
        public const int X = 23;
        public const int Y = 24;
        public const int Z = 25;

        public static string IndexToColumn(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (index < 0)
            {
                return index.ToString();
            }

            int length = letters.Length;
            int acceleratingCutoff = length;
            int cutoffVelocity = length;
            int divisor = 1;
            int startOffset = 0;
            StringBuilder stringBuilder = new StringBuilder(new string(letters[index % length], 1), 2);

            while (index >= acceleratingCutoff)
            {
                divisor *= length;
                startOffset += divisor;
                cutoffVelocity *= length;
                acceleratingCutoff += cutoffVelocity;
                stringBuilder.Insert(0, letters[((index - startOffset) / divisor) % length]);
            }

            return stringBuilder.ToString();
        }
    }
}
