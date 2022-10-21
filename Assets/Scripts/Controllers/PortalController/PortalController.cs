using System.Collections.Generic;
using DG.Tweening;
using Enum;
using Interfaces;
using Signals;
using UnityEngine;

namespace DefaultNamespace
{
    public class PortalController : MonoBehaviour,IGetPoolObject
            {
                #region Serializable Variables
        
                [SerializeField]
                private List<MeshRenderer> portalMeshRenderers = new List<MeshRenderer>();
        
                [SerializeField]
                private Collider portalCollider;
        
                [SerializeField]
                private float dissolveOpenValue = 6;
                [SerializeField]
                private float dissolveCloseValue = 35;

                private IGetPoolObject _getPoolObjectImplementation;

                #endregion
        
                #region Private Variables
        
                private const float dissolveTime = 2f;
                private const string dissolveName = "_DissolveAmount";
        
                #endregion
        
                private void Awake()
                {
                    portalCollider.enabled = false;
                }
                public void OpenPortal()
                {
                    for (int i = 0; i < portalMeshRenderers.Count; i++)
                    {
                        portalMeshRenderers[i].material.DOFloat(dissolveOpenValue, dissolveName, dissolveTime);
                    }
                    DOVirtual.DelayedCall(dissolveTime, () =>
                    {
                        for (int i = 0; i <30; i++)
                        {
                            Debug.Log("Open portal");
                            GameObject obj=GetObjectType(PoolObjectType.Money);
                            obj.transform.position =transform.position-Vector3.forward;
                        }
                        ClosePortal();
                        //portalCollider.enabled = true;
                    });
                }
        
                public void ClosePortal()
                {
                    for (int i = 0; i < portalMeshRenderers.Count; i++)
                    {
                        portalMeshRenderers[i].material.SetFloat(dissolveName, dissolveCloseValue);
                    }
                    portalCollider.enabled = false;
                }

                public GameObject GetObjectType(PoolObjectType poolType)
                {
                   return PoolSignals.Instance.onGetObjectFromPool.Invoke(poolType);
                }
            } 
        }