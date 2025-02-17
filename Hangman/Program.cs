class Program
{
    static void Main()
    {
        List<char> guessedLetters = new List<char>(); // empty list of wrongly guessed characters
        string[] wordList = new string[] // list of possible words
        {
        "Apple", "Tiger", "River", "Mountain", "Puzzle",
        "Chair", "Cloud", "Bottle", "School", "Flower",
        "Window", "Pencil", "Garden", "Rocket", "Banana",
        "Orange", "House", "Bread", "Table", "Friend",
        "Dog", "Sun", "Tree", "Bridge", "Candle",
        "Laptop", "Forest", "Village", "Thunder", "Rainbow",
        "Notebook", "Backpack", "Sandwich", "Adventure", "Chocolate",
        "Morning", "Pebble", "Castle", "Guitar", "Universe"
        };
        string guess = "";
        char letterGuess = '\0';
        int lives = 7;
        Random random = new Random(); //random init
        string[] hangmanStages = new string[]
        {

@"
 
 
 
     __|__
    /     \
    ===============",
@"
 
 
 
       |
     __|__
    /     \
    ===============",
@"
 
 
       |
       |
     __|__
    /     \
    ===============",
@"
 
 
       |
       |
       |
     __|__
    /     \
    ===============",
@"
       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        -
       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        --
       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        ---
       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        ----
       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        -----
       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        -----
       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        ------
       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        -------
       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        -------
       |       |
       |
       |
       |
     __|__
    /     \
    ===============",
@"
        -------
       |       |
       |       0
       |
       |
     __|__
    /     \
    ===============",
@"
        -------
       |       |
       |       0
       |       |
       |
     __|__
    /     \
    ===============",
@"
        -------
       |       |
       |       0
       |      /|
       |
     __|__
    /     \
    ===============",
@"
        -------
       |       |
       |       0
       |      /|\
       |
     __|__
    /     \
    ===============",
@"
        -------
       |       |
       |       0
       |      /|\
       |      /
     __|__
    /     \
    ===============",
@"
        -------
       |       |
       |       0
       |      /|\
       |      / \
     __|__
    /     \
    ==============="
        };
        int hangManDrawingProgress = hangmanStages.Length - lives - 1; // -1 -> zero based array
        bool repeat = true;
        string word;
        string oldWord;
        string[] guessDisplay;
        char[] wordArray;

        while (repeat)
        {

            word = RandomWord(wordList, random); //choosing random word
            guessDisplay = new string[word.Length]; // string array with as many positions as the word is long      
            wordArray = word.ToCharArray(); // word converted to array with characters
            oldWord = word; // store word for loss message

            // create guess display array with blanks
            for (int i = 0; i < wordArray.Length; i++)
            {
                guessDisplay[i] = ("_ ");
            }

            // guess loop
            while (lives >= 0)
            {
                UpdateUI(word, guessDisplay, lives, guessedLetters, hangmanStages, hangManDrawingProgress);

                // win check !!after UI is updated!!
                if (!guessDisplay.Contains("_ "))
                {
                    break;
                }
                if (lives == 0)
                {
                    break;
                }

                // input validation (empty/many characters/already guessed)
                letterGuess = InputValidation(word, guessDisplay, lives, guessedLetters, hangmanStages, hangManDrawingProgress, guess, letterGuess);

                // adds wrong guesses to list
                if (!word.Contains(letterGuess))
                {
                    guessedLetters.Add(letterGuess);
                }


                // lives deduction on wrong guess
                if (!wordArray.Contains(letterGuess))
                {
                    lives--;
                    hangManDrawingProgress = hangmanStages.Length - lives - 1;
                }

                // fill in correct letters to guess display
                for (int i = 0; i < wordArray.Length; i++)
                {
                    if (wordArray[i].Equals(letterGuess))
                    {
                        guessDisplay[i] = (wordArray[i] + " ");
                    }
                }
            }

            Console.WriteLine();

            // win/lose message
            gameEndMessage(lives, oldWord);

            repeatGameDialogue(ref lives, ref hangManDrawingProgress, hangmanStages, ref guessedLetters, ref repeat);
        }
    }
    static string RandomWord(string[] wordList, Random random)
    {
        int randomWordIndex = random.Next(0, wordList.Length); //random index for list of all possible words
        return wordList[randomWordIndex].ToUpper();  //saving randomly selected word in "word" string
    }

    static void UpdateUI(string word, string[] guessDisplay, int lives, List<char> guessedLetters, string[] hangmanStages, int hangManDrawingProgress)
    {
        Console.Clear();

        // print updated guess display
        Console.Write("    ");
        foreach (string c in guessDisplay)
        {
            Console.Write(c);
        }

        // print remaining lives
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"    Lives remaining: {lives}");
        Console.WriteLine();

        // displays the list of guessed letters
        Console.Write("    Guessed Letters: ");
        foreach (char i in guessedLetters)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        // drawing hangman depending on wrong guesses
        Console.WriteLine(hangmanStages[hangManDrawingProgress]);
        Console.WriteLine();
    }

    static char InputValidation(string word, string[] guessDisplay, int lives, List<char> guessedLetters, string[] hangmanStages, int hangManDrawingProgress, string guess, char letterGuess)
    {
        string dialogueMessage = "    Take your guess!";

        while (true)
        {
            UpdateUI(word, guessDisplay,lives, guessedLetters, hangmanStages,  hangManDrawingProgress);
            Console.WriteLine(dialogueMessage);
            // Letter guess input
            Console.Write("    ");
            guess = Console.ReadLine().ToUpper();

            // empty input protection
            if (guess == "" || guess == null)
            {

                dialogueMessage = "    Enter a letter!";
                continue;
            }

            letterGuess = guess[0]; //assigns the first character of the input to the letter guess variable

            if (guess.Length != 1)  //checks for multi letter inputs
            {

                dialogueMessage = "    Guess a single letter!";
                continue;
            }
            else if (!Char.IsLetter(letterGuess))
            {

                dialogueMessage = "    You may only guess letters!";
                continue;
            }
            if (guessDisplay.Contains(letterGuess + " ")) //Checks blanks if the letter is already correclty guessed
            {
                dialogueMessage = "    You already guessed this letter correctly!";
                continue;
            }
            else if (guessedLetters.Contains(letterGuess))  // checks guessed letters for duplicate guess
            {

                dialogueMessage = "    You have already guessed that letter!";
                continue;
            }
            else if (!guessedLetters.Contains(letterGuess) && guess.Length == 1) // exits validation loop if the guessed letter has not been guessed before
            {
                break;
            }
            else //unexpected error
            {
                Console.WriteLine("    Something went wrong");
            }
        }
        return letterGuess;
    }

    static void gameEndMessage(int lives, string oldWord)
    {
        if (lives > 0)
        {
            Console.WriteLine("    You won!");
        }
        else if (lives == 0)
        {
            Console.WriteLine($"    You lost. The word was {oldWord}.");
        }
        else
        {
            Console.WriteLine("    Lives somehow negative");
        }
    }

    static void repeatGameDialogue(ref int lives, ref int hangManDrawingProgress, string[] hangmanStages, ref List<char> guessedLetters, ref bool repeat)
    {
        while (true)
        {
            Console.WriteLine("    Do you want to play another round? (Y/N)");
            Console.Write("    ");
            string anotherRound = Console.ReadLine().ToUpper();
            if (anotherRound == "Y")
            {
                //reset lives/drawing progress/guessed letter list
                lives = 7;
                hangManDrawingProgress = hangmanStages.Length - lives - 1;
                guessedLetters.Clear();
                break;
            }
            else if (anotherRound == "N")
            {
                //exit game loop
                repeat = false;
                break;
            }
            else
            {
                //wrong input (y/n)
                Console.WriteLine("    Enter \"Y\" play another round, \"N\" to stop playing.");
            }
        }
    }
}