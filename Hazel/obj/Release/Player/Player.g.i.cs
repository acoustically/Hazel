﻿#pragma checksum "..\..\..\Player\Player.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "778FE60F551E338C72D39DE8B6A03CCC6EB0BB38"
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
    /// Player
    /// </summary>
    public partial class Player : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\Player\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PlayerThumbnail;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Player\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock titleTextBlock;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Player\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Hazel.Player.VolumeTrackBar volumeTrackBar;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Player\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Hazel.Player.TrackBar playTimeTrackBar;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Player\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image playOrStopImage;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Player\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image loopImage;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Player\Player.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image randomImage;
        
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
            System.Uri resourceLocater = new System.Uri("/Hazel;component/player/player.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Player\Player.xaml"
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
            
            #line 8 "..\..\..\Player\Player.xaml"
            ((Hazel.Player.Player)(target)).MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.PlayerMouseWheel);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PlayerThumbnail = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.titleTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.volumeTrackBar = ((Hazel.Player.VolumeTrackBar)(target));
            return;
            case 5:
            this.playTimeTrackBar = ((Hazel.Player.TrackBar)(target));
            return;
            case 6:
            this.playOrStopImage = ((System.Windows.Controls.Image)(target));
            
            #line 60 "..\..\..\Player\Player.xaml"
            this.playOrStopImage.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ImageMouseEnter);
            
            #line default
            #line hidden
            
            #line 61 "..\..\..\Player\Player.xaml"
            this.playOrStopImage.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ImageMouseLeave);
            
            #line default
            #line hidden
            
            #line 64 "..\..\..\Player\Player.xaml"
            this.playOrStopImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PlayOrStopImageMouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 66 "..\..\..\Player\Player.xaml"
            ((System.Windows.Controls.Image)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.ImageMouseEnter);
            
            #line default
            #line hidden
            
            #line 67 "..\..\..\Player\Player.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.ImageMouseLeave);
            
            #line default
            #line hidden
            
            #line 69 "..\..\..\Player\Player.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PlayNextImageMouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 71 "..\..\..\Player\Player.xaml"
            ((System.Windows.Controls.Image)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.ImageMouseEnter);
            
            #line default
            #line hidden
            
            #line 72 "..\..\..\Player\Player.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.ImageMouseLeave);
            
            #line default
            #line hidden
            
            #line 74 "..\..\..\Player\Player.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PlayBackImageMouseDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.loopImage = ((System.Windows.Controls.Image)(target));
            
            #line 77 "..\..\..\Player\Player.xaml"
            this.loopImage.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ImageMouseEnter);
            
            #line default
            #line hidden
            
            #line 78 "..\..\..\Player\Player.xaml"
            this.loopImage.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ImageMouseLeave);
            
            #line default
            #line hidden
            
            #line 80 "..\..\..\Player\Player.xaml"
            this.loopImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LoopImageMouseDown);
            
            #line default
            #line hidden
            return;
            case 10:
            this.randomImage = ((System.Windows.Controls.Image)(target));
            
            #line 82 "..\..\..\Player\Player.xaml"
            this.randomImage.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ImageMouseEnter);
            
            #line default
            #line hidden
            
            #line 83 "..\..\..\Player\Player.xaml"
            this.randomImage.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ImageMouseLeave);
            
            #line default
            #line hidden
            
            #line 86 "..\..\..\Player\Player.xaml"
            this.randomImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RandomImageMouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

