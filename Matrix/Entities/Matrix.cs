using Matrix.Exceptions;

namespace Matrix.Entities;

public class Matrix : ICloneable
{
    private int[,] Values { get; init; }
    public int Rows => Values.GetLength(0);
    public int Columns => Values.GetLength(1);
    
    
    public Matrix(int[,] matrix) => Values = matrix;
    public Matrix(int rows, int columns) => Values = new int[rows, columns];

    public int this[int rowIndex, int columnIndex]
    {
        get => Values[rowIndex, columnIndex];
        set => Values[rowIndex, columnIndex] = value;
    }

    public static Matrix operator +(Matrix firstMatrix, Matrix secondMatrix)
    {
        try
        {
            if (firstMatrix.Rows != secondMatrix.Rows && firstMatrix.Columns != secondMatrix.Columns)
            {
                throw new MatrixNotInRangeException("Rows and Columns of first and second given matrix aren't matching");
            }
        }
        catch (MatrixNotInRangeException exception)
        {
            Console.WriteLine(exception.Message);
            return new Matrix(firstMatrix.Rows, firstMatrix.Columns);
        }
        
        var addedMatrix = new Matrix(firstMatrix.Rows, firstMatrix.Columns);
        for (var i = 0; i < firstMatrix.Rows; i++)
        {
            for (var j = 0; j < firstMatrix.Columns; j++)
            {
                addedMatrix[i, j] = firstMatrix[i, j] + secondMatrix[i, j];
            }
        }
        return addedMatrix;
    }

    public static Matrix operator *(Matrix matrix, int multiplier)
    {
        var multipliedMatrix = matrix.Copy();
        for (var i = 0; i < matrix.Rows; i++)
        {
            for (var j = 0; j < matrix.Columns; j++)
            {
                multipliedMatrix[i, j] *= multiplier;
            }
        }
        return multipliedMatrix;
    }

    public static Matrix operator *(Matrix firstMatrix, Matrix secondMatrix)
    {
        try
        {
            if (firstMatrix.Rows != secondMatrix.Columns )
            {
                throw new MatrixNotInRangeException("Rows of first matrix and Columns of second matrix doesn't match");
            }

            if (firstMatrix.Columns != secondMatrix.Rows)
            {
                throw new MatrixNotInRangeException("Columns of first matrix and Rows of second matrix doesn't match");
            }
        }
        catch (MatrixNotInRangeException exception)
        {
            Console.WriteLine(exception.Message);
            return new Matrix(firstMatrix.Rows, firstMatrix.Columns);
        }
        
        var multipliedMatrix = new Matrix(firstMatrix.Rows, secondMatrix.Columns);
        
        for (var i = 0; i < firstMatrix.Rows; i++)
        {
            for (var j = 0; j < secondMatrix.Columns; j++)
            {
                multipliedMatrix[i, j] = 0;

                for (var k = 0; k < firstMatrix.Columns; k++)
                {
                    multipliedMatrix[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                }
            }
        }

        return multipliedMatrix;
    }

    public void TransposeMatrixByMainDiagonal()
    {
        for (var i = 0; i < Rows; i++)
        {
            for (var j = i + 1; j < Columns; j++)
            {
                (Values[i, j], Values[j, i])
                    = (Values[j, i], Values[i, j]);
            }
        }
    }

    public void TransposeMatrixBySideDiagonal()
    {
        for (var i = 0; i < Rows / 2; i++)
        {
            for (var j = 0; j < Rows / 2; j++)
            {
                (Values[i, j], Values[Rows - 1 - j, Rows - 1 - i]) 
                    = (Values[Rows - 1 - j, Rows - 1 - i], Values[i, j]);
            }
        }
    }

    public void TransposeMatrixByVerticalLine()
    {
        for (var i = 0; i < Rows / 2; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                (Values[i, j], Values[Rows - 1 - i, j])
                    = (Values[Rows - 1 - i, j], Values[i, j]);
            }
        }
    }

    public void TransposeMatrixByHorizontalLine()
    {
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns / 2; j++)
            {
                (Values[i, j], Values[i, Columns - 1 - j])
                    = (Values[i, Columns - 1 - j], Values[i, j]);
            }
        }
    }
    public object Clone()
    {
        var clonedMatrix = new int[Values.GetLength(0), Values.GetLength(1)];
        Array.Copy(Values, clonedMatrix, Values.Length);
        return new Matrix(clonedMatrix);
    }

    public Matrix Copy() => (Matrix)Clone();
}