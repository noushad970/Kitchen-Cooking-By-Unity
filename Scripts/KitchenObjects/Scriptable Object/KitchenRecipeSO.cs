using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class KitchenRecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;
}
