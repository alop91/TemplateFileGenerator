namespace ConsoleApp1;

/**
 * @brief This class contains all the information about the author
 *
 */
public class Info
{
	/* Region public variables */
	
	/**
	 * @brief The name of the author
	 */
	public static string Name { get; } = "AuthorName";
	
	/**
	 * @brief The email of the author
	 */
	public static string Email { get; } = "eMail@domain.com";
	
	/**
	 * @brief The date
	 */
	public static string Date{ get; } = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");

	/* endregion */
}