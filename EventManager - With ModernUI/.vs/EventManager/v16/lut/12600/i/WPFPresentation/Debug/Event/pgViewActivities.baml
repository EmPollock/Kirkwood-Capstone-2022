   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:WPFPresentation.EventWPFPresentation.Event  5      ,    &WPFPresentation.Event.pgViewActivities   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:WPFPresentation.Event     5      -   5          N? Title$  pgViewActivities??5	      .??5      ?  ? RowDefinitions 5   
   ?? 5      $??40 ?6   6*   ?? 5      $??60 ?6   6*   ?? 5      6   .+?5   
   ?? -   5         ??Name$  stckPanEventActivityFilter??5      $??0??69   $??Right=?6F   $??Center=?6b   $??	0 0 310 0q?6}   .+?5      ?? -   5         ??Name$ cboEventActivityFilter??5      $??32??67   $	??125??6D    ?? 
IsEditable .  ?? 
IsReadOnly .  ?? Text$ Activity Filter??6t   $??Center=?6?   .@?5      ?? -   5         ??Name$ cmbItemsChooseFilter??5      .??5      ?? -   5        $ stckFilterItems??5   !    ?? Orientation$ Vertical=?6:   .+?5      ?? $??Select a Filter??6$   ?? 5      	? 5      -   5        	 	?Name$	 radActivityDateFilter??5   &   $
??Date??6E   6T   	? 5      -   5        $	 radActivtyNameFilter??5   &   
 d? 	IsChecked$

 TrueY?6D   $
??Name??6U   6d   	? 5      -   5        $"	 radActivitySublocationFilter??5   &   $??Sublocation??6L   6b   	? 5      -	   5        $	 radAllActivities??5   &   $??All Activities??6_   5      5      5      5      ?? 5    
   -
   5         ??Name$ 	lblSearch??5       $??0??6#   $??Right=?60   $??Center=?6L   $??	0 0 262 0q?6g   .??6z   	Search:6?   (  "System.Windows.Controls.DatePicker  5!   
   -   5          Name$ datePickerActivityDate??5!      $??0??65   $	??200??6B   $??32??6N   $??Right=?6Z   $y?Hidden=?6v   $??0,0,58,0q?6?   ???	#FF4388CC
??????????	#FF4388CC6?   ?? 5"   
   -   5         ??Name$ 	txtSearch??5"      $??0??6%   $	??200??62   $??20??6>   $??Right=?6J   $??0,0,58,0q?6f   ???	#FF4388CC
??????????	#FF4388CC6?   ?? 5#   
   -   5         ??Name$ btnFind??5#      $??0??6"   $??50??6/   $??31??6:   $??Right=?6F   $??0,0,10,0q?6b   ???	#FF4388CC
???????.??6?   Find6?   ?? 5%   
   -   5        $ lblActivityEventName??5%      $??1??6.   $??Center=?6;   $??Center=?6X   $??24(?6s   .??6?   Event Activities6?   ?? 5&   
   -   5        $ btnAddActivity??5&      $??1??6)   $??10q?66   $??Right=?6B   .??6{   Add Activity6?   &   System.Windows.Controls.DataGrid  5'   
   -   5          Name$ datEventActivities??5'      $??2??6/      SelectionMode$ Single=?6<      AutoGenerateColumns .     
IsReadOnly .    Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ Activity??6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   ?? *ActivityName+6Z     5*      $ Description??6%    6:   ?? *ActivityDescription+6d     5+      $
 Date??6%    63   ?? *EventDateID+ ?? StringFormat$ {0:MMMM dd, yyyy}??6x     5,      $ 
Start Time??6%    69   ?? *	StartTime+$ {0:hh:mm tt}??6v     5-      $ End Time??6%    67   ?? *	EndTime+$ {0:hh:mm tt}??6r     5.      $ Location??6%    67   ?? *SublocationName+6]   4  .System.Windows.Controls.DataGridCheckBoxColumn  5/      $ Public??6)    69   ?? *PublicActivity+6^   51      52      53      