using People.Models;
using System.Collections.Generic;

namespace People;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    public async void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        await App.PersonRepo.AddNewPerson(newPerson.Text);
        statusMessage.Text = App.PersonRepo.StatusMessage;
    }

    public async void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<Person> people = await App.PersonRepo.GetAllPeople();
        peopleList.ItemsSource = people;
    }

    private async void DeleteButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Person personaAEliminar)
        {
            await App.PersonRepo.DeletePerson(personaAEliminar);
            List<Person> personas = await App.PersonRepo.GetAllPeople();
            peopleList.ItemsSource = personas;
            await DisplayAlert("Confirmación", "Tomás Ontaneda acaba de eliminar un registro", "Ok");
        }
    }
}

