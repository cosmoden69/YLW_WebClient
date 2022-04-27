using System;
using System.Collections.Generic;
using System.Text;

namespace YLW_WebClient.Painter.Controller
{
    /// <summary>
    /// IObserver 인터페이스 구독자들이 상속 받아야 하는 인터페이스
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 옵저버로부터 수신된 내용이 있다면  ObserverAction 에 따라 실행한다.
        /// </summary>
        /// <param name="action">Invalidate, Command, New 등</param>
        void OnNext(ObserverAction action);

        /// <summary>
        /// 옵저버로부터 수신된 내용이 있다면 ObserverName 과 ObserverAction 에 따라 실행한다.
        /// </summary>
        /// <param name="obserber">ObserverClass 로 ObserverName 과 ObserverAction 을 속성으로 가진다.</param>
        void OnNext(ObserverClass obserber);
    }
}
