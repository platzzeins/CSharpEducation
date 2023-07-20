namespace Matrix.Ui;

public class ProgramUi
{
    public bool IsUserQuited { get; private set; }

    public void PrintSelectionScreen()
    {
        Console.WriteLine("1. Add matrices");
        Console.WriteLine("2. Multiply matrix by a constant");
        Console.WriteLine("3. Multiply matrices");
        Console.WriteLine("4. Transpose matrix");
        Console.WriteLine("0. Exit");

        var userInput = RequestNumber();

        switch (userInput)
        {
            case 1:
                PrintAddingMatrixScreen();
                break;
            case 2:
                PrintMultiplyingMatrixByConstScreen();
                break;
            case 3:
                PrintMultiplyingMatrixByMatrix();
                break;
            case 4:
                PrintTransposeSelectionScreen();
                break;
            case 0:
                Console.WriteLine("Goodbye!");
                IsUserQuited = true;
                break;
            default:
                Console.WriteLine("Enter one digit from 1 to 4");
                break;
        }
    }

    private void PrintAddingMatrixScreen()
    {
        Console.WriteLine("Enter dimensions of your matrix");
        var (rows, columns) = RequestDimensions();

        var firstMatrix = RequestMatrix(rows, columns);
        var secondMatrix = RequestMatrix(rows, columns);

        var addedMatrix = firstMatrix + secondMatrix;
        
        PrintMatrix(addedMatrix);
    }

    private void PrintMultiplyingMatrixByConstScreen()
    {
        Console.WriteLine("Enter dimensions of your matrix");
        var (rows, columns) = RequestDimensions();

        var matrix = RequestMatrix(rows, columns);
        Console.WriteLine("Enter multiplier for matrix");
        var multiplier = RequestNumber();

        matrix *= multiplier;
        
        PrintMatrix(matrix);
    }

    private void PrintMultiplyingMatrixByMatrix()
    {
        Console.WriteLine("Enter dimensions of your matrix");
        var (rows, columns) = RequestDimensions();

        var firstMatrix = RequestMatrix(rows, columns);
        var secondMatrix = RequestMatrix(rows, columns);

        var matrix = firstMatrix * secondMatrix;
        
        PrintMatrix(matrix);
    }

    private void PrintTransposeSelectionScreen()
    {
        Console.WriteLine("Input, what type of transpose you want to use");
        Console.WriteLine("1. Main diagonal");
        Console.WriteLine("2. Side diagonal");
        Console.WriteLine("3. Vertical line");
        Console.WriteLine("4. Horizontal line");

        var userInput = RequestNumber();

        switch (userInput)
        {
            case >= 1 and <= 4:
                PrintTransposeMatrixScreen(userInput);
                break;
            default:
                Console.WriteLine("Invalid type inputted");
                break;
        }
    }

    private void PrintTransposeMatrixScreen(int typeOfTranspose)
    {
        while (true)
        {
            Console.WriteLine("Enter dimensions of your matrix");
            var (rows, columns) = RequestDimensions();

            if (typeOfTranspose is 1 or 2 && (rows != columns))
            {
                Console.WriteLine("For main and side diagonal transpose X Dimension and Y Dimension must equal each other");
                continue;
            }
        
            var matrix = RequestMatrix(rows, columns);

            switch (typeOfTranspose)
            {
                case 1:
                    matrix.TransposeMatrixByMainDiagonal();
                    break;
                case 2:
                    matrix.TransposeMatrixBySideDiagonal();
                    break;
                case 3:
                    matrix.TransposeMatrixByVerticalLine();
                    break;
                case 4:
                    matrix.TransposeMatrixByHorizontalLine();
                    break;
                default:
                    Console.WriteLine("Something went wrong");
                    continue;
            }
            PrintMatrix(matrix);
            break;
        }
        
    }
    
    private Entities.Matrix RequestMatrix(int rows, int columns)
    {
        var matrix = new Entities.Matrix(rows, columns);


        for (var i = 0; i < rows; i++)
        {
            Console.WriteLine($"Enter values of {i+1} level");
            var values = RequestValuesOfMatrix(columns);

            var counter = 0;
            foreach (var value in values)
            {
                Console.WriteLine(value);
                matrix[i, counter] = value;
            }
        }
        
        return matrix;
    }

    private IEnumerable<int> RequestValuesOfMatrix(int columns)
    {
        for (var column = 0; column < columns - 1; column++)
        {
            var userValues = RequestUserInput().Split();

            if (userValues.Length != columns)
            {
                Console.WriteLine("Wrong quantity of values entered");
                continue;
            }

            foreach (var userValue in userValues)
            {
                if (int.TryParse(userValue, out var value))
                {
                    yield return value;
                }
                else
                {
                    break;
                }
            }
        }
    }
    
    private (int, int) RequestDimensions()
    {
        while (true)
        {
            var notParsedDimension = RequestUserInput().Split();
            if (notParsedDimension.Length != 2)
            {
                Console.WriteLine("Invalid quantity of values. Must be like this: \"2 2\"");
                continue;
            }

            if (int.TryParse(notParsedDimension[0], out var rows) &&
                int.TryParse(notParsedDimension[1], out var columns))
            {
                return (rows, columns);
            }

            Console.WriteLine("Invalid value entered");
        }
    }
    
    private int RequestNumber()
    {
        while (true)
        {
            var notParsedNumber = RequestUserInput();
            if (int.TryParse(notParsedNumber, out var parsedNumber))
            {
                return parsedNumber;
            }
            Console.WriteLine("Invalid value entered");
        }
    }
    
    private string RequestUserInput()
    {
        Console.Write(">");
        var userInput = Console.ReadLine()?.Trim().ToLower() ?? "None";
        return userInput;
    }

    private void PrintMatrix(Entities.Matrix matrix)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        for (var i = 0; i < matrix.Rows; i++)
        {
            for (var j = 0; j < matrix.Columns; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(3) + " ");
            }
            Console.WriteLine();
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    
}