using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TestShooter.UI
{

    public class KeyboardControlScheme : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _actionsAsset;
        [SerializeField] private TextMeshProUGUI _inputText;
        [SerializeField] private LayoutGroup _inputBindingParent;

        private const string KeyboardControlSchemeName = "Keyboard";
        private const string PlayerInputName = "PlayerDefaultInput";

        private void Start()
        {
            if (_actionsAsset == null)
            {
                Debug.LogError("Assign input action asset to keyboard control scheme");
                return;
            }

            DisplayControls(_actionsAsset.FindActionMap(PlayerInputName));
        }

        private void DisplayControls(InputActionMap actionMap)
        {
            foreach (var action in actionMap.actions)
            {
                var processedControls = new HashSet<string>();

                foreach (var binding in action.bindings)
                {
                    if (!binding.groups.Contains(KeyboardControlSchemeName))
                    {
                        continue;
                    }

                    var controls = action.controls;

                    foreach (var control in controls)
                    {
                        if (!(control.device is Keyboard))
                        {
                            continue;
                        }

                        string controlIdentifier = $"{action.name}:{control.name}";

                        if (!processedControls.Contains(controlIdentifier))
                        {
                            TextMeshProUGUI controlText = Instantiate(_inputText, _inputBindingParent.transform);
                            controlText.text = $"{action.name} : {control.name}";
                            processedControls.Add(controlIdentifier);
                        }
                    }                   
                }
            }
        }
    }
}
