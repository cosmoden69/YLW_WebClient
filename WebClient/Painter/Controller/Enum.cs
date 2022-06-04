using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 사용자 정의 변수 모음 클래스
/// </summary>
namespace YLW_WebClient.Painter
{  
    /// <summary>
    /// 옵저버 패턴에서 사용하는 Action 
    /// </summary>    
    public enum ObserverAction
    {
        New,                 //새로 만들기
        FileLoad,             //불러오기
        FileSave,             //저장하기
        SaveAs,               //다른이름으로 저장
        Invalidate,           //다시 그리기
        Undo,                //취소
        Redo,                //다시
        ChangeCreator,        //도구변경
        ChangeProperty,       //그림속성변경
        ChangeFontStyle       //폰트속성변경
    }

    /// <summary>
    /// IObserver 를 상속 받아서 구현하는 구독자의 이름
    /// </summary>   
    public enum ObserverName
    {
        MainView,   //메인화면
        MySheet,    //그리기 상자
        ToolBar     //상단 툴바
    }

    public enum ObjectCreatorType
    {
        None = 0,
        Selector,
        Pencil,
        Eraser,
        Fill,
        Text,
        Line,
        Box,
        Circle,
        Arrow,
        BrokenLine,
        Star,
        Image,
        CreateTypeCount
    }

    public enum PenStyle
    {
        None = 0,
        Line = 1,
        Dash = 2
    }
    public enum FillStyle
    {
        None = 0,
        Fill = 1
    }
}
