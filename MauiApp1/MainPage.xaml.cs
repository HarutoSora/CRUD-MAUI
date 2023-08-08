using MauiApp1.ClassModels;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using System.Reflection.Emit;
using System.Data;
using System;
using Microsoft.Maui.Storage;
using System.Diagnostics;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    string jsonFilePath;


    public MainPage()
	{
        InitializeComponent();

        jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.json");

        LoadJsonData();
    }

    protected override void OnAppearing()
    {
        //base.OnAppearing();

        //Refresh the list here
        LoadJsonData();
    }

    private async void OpenADDPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddNewUser());
    }

    private async void OpenEditPage(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        User s = (User)button.CommandParameter;


        await Navigation.PushAsync(new Edituser(s, jsonFilePath));
    }

    private string ReadJSON()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            return jsonData;
        }
        else
            return null;

    }

    private void LoadJsonData()
    {
        try
        {
            string jsonData = ReadJSON();
            if (!string.IsNullOrEmpty(jsonData))
            {
                List<User> data = JsonConvert.DeserializeObject<List<User>>(jsonData);
                dataListView.ItemsSource = data;
            }
            else
            {
                Console.WriteLine("JSON data is empty.");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading JSON data: {ex.Message}");
        }
    }

    private void DeleteUser(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        User s = (User)button.CommandParameter;

        List<User> users = JsonConvert.DeserializeObject<List<User>>(ReadJSON());


        User userToDelete = users.FirstOrDefault(user => user.email == s.email);

        if (userToDelete != null)
        {
            users.Remove(userToDelete);

            // Serialize and save the updated data
            string updatedJsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, updatedJsonData);
        }
        OnAppearing();
    }

    



}

