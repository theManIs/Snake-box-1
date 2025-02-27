﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Snake_box
{
    public abstract class TurretProjectileAbs : IExecute
    {
        #region Fields

        private IEnemy _targetToPursue;
        private float _timeStart;
        private GameObject _projectileInstance;
        private Transform _firePoint;
        private float _timeToBeDestructedAfter = 10;
        protected bool ToDispose = false;
        public int ObjectId;
        private float _journeyDistance;
        private bool _targetLocked = false;
        private ProjectilePreferences _projectilePreferences;

        protected float ProjectileDamageMod;
        protected float AbilityLevel;

        #endregion


        #region Properties

        public Vector3 AngleLock => _projectilePreferences.AngleLock;
        public float CarryingDamage => _projectilePreferences.ProjectileDamage * ProjectileDamageMod;
        public int BulletSpeed => _projectilePreferences.ProjectileSpeed;

        public ArmorTypes ArmorPiecing => _projectilePreferences.ArmorPiercing;
        public List<IEnemy> ActiveEnemies => Services.Instance.LevelService.ActiveEnemies;

        #endregion


        #region IExecute

        public abstract void Execute(); 

        #endregion


        #region Methods

        public void SetProjectileDamageMod(float newProjectileDamageMod) => ProjectileDamageMod = newProjectileDamageMod;

        public void SetAbilityLevel(int newAbilityLevel) => AbilityLevel = newAbilityLevel;


        public void SetProjectilePreferences(ProjectilePreferences projectilePreferences) => _projectilePreferences = projectilePreferences;

        public void SetTarget(IEnemy enemyTransform)
        {
            _targetToPursue = enemyTransform;
            _targetLocked = true;
        }

        public void SetLookRotation(Transform lookAt)
        {
            if (_projectileInstance)
            {
//                _projectileInstance.transform.LookAt(lookAt);
//
//                Vector3 eulerAngles = _projectileInstance.transform.rotation.eulerAngles;
//                eulerAngles.x = _angleLock.x;
//                _projectileInstance.transform.rotation = Quaternion.Euler(eulerAngles);

                Vector3 direction3d = lookAt.position - _projectileInstance.transform.position;
                Vector3 eulerAngles = Quaternion.LookRotation(direction3d).eulerAngles;
                eulerAngles.x = AngleLock.x;
                _projectileInstance.transform.rotation = Quaternion.Euler(eulerAngles);
            }
        }

        public void CountDistance()
        {
            if (_firePoint && _targetToPursue != null)
                _journeyDistance = Vector3.Distance(_firePoint.position, _targetToPursue.GetTransform().position);
        }

        public void SetFirePoint(Transform firePoint)
        {
            _firePoint = firePoint;

            _projectileInstance.transform.position = _firePoint.position;
//            _projectileInstance.transform.rotation = _firePoint.rotation;
        }

        public void SetGameObject(GameObject gm)
        {
            ObjectId = gm.GetInstanceID();
            
            _projectileInstance = gm;
        }

        public void SetSelfDestruct(float timeToBeDestructedAfter)
        {
            _timeStart = Time.time;
            _timeToBeDestructedAfter = timeToBeDestructedAfter;
        }

        private void DecommissionIfTargetDown()
        {
            if (_targetLocked && (_targetToPursue == null || _targetToPursue.AmIDestroyed()))
            {
                Decommission();
            }
        }

        public void MoveAutoTarget()
        {
            DecommissionIfTargetDown();

            if (_projectileInstance == null 
                || Math.Abs(BulletSpeed) <= 0 
                || _targetToPursue == null 
                || Math.Abs(_journeyDistance) <= 0.0f
                || _targetToPursue.AmIDestroyed())
                return;

            float projectileLifespan = Time.time - _timeStart;
            float coveredDistance = projectileLifespan * BulletSpeed;
            float interpolation = coveredDistance / _journeyDistance;

            _projectileInstance.transform.position = 
                Vector3.Lerp(_firePoint.transform.position, _targetToPursue.GetTransform().position, interpolation);

            if (interpolation >= 1 || _timeToBeDestructedAfter < projectileLifespan)
            {
                _fireConnected = true;

                Decommission();
            }
                
        }

        public Vector3 FinalPosition = Vector3.zero;
        private bool _fireConnected;

        private void DecommissionIfExpired()
        {
            float projectileLifespan = Time.time - _timeStart;

            if (_timeToBeDestructedAfter < projectileLifespan)
                Decommission();
        }

        private void DecommissionWithEnemy(IEnemy activeEnemy)
        {
            _targetToPursue = activeEnemy;
            _projectileInstance.transform.position = _targetToPursue.GetPosition();

            Decommission();
        }

        public void MoveInCone()
        {
            DecommissionIfExpired();

            if (_projectileInstance == null)
                return;

            if (FinalPosition == Vector3.zero)
                FinalPosition = _targetToPursue.GetPosition();

            Vector3 direction3d = FinalPosition - _firePoint.transform.position;

            if (_projectileInstance.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = _projectileInstance.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = (direction3d.normalized + new Vector3(Random.value * 0.5f - 0.25f, 0, Random.value * 0.5f - 0.25f)) * BulletSpeed;
            }

            Ray hitRay = new Ray(_projectileInstance.transform.position, _projectileInstance.transform.forward);

            if (Physics.Raycast(hitRay, out RaycastHit hitInfo, _projectilePreferences.ActivationDistance))
            {
                foreach (IEnemy activeEnemy in ActiveEnemies)
                {
                    if (activeEnemy.AmIDestroyed())
                        continue;

                    Collider enemyCollider = activeEnemy.GetTransform().GetComponent<Collider>();

                    if (enemyCollider == hitInfo.collider)
                    {
                        _fireConnected = true;

                        DecommissionWithEnemy(activeEnemy);

                        Debug.Log(activeEnemy.GetTransform().name);
                    }
                }

            }
        }

        public void MoveInSphere()
        {
            DecommissionIfExpired();

            if (_projectileInstance == null)
                return;

            _targetToPursue = null;

//            if (FinalPosition == Vector3.zero)
//                FinalPosition = _targetToPursue.GetPosition();

//            Vector3 direction3d = FinalPosition - _firePoint.transform.position;
//
//            if (_projectileInstance.GetComponent<Rigidbody>() == null)
//            {
//                Rigidbody rb = _projectileInstance.AddComponent<Rigidbody>();
//                rb.useGravity = false;
//                rb.velocity = (direction3d.normalized + new Vector3(Random.value * 0.5f - 0.25f, 0, Random.value * 0.5f - 0.25f)) * 0;
//            }

            Ray hitRay = new Ray(_projectileInstance.transform.position, _projectileInstance.transform.forward);
            RaycastHit[] hitInfoAll = Physics.SphereCastAll
                    (hitRay, _projectilePreferences.ActivationDistance / 2, _projectilePreferences.ActivationDistance / 2);
            _projectileInstance.transform.parent = _firePoint;
            if (hitInfoAll.Length > 0)
            {
                foreach (IEnemy activeEnemy in ActiveEnemies)
                {
                    if (activeEnemy.AmIDestroyed())
                        continue;

                    Collider enemyCollider = activeEnemy.GetTransform().GetComponent<Collider>();

                    foreach (RaycastHit hit in hitInfoAll)
                    {
//                        Debug.Log("thrhough collider hit: " + hit.collider.gameObject.name + " distance: " + Vector3.Distance(hit.point, _projectileInstance.transform.position));
                        
                        if (enemyCollider == hit.collider)
                        {
                            DecommissionWithEnemy(activeEnemy);

                            Debug.Log(activeEnemy.GetTransform().name);
                        }
                    }
                }

            }
        }

        public bool IsToDispose() => ToDispose;

        protected void Decommission()
        {
            if (_targetToPursue != null && !_targetToPursue.AmIDestroyed())
            {
                if (_targetToPursue is IDamageAddressee ida) 
                    ida.RegisterDamage(CarryingDamage, ArmorPiecing);
            }

            if (_projectilePreferences.ExplosionEffect && _fireConnected)
            {
                Transform explosionEffect = Object.Instantiate(_projectilePreferences.ExplosionEffect,
                    _projectileInstance.transform.position, _projectileInstance.transform.rotation);

                Object.Destroy(explosionEffect.gameObject, 1);
            }

            ToDispose = true;

            Object.Destroy(_projectileInstance.gameObject);
        }

        #endregion
    }
}