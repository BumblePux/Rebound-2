using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BumblePux.Rebound2.UI.Gameplay
{
    public class ComboDisplay : MonoBehaviour
    {
        [Header("Required References")]
        public TMP_Text ComboText;

        public void UpdateComboText(int combo)
        {
            ComboText?.SetText(combo.ToString());

            gameObject.SetActive(combo <= 1 ? false : true);
        }
    }
}