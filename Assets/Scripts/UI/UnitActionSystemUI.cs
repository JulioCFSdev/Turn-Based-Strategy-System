using System;
using Action;
using Environment.Systems;
using UnityEngine;
using Gameplay.Characters;
using Ui;
using UnityEngine.UI;

namespace UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainerTransform;
        private void Start()
        {
            UnitActionSystem.Instance.OnSelectionUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            CreateUnitActionButton();
        }

        private void CreateUnitActionButton()
        {
            foreach (Transform buttonTransform in actionButtonContainerTransform)
            {
                Destroy(buttonTransform.gameObject);
            }
            
            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

            foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
            {
                Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
                ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(baseAction);
            }
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            CreateUnitActionButton();
        }
    }    
}
