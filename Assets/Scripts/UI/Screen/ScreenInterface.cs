﻿using System;


namespace Snake_box
{
    public sealed class ScreenInterface : IDisposable
    {
        #region Fields

        private BaseUi _currentWindow;
        private readonly ScreenFactory _screenFactory;
        private static ScreenInterface _instance;

        #endregion
        

        #region ClassLifeCycles

        private ScreenInterface()
        {
            _screenFactory = new ScreenFactory();
        }

        #endregion


        #region Properties

        public BaseUi CurrentWindow => _currentWindow;
        public ScreenFactory ScreenFactory => _screenFactory;

        #endregion


        #region Methods

        public static ScreenInterface GetInstance()
        {
            return _instance ?? (_instance = new ScreenInterface());
        }

        public void Execute(ScreenType screenType)
        {
            if (CurrentWindow != null)
            {
                CurrentWindow.Hide();
            }

            switch (screenType)
            {
                case ScreenType.GameMenu:
                    _currentWindow = _screenFactory.GetGameMenu();
                    break;
                case ScreenType.MainMenu:
                    _currentWindow = _screenFactory.GetMainMenu();
                    break;
                case ScreenType.TestMenu:
                    _currentWindow = _screenFactory.GetTestMenu();
                    break;

                default:
                    break;
            }

            CurrentWindow.Show();
        }

        public void AddObserver(ScreenType screenType, IListenerScreen listenerScreen)
        {
            switch (screenType)
            {
                case ScreenType.GameMenu:
                    _screenFactory.GetGameMenu().ShowUI += listenerScreen.ShowScreen;
                    _screenFactory.GetGameMenu().HideUI += listenerScreen.HideScreen;
                    _screenFactory.GetGameMenu().Hide();
                    break;
                case ScreenType.MainMenu:
                    _screenFactory.GetMainMenu().ShowUI += listenerScreen.ShowScreen;
                    _screenFactory.GetMainMenu().HideUI += listenerScreen.HideScreen;
                    _screenFactory.GetMainMenu().Hide();
                    break;
                case ScreenType.TestMenu:
                    _screenFactory.GetTestMenu().ShowUI += listenerScreen.ShowScreen;
                    _screenFactory.GetTestMenu().HideUI += listenerScreen.HideScreen;
                    _screenFactory.GetTestMenu().Hide();
                    break;

                default:
                    break;
            }
        }

        public void RemoveObserver(ScreenType screenType, IListenerScreen listenerScreen)
        {
            switch (screenType)
            {
                case ScreenType.GameMenu:
                    _screenFactory.GetGameMenu().ShowUI -= listenerScreen.ShowScreen;
                    _screenFactory.GetGameMenu().HideUI -= listenerScreen.HideScreen;
                    _screenFactory.GetGameMenu().Hide();
                    break;
                case ScreenType.MainMenu:
                    _screenFactory.GetMainMenu().ShowUI -= listenerScreen.ShowScreen;
                    _screenFactory.GetMainMenu().HideUI -= listenerScreen.HideScreen;
                    _screenFactory.GetMainMenu().Hide();
                    break;
                case ScreenType.TestMenu:
                    _screenFactory.GetTestMenu().ShowUI -= listenerScreen.ShowScreen;
                    _screenFactory.GetTestMenu().HideUI -= listenerScreen.HideScreen;
                    _screenFactory.GetTestMenu().Hide();
                    break;

                default:
                    break;
            }
        }

        #endregion


        #region IDisposable

        public void Dispose()
        {
            _instance = null;
        }

        #endregion
    }
}
