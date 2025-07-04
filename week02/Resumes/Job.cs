using System;
public class Job
{
    public string _company, _jobTitle;
    public int _startYear, _endYear;
    public Job()
    {
    }

    public void DisplayJobInfo()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}
