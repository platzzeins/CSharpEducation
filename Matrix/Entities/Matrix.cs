namespace Matrix.Entities;

public class Matrix : ICloneable
{
    public int[,] Values { get; init; }

    public Matrix(int[,] matrix)
    {
        Values = matrix;
    }

    public static Matrix operator +(Matrix firstMatrix, Matrix secondMatrix)
    {
        var firstMatrixValues = firstMatrix.Values;
        var secondMatrixValues = secondMatrix.Values;
        
        var addedMatrix = new Matrix(new int [firstMatrixValues.GetLength(0), firstMatrixValues.GetLength(1)]);
        for (var i = 0; i < firstMatrixValues.GetLength(0); i++)
        {
            for (var j = 0; j < firstMatrixValues.GetLength(1); j++)
            {
                addedMatrix.Values[i, j] = firstMatrixValues[i, j] + secondMatrixValues[i, j];
            }
        } 
        return addedMatrix;
    }

    public static Matrix operator *(Matrix matrix, int multiplier)
    {
        var multipliedMatrix = (Matrix)matrix.Clone();
        for (var i = 0; i < matrix.Values.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.Values.GetLength(1); j++)
            {
                multipliedMatrix.Values[i, j] *= multiplier;
            }
        }
        return multipliedMatrix;
    }

    public static Matrix operator *(Matrix firstMatrix, Matrix secondMatrix)
    {
        var firstMatrixValues = firstMatrix.Values;
        var secondMatrixValues = secondMatrix.Values;
        var multipliedMatrix = new Matrix(new int[firstMatrix.RowsCount(), secondMatrix.ColumnsCount()]);
        
        for (var i = 0; i < firstMatrix.RowsCount(); i++)
        {
            for (var j = 0; j < secondMatrix.ColumnsCount(); j++)
            {
                multipliedMatrix.Values[i, j] = 0;

                for (var k = 0; k < firstMatrix.ColumnsCount(); k++)
                {
                    multipliedMatrix.Values[i, j] += firstMatrixValues[i, k] * secondMatrixValues[k, j];
                }
            }
        }

        return multipliedMatrix;
    }

    public void TransposeMatrixByMainDiagonal()
    {
        var size = Values.GetLength(0);

        for (var i = 0; i < size; i++)
        {
            for (var j = i + 1; j < size; j++)
            {
                (Values[i, j], Values[j, i])
                    = (Values[j, i], Values[i, j]);
            }
        }
    }

    public void TransposeMatrixBySideDiagonal()
    {
        var size = Values.GetLength(0);

        for (var i = 0; i < size / 2; i++)
        {
            for (var j = 0; j < size / 2; j++)
            {
                (Values[i, j], Values[size - 1 - j, size - 1 - i]) 
                    = (Values[size - 1 - j, size - 1 - i], Values[i, j]);
            }
        }
    }

    public void TransposeMatrixByVerticalLine()
    {
        var rows = Values.GetLength(0);
        var columns = Values.GetLength(1);

        for (var i = 0; i < rows / 2; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                (Values[i, j], Values[rows - 1 - i, j])
                    = (Values[rows - 1 - i, j], Values[i, j]);
            }
        }
    }

    public void TransposeMatrixByHorizontalLine()
    {
        var rows = Values.GetLength(0);
        var columns = Values.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns / 2; j++)
            {
                (Values[i, j], Values[i, columns - 1 - j])
                    = (Values[i, columns - 1 - j], Values[i, j]);
            }
        }
    }

    public object Clone()
    {
        var clonedMatrix = new int[Values.GetLength(0), Values.GetLength(1)];
        Array.Copy(Values, clonedMatrix, Values.Length);
        return new Matrix(clonedMatrix);
    }

    private int RowsCount()
    {
        return Values.GetUpperBound(0) + 1;
    }
    
    private int ColumnsCount()
    {
        return Values.GetUpperBound(1) + 1;
    } 
}