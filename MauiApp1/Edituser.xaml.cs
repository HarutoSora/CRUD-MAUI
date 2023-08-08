using MauiApp1.ClassModels;
using Newtonsoft.Json;

namespace MauiApp1;

public partial class Edituser : ContentPage
{
    User MainUser;
    string jsonFilePath;
    private bool allowNumericInput = true;

    public Edituser(User u,string Path)
	{
		InitializeComponent();
        jsonFilePath = Path;
        MainUser = u;
        NameEntry.Text = u.name;
        EmailEntry.Text = u.email;
		AgeEntry.Text = u.age.ToString();
    }


    private void MyEntry_Focused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry)
        {
            entry.Text = string.Empty; // Clear the text of the focused Entry
        }
    }

    private async void EditUser(object sender, EventArgs e)
    {
        string jsonData = File.ReadAllText(jsonFilePath);
        List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonData);


        User user = users.FirstOrDefault(user => user.email == MainUser.email);

        if (user != null)
        {
            user.name = NameEntry.Text;
            user.email = EmailEntry.Text;
            user.age = Convert.ToInt32(AgeEntry.Text);


            string updatedJsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, updatedJsonData);
        }

        bool result = await DisplayAlert("Alert", "User : "+user.name + " was Edited successfully", "OK", "Close");

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