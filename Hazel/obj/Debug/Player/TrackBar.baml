   M S B A M L   `   `   `  ����@  <Hazel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Hazel.PlayerHazel.Player  5          Hazel.Player.TrackBar   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Hazel.Player     5      .��5	      � -   5        $��Center=�5	      ���	#00000000  � RowDefinitions  5   
   �� 5      $��30 �6   �� 5      $��1* �6   .+�5   
   �� -   5         ��Name$ canvas��5      $��0��5      $��Center=�5      .+�5      �� 	 �� X1$ 20��6   	 �� Y1$ 0��6   	 �� X2$	 480��5      	 �� Y2$ 0��6   $��2��5      ���#DDDDDD5      �� 5      -   5         ��Name$ 
lineBorder��5      $ 19��5      $ 0��6   $ 20��5      $ 0��6   ���#DDDDDD$��4��5      �� 5      -   5        $
 line��5      $ 20��5      $ 0��6   $ 20��5       $ 0��6   
�������$��2��5"      A� 5#      -   5         A�Name$	 pin��5#       �� Left$ 15��5$      
	 �� Top$
	 -7.5��5%      
�������$��15��5'      $��15��5(      $��2��5)      ���#DDDDDD5-      5.      � 5/   
   $��1��6   
 � ColumnDefinitions
 50      �� 51      $��20 �6#   �� 52      $
��auto �6#   �� 53      $��1* �6#   �� 54      $
��auto �6#   �� 55      $��20 �6#   .+�57      �� -   5         ��Name$ currentTimeTextBlock��57      
�������$��1��58   +   �� 59      -   5        $ totalTimeTextBlock��59      
�������$��3��5:   +   5;      5<      5=      