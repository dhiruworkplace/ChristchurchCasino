using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ir
{
    public abstract class Toggle : MonoBehaviour
    {

        [SerializeField] private Image image;
        [SerializeField] private Sprite on;
        [SerializeField] private Sprite off;

        protected abstract bool IsOn { get; set; }

        private void OnEnable()
        {
            Show();
        }

        private void Show()
        {
            image.sprite = IsOn ? on : off;
        }

        public void OnClick()
        {
            IsOn = !IsOn;
            Show();
        }
    }
}
