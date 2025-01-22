using PruebaP3.ViewModel;

namespace PruebaP3.Views;

public partial class MovieListPage : ContentPage
{
    private readonly MovieListViewModel _viewModel;

    public MovieListPage(MovieListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadMoviesCommand.ExecuteAsync(null); //  Carga las películas al abrir la página
    }
}

