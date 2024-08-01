using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.UILoader.ControlPanel;
using Project.UILoader.Previews;
using Project.UILoader.Previews.Enums;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

namespace Project.UILoader
{
    public class UIWindowPresenter
    {
        private const float SCALE_ANIMATION_DURATION = 0.1f;
        private const float SELECTED_SCALE = 1.1f;
        private const float UNSELECTED_SCALE = 1f;

        private UIWindowView _uiWindowView;
        private UIWindowModel _uiWindowModel;

        private IAssetLoader _assetLoader;
        private MediaLinks _mediaLinks;

        private List<UIWindowPreview> _previews;
        private UIWindowPreview _currentEpisodePreview;
        private UIWindowPreview _selectedPreview;
        
        private MediaPlayer _mediaPlayer;
        private SwitchVideoStateButton _switchVideoStateButton;

        public UIWindowPresenter(IAssetLoader assetLoader, string uiWindowViewAddress, MediaLinks mediaLinks,
            Camera camera)
        {
            _assetLoader = assetLoader;
            _mediaLinks = mediaLinks;

            Initialize(uiWindowViewAddress, camera);
        }

        private async void Initialize(string uiWindowViewAddress, Camera camera)
        {
            await InitializeWindow(uiWindowViewAddress, camera);
            await InitializePreviews();
        }

        private async UniTask InitializeWindow(string uiWindowViewAddress, Camera camera)
        {
            var uiWindowView = await _assetLoader.Load<UIWindowView>(uiWindowViewAddress);
            
            _uiWindowView = Object.Instantiate(uiWindowView);
            _uiWindowView.Initialize(camera);
            _uiWindowView.OnSwitchVideoStateButtonPressed += OnSwitchVideoStateButtonPressed;
            
            _mediaPlayer = _uiWindowView.GetMediaPlayer();
            _mediaPlayer.OpenMedia(new MediaPath(_mediaLinks.SerializableMediaLinks.First().VideoURL, MediaPathType.AbsolutePathOrURL), false);
            
            _uiWindowView.SetEpisodeName(_mediaLinks.SerializableMediaLinks.First().EpisodeName);

            _switchVideoStateButton = _uiWindowView.GetSwitchVideoStateButton();
            _switchVideoStateButton.GetButton().image.sprite = _uiWindowView.GetButtonStateSprites()[1];
        }

        private async UniTask InitializePreviews()
        {
            _previews = _uiWindowView.GetPreviews();

            for (int i = 0, count = _previews.Count; i < count; i++)
            {
                _previews[i].OnButtonPressed += OnPreviewButtonClicked;
            }

            for (int i = 0, count = _previews.Count; i < count; i++)
            {
                _previews[i].GetImage().sprite =
                    await URLMediaLoader.URLMediaLoader.LoadImageAsync(_mediaLinks.SerializableMediaLinks[i]
                        .PreviewURL);

                _previews[i].EpisodeName = _mediaLinks.SerializableMediaLinks[i].EpisodeName;
                
                _previews[i].GetLoadingOverlay().Disable();
            }
            
            _currentEpisodePreview = _previews.First();
            _selectedPreview = _previews.First();
            SelectPreview(_previews.First());
        }

        private void ChangeVideo(string videoURL)
        {
            _mediaPlayer.OpenMedia(new MediaPath(videoURL, MediaPathType.AbsolutePathOrURL));
            
            _uiWindowView.SetEpisodeName(_selectedPreview.EpisodeName);

            _currentEpisodePreview = _selectedPreview;
        }

        private void OnSwitchVideoStateButtonPressed()
        {
            if (_mediaPlayer.Control.IsPlaying())
            {
                _mediaPlayer.Pause();
                ChangeButtonView(false);
            }
            else
            {
                _mediaPlayer.Play();
                ChangeButtonView(true);

                if (_selectedPreview != _currentEpisodePreview)
                {
                    ChangeVideo(_mediaLinks.SerializableMediaLinks[_selectedPreview.GetIndex()].VideoURL);
                }
            }
        }
        
        private void ChangeButtonView(bool isPlaying)
        {
            if (isPlaying)
            {
                _switchVideoStateButton.GetButton().image.sprite = _uiWindowView.GetButtonStateSprites()[0];
            }
            else
            {
                _switchVideoStateButton.GetButton().image.sprite = _uiWindowView.GetButtonStateSprites()[1];
            }
        }
        
        private void OnPreviewButtonClicked(UIWindowPreview preview)
        {
            for (int i = 0, count = _previews.Count; i < count; i++)
            {
                if (_previews[i] != preview)
                {
                    DeselectPreview(_previews[i]);
                }
            }
            
            if (preview.GetState() == PreviewStates.Unselected)
            {
                _mediaPlayer.Pause();
                _selectedPreview = preview;
                ChangeButtonView(false);
                SelectPreview(preview);
            }
        }

        private void SelectPreview(UIWindowPreview preview)
        {
            if (preview.GetState() == PreviewStates.Unselected)
            {
                preview.SetState(PreviewStates.Selected);
                preview.transform.DOScale(SELECTED_SCALE, SCALE_ANIMATION_DURATION).SetEase(Ease.Linear);
            }
        }

        private void DeselectPreview(UIWindowPreview preview)
        {
            if (preview.GetState() == PreviewStates.Selected)
            {
                preview.SetState(PreviewStates.Unselected);
                preview.transform.DOScale(UNSELECTED_SCALE, SCALE_ANIMATION_DURATION).SetEase(Ease.Linear);
            }
        }
    }
}