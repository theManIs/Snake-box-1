﻿using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Snake_box
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public sealed class Data : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _mainBuildDataPath;
        [SerializeField] private string _shakeDataPath;
        [SerializeField] private string _characterDataPath;
        [SerializeField] private string _enemySpawnDataPath;
        [SerializeField] private string _levelSpawnDataPath;
        [SerializeField] private string _simpleEnemyDataPath;
        [SerializeField] private string _slowEnemyDataPath;
        [SerializeField] private string _fastEnemyDataPath;
        [SerializeField] private string _flyingEnemyDataPath;
        [SerializeField] private string _acceleratingEnemyDataPath;
        [SerializeField] private string _invisibleEnemyDataPath;
        [SerializeField] private string _spawnedEnemyDataPath;
        [SerializeField] private string _spawningEnemyDataPath;
        [SerializeField] private string _spikedEnemyDataPath;
        [SerializeField] private string _LevelDataPath;
        [SerializeField] private string _turretDataPath;
        [SerializeField] private string _borderDataPath;
        [SerializeField] private string _blockSnakeDataPath;
        [SerializeField] private string _bonusCoinsDataPath;
        [SerializeField] private string _bonusHpSnakeDataPath;
        [SerializeField] private string _bonusSpeedDataPath;
        [SerializeField] private string _bonusFireDataPath;
        [SerializeField] private string _shellDataPath;
        [SerializeField] private string _bordersDataPath;
        [SerializeField] private string _allSpawnListsDataPath;
        [SerializeField] private string _spriteDictionaryDataPath;
        private static MainBuildData _mainBuildData;
        private static BonusCoinsData _bonusCoinsData;
        private static BonusFireData _bonusFireData;
        private static BonusHpSnakeData _bonusHpSnakeData;
        private static BonusSpeedData _bonusSpeedData;
        [SerializeField] private string _levelPrefabsDataPath;
        private static ShakesData _shake;
        private static CharacterData _characterData;
        private static SimpleEnemyData _simpleEnemyData;
        private static SlowEnemyData _slowEnemyData;
        private static FastEnemyData _fastEnemyData;
        private static FlyingEnemyData _flyingEnemyData;
        private static AcceleratingEnemyData _acceleratingEnemyData;
        private static InvisibleEnemyData _invisibleEnemyData;
        private static SpawnedEnemyData _spawnedEnemyData;
        private static SpawningEnemyData _spawningEnemyData;
        private static SpikedEnemyData _spikedEnemyData;
        private static LevelData _levelData;
        private static BlockSnakeData _blockSnake;
        private static TurretData _turretData;
        private static BordersData _bordersData;
        private static ShellData _shellData;
        private static SpriteDictionary _spriteDictonary;
        private static readonly Lazy<Data> _instance = new Lazy<Data>(() => Load<Data>("Data/" + typeof(Data).Name));
        
        #endregion
        

        #region Properties

        public static Data Instance => _instance.Value;

        public MainBuildData MainBuildData
        {
            get
            {
                if (_mainBuildData == null)
                {
                    _mainBuildData = Load<MainBuildData>("Data/" + Instance._mainBuildDataPath);
                }

                return _mainBuildData;
            }
        } 

        public BonusFireData BonusFireData
        {

            get
            {
                if (_bonusFireData == null)
                {
                    _bonusFireData = Load<BonusFireData>("Data/" + Instance._bonusFireDataPath);
                }

                return _bonusFireData;
            }
        } 

        public BonusCoinsData BonusCoinsData
        {
            get
            {
                if (_bonusCoinsData == null)
                {
                    _bonusCoinsData = Load<BonusCoinsData>("Data/" + Instance._bonusCoinsDataPath);
                }

                return _bonusCoinsData;
            }
        }

        public BonusHpSnakeData BonusHpSnakeData
        {
            get
            {
                if (_bonusHpSnakeData == null)
                {
                    _bonusHpSnakeData = Load<BonusHpSnakeData>("Data/" + Instance._bonusHpSnakeDataPath);
                }

                return _bonusHpSnakeData;
            }
        }

        public BonusSpeedData BonusSpeedData
        {
            get
            {
                if (_bonusSpeedData == null)
                {
                    _bonusSpeedData = Load<BonusSpeedData>("Data/" + Instance._bonusSpeedDataPath);
                }

                return _bonusSpeedData;
            }
        }

        public BlockSnakeData BlockSnake
        {
            get
            {
                if (_blockSnake == null)
                {
                    _blockSnake = Load<BlockSnakeData>("Data/" + Instance._blockSnakeDataPath);
                }

                return _blockSnake;
            }
        }

        public ShakesData Shakes
        {
            get
            {
                if (_shake == null)
                {
                    _shake = Load<ShakesData>("Data/" + Instance._shakeDataPath);
                }

                return _shake;
            }
        }

        public CharacterData Character
        {
            get
            {
                if (_characterData == null)
                {
                    _characterData = Load<CharacterData>("Data/" + Instance._characterDataPath);
                }

                return _characterData;
            }
        }

        public SimpleEnemyData SimpleEnemy
        {
            get
            {
                if (_simpleEnemyData == null)
                {
                    _simpleEnemyData = Load<SimpleEnemyData>("Data/" + Instance._simpleEnemyDataPath);
                }

                return _simpleEnemyData;
            }
        }
        
        public SlowEnemyData SlowEnemy
        {
            get
            {
                if (_slowEnemyData == null)
                {
                    _slowEnemyData = Load<SlowEnemyData>("Data/" + Instance._slowEnemyDataPath);
                }

                return _slowEnemyData;
            }
        }
        
        public FastEnemyData FastEnemy
        {
            get
            {
                if (_fastEnemyData == null)
                {
                    _fastEnemyData = Load<FastEnemyData>("Data/" + Instance._fastEnemyDataPath);
                }

                return _fastEnemyData;
            }
        }

        public FlyingEnemyData FlyingEnemy
        {
            get
            {
                if (_flyingEnemyData == null)
                {
                    _flyingEnemyData = Load<FlyingEnemyData>("Data/" + Instance._flyingEnemyDataPath);
                }

                return _flyingEnemyData;
            }
        }
        
        public AcceleratingEnemyData AcceleratingEnemy
        {
            get
            {
                if (_acceleratingEnemyData == null)
                {
                    _acceleratingEnemyData = Load<AcceleratingEnemyData>("Data/" + Instance._acceleratingEnemyDataPath);
                }

                return _acceleratingEnemyData;
            }
        }
        
        public InvisibleEnemyData InvisibleEnemy
        {
            get
            {
                if (_invisibleEnemyData == null)
                {
                    _invisibleEnemyData = Load<InvisibleEnemyData>("Data/" + Instance._invisibleEnemyDataPath);
                }

                return _invisibleEnemyData;
            }
        }
        
        public SpawnedEnemyData SpawnedEnemy
        {
            get
            {
                if (_spawnedEnemyData == null)
                {
                    _spawnedEnemyData = Load<SpawnedEnemyData>("Data/" + Instance._spawnedEnemyDataPath);
                }

                return _spawnedEnemyData;
            }
        }
        
        public SpawningEnemyData SpawningEnemy
        {
            get
            {
                if (_spawningEnemyData == null)
                {
                    _spawningEnemyData = Load<SpawningEnemyData>("Data/" + Instance._spawningEnemyDataPath);
                }

                return _spawningEnemyData;
            }
        }
        
        public SpikedEnemyData SpikedEnemy
        {
            get
            {
                if (_spikedEnemyData == null)
                {
                    _spikedEnemyData = Load<SpikedEnemyData>("Data/" + Instance._spikedEnemyDataPath);
                }

                return _spikedEnemyData;
            }
        }

        public LevelData LevelData
        {
            get
            {
                if (_levelData == null)
                {
                    _levelData = Load<LevelData>("Data/" + Instance._LevelDataPath);
                }

                return _levelData;
            }
        }

        public TurretData TurretData
        {
            get
            {
                if (_turretData == null)
                {
                    _turretData = Load<TurretData>("Data/" + Instance._turretDataPath);
                }

                return _turretData;
            }
        }

        public BordersData BordersData
        {
            get
            { 
                if(_bordersData == null)
                    _bordersData = Load<BordersData>("Data/" + Instance._bordersDataPath);
                return _bordersData;
            }
        }

        public ShellData ShellData
        {
            get
            {
                if (_shellData == null)
                {
                    _shellData = Load<ShellData>("Data/" + Instance._shellDataPath);
                }

                return _shellData;
            }
        }

        public SpriteDictionary SpriteDictonary
        {
            get
            {
                if(_spriteDictonary == null)
                    _spriteDictonary = Load<SpriteDictionary>("Data/" + Instance._spriteDictionaryDataPath);
                return _spriteDictonary;
            }
        }

        #endregion


        #region Methods

        private static T Load<T>(string resourcesPath) where T : Object =>
            CustomResources.Load<T>(Path.ChangeExtension(resourcesPath, null));
    
        #endregion
    }
}
