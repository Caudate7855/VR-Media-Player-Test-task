namespace Project.UILoader
{
    public class UIWindowPresenter
    {
        public IAssetLoader AssetLoader;
        
        private UIWindowView _uiWindowView;
        private UIWindowModel _uiWindowModel;

        public UIWindowPresenter(IAssetLoader assetLoader ,string uiWindowViewAddress)
        {
            AssetLoader = assetLoader;
            _uiWindowView = AssetLoader.Load<UIWindowView>(uiWindowViewAddress).GetAwaiter().GetResult();
        }
    }
}