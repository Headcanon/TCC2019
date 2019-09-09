using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Anglo {
    public GameObject go;
    float[] sex;

    public Anglo (GameObject theGameObject) {
        go = theGameObject;
        sex = new float[7];
        for (int i = 0; i < 7; i++) {
            sex[i] = 0f;
        }
    }

    public float Shift (float next) {
        for (int i = 0; i < 6; i++) {
            sex[i] = sex[i + 1];
        }
        if (next > 180) {
            sex[6] = next-360f;
        } else {
            sex[6] = next;
        }
        return Mathf.Lerp(sex[0], sex[1], 0.5f);
    }
}

public class Vine : MonoBehaviour {

    public Sprite[] sprites;
    public Sprite end;
    public int height = 4;
    [System.NonSerialized]
    public float taut = 0, ang;
    [System.NonSerialized]
    public bool boarded = false;
    private float boardedAng;

    private List<Anglo> anglo = new List<Anglo>();

	// Use this for initialization
	void OnValidate () {
        if (sprites.Length > 0 && sprites[0] != null)
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = end;
    }
	
	// Update is called once per frame
	void Start () {
        GameObject next, last = transform.GetChild(0).gameObject;
        anglo.Add(new Anglo(last));
		for (int i = 1; i < height*4; i++) {
            next = Instantiate(last, last.transform);
            next.transform.localPosition = new Vector3(0f, -0.25f, 0f);
            next.name = gameObject.name + i.ToString();
            next.GetComponent<SpriteRenderer>().sprite = sprites[i % sprites.Length];
            last = next;
            anglo.Add(new Anglo(last));
        }
        transform.GetChild(1).position = new Vector3(transform.position.x, transform.position.y-height, transform.position.z+0.1f);
        transform.GetChild(1).parent = last.transform;

    }

    private void FixedUpdate() {

        Transform next, last = transform.GetChild(0);
        for (int i = 1; i < height * 4; i++) {
            next = anglo[i].go.transform;
            //ang += last.localEulerAngles.z;
            //next.localEulerAngles = new Vector3(0f, 0f, ang-Mathf.Lerp(next.eulerAngles.z, ang, 0.5f));
            if (i >= taut*4) {
                next.eulerAngles = new Vector3(0f, 0f, anglo[i].Shift(last.eulerAngles.z));
            } else {
                anglo[i].Shift(ang);
                next.eulerAngles = new Vector3(0f, 0f, ang);
            }
            last = next;
        }

        if (!boarded) {
            taut = Mathf.Max(0f, taut - 5f * Time.deltaTime);
            ang = Mathf.Lerp(Mathf.Sin(Time.time * 2f) * 5f, boardedAng, taut*0.5f);
        } else {
            boardedAng = ang;
        }
        transform.GetChild(0).localEulerAngles = new Vector3(0, 0, ang);
        anglo[0].Shift(ang);
    }

    private void OnDrawGizmos() {
        if (!The.root.debugDraw) return;
        Gizmos.color = new Color(The.root.debugObstacles.r, The.root.debugObstacles.g, The.root.debugObstacles.b, 0.3f);
        Gizmos.DrawSphere(transform.position, 0.25f);
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - height / 2f, transform.position.z + 0.12f), new Vector3(0.2f, height - 0.2f, 0.1f));
    }
}