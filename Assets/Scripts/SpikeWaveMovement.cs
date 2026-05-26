using UnityEngine;

public class SpikeWaveMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.CompareTag("leftSpikeWave"))
        {
            transform.Translate(Vector2.left * 15f * Time.deltaTime);
        }
        else if(gameObject.CompareTag("rightSpikeWave"))
        {
            transform.Translate(Vector2.right * 15f * Time.deltaTime);
        }
    }
}
