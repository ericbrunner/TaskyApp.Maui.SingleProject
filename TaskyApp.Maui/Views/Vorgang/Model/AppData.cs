namespace TaskyApp.Maui.SingleProject.Views.Vorgang.Model;

public partial class AppData
{
    public interface IBookable
    {
        string? GetId();
        string? GetRootId();
        string? GetBookingId();
        string? GetTitle();
        string? GetNumber();
        string? GetName();
    }
    public class AufgabeOverviewData : IBookable
    {
        public string? Id { get; set; }

        public string? RootId { get; set; }

        public string? BookingId { get; set; }
        public string? Nummer { get; set; }
        public string? Bezeichnung { get; set; }
        public string? Notiz { get; set; }

        public string? Bemerkung { get; set; }

        public string? RechnungsanschriftStrasse { get; set; }
        public string? RechnungsanschriftPlz { get; set; }
        public string? RechnungsanschriftOrt { get; set; }

        public string? SearchText { get; set; }

        public AufgabeOverviewData()
        { }

        public string GetId() => Id;

        public string GetRootId() => RootId;

        public string GetBookingId() => BookingId;
        public string GetTitle() => Bezeichnung;

        public string? GetNumber() => Nummer;

        public string? GetName() => "this.GetDisplayName()";
    }
}