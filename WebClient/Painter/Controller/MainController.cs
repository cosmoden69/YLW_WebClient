using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using YLW_WebClient.Painter.PaintControls;

namespace YLW_WebClient.Painter.Controller
{
    /// <summary>
    /// 메인 컨트롤 역활을 할 싱글톤 클래스
    /// 쓰레드에 안전한 싱글톤
    /// </summary>
    public sealed class MainController
    {
        #region 전역 변수

        private static readonly object padlock = new object();

        /// <summary>
        /// 싱글톤 패턴을 위한 인스턴스
        /// </summary>
        private static MainController instance = null;
        /// <summary>
        /// 인스턴스 반환
        /// </summary>
        public static MainController Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new MainController();
                        }
                    }
                }

                return instance;
            }
        }

        private ObjectCreatorType _currentCreator = ObjectCreatorType.None;
        public ObjectCreatorType CurrentCreator
        {
            get
            {
                return _currentCreator;
            }
            set
            {
                _currentCreator = value;
            }
        }

        /// <summary>
        /// 현재 ToolBar
        /// </summary>
        private ToolBar toolBar = null;
        public ToolBar ToolBar
        {
            get { return toolBar; }
            set { toolBar = value; }
        }

        /// <summary>
        /// 현재 Sheet
        /// </summary>
        private MySheet sheet = null;
        public MySheet Sheet
        {
            get { return sheet; }
            set { sheet = value; }
        }

        #endregion
    }
}
