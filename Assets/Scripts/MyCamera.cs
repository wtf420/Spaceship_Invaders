using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts
{
    public class MyCamera : MonoBehaviour
    {
        EdgeCollider2D edgeCollider;

        private void Awake()
        {
            //Debug.Log(Screen.width);

            edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
            CreateEdgeCollider();
        }

        void CreateEdgeCollider()
        {
            List<Vector2> edges = new List<Vector2>();
            edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
            edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
            edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
            edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
            edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
            edgeCollider.SetPoints(edges);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Item item = collision.gameObject.GetComponent<Item>();

            if (item != null && item.Type == Variables.ItemType.Coin)
            {
                Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();

                RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(collision.transform.position, collidingRB.velocity);

                Vector2 contactPoint = new Vector2(-14.2f, 0);
                if (hit2Ds.Length > 1)
                    contactPoint = hit2Ds[1].point;

                Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(collision.transform.position)).normalized;

                collidingRB.velocity = Vector2.Reflect(collidingRB.velocity / 2, normal);
            }
        }


        Vector2 GetClosestPoint(Vector2 position)
        {
            Vector2[] points = edgeCollider.points;
            float shortestDistance = Vector2.Distance(position, points[0]);
            Vector2 closestPoint = points[0];
            foreach(var point in points)
            {
                if (Vector2.Distance(point, position) < shortestDistance)
                {
                    shortestDistance = Vector2.Distance(point, position);
                    closestPoint = point;
                }
            }

            return closestPoint;
        }



    }
}
