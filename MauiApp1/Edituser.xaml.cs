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
        AgeEntry.TextChanged += Entry_TextChanged;
    }


    private void EditUser(object sender, EventArgs e)
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
        Navigation.PushAsync(new MainPage());
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        allowNumericInput = true;
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if (allowNumericInput && !char.IsDigit(e.NewTextValue[0]))
            {                
                AgeEntry.Text = e.OldTextValue;
            }
        }
        else
        {
            allowNumericInput = true;
        }
    }

}