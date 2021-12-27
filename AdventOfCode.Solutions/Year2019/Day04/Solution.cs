namespace AdventOfCode.Solutions.Year2019.Day04;

class Solution : SolutionBase
{

    public Solution() : base(4, 2019, "Secure Container")
    {

    }

    protected override string SolvePartOne()
    {
        int[] range = Input.ToIntArray("-");
        int passwords = 0;
        for(int n = range[0]; n <= range[1]; n++)
        {
            if(Test(n.ToString())) passwords++;
        }
        return passwords.ToString();
    }

    protected override string SolvePartTwo()
    {
        int[] range = Input.ToIntArray("-");
        int passwords = 0;
        for(int n = range[0]; n <= range[1]; n++)
        {
            if(Test(n.ToString(), true)) passwords++;
        }
        return passwords.ToString();
    }

    bool Test(string password, bool part2 = false)
    {
        bool duplicate = false;
        if(password.Length == 6)
        {
            for(int i = 1; i < password.Length; i++)
            {
                if(password[i] < password[i - 1])
                {
                    return false;
                }
                else if(password[i] == password[i - 1])
                {
                    if(part2)
                    {
                        if(password.Split(password[i]).Length - 1 == 2) duplicate = true;
                    }
                    else
                    {
                        duplicate = true;
                    }
                }
            }
        }
        return duplicate;
    }
}
