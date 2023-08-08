using MauiApp1.ClassModels;
using Newtonsoft.Json;

namespace MauiApp1;

public partial class AddNewUser : ContentPage
{
    string jsonFilePath;
    private bool allowNumericInput = true;

    public AddNewUser()
	{
		InitializeComponent();
        jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.json");
    }

    private async void SubmitClicked(object sender, EventArgs e)
    {
        User newUser = new User();
        newUser.name = NameEntry.Text;
        newUser.email = EmailEntry.Text;
        newUser.age = Convert.ToInt32(AgeEntry.Text);



        if (!File.Exists(jsonFilePath))
        {
            List<User> users = new List<User> { newUser };

            string jsonContent = JsonConvert.SerializeObject(users , Formatting.Indented);

            File.WriteAllText(jsonFilePath, jsonContent);



        }
        else
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonData);

            users.Add(newUser);

            string updatedJsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, updatedJsonData);
        }


        bool result = await DisplayAlert("Alert", "User added successfully", "OK", "Close");

        if (result)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }

    private void NumericEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue) && !e.NewTextValue.All(char.IsDigit))
        {
            // Remove non-numeric characters
            ((Entry)sender).Text = new string(e.NewTextValue.Where(char.IsDigit).ToArray());
        }
    }
}