using System;

namespace Graphs
{
    public class Matrix
    {
        public int[,] matrix { get; }
        public Matrix(int[,] newMatrix)
        {
            matrix = newMatrix;
        }
        //creates empty
        public Matrix(int numberV)
        {
            matrix = new int[numberV, numberV];
        }

        //finds minor A[row][col]
        public void FindMinor(ref Matrix minor, int col, int row)
        {
            //col - column
            int n = (int)Math.Sqrt(minor.matrix.Length);
            int di = 0, dj;
            for (int ki = 0; ki < n; ki++)
            {
                //if we came to the row that needs to be 'deleted'
                //we pretend it doesn't exist and jump over it with di koeff
                if (ki == row) di = 1;
                dj = 0;

                for (int kj = 0; kj < n; kj++)
                {
                    //same for colum as for row
                    if (kj == col) dj = 1;
                    minor.matrix[ki, kj] = this.matrix[ki + di, kj + dj];
                }
            }
        }
        public double Determinant() // n - размерность матрицы A
        {
            //only for square matrix
            int n = (int)Math.Sqrt(matrix.Length);
            int k = 1; // minor koeff (+-1)
            double d = 0; // stores final determinant
            Matrix B = new Matrix(new int[n - 1, n - 1]); 

            if (n < 1) return 0;
            if (n == 1) return matrix[0, 0];
            if (n == 2)
            {
                //a11*a22 - a12*a21 for 2x2 matrix
                return (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]); 
            }

            for (int i = 0; i < n; i++)
            {
                this.FindMinor(ref B, i, 0); //Find minors for first row
                d += k * matrix[0, i] * B.Determinant(); //count with a formula
                k *= -1;
            }
            return d;
        }
    }
}
