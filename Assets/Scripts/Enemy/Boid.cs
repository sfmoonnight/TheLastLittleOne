using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    // main fields
    public BoidManager boidManager;
    public Vector2 pos;
    public Vector2 move;
    public float moveSpeed;
    //float shade;
    public List<Boid> friends;
    public GameObject target;

    public bool obstacleInFront;

    Vector2 avoidObjects;

    // timers
    public int thinkTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        move = new Vector2(0, 0);
        pos = transform.position;
        thinkTimer = Random.Range(0, 10);
        friends = new List<Boid>();
        //moveSpeed = Random.Range(1f, boidManager.maxSpeed);
        moveSpeed = boidManager.maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        increment();
        //wrap();

        if (thinkTimer == 0)
        {
            // update our friend array (lots of square roots)
            getFriends();
        }
        flock();
        //pos += move;
        move.Normalize();
        //pos.Normalize();
        //Vector2.ClampMagnitude(pos, BoidManager.maxSpeed);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //print("collide");
        if (other.CompareTag("Obstacle"))
        {
            //boidManager.avoids.Add(other.gameObject);
            obstacleInFront = true;
        }
    }

    /*
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Obstable"))
        {
            obstacleInFront = false;
        }
    }*/

    /*
   Boid(float xx, float yy)
   {
       move = new Vector2(0, 0);
       pos = new Vector2(0, 0);
       pos.x = xx;
       pos.y = yy;
       thinkTimer = Random.Range(0, 10);
       //shade = Random.Range(0, 225);
       friends = new List<Boid>();
   }

   void go()
   {
       increment();
       wrap();

       if (thinkTimer == 0)
       {
           // update our friend array (lots of square roots)
           getFriends();
       }
       flock();
       pos.add(move);
   }
   */

    void flock()
    {
        Vector2 allign = getAverageDir();
        Vector2 avoidDir = getAvoidDir();
        Vector2 avoidObjects = getAvoidAvoids();
        Vector2 noise = new Vector2(Random.Range(0,2) - 1, Random.Range(0, 2) - 1);
        Vector2 cohese = getCohesion();

        allign *= 1;
        if (!boidManager.option_friend)
        {
            allign *= 0;
        }
        

        avoidDir *= 1;
        if (!boidManager.option_crowd)
        {
            avoidDir *= 0;
        }
        
        avoidObjects *= 3;
        if (!boidManager.option_avoid)
        {
            avoidObjects *= 0;
        }

        noise *= 0.1f;
        if (!boidManager.option_noise)
        {
            noise *= 0;
        }
        

        cohese *= 1;
        if (!boidManager.option_cohese)
        {
            cohese *= 0;
        }


        //stroke(0, 255, 160);

        move += allign;
        move += avoidDir;
        move += avoidObjects;
        move += noise;
        move += cohese;

        Vector2 directrion = target.transform.position - transform.position;
        directrion.Normalize();

        move += directrion/10;
        //shade += getAverageColor() * 0.03;
        //shade += (random(2) - 1);
        //shade = (shade + 255) % 255; //max(0, min(255, shade));
    }

    void getFriends()
    {
        List<Boid> nearby = new List<Boid>();
        //print("new friend list");
        foreach (Boid boid in boidManager.boids)
        {
            if (boid != this)
            {
                
            }
            if (Vector2.Distance(boid.transform.position, transform.position) < boidManager.friendRadius)
            {
                //print("Add new friend");
                nearby.Add(boid);
            }
        }
        friends = nearby;
    }

    /*
    float getAverageColor()
    {
        float total = 0;
        float count = 0;
        for (Boid other : friends)
        {
            if (other.shade - shade < -128)
            {
                total += other.shade + 255 - shade;
            }
            else if (other.shade - shade > 128)
            {
                total += other.shade - 255 - shade;
            }
            else
            {
                total += other.shade - shade;
            }
            count++;
        }
        if (count == 0) return 0;
        return total / (float)count;
    }*/

    Vector2 getAverageDir()
    {
        Vector2 sum = new Vector2(0, 0);
        int count = 0;

        if(friends.Count > 0)
        {
            foreach (Boid boid in friends)
            {
                float d = Vector2.Distance(transform.position, boid.transform.position);
                // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself)
                if ((d > 0) && (d < boidManager.friendRadius))
                {
                    Vector2 copy = boid.move;
                    copy.Normalize();
                    copy /= d;
                    sum += copy;
                    count++;
                }
                if (count > 0)
                {
                    //sum /= count;
                }
            }
        }
        
        return sum;
    }

    
    Vector2 getAvoidDir()
    {
        Vector2 steer = new Vector2(0, 0);
        int count = 0;

        foreach (Boid other in friends)
        {
            float d = Vector2.Distance(transform.position, other.transform.position);
            // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself)
            if ((d > 0) && (d < boidManager.crowdRadius))
            {
                // Calculate vector pointing away from neighbor
                Vector2 diff = transform.position - other.transform.position;
                diff.Normalize();
                diff /= d;        // Weight by distance
                steer += diff;
                count++;            // Keep track of how many
            }
        } 
        if (count > 0)
        {
            //steer /= count;
        }
        return steer;
    }

    
    Vector2 getAvoidAvoids()
    {
        Vector2 steer = new Vector2(0, 0);
        int count = 0;

        foreach (GameObject other in boidManager.avoids)
        {
            float d = Vector2.Distance(transform.position, other.transform.position);
            // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself)
            if ((d > 0) && (d < boidManager.avoidRadius))
            {
                // Calculate vector pointing away from neighbor
                Vector2 diff = transform.position - other.transform.position;
                diff.Normalize();
                diff /= d;        // Weight by distance
                steer += diff;
                count++;            // Keep track of how many
            }
        }
        return steer;
    }

    Vector2 getCohesion()
    {
        float neighbordist = 50;
        Vector2 sum = new Vector2(0, 0);   // Start with empty vector to accumulate all locations
        int count = 0;
        foreach (Boid other in friends)
        {
            float d = Vector2.Distance(transform.position, other.transform.position);
            if ((d > 0) && (d < boidManager.coheseRadius))
            {
                sum += (Vector2)other.transform.position; // Add location
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;

            Vector2 desired = sum - (Vector2)transform.position;
            return Vector2.ClampMagnitude(desired, 0.05f);
        }
        else
        {
            return new Vector2(0, 0);
        }
    }

    /*
    void draw()
    {
        for (int i = 0; i < friends.size(); i++)
        {
            Boid f = friends.get(i);
            stroke(90);
            //line(this.pos.x, this.pos.y, f.pos.x, f.pos.y);
        }
        noStroke();
        fill(shade, 90, 200);
        pushMatrix();
        translate(pos.x, pos.y);
        rotate(move.heading());
        beginShape();
        vertex(15 * globalScale, 0);
        vertex(-7 * globalScale, 7 * globalScale);
        vertex(-7 * globalScale, -7 * globalScale);
        endShape(CLOSE);
        popMatrix();
    }
    */

    // update all those timers!
    void increment()
    {
        thinkTimer = (thinkTimer + 1) % 5;
    }

    
    void wrap()
    {
        pos.x = (pos.x + GetComponent<SpriteRenderer>().bounds.size.x) % GetComponent<SpriteRenderer>().bounds.size.x;
        pos.y = (pos.y + GetComponent<SpriteRenderer>().bounds.size.y) % GetComponent<SpriteRenderer>().bounds.size.y;
    }
    
}
