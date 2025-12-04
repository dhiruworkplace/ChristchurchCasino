using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction {
    void Run(object data, Complete complete);
}
