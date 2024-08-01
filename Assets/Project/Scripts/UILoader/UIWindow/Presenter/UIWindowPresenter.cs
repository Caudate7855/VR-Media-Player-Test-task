using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        private MediaPlayer _mediaPlayer;

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
            _mediaPlayer = _uiWindowView.GetMediaPlayer();
            _mediaPlayer.OpenMedia(new MediaPath(_mediaLinks.SerializableMediaLinks.First().VideoURL, MediaPathType.AbsolutePathOrURL));
            _mediaPlayer.Play();
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
        }

        private void ChangeVideo(int index)
        {
            _mediaPlayer.OpenMedia(new MediaPath(_mediaLinks.SerializableMediaLinks[index].VideoURL, MediaPathType.AbsolutePathOrURL));
            _mediaPlayer.Play();
        }

        private void OnPreviewButtonClicked(UIWindowPreview preview)
        {
            _mediaPlayer.Stop();
            
            for (int i = 0, count = _previews.Count; i < count; i++)
            {
                if (_previews[i] != preview)
                {
                    DeselectPreview(_previews[i]);
                }
                else
                {
                    ChangeVideo(i);
                }
            }
            
            if (preview.GetState() == PreviewStates.Unselected)
            {
                SelectPreview(preview);
            }
            else
            {
                DeselectPreview(preview);
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