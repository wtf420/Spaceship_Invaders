using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class DictionarySkill
    {
        [SerializeField] public KeyCode key;
        [SerializeField] public Variables.Skill_Type skill;
    }

    public class Player : Entity
    {
        private Camera mainCamera;
        public List<Weapon> weapons;
        int currentWeapon;
        public int CurrentWeapon { get { return currentWeapon; } }

        [SerializeField]
        List<DictionarySkill> playerSkill;

        [SerializeField]
        FuelStateBar fuel;

        [SerializeField]
        Exhaust exhaust;

        static Player instance;
        public static Player Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        public Animator animator;
        private Vector2 Velocity;
        private Vector2 TopRightCorner;
        private Vector2 BottomLeftCorner;

        public PauseMenuScript pauseMenu;
        private bool paused = false;

        public GameObject animationSpriteHolder;
        public GameObject spawnPoint;

        private Vector3 lastFrameMousePosition;
        public float sensitivity = 1.0f;

        public bool invincible = false;
        private bool controlEnable = true;
        private Cheats cheats;

        public float MaximumSpeed;
        public float MinimumSpeed;
        private float SpeedCap;
        private BoxCollider2D Collider;

        private VariableJoystick variableJoystick;

        //float halfHeight;
        //float halfWidth;

        private void Start()
        {
            if (instance != null) Destroy(instance);
            instance = this;

            Body = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
            variableJoystick = GetComponentInChildren<VariableJoystick>();
            cheats = GetComponent<Cheats>();
            lastFrameMousePosition = Body.position;

            TopRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
            BottomLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));

            Body.isKinematic = false; // turn on OncollisionEnter2d
            Body.gravityScale = 0.0f;

            //halfHeight = stageDimensions.y / 2;
            //halfWidth = stageDimensions.x / 2;
            currentWeapon = 0;

            mainCamera = Camera.main;
            SpeedCap = MaximumSpeed;

            HP = Variables.PlayerHPDefault;

            Cursor.lockState = CursorLockMode.Locked;
            animator.SetTrigger("Spawn");
            StartCoroutine(SetInvincible(3.0f));
        }

        // Update is called once per frame
        void Update()
        {
            //SpeedCap = MaximumSpeed;
            if (fuel.Contain <= 1)
            {
                SpeedCap = MinimumSpeed;
            }
            else
                SpeedCap = MaximumSpeed;

            if (IsDeleted)
            {
                if (State != Variables.Player_DESTROYED)
                    StartCoroutine(Destroyed());
            }
            else
            {
                KeyboardController();
                if (!paused && controlEnable)
                {
                    MouseController2();
                }

                SetState();
            }

            UpdateStatusEffect();
        }

        public IEnumerator SetInvincible(float duration)
        {
            invincible = true;
            //SkillManager.Instance.Invincible(Variables.ByPlayer, Body.position, new Vector2(0, 1), gameObject);
            Skill skill = Skill.ActivateByPlayer(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(Variables.Skill_Type.Invincible)]);
            skill.SetParent(this.gameObject);
            yield return new WaitForSeconds(duration);
            invincible = false;
        }

        public void ResetPosition()
        {
            Body.position = spawnPoint.transform.position;
        }

        public void ResetPositionAnimation()
        {
            Body.position = animationSpriteHolder.transform.position;
        }

        public void EnableControl(int state)
        {
            if (state == 0)
            {
                controlEnable = false;
            } else
            {
                controlEnable = true;
            }
        }

        public override void DamageTaken(int dame)
        {
            if (!invincible)
            {
                HP -= dame;
                if (HP <= 0)
                    IsDeleted = true;
            }
        }

        //public override void EffectTaken(float time, Variables.Skill_Effect effect)
        //{
        //    if (invincible)
        //        return;

        //    isEffecting = effect;
        //    Debug.Log("Effect: " + effect);

        //    switch (effect)
        //    {
        //        case Variables.Skill_Effect.None:

        //            break;

        //        case Variables.Skill_Effect.OppositeDirection:

        //            break;
        //    }

        //    Invoke("EndEffect", time);
        //}

        void SetState()
        {
            if (IsDeleted)
                State = Variables.Player_DESTROYED;
            else
                if (Velocity.x == 0 && Velocity.y == 0)
                State = Variables.Player_IDLE;
            else if (Velocity.y > 0)
                State = Variables.Player_BOOST;
            else
                State = Variables.Player_MOVE;

            animator.SetInteger("State", State);
        }

        void MouseController2()
        {
            Vector3 mousePosition = Body.position;
            if (variableJoystick != null)
            {
                mousePosition.x += variableJoystick.Horizontal * sensitivity * SpeedCap * Time.deltaTime * Ratio;
                mousePosition.y += variableJoystick.Vertical * sensitivity * SpeedCap * Time.deltaTime * Ratio;
            } else
            {
                mousePosition.x += Input.GetAxis("Mouse X") * sensitivity * SpeedCap * Time.deltaTime * Ratio;
                mousePosition.y += Input.GetAxis("Mouse Y") * sensitivity * SpeedCap * Time.deltaTime * Ratio;
            }


            // Adding distance to handle Fuel
            fuel.Distance += Vector3.Distance(mousePosition, lastFrameMousePosition);
            if (Vector3.Distance(mousePosition, lastFrameMousePosition) <= 25)
            {
                fuel.Contain += 0.25f * Time.deltaTime;
            }


            Velocity = (mousePosition - lastFrameMousePosition) / Time.deltaTime;

            Body.position = mousePosition;
            // limit it on main Screen
            Body.position = new Vector3(
                Mathf.Clamp(Body.position.x, 
                            BottomLeftCorner.x,
                             TopRightCorner.x),
                Mathf.Clamp(Body.position.y,
                             BottomLeftCorner.y,
                              TopRightCorner.y),
                0f
            );

            lastFrameMousePosition = Body.position;


            if (variableJoystick == null)
            {
                //Debug.Log(lastFrameMousePosition);

                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
                else if (Input.GetMouseButtonDown(1)) // hack level weapon
                {
                    SwitchWeapon();
                    //WeaponStateBar.Instance.Level = Weapon.Level;
                }
            }

            Cursor.visible = false; // invisible cursor
            Cursor.lockState = CursorLockMode.Locked;// block cursor into Game screen
        }

        public void Shoot()
        {
            weapons[currentWeapon].Trigger();
        }

        public void SwitchWeapon()
        {
            currentWeapon++;
            if (currentWeapon == weapons.Count)
                currentWeapon = 0;
        }

        void KeyboardController()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                weapons[currentWeapon].Trigger();

            DictionarySkill skill = null;

            if (Input.GetKeyDown(KeyCode.BackQuote) && cheats != null && !cheats.Activated)
            {
                HUD.Instance.DisplayFloatingText("Cheatmode Enabled", Body.position);
                cheats.Activated = true;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                skill = playerSkill.Find(x => x.key == KeyCode.Q);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                skill = playerSkill.Find(x => x.key == KeyCode.W);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                skill = playerSkill.Find(x => x.key == KeyCode.E);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                skill = playerSkill.Find(x => x.key == KeyCode.R);
            }

            if (skill != null && fuel.Contain >= 2)
            {
                fuel.Contain -= 2;
                ActiveSkill(skill.skill);
            }

            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                PauseOrResumeGame();
            }
        }

        public void PauseOrResumeGame()
        {
            PauseMenuScript p = GameObject.FindObjectOfType<PauseMenuScript>();
            p.PauseOrResumeGame();
        }

        void ActiveSkill(Variables.Skill_Type skill)
        {
            Vector2 position = Body.position;
            switch (skill)
            {
                case Variables.Skill_Type.CircleShooting:
                    //SkillManager.Instance.CircleShoot(Variables.ByPlayer, 10, position, new Vector2(0, -1));
                    BeautifulShapeSkill.ActivateByPlayer(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(skill)]);
                    break;

                case Variables.Skill_Type.DivineDeparture:
                    //SkillManager.Instance.DivineDeparture(Variables.ByPlayer, position, new Vector2(0, 1));
                    Skill.ActivateByPlayer(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(skill)]);
                    break;
                    
                case Variables.Skill_Type.EnergyWave:
                    //SkillManager.Instance.EnergyWave(Variables.ByPlayer, position, new Vector2(0, 1));
                    Skill.ActivateByPlayer(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(skill)]);
                    break;

                case Variables.Skill_Type.Invincible:
                    if(!invincible)
                        StartCoroutine(SetInvincible(3.0f));
                    break;

                case Variables.Skill_Type.ElectricShooting:
                    //SkillManager.Instance.ElectricShooting(Variables.ByPlayer, position, new Vector2(0, 1));
                    Skill.ActivateByPlayer(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(skill)]);

                    break;

                case Variables.Skill_Type.SectorShooting:
                    //SkillManager.Instance.SectorShooting(Variables.ByPlayer, 5, position, new Vector2(0, 1));
                    Skill.ActivateByPlayer(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(skill)]);
                    break;
            }
        }

        public void GamePaused()
        {
            paused = true;
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void GameResume()
        {
            //Since movement is detected by mouse position difference, reset last mouse position here so
            //there wont be a difference, then the player position wont jump.
            lastFrameMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Time.timeScale = 1.0f;

            paused = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsDeleted)
                return;

            GameObject Object = collision.gameObject;

            if (Object.tag == "Item")
            {
                Item item = collision.gameObject.GetComponent<Item>();
                CollectItem(item.Type);

                Destroy(Object);
            }
            else if (Object.tag == "Enemy")
            {
                DamageTaken(9999);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsDeleted)
                return;

            GameObject Object = collision.gameObject;


            if (Object.tag == "Enemy")
                DamageTaken(9999);

        }

        private void CollectItem(Variables.ItemType ItemType)
        {
            switch (ItemType)
            {
                case Variables.ItemType.Star:
                    WeaponLevelUp(1);

                    HUD.Instance.Score += 2000;
                    HUD.Instance.DisplayFloatingText("2000", Body.position);
                    break;

                case Variables.ItemType.Fuel:
                    fuel.Contain++;
                    HUD.Instance.DisplayFloatingText("Fuel Refilled", Body.position);
                    break;

                case Variables.ItemType.Coin:
                    HUD.Instance.Coin++;
                    HUD.Instance.DisplayFloatingText("Coin +1", Body.position);
                    break;
            }

            Debug.Log(ItemType);
        }

        public void SetSmokeState(string state)
        {
            exhaust.SetSmokeState(state);
        }

        public void WeaponLevelUp(int amount)
        {
            Weapon.Level += amount;
            Weapon.Level = Mathf.Clamp(Weapon.Level, 1, 3);
            HUD.Instance.DisplayFloatingText("Weapon Level Up", Body.position);
        }

        public IEnumerator Destroyed()
        {
            AudioManager.Instance.PlayPlayerExplosion();

            Instantiate(Explosion, transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            SetState();
            Body.isKinematic = true; // turn off OncollisionEnter2d
            Debug.Log("Player Destroyed");
            HUD.Instance.Life--;
            Weapon.Level--;
            if (Weapon.Level < 1)
                Weapon.Level = 1;

            yield return new WaitForSeconds(0f);
            if (!IsGameOver())
            {
                Body.isKinematic = false; // turn on OncollisionEnter2d
                IsDeleted = false;
                SetState();

                HP = Variables.PlayerHPDefault;
                fuel.RenderNewState();
                animator.SetTrigger("Spawn");
                StartCoroutine(SetInvincible(3.0f));
            }
        }

        public bool IsGameOver()
        {
            if (HUD.Instance.Life <= 0)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.NavigateTo(pauseMenu.FindCanvasByName("GameOverScene"));

                return true;
            }

            return false;
        }

    }
}