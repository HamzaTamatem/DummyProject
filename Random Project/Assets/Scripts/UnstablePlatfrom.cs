using System.Collections;
using UnityEngine;

public class UnstablePlatfrom : MonoBehaviour
{
    enum Anim { Idle, Fall, Return }
    Anim currentAnim;

    Animator anim => GetComponent<Animator>();

    SpriteRenderer sr => GetComponent<SpriteRenderer>();
    Collider2D myCol => sr.GetComponent<Collider2D>();

    [SerializeField] float waitTimeToFall;
    [SerializeField] float waitTimeToReturn;

    bool canDetect;

    [SerializeField] float startStayTime;
    float stayTime;

    //private void Update()
    //{
    //    if (!canDetect)
    //        return;

    //    if (stayTime > 0)
    //    {
    //        stayTime -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        StartCoroutine(ActiveDeactive_Platform());
    //        canDetect = false;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //stayTime = startStayTime;
            //canDetect = true;

            StartCoroutine(ActiveDeactive_Platform());
        }
    }

    //private void OnCollisionExit2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        canDetect = false;
    //        stayTime = 0;
    //    }
    //}

    private IEnumerator ActiveDeactive_Platform()
    {
        yield return new WaitForSeconds(waitTimeToFall);

        //sr.enabled = false;
        //myCol.enabled = false;

        ChangeAnim(Anim.Fall);

        yield return new WaitForSeconds(waitTimeToReturn);

        //sr.enabled = true;
        //myCol.enabled = true;

        ChangeAnim(Anim.Return);
    }

    //Call from the animation
    //public void BackToNormal()
    //{
    //    sr.enabled = true;
    //    myCol.enabled = true;
    //}

    private void ChangeAnim(Anim newAnim)
    {
        if (currentAnim == newAnim)
            return;

        anim.Play(newAnim.ToString());

        currentAnim = newAnim;
    }
}
