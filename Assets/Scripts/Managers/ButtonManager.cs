// using System;
// using Commands;
// using Controllers;
// using Interfaces;
// using UnityEngine;
//
// namespace Managers
// {
//     public class ButtonManager : MonoBehaviour
//     {
//         [Header("ButtonSettings")]
//         [SerializeField] private bool isReusable;
//         [Header("Time Settings")]
//         [SerializeField] private bool hasInitialTime;
//         [SerializeField] private float PayOffset;
//         [SerializeField] private float InitialOffset;
//         [Header("References")]
//         [SerializeField] private ButtonPhysicsController buttonPhysicsController;
//         public ButtonMeshController buttonMeshController;
//         IBuyable iBuyable;
//         InitialTimer InitialTimer = new InitialTimer();
//         CheckPayOffset CheckPayOffset = new CheckPayOffset();
//         CheckCanBuy  CheckCanBuy= new CheckCanBuy();
//         CheckCanIncreaseAmount CheckCanIncreaseAmount = new CheckCanIncreaseAmount();
//         public Purchase Purchase;
//         
//         private void Awake()
//         {
//             iBuyable=gameObject.GetComponentInParent<IBuyable>();
//             Purchase = new Purchase(iBuyable.GetRequiredAmount(),0/*Saveden Gelecek*/,PayOffset,InitialOffset,buttonMeshController, iBuyable);
//             
//             
//             InitialTimer.SetSuccesor(CheckPayOffset);
//             CheckPayOffset.SetSuccesor(CheckCanIncreaseAmount);
//             CheckCanIncreaseAmount.SetSuccesor(CheckCanBuy);
//             
//            
//             //var purchase = new Purchase();
//         }
//         public void StartButtonEvent()
//         {
//             if (hasInitialTime&&!Purchase.alreadyBuyed)
//             {
//                 
//                 InitialTimer.ProcessRequest(Purchase);
//             }
//             else if(!hasInitialTime&&!Purchase.alreadyBuyed)
//             {
//                 CheckPayOffset.ProcessRequest(Purchase);
//             }
//         }
//     }
//
//     abstract class ButtonHandler
//     {
//         protected ButtonHandler successor;
//
//         public void SetSuccesor(ButtonHandler successor)
//         {
//             this.successor = successor;
//         }
//
//         public abstract void ProcessRequest(Purchase purchase);
//     }
//
//     public class Purchase
//     {
//         public int Number;
//         public bool alreadyBuyed;
//         public float InitialTimer;
//         public float OffsetTimer;
//         public double RequiredAmount;
//         public int PayedAmount;
//         public float InitialTimeOffset;
//         public float TimeOffset;
//         public ButtonMeshController ButtonMeshController;
//         public IBuyable iBuyable;
//
//         // Constructor
//         public Purchase( int requiredAmount, int payedAmount, float timeOffset, float ınitialTimeOffset,ButtonMeshController buttonMeshController,IBuyable iBuyable)
//         {
//             ButtonMeshController = buttonMeshController;
//             this.RequiredAmount = requiredAmount;
//             this.PayedAmount = payedAmount;
//             this.InitialTimeOffset = ınitialTimeOffset;
//             this.TimeOffset = timeOffset;//factory pattern Composite pattern
//         }
//
//         public void DecreaseAmount()
//         {
//
//         }
//
//         public int GetAmount()
//         {
//             return 10;
//         }
//
//         public void IncreaseAmount()
//         {
//
//         }
//
//
//     }
//
//     class InitialTimer : ButtonHandler
//     {
//         public override void ProcessRequest(Purchase purchase)
//         {
//             if (purchase.InitialTimer > purchase.InitialTimeOffset)
//             {
//                 successor.ProcessRequest(purchase);
//             }
//             else
//             {
//                 purchase.ButtonMeshController.RadialProgress(purchase.InitialTimer,purchase.InitialTimeOffset);
//                 purchase.InitialTimer += Time.deltaTime;
//             }
//         }
//     }
//
//     class CheckPayOffset : ButtonHandler
//     {
//         public override void ProcessRequest(Purchase purchase)
//         {
//             if (purchase.OffsetTimer > purchase.TimeOffset)
//             {
//                 successor.ProcessRequest(purchase);
//                 purchase.OffsetTimer = 0;
//                 
//             }
//             else
//             {
//                 purchase.OffsetTimer += Time.deltaTime;
//             }
//         }
//     }
//
//     class CheckCanIncreaseAmount : ButtonHandler
//     {
//         public override void ProcessRequest(Purchase purchase)
//         {
//             if (purchase.GetAmount() > 0) //Amount invoke sinyali ile biryerlerden gelecek
//             {
//                 purchase.DecreaseAmount();
//                 purchase.PayedAmount++;
//                 successor.ProcessRequest(purchase);
//             }
//             else
//             {
//                 Debug.Log("not have enough money to increase");
//             }
//
//         }
//     }
//     class CheckCanBuy : ButtonHandler
//     {
//         public override void ProcessRequest(Purchase purchase)
//         {
//             if (purchase.PayedAmount >= purchase.RequiredAmount) //Amount invoke sinyali ile biryerlerden gelecek
//             {
//                 purchase.PayedAmount++;
//                 purchase.InitialTimer = 0;
//                 purchase.alreadyBuyed = true;
//                 purchase.iBuyable.TriggerBuyingEvent();
//                 //Olay Gerceklessin
//             }
//             else
//             {
//                 Debug.Log("not have enough money to buy");
//             }
//
//         }
//     }
// }