global using System;
global using System.IO;
global using System.Collections.Generic;
namespace ConsoleApp1;

class Program
{
	static void Main(string[] args)
	{	
		//Create a dictionary for couple value/key
		Dictionary<string, string> optionWithValue = new Dictionary<string, string>();
		
		//Create a list for flags
		List<string> flag = new List<string>();
		
		//Create a list for arguments
		List<string> argument = new List<string>();
		
		//Initialize the current directory path
		string filePath = Directory.GetCurrentDirectory();
		
		
		//cycle through the arguments
		for(int i = 0; i < args.Length; i++)
		{
			//Check if arg starts with -- or -
			if (args[i].StartsWith("--") || args[i].StartsWith("-"))
			{	
				//Check if the next argument is a value
				if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
				{
					//Yes, then add the couple value/key to the dictionary
					optionWithValue[args[i]] = args[i + 1];
				}
				else
				{
					// No the is a flag
					flag.Add(args[i]);
				}
			}
			else
			{
				//No, then add the argument to the list
				argument.Add(args[i]);
			}
		}
		
		//Each argument is a file
		foreach (string file in argument)
		{
			//Create the file
			string content = FileData.CreateFile(file);
			
			try
			{
				//Create txt file 
				File.WriteAllText(filePath + "\\" + file, content);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error during the file creation: {ex.Message}");
			}
			
		}
		
	}
	
}