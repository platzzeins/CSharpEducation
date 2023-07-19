namespace Matrix.Entities;

public class IdentityMatrix : ICloneable
{
    public int[,] Values { get; init; }
    public int GetLength(int dimension) => Values.GetLength(dimension);
    
    public IdentityMatrix(int[,] matrix) => Values = matrix;
    public IdentityMatrix(int xDimension, int yDimension) => Values = new int[xDimension, yDimension];

    public int this[int rowIndex, int columnIndex]
    {
        get => Values[rowIndex, columnIndex];
        set => Values[rowIndex, columnIndex] = value;
    }

    public static IdentityMatrix operator +(IdentityMatrix firstIdentityMatrix, IdentityMatrix secondIdentityMatrix)
    {
        var firstMatrixRows = firstIdentityMatrix.GetLength(0);
        var firstMatrixColumns = firstIdentityMatrix.GetLength(1);
        
        var addedMatrix = new IdentityMatrix(firstMatrixRows, firstMatrixColumns);
        for (var i = 0; i < firstMatrixRows; i++)
        {
            for (var j = 0; j < firstMatrixColumns; j++)
            {
                addedMatrix[i, j] = firstIdentityMatrix[i, j] + secondIdentityMatrix[i, j];
            }
        }
        return addedMatrix;
    }

    public static IdentityMatrix operator *(IdentityMatrix identityMatrix, int multiplier)
    {
        var rows = identityMatrix.GetLength(0);
        var columns = identityMatrix.GetLength(1);
        
        var multipliedMatrix = identityMatrix.Copy();
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                multipliedMatrix[i, j] *= multiplier;
            }
        }
        return multipliedMatrix;
    }

    public static IdentityMatrix operator *(IdentityMatrix firstIdentityMatrix, IdentityMatrix secondIdentityMatrix)
    {
        var firstMatrixRows = firstIdentityMatrix.GetLength(0);
        var firstMatrixColumns = firstIdentityMatrix.GetLength(1);
        var secondMatrixColumns = secondIdentityMatrix.GetLength(1);
        
        var multipliedMatrix = new IdentityMatrix(firstMatrixRows, secondMatrixColumns);
        
        for (var i = 0; i < firstMatrixRows; i++)
        {
            for (var j = 0; j < secondMatrixColumns; j++)
            {
                multipliedMatrix[i, j] = 0;

                for (var k = 0; k < firstMatrixColumns; k++)
                {
                    multipliedMatrix[i, j] += firstIdentityMatrix[i, k] * secondIdentityMatrix[k, j];
                }
            }
        }

        return multipliedMatrix;
    }

    public void TransposeMatrixByMainDiagonal()
    {
        var rows = GetLength(0);
        var columns = GetLength(1);
        for (var i = 0; i < rows; i++)
        {
            for (var j = i + 1; j < columns; j++)
            {
                (Values[i, j], Values[j, i])
                    = (Values[j, i], Values[i, j]);
            }
        }
    }

    public void TransposeMatrixBySideDiagonal()
    {
        var size = GetLength(0);

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
        var rows = GetLength(0);
        var columns = GetLength(1);

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
        var rows = GetLength(0);
        var columns = GetLength(1);

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
        return new IdentityMatrix(clonedMatrix);
    }

    public IdentityMatrix Copy()
    {
        return (IdentityMatrix)Clone();
    }
}