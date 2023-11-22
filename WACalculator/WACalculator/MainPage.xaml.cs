using System.Text.Json;

namespace WACalculator
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        HttpClient client = new();

        public MainPage()
        {
            InitializeComponent();
            
            client.BaseAddress = new Uri("http://api.wolframalpha.com/v1/");
        }

        private void Submit(object sender, EventArgs e)
        {
            Query query = new Query(input.Text);

            var json = JsonSerializer.Serialize(query);

            output.Text += json;


            /*
            StringContent stringContent = new StringContent(json);

            output.Text += stringContent.ReadAsStringAsync().Result;

            var response = client.PostAsync("result", stringContent).Result;
            

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;

                output.Text += responseContent;
            }
            else
            {
                output.Text += "error: " + response.StatusCode;
                
            }
            */
        }
    }
}
