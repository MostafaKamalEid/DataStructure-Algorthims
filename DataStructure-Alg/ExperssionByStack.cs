using System.Text;

public static class ExperssionByStack
{
 
    private static readonly string Brackets = "{[(}])";
    private static readonly Dictionary<Char, int> Weights = new Dictionary<Char, int>
    {
        {'/' , 1 },
        {'*' , 1 },
        {'-', 0 },
        {'+', 0 },
    };
    public static bool CheckBracketsIsCorrect(string Expression)
    {
        var Stack = new Stack<char>();
        foreach (var Char in Expression)
        {
            //Check char is bracket 
            if (Brackets.Contains(Char))
            {
                if (Stack.TryPeek(out var Last))
                {
                    if (Brackets.IndexOf(Last) + 3 < Brackets.Length && Brackets[Brackets.IndexOf(Last) + 3] == Char)
                    {
                        Stack.Pop();
                    }
                    else
                    {
                        Stack.Push(Char);
                    }
                    continue;

                }
                Stack.Push(Char);
            }

        }
        return Stack.Count > 0 ? false : true;
    }
    public static string InfixToPostfixExpression(string Expression)
    {

        if (!CheckBracketsIsCorrect(Expression))
        {
            throw new InvalidOperationException();
        }

        var PostfixString = new StringBuilder();
        var Stack = new Stack<Char>();
        foreach (var Char in Expression)
        {

            if (Char.IsLetter(Char) || Char.IsWhiteSpace(Char) || Char == ' ')
            {
                continue;

            }
            else if (Char.IsDigit(Char))
            {
                PostfixString.Append(Char);

            }
            // Brackets openBracket Push to Stack
            // Closed Bracket Pop All Bet Them in the Stack
            else if (Brackets.Contains(Char))
            {

                // Closed Brackets
                if (Brackets.IndexOf(Char) > 2)
                {
                    var OpenBracket = Brackets[Brackets.IndexOf(Char) - 3];
                    while (Stack.Count > 0 && Stack.TryPop(out Char Last) && Last != OpenBracket)
                    {
                        PostfixString.Append(' ');
                        PostfixString.Append(Last);
                        PostfixString.Append(' ');
                    }

                }
                // Open Brackets
                else
                {
                    Stack.Push(Char);
                }

            }
            else if (Stack.TryPeek(out var Last))
            {
                PostfixString.Append(' ');
                if (Weights.ContainsKey(Last) && BiggerOrEqualInWeight(Last, Char))
                {

                    while (Stack.Count > 0 && Stack.TryPop(out Last) && !Brackets.Contains(Last))
                    {
                        PostfixString.Append(' ');
                        PostfixString.Append(Last);
                    }
                }
                Stack.Push(Char);

            }
            else
            {
                PostfixString.Append(' ');
                Stack.Push(Char);
            }




        }
        foreach (var item in Stack)
        {
            PostfixString.Append(' ');
            PostfixString.Append(item);
            PostfixString.Append(' ');

        }


        return PostfixString.ToString();


    }
    private static bool BiggerOrEqualInWeight(Char First, Char Second)
    {
        return Weights.GetValueOrDefault(First) >= Weights.GetValueOrDefault(Second);
    }
}