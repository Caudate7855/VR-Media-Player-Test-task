using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UILoader.ControlPanel
{
    public class SwitchVideoStateButton : MonoBehaviour
    {
        public event Action<SwitchVideoStateButton> OnSwitchButtonPressed;
        
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        private bool _isPlaying;

        private void Start()
        {
            _button.onClick.AddListener(OnSwitchButtonPressedHandler);
        }

        public Button GetButton()
        {
            return _button;
        }

        private void OnSwitchButtonPressedHandler()
        {
            OnSwitchButtonPressed?.Invoke(this);
        }
    }
}