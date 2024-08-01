using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UILoader.ControlPanel
{
    public class SwitchVideoStateButton : MonoBehaviour
    {
        public event Action OnSwitchButtonPressed;

        [SerializeField] private List<Sprite> _buttonStateSprites; 
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        private bool _isPlaying;

        private void Start()
        {
            _buttonImage.sprite = _buttonStateSprites[0];
            _button.onClick.AddListener(OnSwitchButtonPressedHandler);
        }

        public Button GetButton()
        {
            return _button;
        }

        private void OnSwitchButtonPressedHandler()
        {
            ChangeButtonView();
            OnSwitchButtonPressed?.Invoke();
        }

        private void ChangeButtonView()
        {
            if (_isPlaying)
            {
                _isPlaying = false;
                _buttonImage.sprite = _buttonStateSprites[0];
            }
            else
            {
                _isPlaying = true;
                _buttonImage.sprite = _buttonStateSprites[1];
            }
        }
    }
}