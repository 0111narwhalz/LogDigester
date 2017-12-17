using System;
using System.Collections;
using System.Linq;
using System.IO;

class LogDigester
{
	public static void Main()
	{
		Console.WriteLine("Specify source directory (absolute or relative).");
		string[] files = Directory.GetFiles(Console.ReadLine());
		Console.WriteLine("Specify destination directory (absolute or relative).");
		string outputDir = Console.ReadLine();
		var fileQuery = (from s in files where (s.Split('.')[s.Split('.').Length-1] == "log") select s);
		foreach(string s in fileQuery)
		{
			Console.WriteLine(s);
			Digest(s, outputDir);
		}
	}
	
	static void Digest(string inputFile, string outputDir)
	{
		string[] months = {"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
		string outputFilePrefix = inputFile.Split('/')[inputFile.Split('/').Length - 1];
		outputFilePrefix = outputDir + "/" + outputFilePrefix.Remove(outputFilePrefix.Length - 4);
		Directory.CreateDirectory(outputFilePrefix);
		string[] rawContent = File.ReadAllLines(inputFile);
		var content = (from string s in rawContent where (Array.Exists(months, element => element == s.Split(' ')[0])) select s);
		foreach(string line in content)
		{
			File.AppendAllText(outputFilePrefix + "/" + line.Split(' ')[0] + ".log", line + "\n");
		}
	}
}
