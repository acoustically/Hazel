﻿#pragma checksum "..\..\..\Player\PlayerPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "917B43F49B1AF66BA18D9F2BDDB655CDA0544653"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using Hazel.Player;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Hazel.Player {
    
    
    /// <summary>
    /// PlayerPage
    /// </summary>
    public partial class PlayerPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\Player\PlayerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border windowCloseImage;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Player\PlayerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Hazel.Player.Player Player;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Player\PlayerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Hazel.Player.PlayListPage playListPage;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Player\PlayerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Hazel.Player.SearchPage youtubeSearchPage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Hazel;component/player/playerpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Player\PlayerPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 16 "..\..\..\Player\PlayerPage.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TitleBarMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.windowCloseImage = ((System.Windows.Controls.Border)(target));
            
            #line 32 "..\..\..\Player\PlayerPage.xaml"
            this.windowCloseImage.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.WindowCloseImageMouseUp);
            
            #line default
            #line hidden
            
            #line 33 "..\..\..\Player\PlayerPage.xaml"
            this.windowCloseImage.MouseEnter += new System.Windows.Input.MouseEventHandler(this.WindowCloseImageMouseEnter);
            
            #line default
            #line hidden
            
            #line 34 "..\..\..\Player\PlayerPage.xaml"
            this.windowCloseImage.MouseLeave += new System.Windows.Input.MouseEventHandler(this.WindowCloseImageMouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Player = ((Hazel.Player.Player)(target));
            return;
            case 4:
            this.playListPage = ((Hazel.Player.PlayListPage)(target));
            return;
            case 5:
            this.youtubeSearchPage = ((Hazel.Player.SearchPage)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

