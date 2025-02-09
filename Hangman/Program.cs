using System;
using System.Collections;

List<char> guessedLetters = new List<char>(); // empty list of wrongly guessed characters
string[] wordList = new string[] // list of possible words
{
    "Apple", "Tiger", "River", "Mountain", "Puzzle",
    "Chair", "Cloud", "Bottle", "School", "Flower",
    "Window", "Pencil", "Garden", "Rocket", "Banana",
    "Orange", "House", "Bread", "Table", "Friend"
};
string guess = ""; // empty input string
char letterGuess = '\0'; // empty character guess
int lives = 6; // lives
Random random = new Random(); //random init
int hangManDrawingProgress = 0;
bool repeat = true;
bool cheating = false;
string oldWord = "";

while (repeat)
{
    int randomWordIndex = random.Next(0,wordList.Length); //random index for list of all possible words
    string word = wordList[randomWordIndex].ToUpper();  //saving randomly selected word in "word" string
    string[] guessDisplay = new string[word.Length]; // string array with as many positions as the word is long      
    char[] wordArray = word.ToCharArray(); // word converted to array with characters
    oldWord = word; // store word for loss message

    // create guess display array with blanks
    for (int i = 0; i < wordArray.Length; i++)
    {
        guessDisplay[i] = ("_ ");
    }

    // guess loop
    while (lives >= 0)
    {
        Console.Clear();

        // cheating  
        if (cheating)
        {
            Console.WriteLine(word);
        }
        Console.WriteLine();

        // print updated guess display
        Console.Write("   ");
        foreach (string c in guessDisplay)
        {
            Console.Write(c);
        }

        // print remaining lives
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"   Lives remaining: {lives}");
        Console.WriteLine();

        // displays the list of guessed letters
        Console.Write("   Guessed Letters: ");
        foreach (char i in guessedLetters)
        {
            Console.Write(i+" ");
        }
        Console.WriteLine();
        // drawing hangman depending on wrong guesses
        switch (hangManDrawingProgress)
        {
            case 0:
                Console.WriteLine();
                Console.WriteLine(@"        _______");
                Console.WriteLine(@"       |       |");
                Console.WriteLine(@"       |");
                Console.WriteLine(@"       |");
                Console.WriteLine(@"       |");
                Console.WriteLine(@"     __|__");
                Console.WriteLine(@"    /     \");
                Console.WriteLine(@"    ===============");
                break;
            case 1:
                Console.WriteLine();
                Console.WriteLine(@"        _______");
                Console.WriteLine(@"       |       |");
                Console.WriteLine(@"       |       0");
                Console.WriteLine(@"       |");
                Console.WriteLine(@"       |");
                Console.WriteLine(@"     __|__");
                Console.WriteLine(@"    /     \");
                Console.WriteLine(@"    ===============");
                break;
            case 2:
                Console.WriteLine();
                Console.WriteLine(@"        _______");
                Console.WriteLine(@"       |       |");
                Console.WriteLine(@"       |       0");
                Console.WriteLine(@"       |       |");
                Console.WriteLine(@"       |");
                Console.WriteLine(@"     __|__");
                Console.WriteLine(@"    /     \");
                Console.WriteLine(@"    ===============");
                break;
            case 3:
                Console.WriteLine();
                Console.WriteLine(@"        _______");
                Console.WriteLine(@"       |       |");
                Console.WriteLine(@"       |       0");
                Console.WriteLine(@"       |      /|");
                Console.WriteLine(@"       |");
                Console.WriteLine(@"     __|__");
                Console.WriteLine(@"    /     \");
                Console.WriteLine(@"    ===============");
                break;
            case 4:
                Console.WriteLine();
                Console.WriteLine(@"        _______");
                Console.WriteLine(@"       |       |");
                Console.WriteLine(@"       |       0");
                Console.WriteLine(@"       |      /|\");
                Console.WriteLine(@"       |");
                Console.WriteLine(@"     __|__");
                Console.WriteLine(@"    /     \");
                Console.WriteLine(@"    ===============");
                break;
            case 5:
                Console.WriteLine();
                Console.WriteLine(@"        _______");
                Console.WriteLine(@"       |       |");
                Console.WriteLine(@"       |       0");
                Console.WriteLine(@"       |      /|\");
                Console.WriteLine(@"       |      /");
                Console.WriteLine(@"     __|__");
                Console.WriteLine(@"    /     \");
                Console.WriteLine(@"    ===============");
                break;
            case 6:
                Console.WriteLine();
                Console.WriteLine(@"        _______");
                Console.WriteLine(@"       |       |");
                Console.WriteLine(@"       |       0");
                Console.WriteLine(@"       |      /|\");
                Console.WriteLine(@"       |      / \");
                Console.WriteLine(@"     __|__");
                Console.WriteLine(@"    /     \");
                Console.WriteLine(@"    ===============");
                break;
            default:
                Console.WriteLine("You should never see this");
                break;
        }
        Console.WriteLine();

        // win check !!after UI is updated!!
        if (!guessDisplay.Contains("_ "))
        {
            break;
        }
        if (lives == 0)
        {
            break;
        }

        // Letter guess input
        Console.WriteLine("    Guess a letter!");
        Console.Write("    ");
        guess = Console.ReadLine().ToUpper();

        //cheats off/on
        if (guess == "CHEATS ON")
        {
            cheating = true;
        }
        else if (guess == "CHEATS OFF")
        {
            cheating = false;
        }

        // single letter input validation
        bool inputValidation = true;

        while (inputValidation)
        {
            letterGuess = guess[0]; //assigns the first character of the input to the letter guess variable

            if (guessDisplay.Contains(letterGuess +" ")) //Checks blanks if the letter is already correclty guessed
            {
                Console.WriteLine("    You already guessed this letter correctly!");
                Console.Write("    ");
                guess = Console.ReadLine().ToUpper();
            }
            else if (!guessedLetters.Contains(letterGuess) && guess.Length == 1) // exits validation loop if the guessed letter has not been guessed before
            {    
                inputValidation = false; 
            }
            else if (guessedLetters.Contains(letterGuess))  // checks guessed letters for duplicate guess
            {
                Console.WriteLine("    You have already guessed that letter!");
                Console.Write("    ");
                guess = Console.ReadLine().ToUpper();
            }
            else if(guess.Length != 1)  //checks for multi letter inputs
            {
                Console.WriteLine("    Guess a single letter!");
                Console.Write("    ");
                guess = Console.ReadLine().ToUpper();
            }
            else //unexpected error
            {
                Console.WriteLine("    Something went wrong");
            }    
        }

        // adds wrong guesses to list
        if (!word.Contains(letterGuess))
        {
            guessedLetters.Add(letterGuess);
        }
        

        // lives deduction on wrong guess
        if (!wordArray.Contains(letterGuess))
        {
            lives--;
            hangManDrawingProgress++;
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

    // repeat game
    while (true)
    {
        Console.WriteLine("    Do you want to play another round? (Y/N)");
        Console.Write("    ");
        string anotherRound = Console.ReadLine().ToUpper();
        if (anotherRound == "Y")
        {
            //reset lives/drawing progress/guessed letter list
            lives = 6;
            hangManDrawingProgress = 0;
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


