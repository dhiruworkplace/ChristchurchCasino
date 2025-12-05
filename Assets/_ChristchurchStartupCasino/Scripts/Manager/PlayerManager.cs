using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager {

    private static PlayerManager instance;

    public static PlayerManager Instance {
        get {
            if (instance == null) {
                instance = (PlayerManager)GameManager.Instance.GetManager<PlayerManager>();
            }

            return instance;
        }
    }
}
