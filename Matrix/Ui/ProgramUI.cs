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
        var (xDimension, yDimension) = RequestDimensions();

        var firstMatrix = RequestMatrix(xDimension, yDimension);
        var secondMatrix = RequestMatrix(xDimension, yDimension);

        var addedMatrix = firstMatrix + secondMatrix;
        
        PrintMatrix(addedMatrix);
    }

    private void PrintMultiplyingMatrixByConstScreen()
    {
        Console.WriteLine("Enter dimensions of your matrix");
        var (xDimension, yDimension) = RequestDimensions();

        var matrix = RequestMatrix(xDimension, yDimension);
        Console.WriteLine("Enter multiplier for matrix");
        var multiplier = RequestNumber();

        matrix *= multiplier;
        
        PrintMatrix(matrix);
    }

    private void PrintMultiplyingMatrixByMatrix()
    {
        Console.WriteLine("Enter dimensions of your matrix");
        var (xDimension, yDimension) = RequestDimensions();

        var firstMatrix = RequestMatrix(xDimension, yDimension);
        var secondMatrix = RequestMatrix(xDimension, yDimension);

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
            var (xDimension, yDimension) = RequestDimensions();

            if (typeOfTranspose is 1 or 2 && (xDimension != yDimension))
            {
                Console.WriteLine("For main and side diagonal transpose X Dimension and Y Dimension must equal each other");
                continue;
            }
        
            var matrix = RequestMatrix(xDimension, yDimension);

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
    
    private Entities.Matrix RequestMatrix(int xDimension, int yDimension)
    {
        var matrix = new Entities.Matrix(new int[xDimension,yDimension]);


        for (var i = 0; i < xDimension; i++)
        {
            Console.WriteLine($"Enter values of {i} level");
            var values = RequestValuesOfMatrix(yDimension);
            for (var j = 0; j < yDimension; j++)
            {
                matrix.Values[i, j] = values[j];
            }
        }
        return matrix;
    }

    private List<int> RequestValuesOfMatrix(int dimension)
    {
        
        var formattedValues = new List<int>();
        while (true)
        {
            
            var userValues = RequestUserInput().Split();

            if (userValues.Length != dimension)
            {
                Console.WriteLine("Wrong quantity of values entered");
                continue;
            }
            
            foreach (var userValue in userValues)
            {
                if (int.TryParse(userValue, out var value))
                {
                    formattedValues.Add(value);
                }
                else
                {
                    break;
                }
            }

            if (userValues.Length == formattedValues.Count)
            {
                return formattedValues;
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

            if (int.TryParse(notParsedDimension[0], out var xDimension) &&
                int.TryParse(notParsedDimension[1], out var yDimension))
            {
                return (xDimension, yDimension);
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
        var values = matrix.Values;
        var rows = values.GetLength(0);
        var columns = values.GetLength(1);

        Console.ForegroundColor = ConsoleColor.Red;
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                Console.Write(values[i, j].ToString().PadLeft(3) + " ");
            }
            Console.WriteLine();
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    
}