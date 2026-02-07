using SixLetterWord;

// todo: read word length from command arguments 
const int targetWordLength = 6;
const string fileName = "input.txt";

string inputFilePath = Path.Combine(AppContext.BaseDirectory, fileName);

// todo: call asynchronously
var words = File.ReadLines(inputFilePath) // space complexity O(1)
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(line => line.Trim());

var finder = new WordCombinationFinder(words, targetWordLength);
var combinations = finder.FindCombinations();

foreach (var combination in combinations)
{
    var parts = string.Join("+", combination);
    var result = string.Concat(combination);
    Console.WriteLine($"{parts}={result}");
}