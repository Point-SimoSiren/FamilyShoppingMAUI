using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace FamilyShoppingMAUI;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();

        LoadDataFromRestAPI();

    }

    async void LoadDataFromRestAPI()
    {
        itemList.ItemsSource = new List<string> { "Ladataan", "Ostoslista", "palvelimelta..." };
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://familyshoppingrestapi.azurewebsites.net");
        string json = await client.GetStringAsync("/api/shopping");

        IEnumerable<Item> ienumItems = JsonConvert.DeserializeObject<Item[]>(json);

        ObservableCollection<Item> items = new ObservableCollection<Item>(ienumItems);

        itemList.ItemsSource = items;
    }





    async void kerätty_nappi_Clicked(object sender, EventArgs e)
    {
        Item selected = itemList.SelectedItem as Item;
        bool answer = await DisplayAlert("Menikö oikein?", selected.ItemName + " kerätty?", "Yess! Kyllä meni!", "Ei, Yritän uusiksi");
        if (answer == false)
        {
            return;
        }

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://familyshoppingrestapi.azurewebsites.net");
        HttpResponseMessage res = await client.DeleteAsync("/api/shopping/" + selected.Id.ToString());
        if (res.StatusCode == System.Net.HttpStatusCode.OK)
        {
            LoadDataFromRestAPI();
        }
        else
        {
            await DisplayAlert("Virhe ohjelmassa", "Ota yhteyttä Simoon", "ok");
        }

    }

    private void newPageBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NewPage1());   
    }
}

