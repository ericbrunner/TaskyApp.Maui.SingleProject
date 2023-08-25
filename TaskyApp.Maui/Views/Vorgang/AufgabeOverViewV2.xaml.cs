using System.Text.Json;
using TaskyApp.Maui.SingleProject.Views.Vorgang.Model;

namespace TaskyApp.Maui.SingleProject.Views.Vorgang
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AufgabeOverViewV2
    {

        public AufgabeOverViewV2(Func<AppData.AufgabeOverviewData, Task> onItemSelectedFunc)
        {
            InitializeComponent();
        }

        
        public AppData.AufgabeOverviewData ItemSelected { get; set; }




        private async Task<List<AppData.AufgabeOverviewData>> GetTestSource()
        {
            await using var stream = await FileSystem.OpenAppPackageFileAsync("AppData.AufgabeOverviewData.json");
            
            List<AppData.AufgabeOverviewData>? aufgabeOverviewItems =
                JsonSerializer.Deserialize<List<AppData.AufgabeOverviewData>>(stream);

            return aufgabeOverviewItems;
        }

        public AufgabeListItemModel? AufgabeListItemModelToScrollTo { get; private set; }



        List<AufgabeListItemModel> GetAufgabeListItemModels(List<AppData.AufgabeOverviewData>? AufgabeOverviewDataItems, List<string>? listFav)
        {
            var listItemDataContainer = new List<AufgabeListItemModel>();

            if (AufgabeOverviewDataItems == null)
                return listItemDataContainer.OrderBy((item) => item.IsFavorite ? -1 : 1).ToList();
            
            foreach (var aufgabeOverviewData in AufgabeOverviewDataItems)
            {
                var isFav = listFav != null && listFav.Contains(aufgabeOverviewData.Id);

                var aufgabeListItemModel = new AufgabeListItemModel(aufgabeOverviewData, isFav)
                    ;
                if (AufgabeListItemModelToScrollTo == null && ItemSelected != null &&
                    string.Equals(ItemSelected.Id, aufgabeListItemModel?.Id))
                {
                    AufgabeListItemModelToScrollTo = aufgabeListItemModel;
                }


                listItemDataContainer.Add(aufgabeListItemModel);
            }

            return listItemDataContainer.OrderBy((item) => item.IsFavorite ? -1 : 1).ToList();
        }

        private List<AufgabeListItemModel> aufgabeListItemModels;
        private async Task UpdateViewAsync(List<AppData.AufgabeOverviewData> listData, List<string> listFav)
        {

            AufgabeListItemModelToScrollTo = null;

            aufgabeListItemModels = GetAufgabeListItemModels(listData, listFav);


            SetSource(aufgabeListItemModels);


            if (AufgabeListItemModelToScrollTo != null)
                collectionView.ScrollTo(AufgabeListItemModelToScrollTo, position: ScrollToPosition.MakeVisible,
                    animate: false);
            SizeChanged += (sender, e) =>
            {
                if (AufgabeListItemModelToScrollTo != null)
                    collectionView.ScrollTo(AufgabeListItemModelToScrollTo, position: ScrollToPosition.Center,
                        animate: false);
            };


            await Task.CompletedTask;
        }

        private void SetSource(IEnumerable<AufgabeListItemModel> listSource)
        {
            collectionView.ItemsSource = listSource;
        }

        public async Task Init()
        {
            var data = await GetTestSource();

            await UpdateViewAsync(data, null);
        }
    }
}