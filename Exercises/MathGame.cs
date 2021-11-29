namespace Anterpreter.Exercises
{
    
    class CustomException : Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }
    }

    class InvalidOptionException : CustomException
    {
        public InvalidOptionException() { }
    }

    class InputNotNumberException : CustomException
    {
        public InputNotNumberException() : base("Input wasn't number.") { }
    }

    class NotEvenException : CustomException
    {
        public NotEvenException() : base("Input wasn't even.") { }
    }

    class NotOddException : CustomException
    {
        public NotOddException() : base("Input wasn't odd.") { }
    }

    class NotPrimeException : CustomException
    {
        public NotPrimeException() : base("Input wasn't prime.") { }
    }

    class NotZeroException : CustomException
    {
        public NotZeroException() : base("Input wasn't zero.") { }
    }

    class NotNegativeException : CustomException
    {
        public NotNegativeException() : base("Input wasn't negative.") { }
    }

    class FiveRunsException : CustomException
    {
        public FiveRunsException() { }
    }

    internal class MathGame : IExercise
    {

        private uint GameRuns = 0;

        public uint Number()
        {
            return 17;
        }

        public void Run()
        {
            uint option;
            string input;
            bool isValidOption;

            DisplayOptions();

            do
            {
                
                input = GetInput("(mathgame)> ");

                if (input.StartsWith("q"))
                    break;

                try 
                {
                    isValidOption = ValidateOption(input);
                } catch (InvalidOptionException)
                {
                    Console.WriteLine("Invalid option provided. Try again.");
                    continue;
                }

                option = uint.Parse(input);

                // This is an extremely stupid way of doing this.
                // But that's the assignment requirement.
                try
                {
                    RunOption(option);
                } catch (FiveRunsException)
                {
                    Console.WriteLine("You have played the game 5 times!");
                    RunOption(option);
                }
            } while (true);
        }

        private string GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        private void DisplayOptions()
        {
            Console.WriteLine("1. Enter even number");
            Console.WriteLine("2. Enter odd number");
            Console.WriteLine("3. Enter prime number");
            Console.WriteLine("4. Enter negative number");
            Console.WriteLine("5. Enter zero");
        }

        private void RunOption(uint option)
        {
            if (++GameRuns == 5)
                throw new FiveRunsException();

            try 
            {
                switch (option)
                {
                    case 1:
                        EnterEvenNumber();
                        break;
                    case 2:
                        EnterOddNumber();
                        break;
                    case 3:
                        EnterPrimeNumber();
                        break;
                    case 4:
                        EnterNegativeNumber();
                        break;
                    case 5:
                        EnterZero();
                        break;
                }
            } catch (CustomException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private bool EnterEvenNumber()
        {
            string input = GetInput("Enter an even number: ");

            bool isInt = int.TryParse(input, out int number);

            if (!isInt)
                throw new InputNotNumberException();

            if (!number.IsEven())
                throw new NotEvenException();

            return true;
        }

        private bool EnterOddNumber() 
        {
            string input = GetInput("Enter an odd number: ");

            bool isInt = int.TryParse(input, out int number);

            if (!isInt)
                throw new InputNotNumberException();

            if (!number.IsOdd())
                throw new NotOddException();

            return true;
        }

        private bool EnterPrimeNumber() 
        {
            string input = GetInput("Enter a prime number: ");

            bool isInt = int.TryParse(input, out int number);

            if (!isInt)
                throw new InputNotNumberException();

            if (!number.IsPrime())
                throw new NotPrimeException();

            return true;
        }

        private bool EnterNegativeNumber() 
        {
            string input = GetInput("Enter a negative number: ");

            bool isInt = int.TryParse(input, out int number);

            if (!isInt)
                throw new InputNotNumberException();

            if (number >= 0)
                throw new NotNegativeException();

            return true;
        }

        private bool EnterZero() 
        {
            string input = GetInput("Enter the number zero: ");

            bool isInt = int.TryParse(input, out int number);

            if (!isInt)
                throw new InputNotNumberException();

            if (number != 0)
                throw new NotZeroException();

            return true;
        }

        private bool ValidateOption(string input)
        {
            bool isUint = uint.TryParse(input, out uint option);

            if (!isUint)
                throw new InvalidOptionException();
            
            if (option < 1 || option > 5)
                throw new InvalidOptionException();
            
            return true;
        }

    }

}