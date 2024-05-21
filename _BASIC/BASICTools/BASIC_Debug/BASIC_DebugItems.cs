using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BASIC.Debugging
{
    public class BASIC_DebugItems : BASIC_DebugBase
    {
        private GameControllerScript currentGameController;
        private GameObject buttonTemplate;

        private void Start()
        {
            currentGameController = GameControllerScript.Instance;

            if (currentGameController == null)
            {
                Debug.LogError("BASIC // DEBUG_ITEMS ERROR // GAMECONTROLELER NOT FOUND");
                Destroy(gameObject);
                return;
            }

            buttonTemplate = GetComponentInChildren<Button>(true).gameObject;

            this.RefreshList();
        }
        public override void RefreshList()
        {
            foreach (Button go in GetComponentsInChildren<Button>())
            {
                if (go.gameObject != buttonTemplate)
                {
                    Destroy(go.gameObject);
                }
            }

            foreach (ItemInfo item in currentGameController.itemProfile.items)
            {
                GameObject newItem = Instantiate(buttonTemplate);
                newItem.transform.SetParent(transform, false);

                newItem.GetComponent<Button>().onClick.AddListener(() => giveItem(item));
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;

                newItem.SetActive(true);
            }
        }

        void giveItem(ItemInfo currItem)
        {
            currentGameController.CollectItem_BASIC(currItem.Value);
        }
    }
}