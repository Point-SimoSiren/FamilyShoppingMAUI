using Newtonsoft.Json;
using System.Text;

namespace FamilyShoppingMAUI;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}

    private async void AddBtn_Clicked(object sender, EventArgs e)
    {
        Item newItem = new Item();
        newItem.ItemName = ItemField.Text;
        newItem.Pieces = int.Parse(PiecesField.Text);
        newItem.TimeAdded = DateTime.Now;

        // Muutetaan em. data objekti Jsoniksi
        var input = JsonConvert.SerializeObject(newItem);

        HttpContent content = new StringContent(input, Encoding.UTF8, "application/json");

        HttpClient client = new();
        client.BaseAddress = new Uri("https://familyshoppingrestapi.azurewebsites.net");
        HttpResponseMessage res = await client.PostAsync("/api/shopping/", content);
        if (res.StatusCode == System.Net.HttpStatusCode.OK)
        {
            ItemField.Text = "";
            PiecesField.Text = "";

            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Virhe ohjelmassa", "Ota yhteyttä Simoon", "ok");
        }
    }
}