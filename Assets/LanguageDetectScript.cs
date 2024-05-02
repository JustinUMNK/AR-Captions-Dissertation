using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

public class LanguageDetectScript : MonoBehaviour
{
    static string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
    static string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");

    [SerializeField] private TextMeshProUGUI canvasText;

    private SpeechRecognizer speechRecognizer;
    private AutoDetectSourceLanguageConfig autoDetectSourceLanguageConfig;
    private SpeechConfig speechConfig;
    private AudioConfig audioConfig;

    void Start()
    {
        //Request microphone permissions
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
        speechConfig.SetProperty(PropertyId.SpeechServiceConnection_LanguageIdMode, "Continuous");
        speechConfig.SetProperty(PropertyId.Speech_SegmentationSilenceTimeoutMs, "300");
        audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        SetLanguageToEnglish(); //Sets default language to English
        InitializeSpeechRecognizer();
    }

    async void InitializeSpeechRecognizer()
    {
        if (speechRecognizer != null)
        {
            await speechRecognizer.StopContinuousRecognitionAsync();
            speechRecognizer.Dispose();
        }

        speechRecognizer = new SpeechRecognizer(speechConfig, autoDetectSourceLanguageConfig, audioConfig);

        speechRecognizer.Recognizing += SpeechRecognizer_Recognizing;
        speechRecognizer.Recognized += SpeechRecognizer_Recognized;
        speechRecognizer.Canceled += SpeechRecognizer_Canceled;

        await speechRecognizer.StartContinuousRecognitionAsync();
    }

    public void SetLanguageToMaltese()
    {
        autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(new string[] { "mt-MT" });
        InitializeSpeechRecognizer();
    }

    public void SetLanguageToEnglish()
    {
        autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(new string[] { "en-GB" });
        InitializeSpeechRecognizer();
    }

    public void SetLanguageToBoth()
    {
        autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(new string[] { "mt-MT", "en-GB" });
        InitializeSpeechRecognizer();
    }

    //Update the UI as speech is being recognised
    private void SpeechRecognizer_Recognizing(object sender, SpeechRecognitionEventArgs e)
    {
        MainThreadDispatcher.ExecuteOnMainThread(() =>
        {
            canvasText.text = $"{e.Result.Text}" + "\n";
        });
    }
    
    //Update UI when speech recognition is complete
    private void SpeechRecognizer_Recognized(object sender, SpeechRecognitionEventArgs e)
    {
        MainThreadDispatcher.ExecuteOnMainThread(() =>
        {
            canvasText.text = $"{e.Result.Text}" + "\n";
        });
    }

    //Print log on error
    private void SpeechRecognizer_Canceled(object sender, SpeechRecognitionCanceledEventArgs e)
    {
        if (e.Reason == CancellationReason.Error)
        {
            Debug.LogError($"Speech Recognition Error: {e.ErrorDetails}");
        }
    }

    //Stop running when script is destroyed
    private async void OnDestroy()
    {
        if (speechRecognizer != null)
        {
            await speechRecognizer.StopContinuousRecognitionAsync();
            speechRecognizer.Dispose();
        }
    }
}