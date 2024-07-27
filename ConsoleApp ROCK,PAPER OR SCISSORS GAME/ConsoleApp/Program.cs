using System.Runtime.CompilerServices;

Console.Clear();


DateTime date = DateTime.Now;
System.Console.WriteLine($"Date: {date:yyyy/MM/dd dddd}");
System.Console.WriteLine("Welcom back");
System.Console.WriteLine("------------------------");


Random random = new Random();
bool palyAgain = true;
String player, computer;
int computerScore = 0, playerScore = 0;
while (palyAgain)
{
    player = "";
    computer = "";

    while (player != "ROCK" && player != "PAPER" && player != "SCISSORS")
    {
        System.Console.Write("Enter ROCK, PAPER, or SCISSORS: ");
        player = Console.ReadLine();
        player = player.ToUpper();
    }

    switch (random.Next(1, 4))
    {
        case 1:
            computer = "ROCK";
            break;
        case 2:
            computer = "PAPER";
            break;
        case 3:
            computer = "SCISSORS";
            break;
    }

    System.Console.WriteLine($"player: {player} / computer: {computer}");

    switch (player)
    {
        case "ROCK":
            if (computer == "ROCK")
                System.Console.WriteLine("It's a draw!");
            else if (computer == "PAPER")
            {
                System.Console.WriteLine("You lose!");
                computerScore = computerScore + 1;
            }
            else
            {
                System.Console.WriteLine("You win!");
                playerScore++;
            }
            break;
        case "PAPER":
            if (computer == "PAPER")
                System.Console.WriteLine("It's a draw!");
            else if (computer == "SCISSORS")
            {
                System.Console.WriteLine("You lose!");
                computerScore = computerScore + 1;
            }
            else
            {
                System.Console.WriteLine("You win!");
                playerScore++;
            }
            break;
        case "SCISSORS":
            if (computer == "SCISSORS")
                System.Console.WriteLine("It's a draw!");
            else if (computer == "ROCK")
            {
                System.Console.WriteLine("You lose!");
                computerScore = computerScore + 1;
            }
            else
            {
                System.Console.WriteLine("You win!");
                playerScore++;
            }
            break;
    }
    System.Console.WriteLine($"palyer: {playerScore} / computer: {computerScore}");
}