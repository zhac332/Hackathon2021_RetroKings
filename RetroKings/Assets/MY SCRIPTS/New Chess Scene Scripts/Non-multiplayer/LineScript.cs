using UnityEngine;
using UnityEngine.UI;

public class LineScript : MonoBehaviour
{
    [SerializeField] private Text Nr;
    [SerializeField] private Text Move1;
    [SerializeField] private Text Move2;

    public void SetNr(int nr)
    {
        Nr.text = nr.ToString();
    }

    public void SetMove1(string m)
    {
        Move1.text = m;
    }

    public void SetMove2(string m)
    {
        Move2.text = m;
    }
}
