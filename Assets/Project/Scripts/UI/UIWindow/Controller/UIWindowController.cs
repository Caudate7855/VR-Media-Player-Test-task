using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.UI.ControlPanel;
using Project.UI.Previews;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

namespace Project.UI
{
    public class UIWindowController
    {
        private const float SCALE_ANIMATION_DURATION = 0.1f;
        private const float SELECTED_SCALE = 1.05f;
        private const float UNSELECTED_SCALE = 1f;
        private const string WINDOW_VIEW_ADDRESSABLE_PATH = "UIWindow";

        private UIWindowView _uiWindowView;

        private IAssetLoader _assetLoader;
        private MediaLinks _mediaLinks;

        private List<UIWindowPreview> _previews;
        private UIWindowPreview _currentEpisodePreview;
        private UIWindowPreview _selectedPreview;
        
        private MediaPlayer _mediaPlayer;
        private SwitchVideoStateButton _switchVideoStateButton;

        public UIWindowController(IAssetLoader assetLoader, MediaLinks mediaLinks, Camera camera)
        {
            _assetLoader = assetLoader;
            _mediaLinks = mediaLinks;

            Initialize(camera);
        }

        private async void Initialize(Camera camera)
        {
            await InitializeWindow(camera);
            await InitializePreviews();
        }

        private async UniTask InitializeWindow(Camera camera)
        {
            var uiWindowView = await _assetLoader.Load<UIWindowView>(WINDOW_VIEW_ADDRESSABLE_PATH);
            
            _uiWindowView = Object.Instantiate(uiWindowView);
            _uiWindowView.Initialize(camera);
            _uiWindowView.OnSwitchVideoStateButtonPressed += OnSwitchVideoStateButtonPressed;

            _mediaPlayer = _uiWindowView.GetMediaPlayer();
            _mediaPlayer.Events.AddListener(OnVideoFinished);
            
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

                _previews[i].EpisodeName.text = _mediaLinks.SerializableMediaLinks[i].EpisodeName;
                
                _previews[i].GetLoadingOverlay().Disable();
            }
            
            _currentEpisodePreview = _previews.First();
            _selectedPreview = _previews.First();
            SelectPreview(_previews.First());
        }

        private void OnVideoFinished(MediaPlayer mp, MediaPlayerEvent.EventType eventType, ErrorCode errorCode)
        {
            if (eventType == MediaPlayerEvent.EventType.FinishedPlaying)
            {
                ChangeButtonView(SwitchButtonStates.Pause);
            }
        }
        
        private void ChangeVideo(string videoURL)
        {
            _mediaPlayer.OpenMedia(new MediaPath(videoURL, MediaPathType.AbsolutePathOrURL));
            
            _uiWindowView.SetEpisodeName(_selectedPreview.EpisodeName.text);

            _currentEpisodePreview = _selectedPreview;
        }

        private void OnSwitchVideoStateButtonPressed()
        {
            if (_mediaPlayer.Control.IsPlaying())
            {
                _mediaPlayer.Pause();
                ChangeButtonView(SwitchButtonStates.Pause);
            }
            else
            {
                _mediaPlayer.Play();
                ChangeButtonView(SwitchButtonStates.Play);

                if (_selectedPreview != _currentEpisodePreview)
                {
                    ChangeVideo(_mediaLinks.SerializableMediaLinks[_selectedPreview.GetIndex()].VideoURL);
                }
            }
        }
        
        private void ChangeButtonView(SwitchButtonStates switchButtonState)
        {
            if (switchButtonState == SwitchButtonStates.Play)
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
                ChangeButtonView(SwitchButtonStates.Pause);
                SelectPreview(preview);
            }
        }

        private void SelectPreview(UIWindowPreview preview)
        {
            if (preview.GetState() == PreviewStates.Unselected)
            {
                preview.SetState(PreviewStates.Selected);
                preview.Window.transform.DOScale(SELECTED_SCALE, SCALE_ANIMATION_DURATION).SetEase(Ease.Linear);
            }
        }

        private void DeselectPreview(UIWindowPreview preview)
        {
            if (preview.GetState() == PreviewStates.Selected)
            {
                preview.SetState(PreviewStates.Unselected);
                preview.Window.transform.DOScale(UNSELECTED_SCALE, SCALE_ANIMATION_DURATION).SetEase(Ease.Linear);
            }
        }
    }
}