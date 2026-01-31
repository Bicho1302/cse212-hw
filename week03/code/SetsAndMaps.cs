using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
{
    // Use a set for O(1) average lookups
    var seen = new HashSet<string>();
    var results = new List<string>();

    // Optional: prevent duplicates if input ever has duplicates
    var produced = new HashSet<string>();

    foreach (var word in words)
    {
        if (string.IsNullOrEmpty(word) || word.Length != 2)
            continue;

        // Special case: "aa" should not match anything
        if (word[0] == word[1])
        {
            seen.Add(word);
            continue;
        }

        var rev = new string(new[] { word[1], word[0] });

        if (seen.Contains(rev))
        {
            // canonical key avoids duplicates if input repeats
            var a = string.CompareOrdinal(word, rev) < 0 ? word : rev;
            var b = a == word ? rev : word;
            var key = $"{a}|{b}";

            if (produced.Add(key))
            {
                results.Add($"{rev} & {word}");
            }
        }

        seen.Add(word);
    }

    return results.ToArray();
}

    public static Dictionary<string, int> SummarizeDegrees(string filename)
{
    var degrees = new Dictionary<string, int>();

    foreach (var line in File.ReadLines(filename))
    {
        var fields = line.Split(",");
        if (fields.Length < 4) continue;

        var degree = fields[3].Trim();

        if (degrees.ContainsKey(degree))
            degrees[degree]++;
        else
            degrees[degree] = 1;
    }

    return degrees;
}


      public static bool IsAnagram(string word1, string word2)
{
    if (word1 == null || word2 == null) return false;

    // Dictionary required by the assignment
    var counts = new Dictionary<char, int>(capacity: 256);

    int len1 = 0;
    foreach (var ch in word1)
    {
        if (ch == ' ') continue;
        len1++;

        // faster case normalization than ToLowerInvariant for ASCII letters
        char c = ch;
        if (c >= 'A' && c <= 'Z') c = (char)(c + 32);

        if (counts.TryGetValue(c, out var val))
            counts[c] = val + 1;
        else
            counts[c] = 1;
    }

    int len2 = 0;
    foreach (var ch in word2)
    {
        if (ch == ' ') continue;
        len2++;

        char c = ch;
        if (c >= 'A' && c <= 'Z') c = (char)(c + 32);

        if (!counts.TryGetValue(c, out var val))
            return false;

        val--;
        if (val < 0) return false;
        if (val == 0) counts.Remove(c);
        else counts[c] = val;
    }

    return len1 == len2 && counts.Count == 0;
}
    public static string[] EarthquakeDailySummary()
{
    const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
    using var client = new HttpClient();
    using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
    using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
    using var reader = new StreamReader(jsonStream);
    var json = reader.ReadToEnd();
    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

    if (featureCollection?.Features == null || featureCollection.Features.Length == 0)
        return Array.Empty<string>();

    var results = new List<string>(featureCollection.Features.Length);

    foreach (var feature in featureCollection.Features)
    {
        var place = feature?.Properties?.Place ?? "Unknown location";
        var mag = feature?.Properties?.Mag;

        // Ensure formatting always contains " - Mag "
        var magText = mag.HasValue ? mag.Value.ToString() : "Unknown";
        results.Add($"{place} - Mag {magText}");
    }

    return results.ToArray();
}

}