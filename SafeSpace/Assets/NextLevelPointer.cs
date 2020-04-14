using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelPointer : MonoBehaviour
{
    private SceneIndexes pointer;
    private SceneIndexes current;
    public void SetPointer(SceneIndexes i)
    {
        pointer = i;
    }

    public void SetCurrent(SceneIndexes j)
    {
        current = j;
    }
    public SceneIndexes GetPointer()
    {
        return this.pointer;
    }

    public SceneIndexes GetCurrent()
    {
        return this.current;
    }
    
}
