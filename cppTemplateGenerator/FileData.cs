using System.Runtime.CompilerServices;

namespace ConsoleApp1;

public class FileData
{
	/* Region private definitions */
	private static readonly List<string> _validFormat = new List<string> { ".cpp", ".h" };
	/* endregion */
	
	/* Region public methods */
	public static string CreateFile(string file)
	{
		//Get file name
		string nameFile = Path.GetFileNameWithoutExtension(file);
		
		//Check if the file name is valid 
		if (string.IsNullOrEmpty(nameFile))
		{
			throw new ArgumentException("The file name is not valid");
		}
		
		//Get the file format
		string format = Path.GetExtension(file);
		//Check if the file format is valid
		if (!_validFormat.Contains(format))
		{
			throw new ArgumentException("The file format: " + format +", is not valid. The valid format are: " + string.Join(", ", _validFormat));
		}
		
		string header = CreateHeader(nameFile, format);
		string body = CreateBody(nameFile, format);

		return header + body;
	}

	public static string GetNameFile(string file)
	{
		//Get file name
		string nameFile = Path.GetFileNameWithoutExtension(file);
		
		//Check if the file name is valid 
		if (string.IsNullOrEmpty(nameFile))
		{
			throw new ArgumentException("The file name is not valid");
		}
		
		//Return the file name
		return nameFile;
	}
	
	/* endregion */
	
	/* Region private methods */
	private static string CreateHeader(string nameFile, string format)
	{
		//Inizialize the author info
		string fileDescription = "/*******************************************" + Environment.NewLine +
		                         " * @filename: " + nameFile.ToLower() + format + Environment.NewLine +
		                         " * @author: " + Info.Name + Environment.NewLine +
		                         " * @email: " + Info.Email + Environment.NewLine +
		                         " * @date: " + Info.Date + Environment.NewLine +
		                         " * @description: " + Environment.NewLine +
		                         " *******************************************/" + Environment.NewLine;
		
		return fileDescription;
	}

	private static string CreateBody(string nameFile, string format)
	{
		//Initialize the body file
		string bodyFile;
		
		switch (format)
		{
			case ".h":
				bodyFile = CreateHeaderBody(nameFile);
				break;
			case ".cpp":
				bodyFile = CreateSourceBody(nameFile);
				break;
			default:
				throw new ArgumentException("The file format is not valid");
		}

		return bodyFile;
	}

	private static string CreateHeaderBody(string nameFile)
	{
		//Create the start guards
		string startGuard = "#ifndef " + nameFile.ToUpper() + "_H" + Environment.NewLine +
		                     "#define " + nameFile.ToUpper() + "_H" + Environment.NewLine + Environment.NewLine;
		
		//Create the end guards
		string endGuard = "#endif //" + nameFile.ToUpper() + "_H" + Environment.NewLine;
		
		//create class Name
		string className = char.ToUpper(nameFile[0]) + nameFile.Substring(1);
		
		//Create class Body
		string headerBody = "class " + className + Environment.NewLine +
		                   "{" + Environment.NewLine +
		                   "public:" + Environment.NewLine +
		                   "\t" + "/* #region public definitions */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "\t" + "using Ptr = " + className + "*;" + Environment.NewLine +
		                   Environment.NewLine +
		                   "\t" + "using CPtr = " + className + " const*;" + Environment.NewLine +
		                   Environment.NewLine +
		                   "\t" + "/* #endregion */" + Environment.NewLine +
		                   "protected:" + Environment.NewLine +
		                   "\t" + "/* #region protected definitions */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "\t" + "/* endregion */" + Environment.NewLine +
		                   "private:" + Environment.NewLine +
		                   "\t" + "/* #region private definitions */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "\t" + "/* #endregion */" + Environment.NewLine +
		                   "public:" + Environment.NewLine +
		                   "\t" + "/* region public methods */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "\t" + className + "() = default;" + Environment.NewLine + 
		                   Environment.NewLine +
		                   "\t" + "/* #endregion */" + Environment.NewLine +
		                   "protected:" + Environment.NewLine +
		                   "\t" + "/* #region protected methods */" + Environment.NewLine +
		                   Environment.NewLine + 
		                   "\t" + "/* #endregion */" + Environment.NewLine +
		                   "private:" + Environment.NewLine +
		                   "\t" + "/* #region private methods */" + Environment.NewLine +
		                   Environment.NewLine + 
		                   "\t" + "/* #endregion */" + Environment.NewLine +
		                   "public:" + Environment.NewLine +
		                   "\t" + "/* #region public variables */" + Environment.NewLine +
		                   Environment.NewLine + 
		                   "\t" + "/* #endregion */" + Environment.NewLine +
		                   "protected:" + Environment.NewLine +
		                   "\t" + "/* #region protected variables */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "\t" + "/* #endregion */" + Environment.NewLine +
		                   "private:" + Environment.NewLine +
		                   "\t" + "/* #region private variables */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "\t" + "/* #endregion */" + Environment.NewLine +
		                   "};" + Environment.NewLine;
		
		return (startGuard + headerBody + endGuard);
	}

	private static string CreateSourceBody(string nameFile)
	{
		string include = "#include \"" + nameFile.ToLower() + ".h\"" + Environment.NewLine + Environment.NewLine;
		string sourceBody = "/* #region public methods */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "/* #endregion */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "/* #region protected methods */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "/* #endregion */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "/* #region private methods */" + Environment.NewLine +
		                   Environment.NewLine +
		                   "/* #endregion */" + Environment.NewLine;
		
		return (include + sourceBody);
	}

	/* endregion */
	
	/* Region public variables */
	
	/* endregion */
	
	/* Region private variables */
	
	/**
	 * @bried Define the file name 
	 */
	
	/**
	 * @brief Define the file format
	 */

	
	/* endregion */

}