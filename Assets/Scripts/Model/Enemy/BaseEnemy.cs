using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;


namespace Snake_box
{
    public abstract class BaseEnemy : IEnemy, IDamageAddressee
    {
        #region PrivateData

        protected ArmorType _armor;
        protected NavMeshAgent _navMeshAgent;
        protected GameObject _prefab;
        protected GameObject _enemyObject;
        protected Transform _transform;
        protected Transform _target;
        protected LevelService _levelService = Services.Instance.LevelService;
        protected float _hp;
        protected float _speed;
        protected float _damage;
        protected float _meleeHitRange;
        protected float _hitCooldown;
        protected float _currentHitCooldown = 0;
        protected bool _isNeedNavMeshUpdate = false;
        protected bool _isValidTarget;
        protected int _killReward;
        private TimeRemaining _stoping;

        #endregion


        #region ClassLifeCycle

        protected BaseEnemy(BaseEnemyData data)
        {
            _prefab = data.Prefab;
            _speed = data.Speed;
            _hp = data.Hp;
            _damage = data.Damage;
            _armor = data.ArmorType;
            _meleeHitRange = data.MeleeHitRange;
            _killReward = data.KillReward;
            _hitCooldown = data.HitCooldown;
            _stoping = new TimeRemaining(StopDancing, 1f);
        }

        #endregion
        

        #region Properties

        public EnemyType Type { get; protected set; }

        #endregion


        #region IEnemy

        public virtual void Spawn(Vector3 position)
        {
            if (_levelService.Target == null)
            {
               _levelService.FindGameObject(); 
            }
            _target = _levelService.Target.transform;
            _enemyObject = GameObject.Instantiate(_prefab, position, Quaternion.identity);
            _navMeshAgent = _enemyObject.GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _speed;
            _transform = _enemyObject.transform;
            _isNeedNavMeshUpdate = true;
            _isValidTarget = true;
            if (!_levelService.ActiveEnemies.Contains(this))
                _levelService.ActiveEnemies.Add(this);
        }

        public virtual void OnUpdate()

        {
            if (_isNeedNavMeshUpdate)
            {
                if (_target != null && _navMeshAgent.isOnNavMesh)
                    _navMeshAgent.SetDestination(_target.transform.position);
                _isNeedNavMeshUpdate = false;
            }
            DecreaseCurrentHitCooldown();
            HitCheck();
        }

        public Transform GetTransform() => _transform;

        public bool AmIDestroyed()
        {
            return _enemyObject == null;
        }

        public Vector3 GetPosition() => _transform.position;
        public EnemyType GetEnemyType() => Type;
        public bool IsValidTarget() => _isValidTarget;

        #endregion


        #region Methods

        protected virtual void HitCheck()
        {
            Collider[] colliders = new Collider[10];
            Physics.OverlapSphereNonAlloc(_transform.position, _meleeHitRange, colliders);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null)
                {
                    if (colliders[i].CompareTag(TagManager.GetTag(TagType.Target)))
                    {
                        var mainBuilding = Services.Instance.LevelService.MainBuilds;
                        mainBuilding.GetDamage(_damage);
                        if (_levelService.ActiveEnemies.Contains(this))
                            _levelService.ActiveEnemies.Remove(this);
                        Object.Destroy(_enemyObject);
                        if (_levelService.ActiveEnemies.Count == 0 && Services.Instance.LevelService.IsLevelSpawnEnded)
                        {
                            _levelService.EndLevel();
                        }
                    }
                    else if (colliders[i].CompareTag(TagManager.GetTag(TagType.Player)))
                    {
                        if (_currentHitCooldown == 0)
                        {
                            Services.Instance.LevelService.CharacterBehaviour.SetArmor(_damage);
                            _currentHitCooldown = _hitCooldown;
                        }
                        Services.Instance.LevelService.CharacterBehaviour.RamEnemy(this);
                        _stoping.AddTimeRemaining();
                       
                       
                    }
                    else if (colliders[i].CompareTag(TagManager.GetTag(TagType.Block)))
                    {
                        if (_currentHitCooldown == 0)
                        {
                            Services.Instance.LevelService.CharacterBehaviour.SetDamage(_damage);
                            _currentHitCooldown = _hitCooldown;
                        }
                        _stoping.AddTimeRemaining();
                    }
                }  
            }
        }

        protected virtual void GetTarget()
        {    
            _target = GameObject.FindWithTag(TagManager.GetTag(TagType.Target)).transform;
        }

        protected virtual void GetDamage(float damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            if (_levelService.ActiveEnemies.Contains(this))
                _levelService.ActiveEnemies.Remove(this);
            Object.Destroy(_enemyObject);
            Wallet.PutLocalCoins(_killReward);
            Services.Instance.FlyingIconsService.CreateFlyingMoney(_enemyObject.transform.position);
            if (_levelService.ActiveEnemies.Count == 0 && Services.Instance.LevelService.IsLevelSpawnEnded)
            {
                _levelService.EndLevel();
            }
        }

        private void StopDancing()
        {
            if (_enemyObject != null)
            {
                _enemyObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        private void DecreaseCurrentHitCooldown()
        {
            _currentHitCooldown -= Services.Instance.TimeService.DeltaTime();
            if (_currentHitCooldown < 0)
                _currentHitCooldown = 0;
        }

        #endregion


        #region IDamageAdressee

        public void RegisterDamage(float damageAmount, ArmorTypes damageType)
        {
            GetDamage(damageAmount);
        }

        #endregion
    }  
}

