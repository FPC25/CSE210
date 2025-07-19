using System;
using System.Text.Json;

class ScriptureManager
{
    private List<string> _options, _bom_list, _nt_list, _ot_list, _pog_list;
    private List<int> _dec_list;
    private string _path;

    public ScriptureManager(string pathToFolder)
    {
        _path = pathToFolder;
        _options = new List<string> { "Enter a scripture.", "I am feeling lucky!" };

    }

    public string Menu()
    {
        Console.WriteLine("Welcome to the ScriptureMemorizer, a tool to help you memorize the scriptures! Please, select the number of the option you prefer: ");

        string option = Utils.Decision(_options);

        string scripture = "a";
        if (option.Contains("Enter"))
        {
            //ask to the user enter the scripture
        }
        else
        {
            //randomly select a scripture for any book 
        }

        return scripture;
    }
}
