using _3Eva_IA.Properties;
using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Eva_IA.Acceso_a_datos
{
    public class AzureAPI
    {


        public static string AnalyzeImage(string pathImage)
        {
            try
            {
                //NuGet\Install-Package Azure.AI.Vision.ImageAnalysis -Version 1.0.0-beta.2
                StringBuilder texto = new StringBuilder();

                ImageAnalysisClient client = new ImageAnalysisClient(
                    new Uri(ClavesSingleton.Instancia.VisionEndpoint),
                    new AzureKeyCredential(ClavesSingleton.Instancia.VisionKey));

                byte[] imageData = File.ReadAllBytes(pathImage);
                BinaryData imageBinaryData = new BinaryData(imageData);
                ImageAnalysisResult result = client.Analyze(imageBinaryData,
                    VisualFeatures.Caption | VisualFeatures.Read,
                    new ImageAnalysisOptions { GenderNeutralCaption = true });

                texto.AppendLine(result.Caption.Text + "#");

                foreach (DetectedTextBlock block in result.Read.Blocks)
                    foreach (DetectedTextLine line in block.Lines)
                    {
                        texto.AppendLine(line.Text);
                    }
                return texto.ToString();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            return null;
        }
        private async Task<string> getTextImage(string imagePath)
        {
            try
            {
                StringBuilder texto = new StringBuilder();
                ComputerVisionClient client = Authenticate(ClavesSingleton.Instancia.VisionEndpoint, ClavesSingleton.Instancia.VisionKey);

                using (Stream imageStream = File.OpenRead(imagePath))
                {
                    OcrResult result = await client.RecognizePrintedTextInStreamAsync(true, imageStream, language: OcrLanguages.En);

                    foreach (OcrRegion region in result.Regions)
                    {
                        foreach (OcrLine line in region.Lines)
                        {
                            foreach (OcrWord word in line.Words)
                            {
                                texto.Append(word.Text + " ");
                            }
                            texto.AppendLine();
                        }
                    }

                }
                return texto.ToString();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            return null;
        }
        static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key));
            client.Endpoint = endpoint;
            return client;
        }

        public static async void readText(string text, string voice)
        {
            try
            {
                var config = SpeechConfig.FromSubscription(ClavesSingleton.Instancia.SpeechKey, ClavesSingleton.Instancia.Region);
                config.SpeechSynthesisVoiceName = voice;

                using (var synthesizer = new SpeechSynthesizer(config))
                {
                    text = text.Replace("\n", "").Replace("\r", "");
                    using (var result = await synthesizer.SpeakTextAsync(text))
                    {
                        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                        {
                            Console.WriteLine($"Speech synthesized for text [{text}]");
                        }
                        else if (result.Reason == ResultReason.Canceled)
                        {
                            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                            Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                            if (cancellation.Reason == CancellationReason.Error)
                            {
                                Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                                Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                                Console.WriteLine($"CANCELED: Did you update the subscription info?");
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
        public static async Task<System.Collections.ObjectModel.ReadOnlyCollection<VoiceInfo>> getVoices()
        {
            try
            {
                var config = SpeechConfig.FromSubscription(ClavesSingleton.Instancia.SpeechKey, ClavesSingleton.Instancia.Region);

                using (var synthesizer = new SpeechSynthesizer(config))
                {
                    var availableVoices = await synthesizer.GetVoicesAsync(); 
                    return availableVoices.Voices;

                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            return null;
        }
    }
}

