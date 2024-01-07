using Assets.Scripts.DataPersistence;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class SkillEffect
    {
        [SerializeField] public StatusEffectTypes effect;
        [SerializeField] public float duration;
    }


    public class Skill : Entity
    {

        public int Type { get; set; }

        public int Damage;

        public Vector2 Direction { get; set; } = new Vector2(0, -1);

        public SkillEffect Effect; // Stun, Slow, Ignite, ...

        public bool isMovable = true;

        [SerializeField]
        bool isUnstoppable = false;
        
        public float Duration;
        float TotalTime = 0;

        //public GameObject Explosion;

        private void Start()
        {
            Body = GetComponent<Rigidbody2D>();
            ID = Variables.SKILL;

        }

        public virtual void Init(int type, int damage, Vector2 direction)
        {
            Type = type;
            Damage = damage;

            Direction = direction;
            float angle = Vector2.Angle(direction, new Vector2(1, 0));
            if (direction.y < 0)
                transform.rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
            else
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //Debug.Log("Angle: " + angle);
        }

        public virtual void Init(int type, int damage, Vector2 direction, float duration)
        {
            Init(type, damage, direction);
            Duration = duration;
        }

        public virtual void Init(int type, int damage, Vector2 direction, float duration, SkillEffect effect)
        {
            Init(type, damage, direction, duration);
            Effect = effect;
        }

        public void SetParent(GameObject parent)
        {
            gameObject.transform.parent = parent.transform;
        }

        private void Update()
        {
            if(isMovable)
            {
                Vector2 Position = Body.position;
                switch (Type)
                {
                    case Variables.ByPlayer:
                        Position += Variables.PlayerBulletSpeed * Direction * Time.deltaTime;
                        //Debug.Log("Player shooting");
                        break;

                    default:
                        Position += Variables.EnemyBulletSpeed * Direction * Time.deltaTime;
                        break;
                }

                Body.position = Position;
            }

            TotalTime += Time.deltaTime;
            if (TotalTime >= Duration)
                HandleDestroy();
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Entity entity = collision.GetComponent<Entity>();

            if (entity != null && entity.IsDeleted == false)
            {
                switch (entity.ID)
                {
                    case Variables.ASTEROID:
                    case Variables.ENEMY:
                        if (this.Type == Variables.ByPlayer)
                        {
                            entity.DamageTaken(Damage); 
                            StatusEffectManager.InflictStatusEffect(entity, Effect.effect, Effect.duration, Damage);

                            if (entity is Boss)
                                HUD.Instance.Score += Damage;


                            if (!isUnstoppable)
                                HandleDestroy();
                        }
                        break;

                    case Variables.PLAYER:
                        if (this.Type == Variables.ByEnemy)
                        {
                            entity.DamageTaken(Damage);

                            StatusEffectManager.InflictStatusEffect(entity, Effect.effect, Effect.duration, Damage);
                            //entity.EffectTaken(Effect.duration, Effect.effect);
                            
                            if (!isUnstoppable)
                                HandleDestroy();
                        }
                        break;
                }
            }

            if (this.name.Contains("EnergyWave"))
            {
                Bullet bullet = collision.GetComponent<Bullet>();
                if (bullet && bullet.Type != this.Type)
                    Destroy(collision.gameObject);
            }
        }

        public static void ActivateByPlayer(Player player, Skill skill) { 
            Skill Instantiate_Skill = Instantiate(skill, player.GetPosition(), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            Instantiate_Skill.Init(Variables.ByPlayer, skill.Damage, new Vector2(0, 1), skill.Duration, skill.Effect);
        }

        public static void ActivateByEntity(Entity entity, Skill skill)
        {
            Skill Instantiate_Skill = Instantiate(skill, entity.GetPosition(), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            Instantiate_Skill.Init(Variables.ByEnemy, skill.Damage, new Vector2(0, -1), skill.Duration, skill.Effect);
        }

        public virtual void HandleDestroy()
        {
            if(Explosion != null)
                Instantiate(Explosion, transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

            Destroy(gameObject);
        }

    }
}
