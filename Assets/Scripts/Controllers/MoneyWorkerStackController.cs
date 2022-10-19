using System.Collections.Generic;
using Data;
using Data.UnityObjects;
using Data.ValueObjects.GridData;
using Enum;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class MoneyWorkerStackController : StackBase
        {
            #region Self Variables

            #region Serialized Variables

            [SerializeField] private MoneyWorkerManager moneyWorkerManager;

            [SerializeField] private Transform GridParent;

            [SerializeField] private StackingSystem stackingSystem;

            [ShowIf("stackingSystem", StackingSystem.Static)] [SerializeField]
            private StackAreaType stackAreaType;


            [ShowIf("stackingSystem", StackingSystem.Static)] [SerializeField] [ReadOnly]
            private GridData stackAreaGridData;

            [ShowIf("stackingSystem", StackingSystem.Dynamic)] [SerializeField]
            private StackerType stackerType;


            [ShowIf("stackingSystem", StackingSystem.Dynamic)] [SerializeField] [ReadOnly]
            private GridData stackerGridData;

            #endregion

            #region Private Variables

            [ShowInInspector] private List<Vector3> gridPositionsData = new List<Vector3>();

            private Vector3 _gridPositions;

            private StackData _stackData;

            private GridData _gridData;

            #endregion

            #region Public Variables

            #endregion

            #endregion

            private void GetData()
            {
                if (stackingSystem == StackingSystem.Dynamic)
                    stackerGridData = GetStackerGridData();
                else
                    stackAreaGridData = GetAreaStackGridData();
            }

            private GridData GetStackerGridData()
            {
                return Resources.Load<CD_Stack>("Data/CD_Stack").StackDatas[(int)stackingSystem]
                    .DynamicGridDatas[(int)stackerType];
            }

            private GridData GetAreaStackGridData()
            {
                return Resources.Load<CD_Stack>("Data/CD_Stack").StackDatas[(int)stackingSystem]
                    .StaticGridDatas[(int)stackAreaType];
            }

            private void Awake()
            {
                GetData();
                SetGrid();
                SendGridDataToStacker();
            }

            public void SetStackHolder(GameObject gameObject)
            {
                gameObject.transform.SetParent(transform);
            }


            public void SetGrid()
            {
                if (stackingSystem == StackingSystem.Static)
                    _gridData = stackAreaGridData;
                else
                    _gridData = stackerGridData;

                var gridCount = _gridData.GridSize.x * _gridData.GridSize.y * _gridData.GridSize.z;
                for (int i = 0; i < gridCount; i++)
                {
                    var modX = (int)(i % _gridData.GridSize.x);
                    var divideZ = (int)(i / _gridData.GridSize.x);
                    var modZ = (int)(divideZ % _gridData.GridSize.z);
                    var divideXZ = (int)(i / (_gridData.GridSize.x * _gridData.GridSize.z));

                    if (_gridData.isDynamic)
                        _gridPositions = new Vector3(modX * _gridData.Offset.x,
                            modZ * _gridData.Offset.z, divideXZ * _gridData.Offset.y);
                    else
                    {
                        var position = GridParent.transform.position;
                        _gridPositions = new Vector3(modX * _gridData.Offset.x + position.x,
                            divideXZ * _gridData.Offset.y + position.y,
                            modZ * _gridData.Offset.z + position.z);
                    }


                    gridPositionsData.Add(_gridPositions);

                }
            }

            public void SendGridDataToStacker()
            {
                moneyWorkerManager.GetStackPositions(gridPositionsData);
            }

        }
    }
