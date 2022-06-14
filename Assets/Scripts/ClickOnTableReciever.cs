using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnTableReciever : MonoBehaviour, IPointerClickHandler
{
    public GameObject target;
    public PlayerControls player;
    public static bool activate = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            if (this.transform.rotation.y == 0)
            {
                target.transform.position = new Vector3(this.transform.position.x + 1, 0f, this.transform.position.z + 0.55f);
            }
           activate = true;
           player.MoveToTable();
        }
    }
}
