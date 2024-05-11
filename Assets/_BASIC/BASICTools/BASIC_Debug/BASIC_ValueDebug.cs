using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BASIC.Debugging; 

namespace BASIC.Debugging
{
    public class BASIC_ValueDebug : MonoBehaviour
    {
        private bool isEsp;

        private void OnGUI()
        {
            if (isEsp)
            {
                foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC") as GameObject[])
                {
                    //In-Game Position
                    Vector3 pivotPos = npc.transform.position; //Pivot point NOT at the feet, at the center
                    Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - 2f; //At the feet
                    Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f; //At the head

                    //Screen Position
                    Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                    Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);

                    if (w2s_footpos.z > 0f)
                    {
                        DrawBoxESP(w2s_footpos, w2s_headpos, Color.black, npc.name);
                    }
                }
            }
        }
        public void DrawBoxESP(Vector3 footpos, Vector3 headpos, Color color, string npcName) //Rendering the ESP
        {
            float height = headpos.y - footpos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;

            BASIC_DebugRenderer.DrawBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 2f);
            BASIC_DebugRenderer.DrawString(new Vector2(footpos.x - (width / 2), (float)Screen.height - footpos.y - height), npcName, Color.red, false);
            BASIC_DebugRenderer.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), color, 2f);
        }


        public void enterNotebook(string str)
        {
            try
            {
                int.TryParse(str, out int val);
                GameControllerScript.Instance.notebooks = val;
                GameControllerScript.Instance.CollectNotebook(true);
            }
            catch (System.Exception e)
            {
                Debug.LogError("BASIC // VALUEDEBUG // ERROR WHILE ENTERING NOTEBOOK: " + e);
            }
        }

        public void setStamina(string str)
        {
            try
            {
                int.TryParse(str, out int val);
                GameControllerScript.Instance.player.stamina = val;
            }
            catch (System.Exception e)
            {
                Debug.LogError("BASIC // VALUEDEBUG // ERROR WHILE SETTING STAMINA: " + e);
            }
        }

        public void activateSpoop()
        {
            GameControllerScript.Instance.ActivateSpoopMode();
        }

        public void actibvateFinale()
        {
            GameControllerScript.Instance.ActivateFinaleMode();
        }

        public void toggleNoclip(TextMeshProUGUI tex)
        {
            Debug.Log(GameControllerScript.Instance.player.GetComponent<CharacterController>().excludeLayers.ToString());
            if (GameControllerScript.Instance.player.GetComponent<CharacterController>().excludeLayers == Physics.AllLayers)
            {
                GameControllerScript.Instance.player.GetComponent<CharacterController>().excludeLayers = 0;
                tex.text = "Noclip: false";
            }
            else
            {
                GameControllerScript.Instance.player.GetComponent<CharacterController>().excludeLayers = Physics.AllLayers;
                tex.text = "Noclip: true";
            }
        }

        public void toggleESP(TextMeshProUGUI tex)
        {
            isEsp = !isEsp;

            if (isEsp)
            {
                tex.text = "NPC ESP: true";
            }
            else
            {
                tex.text = "NPC ESP: false";
            }
        }
    }
}