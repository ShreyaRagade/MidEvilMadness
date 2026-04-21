using System;
using UnityEngine;


    public class CurrencyController : MonoBehaviour
    {
        public static CurrencyController CurrencyInstance;

        [SerializeField] private int startingGold = 100; //starting gold for new game
        private int playerGold;
        public event Action<int> OnGoldChanged;


        private void Awake()
        {
            if (CurrencyInstance != null && CurrencyInstance != this)
            {
                Destroy(gameObject);
                return;
            }

            CurrencyInstance = this;
            DontDestroyOnLoad(gameObject);
            playerGold = startingGold;

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                playerGold += 100;
                Debug.Log(GetGold());
            }

            //  Debug.Log(GetGold());
        }

        private void Start()
        {
            Debug.Log("gold: " + playerGold);


        }

        public int GetGold() => playerGold;

        public bool SpendGold(int amount)
        {
            if (playerGold >= amount)
            {
                playerGold -= amount;
                OnGoldChanged?.Invoke(playerGold);
                return true; //player gold was more than the amount
            }

            return false;
        }

        public void AddGold(int amount)
        {
            playerGold += amount;
            OnGoldChanged?.Invoke(playerGold);

        }

        public void SetGold(int amount)
        {
            playerGold = amount;
            OnGoldChanged?.Invoke(playerGold);
        }

    }


