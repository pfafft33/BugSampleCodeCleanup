using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace BugSampleCodeCleanup;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    public const string pattern = @"^\d{5}(?:[-\s]\d{4})?$";

    public static bool IsZipCode(string zipCode)
    {
        if (zipCode == null)
            return true;
        if (zipCode.Length == 0)
            return true;
        return Regex.IsMatch(zipCode, pattern);
    }

    public static void SerializeObjectToXML<T>(T item, string FilePath)
    {
        XmlSerializer x = new(typeof(T));

        FileInfo fi = new(FilePath);
        if (!fi.Directory.Exists)
        {
            System.IO.Directory.CreateDirectory(fi.DirectoryName);
        }

        XmlWriterSettings settings = new()
        {
            CloseOutput = true,
            Indent = true,
            IndentChars = "\t"
        };

        using (XmlWriter w = XmlWriter.Create(File.CreateText(FilePath), settings))
        {
            x.Serialize(w, item);
            w.Close();
            w.Dispose();
        }
    }

}

