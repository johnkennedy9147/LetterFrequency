using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Frequency.Test")]
namespace FrequencyCounter
{
    public class Frequency
    {
        public static void Main(string[] args)
        {
            bool CaseSensitive = true;

            if (args.Length == 0)
            {
                Console.WriteLine("Please supply a file to process.");
                displayUsage();
                return;
            }

            if (args.Contains("-u"))
            {
                displayUsage();
                return;
            }

            if (args.Length == 1 && args.Contains("-i"))
            {
                Console.WriteLine("Please supply a file to process.");
                displayUsage();
                return;
            }

            if (args.Length >= 2)
            {
                if (args.Contains("-i"))
                {
                    CaseSensitive = false;
                }
            }
            Console.WriteLine($"Case sensitivity set to: {CaseSensitive}");

            try
            {
                string FileContents = ProcessFile(args[0], CaseSensitive);
                var (Counts, TotalLength) = CleanAndCountContents(FileContents);
                ConsoleResults(Counts, TotalLength);
            }
            catch
            {
                return;
            }
        }

        static void ConsoleResults(Dictionary<char, int> counts, int totalLength)
        {
            Console.WriteLine($"Total characters: {totalLength}");
            // sort descending and select top 10, then console
            foreach (KeyValuePair<char, int> entry in counts.OrderByDescending(key => key.Value).Take(10))
                Console.WriteLine($"{entry.Key} ({entry.Value})");
        }

        static (Dictionary<char, int>, int) CleanAndCountContents(string contents)
        {
            // convert to array and remove white space
            char[] _charArray = contents.ToArray();
            char[] _cleanedTextArray = Array.FindAll(_charArray, c => !Char.IsWhiteSpace(c));

            // count total input characters
            int _totalLength = _cleanedTextArray.Length;

            // count frequency of characters
            var _keys = _cleanedTextArray.Distinct();
            Dictionary<char, int> _dictionary = _keys.ToDictionary(x => x, y => 0);
            foreach (var key in _keys)
            {
                _dictionary[key] = _cleanedTextArray.Count(ch => ch == key);
            }

            return (
                _dictionary,
                _totalLength
            );
        }

        static string ProcessFile(string filename, bool caseSensitive)
        {
            string _fileContents = string.Empty;

            try
            {
                _fileContents = File.ReadAllText(filename);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading file: {e.Message}");
                throw;
            }
            finally
            {
                if (caseSensitive == false)
                {
                    _fileContents = _fileContents.ToUpper();
                }
            }
            return _fileContents;
        }

        static void displayUsage()
        {
            Console.WriteLine("Usage: frequency.exe filename [-u] [-i]");
            Console.WriteLine("""Usage: Optional arguments "-u" - display usage""");
            Console.WriteLine("""Usage: Optional arguments "-i" - ignore letter case [-i]""");
        }

    }
}
