using System;
using System.Collections.Generic;
using System.Text;

namespace YLW_WebClient.Painter.Controller
{
    /// <summary>
    /// 옵저버를 구성하고 관리하는 인터페이스
    /// </summary>
    interface IObservable
    {
         /// <summary>
         /// 구독자들로부터 알려야 할 내용이 있을 때 호출되는 함수
         /// </summary>
         /// <param name="action">Invalidated, Command, New 등</param>
         void Notify(ObserverAction action);
       
         /// <summary>    
         /// 구독자들로부터 알려야 할 내용이 있을 때 호출되는 함수  
         /// </summary>
         /// <param name="obserber">ObserverClass 로 ObserverName 과 ObserverAction 을 속성으로 가진다.</param>
         void Notify(ObserverClass observer);

         /// <summary>
         /// 구독자들을 등록하는 함수
         /// </summary>
         /// <param name="observer">Iobserver 를 상속 받은 클래스를 구독자로 등록한다.</param>
         IDisposable Subscribe(IObserver observer);
        
         /// <summary>
         /// 구독자들을 해제하는 함수
         /// </summary>
         /// <param name="observer">Iobserver 를 상속 받은 클래스를 구독 해제한다.</param>
         void Unsubscribe(IObserver observer);
    }
}
