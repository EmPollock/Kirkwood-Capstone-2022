   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:WPFPresentation.EventWPFPresentation.Event  5      5    /WPFPresentation.Event.pgViewEventDateActivities   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:WPFPresentation.Event     5      -   5          N? Title$  pgViewEventDateActivities??5	       N? 
Background
 ?????.??5      ?  ? RowDefinitions 5   
   ?? 5      $??40 ?6   6*   ?? 5      $??60 ?6   6*   ?? 5      6    ? ColumnDefinitions 5   
   ?? 5      $	??250 ?6   6-   ?? 5      6!   .+?5   
   ?? -   5         ??Name$ 	txtSearch??5      $??0??6%   $??1??62   $	??200??6B   $??20??6N   $??Right=?6Z   $??0,0,58,0q?6v   ???	#FF4388CC
??????????	#FF4388CC.??6?   Search6?   ?? 5   
   -   5         ??Name$ btnFind??5      $??0??6"   $??1??6/   $??50??6?   $??31??6J   $??Right=?6V   $??0,0,10,0q?6r   ???	#FF4388CC
???????.??6?   Find6?   ?? 5   
   -   5         ??Name$ lblActivityEventName??5      $??1??6.   $??1??6;   $??Center=?6K   $??Center=?6h   $??24(?6?   .??6?   Event Activities6?   ?? 5   
   -   5        $ 
btnAddEdit??5      $??1??6%   $??1??62   $??Right=?6B   $??0 0 10 0q?6^   ???	#FF4388CC
???????.??6?   
Add/Edit6?   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datEventDateActivities??5      $??2??63   $??1??6@      SelectionMode$ Single=?6P   	   AutoGenerateColumns	 .  
   CanUserAddRows
 .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Start Time??6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   ?? *	StartTime+ ?? StringFormat$ {0:H:mm tt}??6u     5      $ End Time??6%    67   ?? *	EndTime+$ {0:H:mm tt}??6q     5      $ Activity??6%    67   ?? *ActivityName+6Z     5      $ Description??6%    6:   ?? *ActivityDescription+6d   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Public??6)    69   ?? *PublicActivity+6^     5       $ Location??6%    67   ?? *%#ActivitySublocation.SublocationName+6q   5"      ?? 5#   
   -   5        $ lblNoActivities??5#      $??2??6)   $??1??66   $??Center=?6F   $??0 50 0 0q?6c   $??40??6u   $	??Top=?6?   6?   5$      5%      