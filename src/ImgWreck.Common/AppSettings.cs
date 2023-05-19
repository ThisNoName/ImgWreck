namespace ImgWreck.Common;

// global singleton settings class for the app
public class App
{
    public static AppSettings Settings { get; set; }
    static App() { Settings = new AppSettings(); }
}

public class AppSettings
{
    public string AppName { get; set; } = "";
    public string AppEnv { get; set; } = "";
    public Dictionary<string, int> MedicalDocDelayCalendarDays { get; set; } = new Dictionary<string, int>();
    public Dictionary<string, string> MedicalDocTemplates { get; set; }
}