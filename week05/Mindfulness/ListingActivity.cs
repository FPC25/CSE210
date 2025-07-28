using System;

class ListeningActivity
{
    private int varName;
    public ListeningActivity()
    {
    }

    public void MethodName()
    {

    }
    public static void Ellipsis(int timeInSeconds)
    {
        var ellipsis = new List<string> { ".", ".", "." };

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(timeInSeconds);

        int i = 0;

        while (DateTime.Now < endTime)
        {
            string s = ellipsis[i];

            Console.Write(s);
            Thread.Sleep(1000);

            i++;

            if (i >= ellipsis.Count)
            {
                i = 0;
                Console.Write(Utils.BuiltCleanTerminalString(ellipsis.Count));
            }
        }
    }
}