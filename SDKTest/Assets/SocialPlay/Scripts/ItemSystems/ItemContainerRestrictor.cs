using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerRestrictor : MonoBehaviour
{
    public int ContainerItemLimit = 0;
    public List<int> ClassFilterList = new List<int>();
    public List<int> ItemTypeFilterList = new List<int>();

    public enum RestrictorState
    {
        Normal,
        AddOnly,
        RemoveOnly,
        NoAction,
        ItemLimit,
        ClassTypeFilter,
        ItemTypeFilter
    }

    public enum ContainerAction{
        add,
        remove
    }

    public RestrictorState restrictorState = RestrictorState.Normal;

    public ItemContainer restrictedContainer;

    void Awake()
    {
        CheckForValidRestrictedContainer();
    }

    public void CheckForValidRestrictedContainer()
    {
        if (!restrictedContainer)
            throw new Exception("ItemContainerRestrictor could not find a container to restrict.");
    }


    public bool IsRestricted(ContainerAction action)
    {
        switch (restrictorState)
        {
            case RestrictorState.Normal:
                return false;                
            case RestrictorState.AddOnly:
                if (action == ContainerAction.add)
                {
                    return false;
                }
                return true;
            case RestrictorState.RemoveOnly:
                if (action == ContainerAction.remove)
                {
                    return false;
                }
                return true;
            case RestrictorState.NoAction:
                return true;           
            case RestrictorState.ItemLimit:
                if (action == ContainerAction.add)
                {
                    if (restrictedContainer.containerItems.Count >= ContainerItemLimit)
                        return true;
                }

                return false;


        }
        return true;
    }
}