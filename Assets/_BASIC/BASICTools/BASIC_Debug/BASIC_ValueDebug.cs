using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASIC_ValueDebug : MonoBehaviour
{
    public void enterNotebook(string str)
    {
        try
        {
            int.TryParse(str, out int val);
            GameControllerScript.Instance.notebooks = val;
            GameControllerScript.Instance.CollectNotebook(true);
        } catch (System.Exception e)
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
}
