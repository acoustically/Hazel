﻿#pragma checksum "..\..\..\Player\TrackBar.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8951165D23C4C8C16DC24FCE6E2C636FAD04A160"
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
    /// TrackBar
    /// </summary>
    public partial class TrackBar : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Player\TrackBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Player\TrackBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Line lineBorder;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Player\TrackBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Line line;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Player\TrackBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse pin;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Player\TrackBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock currentTimeTextBlock;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Player\TrackBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock totalTimeTextBlock;
        
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
            System.Uri resourceLocater = new System.Uri("/Hazel;component/player/trackbar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Player\TrackBar.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 11 "..\..\..\Player\TrackBar.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LineMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.canvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.lineBorder = ((System.Windows.Shapes.Line)(target));
            return;
            case 4:
            this.line = ((System.Windows.Shapes.Line)(target));
            return;
            case 5:
            this.pin = ((System.Windows.Shapes.Ellipse)(target));
            
            #line 41 "..\..\..\Player\TrackBar.xaml"
            this.pin.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PinMouseDown);
            
            #line default
            #line hidden
            
            #line 42 "..\..\..\Player\TrackBar.xaml"
            this.pin.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.PinMouseUp);
            
            #line default
            #line hidden
            
            #line 43 "..\..\..\Player\TrackBar.xaml"
            this.pin.MouseMove += new System.Windows.Input.MouseEventHandler(this.PinMouseMove);
            
            #line default
            #line hidden
            return;
            case 6:
            this.currentTimeTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.totalTimeTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
