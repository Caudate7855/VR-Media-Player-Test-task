using System;
using System.Collections.Generic;
using Project.UI.ControlPanel;
using Project.UI.Previews;
using RenderHeads.Media.AVProVideo;
using TMPro;
using UnityEngine;

namespace Project.UI
{
    public class UIWindowView : MonoBehaviour
    {
        public event Action OnSwitchVideoStateButtonPressed;

        [SerializeField] private MediaPlayer _mediaPlayer;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<UIWindowPreview> _previews;
        [SerializeField] private List<Sprite> _buttonStateSprites; 
        [SerializeField] private SwitchVideoStateButton _switchVideoStateButton;
        [SerializeField] private TMP_Text _episodeName;
        
        public void Initialize(Camera camera)
        {
            _canvas.worldCamera = camera;
            _switchVideoStateButton.GetButton().onClick.AddListener(OnSwitchVideoStateButtonPressedHandler);
        }

        public void SetEpisodeName(string value)
        {
            _episodeName.text = value;
        }

        public SwitchVideoStateButton GetSwitchVideoStateButton()
        {
            return _switchVideoStateButton;
        }

        public List<Sprite> GetButtonStateSprites()
        {
            return _buttonStateSprites;
        }

        public MediaPlayer GetMediaPlayer()
        {
            return _mediaPlayer;
        }

        public List<UIWindowPreview> GetPreviews()
        {
            return _previews;
        }

        private void OnSwitchVideoStateButtonPressedHandler()
        {
            OnSwitchVideoStateButtonPressed?.Invoke();
        }
    }
}