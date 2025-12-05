using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager {
    private static UIManager instance;

    public static UIManager Instance {
        get {
            if (instance == null) {
                instance = (UIManager)GameManager.Instance.GetManager<UIManager>();
            }

            return instance;
        }
    }

}
