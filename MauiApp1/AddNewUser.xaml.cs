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
        AgeEntry.TextChanged += Entry_TextChanged;
    }

    private void SubmitClicked(object sender, EventArgs e)
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

        Navigation.PushAsync(new MainPage());
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        allowNumericInput = true;
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if (allowNumericInput && !char.IsDigit(e.NewTextValue[0]))
            {
                // If the first character is not a digit, prevent input
                
                AgeEntry.Text = e.OldTextValue;
            }
        }
        else
        {
            // Reset the flag if the Entry becomes empty
            allowNumericInput = true;
        }
    }

}