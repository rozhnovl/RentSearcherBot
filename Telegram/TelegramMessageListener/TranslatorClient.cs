using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TelegramMessageListener;

public class TranslatorClient
{
    public TranslatorClient(ILogger log)
    {
        this.Log = log;
    }

    private const string endpoint = "https://api.cognitive.microsofttranslator.com";

    private ILogger Log;

    // Async call to the Translator Text API
    public async Task<string> TranslateTextRequest(string subscriptionKey, string route,
        string inputText)
    {
        try
        {
            object[] body = new object[] { new { Text = inputText } };
            var requestBody = JsonSerializer.Serialize(body);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", "northeurope");

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                try
                {
                    TranslationResult[] deserializedOutput =
                        JsonSerializer.Deserialize<TranslationResult[]>(result);
                    // Iterate over the deserialized results.
                    foreach (TranslationResult o in deserializedOutput)
                    {
                        // Print the detected input languge and confidence score.
                        Log.LogInformation("Detected input language: {0}\nConfidence score: {1}\n",
                            o?.DetectedLanguage?.Language, o?.DetectedLanguage?.Score);
                        // Iterate over the results and print each translation.
                        foreach (Translation t in o.Translations)
                        {
                            return t.Text;
                        }
                    }

                    throw new Exception("Failed to translate, output: " + result);
                }
                catch (Exception e)
                {
                    Log.LogError(e, "Translation attempt failed. Service response: " + result);
                    throw;
                }
            }
        }
        catch (Exception e)
        {
            Log.LogError(e, "Translation attempt failed with error: " + e.Message);
            return inputText;
        }
    }

    /// <summary>
    /// The C# classes that represents the JSON returned by the Translator Text API.
    /// </summary>
    public class TranslationResult
    {
        [JsonPropertyName("detectedLanguage")] public DetectedLanguage DetectedLanguage { get; set; }
        [JsonPropertyName("sourceText")] public TextResult SourceText { get; set; }
        [JsonPropertyName("translations")] public Translation[] Translations { get; set; }
    }

    public class DetectedLanguage
    {
        [JsonPropertyName("language")] public string Language { get; set; }
        [JsonPropertyName("score")] public float Score { get; set; }
    }

    public class TextResult
    {
        public string Text { get; set; }
        public string Script { get; set; }
    }

    public class Translation
    {
        [JsonPropertyName("text")] public string Text { get; set; }
        public TextResult Transliteration { get; set; }
        [JsonPropertyName("to")] public string To { get; set; }
        public Alignment Alignment { get; set; }
        public SentenceLength SentLen { get; set; }
    }

    public class Alignment
    {
        public string Proj { get; set; }
    }

    public class SentenceLength
    {
        public int[] SrcSentLen { get; set; }
        public int[] TransSentLen { get; set; }
    }

}