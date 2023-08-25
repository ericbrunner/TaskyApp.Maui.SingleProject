using MvvmHelpers;

namespace TaskyApp.Maui.SingleProject.Views.Vorgang.Model;

public sealed class AufgabeListItemModel : ObservableObject
{

    private bool _isSelected;

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    private bool _isFavorite;

    public bool IsFavorite
    {
        get => _isFavorite;
        set => SetProperty(ref _isFavorite, value);
    }

    private string? _nummer;

    public string? Nummer
    {
        get => _nummer;
        set => SetProperty(ref _nummer, value);
    }

    private string? _rechnungsanschriftPlz;

    public string? RechnungsanschriftPlz
    {
        get => _rechnungsanschriftPlz;
        set => SetProperty(ref _rechnungsanschriftPlz, value);
    }

    private string? _rechnungsanschriftOrt;

    public string? RechnungsanschriftOrt
    {
        get => _rechnungsanschriftOrt;
        set => SetProperty(ref _rechnungsanschriftOrt, value);
    }

    private string? _rechnungsanschriftStrasse;

    public string? RechnungsanschriftStrasse
    {
        get => _rechnungsanschriftStrasse;
        set => SetProperty(ref _rechnungsanschriftStrasse, value);
    }

    private string? _bemerkung;

    public string? Bemerkung
    {
        get => _bemerkung;
        set => SetProperty(ref _bemerkung, value);
    }

    private string? _bezeichnung;

    public string? Bezeichnung
    {
        get => _bezeichnung;
        set => SetProperty(ref _bezeichnung, value);
    }

    public string? Id { get; set; }

    public AufgabeListItemModel(AppData.AufgabeOverviewData itemData, bool isFavorite)
    {
        IsFavorite = isFavorite;
        
        Id = itemData.Id;
        Nummer = itemData.Nummer;
        Bemerkung = itemData?.Bemerkung;
        Bezeichnung = itemData?.Bezeichnung;
        RechnungsanschriftPlz = itemData.RechnungsanschriftPlz;
        RechnungsanschriftOrt = itemData.RechnungsanschriftOrt;
        RechnungsanschriftStrasse = itemData.RechnungsanschriftStrasse;
    }
}