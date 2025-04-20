global using System;
global using System.IO;
global using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ConsoleApp1;

class Program
{
	/* Region private definitions */
	
	/**
	 * @brief Define the valid format for the files.
	 */
	private static readonly List<string> _validFormats = new List<string> { ".cpp", ".h" };
	
	/**
	 * @brief Define the valid keys for the files.
	 */
	private static readonly List<string> _validKeys = new List<string> { "-n", "--namespace" };
	
	/**
	 * @brief Define the valid flags for the files.
	 */
	private static readonly List<string> _validFlags = new List<string> {};
	
	/* endregion */
	
	/* #region public variables */
	static void Main(string[] args)
	{	
		
		//Handle the arguments
		Program._handleInputArguments(args);
		
		 //Each argument is a file
		 for(int i = 0; i < Program._relativeArgument.Count; i++)
		 {	
			
			 //Check if the file has a path
			 string fileDirectory = Path.GetDirectoryName(_relativeArgument[i]);
			 if (fileDirectory == null)
			 {
				 //No, then the file is in the current directory
				 fileDirectory = "";
			 }
			 
			 //Check if the first argument is a slash or a backslash
			 if (fileDirectory.StartsWith('/') || fileDirectory.StartsWith('\\'))
			 {
				 // Salva il colore corrente
				 ConsoleColor originalColor = Console.ForegroundColor;
    
				 // Imposta il colore giallo per il warning
				 Console.ForegroundColor = ConsoleColor.Yellow;
    
				 // Stampa il messaggio di warning con un prefisso
				 Console.WriteLine($"WARNING: The path " + fileDirectory + " is an absolute path. It will be replaced by a relative path.");
    
				 // Ripristina il colore originale
				 Console.ForegroundColor = originalColor;
				 System.Diagnostics.Trace.TraceWarning("The path " + fileDirectory + " is an absolute path. It will be replaced by a relative path.");
				 
				 //Remove the initial backslash to avoid absolute paths
				 fileDirectory = fileDirectory.TrimStart('/', '\\');;
	
			 }

			 //Combine the program path and the file directory
			 string filePath = Path.Combine(Program._programPath, fileDirectory);
			 
			 //normalize the path
			 filePath = Path.GetFullPath(filePath);
			 
		 	//Extract the file name
		 	string fileName = Path.GetFileName(_relativeArgument[i]);
		    
		    //Initialize the nameSpace
		    string[] nameSpace = new string[0];
		    ;
		    //Get the nameSpace
		    Program._handleNameSpace(out nameSpace);
		    
		 	//Check if the path exists
		    if (!Directory.Exists(filePath))
		    {
			    Directory.CreateDirectory(filePath);
				Console.WriteLine($"Created new file path at " + filePath);
		    }
	
		 	 //Create the file
		 	 string content = FileData.CreateFile(fileName, nameSpace);
			 
		 	 try
		 	 {
		 	 	//Create txt file 
		 	 	File.WriteAllText(filePath + "\\" + fileName, content);
			    
			    Console.WriteLine($"Created new file at " + filePath + "\\" + fileName);
		 	 }
		 	 catch (Exception ex)
		 	 {
		 	 	Console.WriteLine($"Error the file creation: {ex.Message}");
		 	 	
		 	}
			
		}
		
	}

	private static void _handleNameSpace(out string[] nameSpace)
	{
		//Initialize the nameSpaceString
		string nameSpaceString = "";
		
		//Check if there is a key -n
		if (_optionWithValue.ContainsKey("-n"))
		{
			//Yes, then get the value
			nameSpaceString = _optionWithValue["-n"];
		}//No, then check if there is a key --namespace
		else if (_optionWithValue.ContainsKey("--namespace"))
		{
			//Yes, then get the value
			nameSpaceString = _optionWithValue["--namespace"];
		}
		
		//Replace the backslashes with slashes
		nameSpaceString = nameSpaceString.Replace("\\", "/");
		
		//Split the nameSpaceString in parts
		nameSpace = nameSpaceString.Split('/', StringSplitOptions.RemoveEmptyEntries);
		
	}
	
	/**
	 * @brief Helper function to handle the arguments.
	 *
	 * @param[in] args The arguments to handle.
	 */
	private static void _handleInputArguments(string[] args)
	{
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
					Program._optionWithValue[args[i]] = args[i + 1];
					
					//Skip the next argument
					i++;
				}
				else
				{
					// No the is a flag
					Program._flag.Add(args[i]);
				}
			}
			else
			{
				//No, then is a relative argument:"
				Program._relativeArgument.Add(args[i]);
			}
		}
		
		//Check the flags
		foreach(string flag in _flag)
		{
			if(!Program._validFlags.Contains(flag))
				throw new ArgumentException("The flag: " + flag +", is not valid. The valid flags are: " + string.Join(", ", Program._validFlags));
		}
		
		//Check the arguments
		foreach (string arg in _relativeArgument)
		{
			if(!Program._validFormats.Contains(Path.GetExtension(arg)))
				throw new ArgumentException("The argument: " + arg +", is not valid. The valid arguments are: " + string.Join(", ", Program._validFormats));
		}
		
		//Check the keys
		foreach (string key in _optionWithValue.Keys)
		{
			if (!Program._validKeys.Contains(key))
				throw new ArgumentException("The key: " + key +", is not valid. The valid keys are: " + string.Join(", ", Program._validKeys));
		}
	}

	/**
	 * @brief A dictionary for couple value/key inputs.
	 */
	private static Dictionary<string, string> _optionWithValue { get; set; } = new Dictionary<string, string>();
		
	/**
	 * @brief A list for flags inputs.
	 */
	private static List<string> _flag { get; set; } = new List<string>();

	/**
	 * @brief A list for arguments inputs.
	 */
	private static List<string> _relativeArgument { get; set; } = new List<string>();
	
	/**
	 * @ Define the current file path.
	 */
	private static string _programPath = Directory.GetCurrentDirectory();

}