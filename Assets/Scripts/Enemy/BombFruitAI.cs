using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFruitAI : EnemyAI
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Activate()
    {
        base.Activate();
        self.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void StartAttack()
    {
        print("BombAttack");
        GetComponent<Enemy>().DestroyEnemy();
    }
}
