using UnityEngine;

[System.Serializable]
public class Task
{
    public string name;
    public int color;
    public float size;
    public Vector2 target;
    public bool compleated;

    public Task(string n, int c, float s, Vector2 t){
        name = n;
        color = c;
        size = s;
        target = t;
        compleated = false;
    }
}