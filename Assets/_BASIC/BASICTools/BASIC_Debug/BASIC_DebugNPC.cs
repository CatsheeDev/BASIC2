using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace BASIC.Debugging
{
    public class BASIC_DebugNPC : BASIC_DebugBase
    {
        private GameControllerScript currentGameController;
        private GameObject buttonTemplate;

        private void Start()
        {
            currentGameController = GameControllerScript.Instance;

            if (currentGameController == null)
            {
                Debug.LogError("BASIC // DEBUG_NPC ERROR // GAMECONTROLELER NOT FOUND");
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
                if (go != buttonTemplate)
                {
                    Destroy(go.gameObject);
                }
            }

            foreach (GameObject NPC in GameObject.FindGameObjectsWithTag("NPC")) //no easy way  to find inactive stuff with tag and im too drunk to make my own way
            {
                GameObject newItem = Instantiate(buttonTemplate);
                newItem.transform.SetParent(transform, false);

                newItem.GetComponent<Button>().onClick.AddListener(() => teleportTo(NPC.transform));
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = NPC.name;

                newItem.SetActive(true);
            }
        }
        void teleportTo(Transform goT)
        {
            currentGameController.playerTransform.position = goT.position;
        }
    }
}