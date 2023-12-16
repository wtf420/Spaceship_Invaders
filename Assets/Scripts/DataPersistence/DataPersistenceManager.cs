using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.DataPersistence
{
    public class DataPersistenceManager: MonoBehaviour
    {
        private static DataPersistenceManager instance;
        public static DataPersistenceManager Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        [Header("File Storage Config")]

        [SerializeField] private string fileName = "data.Game";

        private FileDataHandler dataHandler;

        public GameData gameData;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(instance);

            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

            LoadData();
        }

        private void Start()
        {
        }

        public void NewData()
        {
            this.gameData = new GameData();
        }

        public void LoadData()
        {
            dataHandler.Load(ref gameData);

            if (this.gameData == null || (gameData != null && gameData.Life <= 0))
            {
                NewData();
            }
        }

        public void SaveData()
        {
            dataHandler.Save(gameData);
        }

        private void OnApplicationQuit()
        {
            Debug.Log("Save data");
            if(SceneManager.GetActiveScene().buildIndex != 0)
                SaveData();
        }

    }
}
