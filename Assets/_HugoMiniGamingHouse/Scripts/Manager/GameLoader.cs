using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

    public Transform customerStart;
    private WaitForSeconds one;
    private List<GameObject> customers = new List<GameObject>();

    public List<SlotMachineView> SlotMachineViews
    {
        get
        {
            List<SlotMachineView> slotMachineViews = new List<SlotMachineView>();
            List<SlotView> slotViews = Game.Instance.slotViews.FindAll((SlotView s) => s is SlotMachineView);
            foreach (var item in slotViews)
            {
                slotMachineViews.Add(item as SlotMachineView);
            }
            return slotMachineViews;
        }
    }

    public List<SlotMachineView> ActiveSlots
    {
        get
        {
            return SlotMachineViews.FindAll((SlotMachineView s) => s.SlotBase != null);
        }
    }

    private IEnumerator Start()
    {
        one = new WaitForSeconds(1);
        while (true)
        {
            if (ActiveSlots.Count > customers.Count)
            {
                GameObject g = Instantiate(GameResource.Instance.customerPrefab);
                g.transform.position = customerStart.position;
                customers.Add(g);
            }
            yield return one;
        }
    }

    public void RemoveCustomer(GameObject g)
    {
        customers.Remove(g);
    }
}
