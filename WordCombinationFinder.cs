namespace SixLetterWord;

public class WordCombinationFinder
{
    private readonly HashSet<string> _wordSet;
    private readonly int _targetLength;

    public WordCombinationFinder(IEnumerable<string> words, int targetLength = 6)
    {
        _targetLength = targetLength;
        _wordSet = new HashSet<string>(words, StringComparer.OrdinalIgnoreCase);
    }

    public IEnumerable<List<string>> FindCombinations()
    {
        var targetWords = _wordSet.Where(w => w.Length == _targetLength);

        foreach (var target in targetWords)
        {
            foreach (var combination in FindCombinationsForWord(target))
            {
                yield return combination;
            }
        }
    }

    private IEnumerable<List<string>> FindCombinationsForWord(string target)
    {
        var results = new List<List<string>>();
        FindCombinationsRecursive(target, 0, [], results);
        return results;
    }

    private void FindCombinationsRecursive(
        string target,
        int startIndex,
        List<string> currentParts,
        List<List<string>> results)
    {
        // Base case: we've matched the entire target word
        if (startIndex == target.Length)
        {
            // Only valid if we used more than one word
            if (currentParts.Count > 1)
            {
                results.Add(new List<string>(currentParts));
            }
            return;
        }

        // Try all possible prefix lengths from current position
        for (int length = 1; length <= target.Length - startIndex; length++)
        {
            string prefix = target.Substring(startIndex, length);

            if (_wordSet.Contains(prefix))
            {
                currentParts.Add(prefix);
                FindCombinationsRecursive(target, startIndex + length, currentParts, results);
                currentParts.RemoveAt(currentParts.Count - 1); // Backtrack
            }
        }
    }
}