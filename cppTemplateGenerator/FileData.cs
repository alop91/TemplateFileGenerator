using System.Runtime.CompilerServices;

namespace ConsoleApp1;

public class FileData
{
	/* Region public methods */
	public static string CreateFile(string fileName, string[] nameSpace)
	{
		//Get file name
		string fileNameWithNoExtension = Path.GetFileNameWithoutExtension(fileName);
		
		//Check if the file name is valid 
		if (string.IsNullOrEmpty(fileNameWithNoExtension))
		{
			throw new ArgumentException("The file name is null");
		}
		
		//Get the file format
		string format = Path.GetExtension(fileName);

		string header = CreateHeader(fileNameWithNoExtension, format);
		string body = CreateBody(fileNameWithNoExtension, format, nameSpace);

		return header + body;
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

	private static string CreateBody(string nameFile, string format, string[] nameSpaceParts)
	{
		//Initialize the body file
		string bodyFile;
		
		switch (format)
		{
			case ".h":
				bodyFile = CreateHeaderBody(nameFile, nameSpaceParts);
				break;
			case ".cpp":
				bodyFile = CreateSourceBody(nameFile, nameSpaceParts);
				break;
			default:
				throw new ArgumentException("The file format is not valid");
		}

		return bodyFile;
	}

	private static string CreateHeaderBody(string nameFile, string[] nameSpaceParts)
	{
		//Create the start guards
		string startGuard = "#ifndef " + nameFile.ToUpper() + "_H" + Environment.NewLine +
		                     "#define " + nameFile.ToUpper() + "_H" + Environment.NewLine + Environment.NewLine;
		
		//Create the end guards
		string endGuard = "#endif //" + nameFile.ToUpper() + "_H" + Environment.NewLine;
		
		//create class Name
		string className = char.ToUpper(nameFile[0]) + nameFile.Substring(1);
		
		//Initialize the string for the namespace
		string namespaceOpenString = "";
		
		//Create the namespace opening
		for(int i = 0; i < nameSpaceParts.Length; i++)
		{
			//Insert a number of tab equal to the actual number of parts
			for (int j = 0; j < i; j++)
			{
				namespaceOpenString += "\t";
			}
			
			//Add the namespace opening
			namespaceOpenString += "namespace " + nameSpaceParts[i] + Environment.NewLine;
			
			//Insert a number of tab equal to the actual number of parts
			for (int j = 0; j < i; j++)
			{
				namespaceOpenString += "\t";
			}
			
			//Add the namespace body
			namespaceOpenString += "{" + Environment.NewLine;

		}
		
		//Initialize the namespace closing string
		string namespaceCloseString = "";
		
		//Create the namespace closing
		for (int i = nameSpaceParts.Length; i > 0; i--)
		{
			//Insert a number of tab equal to the actual number of parts
			for (int j = 0; j < i - 1; j++)
			{
				namespaceCloseString += "\t";
			}
			namespaceCloseString += "} // namespace " + nameSpaceParts[i - 1] + Environment.NewLine;
		}
		
		//Initialize the total number of tabs
		string tabs = new string('\t', nameSpaceParts.Length);
		
		//Create class Body
		string headerBody = tabs + "class " + className + Environment.NewLine +
		                   tabs + "{" + Environment.NewLine +
		                   tabs + "public:" + Environment.NewLine +
		                   tabs + "\t" + "/* #region public definitions */" + Environment.NewLine +
		                   tabs + Environment.NewLine +
		                   tabs + "\t" + "using Ptr = " + className + "*;" + Environment.NewLine +
		                   tabs + Environment.NewLine +
		                   tabs + "\t" + "using CPtr = " + className + " const*;" + Environment.NewLine +
		                   tabs + Environment.NewLine +
		                   tabs + "\t" + "/* #endregion */" + Environment.NewLine +
		                   tabs + "protected:" + Environment.NewLine +
		                   tabs + "\t" + "/* #region protected definitions */" + Environment.NewLine +
		                   tabs + Environment.NewLine +
		                   tabs + "\t" + "/* #endregion */" + Environment.NewLine +
		                   tabs + "private:" + Environment.NewLine +
		                   tabs + "\t" + "/* #region private definitions */" + Environment.NewLine +
		                   tabs + Environment.NewLine +
		                   tabs + "\t" + "/* #endregion */" + Environment.NewLine +
		                   tabs + "public:" + Environment.NewLine +
		                   tabs + "\t" + "/* #region public methods */" + Environment.NewLine +
		                   tabs + Environment.NewLine +
		                   tabs + "\t" + className + "() = default;" + Environment.NewLine + 
		                   tabs + Environment.NewLine +
		                   tabs + "\t" + "/* #endregion */" + Environment.NewLine +
		                   tabs + "protected:" + Environment.NewLine +
		                   tabs + "\t" + "/* #region protected methods */" + Environment.NewLine +
		                   tabs + Environment.NewLine + 
		                   tabs + "\t" + "/* #endregion */" + Environment.NewLine +
		                   tabs + "private:" + Environment.NewLine +
		                   tabs + "\t" + "/* #region private methods */" + Environment.NewLine +
		                   tabs + Environment.NewLine + 
		                   tabs + "\t" + "/* #endregion */" + Environment.NewLine +
		                   tabs + "public:" + Environment.NewLine +
		                   tabs + "\t" + "/* #region public variables */" + Environment.NewLine +
		                   tabs + Environment.NewLine + 
		                   tabs + "\t" + "/* #endregion */" + Environment.NewLine +
		                   tabs + "protected:" + Environment.NewLine +
		                   tabs + "\t" + "/* #region protected variables */" + Environment.NewLine +
		                   tabs + Environment.NewLine +
		                   tabs + "\t" + "/* #endregion */" + Environment.NewLine +
		                   tabs + "private:" + Environment.NewLine +
		                   tabs + "\t" + "/* #region private variables */" + Environment.NewLine +
		                   tabs + Environment.NewLine +
		                   tabs + "\t" + "/* #endregion */" + Environment.NewLine +
		                   tabs + "};" + Environment.NewLine;
		
		return (startGuard + namespaceOpenString + headerBody + namespaceCloseString + endGuard);
	}

	private static string CreateSourceBody(string nameFile, string[] nameSpaceParts)
	{
		//Initialize the namespace body
		string namespaceBody = "";
		
		//Check if the nameSpaceParts is not empty
		if (nameSpaceParts.Length != 0)
		{
			//Yes, then create the namespace body
			namespaceBody = "using namespace ";
		}
		
		//Cycle through the nameSpaceParts
		for (int i = 0; i < nameSpaceParts.Length; i++)
		{
			//Check if the nameSpaceParts is empty
			if (string.IsNullOrEmpty(nameSpaceParts[i]))
			{
				//Yes, then skip it
				continue;
			}
			
			//Add the nameSpacePart
			namespaceBody += nameSpaceParts[i];
			
			//Check if i is not the last iteration
			if (i < nameSpaceParts.Length - 1)
			{
				//Yes, then add a ::
				namespaceBody += "::";
			}
			else
			{
				// i is the last iteration, then add a ;
				namespaceBody += ";";
				
				//Add two new lines
				namespaceBody += Environment.NewLine + Environment.NewLine;
			}
			
		}
		
		//Create the include file name
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
		
		return (include + namespaceBody + sourceBody);
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