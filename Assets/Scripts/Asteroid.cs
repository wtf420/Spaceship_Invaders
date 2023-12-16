using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Asteroid: Entity
    {
        public Asteroid Medium;
        public Asteroid Small;

        public Level level;

        public Path path;

        public enum Level{
            Large,
            Medium,
            Small
        }

        private void Start()
        {
            Body = GetComponent<Rigidbody2D>();
            
            ID = Variables.ASTEROID;
            
            //init HP
            switch (level)
            {
                case Level.Large:
                    HP = 200;
                    break;

                case Level.Medium:
                    HP = 100;
                    break;

                default:
                    HP = 50;
                    break;
            }
        }

        private void Update()
        {
            if(HP <= 0)
            {
                IsDeleted = true;
            }

            Movement();
        }

        void Movement()
        {
            if (Mathf.Pow(Vector3.Distance(Body.position, path.GetNodePosition(0)), 2) >
                Mathf.Pow(Vector3.Distance(path.GetNodePosition(1), path.GetNodePosition(0)), 2)
                + Mathf.Pow(Vector3.Distance(Body.position, path.GetNodePosition(1)), 2))
                IsDeleted = true;
            
            Vector2 direction = path.GetNodePosition(1) - path.GetNodePosition(0);

            Body.position += direction * Time.deltaTime * Variables.AsteroidSpeed / 5;

        }

        public void MyDestroy(List<Entity> EnemiesOrAsteroid)
        {
            Vector3 pos = Body.position;

            
            switch (level)
            {
                case Level.Large:

                    float radius = Vector3.Distance(pos, this.path.GetNodePosition(1));
                    Vector2 position = new Vector2(pos.x + 1, pos.y);
                    Asteroid asteroid = Instantiate(Medium, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    Path newPath = Instantiate(path, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    newPath.Clear();

                    newPath.AddNode(pos);

                    var vector2 = Random.insideUnitCircle.normalized * radius;
                    newPath.AddNode(new Vector3(vector2.x, vector2.y, 0));

                    asteroid.path = newPath;
                    asteroid.level = Level.Medium;
                    asteroid.Medium = Medium;
                    asteroid.Small = Small;

                    EnemiesOrAsteroid.Add(asteroid);

                    asteroid = Instantiate(Medium, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    newPath = Instantiate(path, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    newPath.Clear();

                    newPath.AddNode(pos);

                    newPath.AddNode(new Vector3(vector2.x, -vector2.y, 0));

                    asteroid.path = newPath;
                    asteroid.level = Level.Medium;
                    asteroid.Medium = Medium;
                    asteroid.Small = Small;

                    EnemiesOrAsteroid.Add(asteroid);

                    break;

                case Level.Medium:
                    radius = Vector3.Distance(pos, this.path.GetNodePosition(1));
                    position = new Vector2(pos.x + 1, pos.y);
                    asteroid = Instantiate(Small, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    newPath = Instantiate(path, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    newPath.Clear();

                    newPath.AddNode(pos);

                    vector2 = Random.insideUnitCircle.normalized * radius;
                    newPath.AddNode(new Vector3(vector2.x, vector2.y, 0));

                    asteroid.path = newPath;
                    asteroid.level = Level.Small;
                    asteroid.Medium = Medium;
                    asteroid.Small = Small;

                    EnemiesOrAsteroid.Add(asteroid);

                    asteroid = Instantiate(Small, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    newPath = Instantiate(path, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    newPath.Clear();

                    newPath.AddNode(pos);

                    newPath.AddNode(new Vector3(vector2.x, -vector2.y, 0));

                    asteroid.path = newPath;
                    asteroid.level = Level.Small;
                    asteroid.Medium = Medium;
                    asteroid.Small = Small;

                    EnemiesOrAsteroid.Add(asteroid);

                    break;
            }


            //Destroy(gameObject);
            //Destroy(path.gameObject);

        }

    }
}
