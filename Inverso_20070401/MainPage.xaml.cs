namespace Inverso_20070401
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            listView.ItemsSource = await _dbService.GetResultados();
        }

        private async void btnProcesar_Clicked(object? sender, EventArgs e)
        {
           
            if (int.TryParse(entryA.Text, out int a) &&
                int.TryParse(entryB.Text, out int b) &&
                int.TryParse(entryC.Text, out int c) &&
                int.TryParse(entryD.Text, out int d))
            {
              
                string inversoText = $"{d}, {c}, {b}, {a}";
                labelResultado.Text = $"Inverso: {inversoText}";

        
                if (_editId == 0)
                {
                    await _dbService.Create(new InversoResult
                    {
                        A = entryA.Text,
                        B = entryB.Text,
                        C = entryC.Text,
                        D = entryD.Text,
                        ResultadoInverso = inversoText
                    });
                }
                else
                {
                    await _dbService.Update(new InversoResult
                    {
                        Id = _editId,
                        A = entryA.Text,
                        B = entryB.Text,
                        C = entryC.Text,
                        D = entryD.Text,
                        ResultadoInverso = inversoText
                    });
                    _editId = 0;
                }

            
                entryA.Text = string.Empty;
                entryB.Text = string.Empty;
                entryC.Text = string.Empty;
                entryD.Text = string.Empty;

                await LoadDataAsync();
            }
            else
            {
                await DisplayAlert("Error", "Por favor ingresa números enteros válidos en todos los campos.", "OK");
            }
        }

        private async void listView_ItemTapped(object? sender, ItemTappedEventArgs e)
        {
            if (e.Item is not InversoResult item) return;

            var action = await DisplayActionSheet("Opciones", "Cancelar", null, "Editar", "Eliminar");

            switch (action)
            {
                case "Editar":
                    _editId = item.Id;
                    entryA.Text = item.A;
                    entryB.Text = item.B;
                    entryC.Text = item.C;
                    entryD.Text = item.D;
                    labelResultado.Text = $"Inverso: {item.ResultadoInverso}";
                    break;

                case "Eliminar":
                    await _dbService.Delete(item);
                    await LoadDataAsync();
                    break;
            }
        }
    }
}