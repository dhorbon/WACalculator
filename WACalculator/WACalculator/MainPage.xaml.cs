using System.Diagnostics;
using System.Text.Json;

namespace WACalculator
{
    public partial class MainPage : ContentPage
    {
        private List<(string, string)> history = new();
        public MainPage()
        {
            InitializeComponent();
        }

        private void Submit(object sender, EventArgs e) =>
            Submit("");
        private void Submit(string query)
        {
            HttpClient client = new HttpClient();

            string uriString = "http://api.wolframalpha.com/v1/result?appid=LJQJLL-V8UX7J2499&i=";

            if (query == "")
            {
                uriString += input.Text.Replace(' ', '+');
            }
            else
            {
                uriString += query.Replace(' ', '+');
            }

            Debug.WriteLine(uriString);
            
            Uri uri = new Uri(uriString);
  
            var response = client.GetAsync(uri).Result;
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;

                output.Text = responseContent;

                history.Add((input.Text, responseContent));
            }
            else
            {
                output.Text = $"error: {response.StatusCode}";
                
            }
        }

        private async void Save(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync();

                string text = "";
                for (int i = 0; i < history.Count; i++)
                {
                    text += $"{history[i].Item1}: {history[i].Item2}\n";
                }

                var streamWriter = new StreamWriter(File.OpenWrite(result.FullPath));
                streamWriter.Write(text);
                streamWriter.Close();

                ioMessage.Text = "Saved succesfully";
            }
            catch
            {
                ioMessage.Text = "Save failed";
            }
        }

        private async void Read(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync();

                Submit(new StreamReader(File.OpenRead(result.FullPath)).ReadLine());
            }
            catch
            {
                ioMessage.Text = "Read operation failed";
            }
        }
    }
}
