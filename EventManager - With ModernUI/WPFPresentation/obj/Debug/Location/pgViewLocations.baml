   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WPFPresentation.LocationWPFPresentation.Location  5      .    (WPFPresentation.Location.pgViewLocations   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:WPFPresentation.Location     5      -   5          N? Title$  pgViewLocations??5	      .??5      ?  ? RowDefinitions 5   
   ?? 5      $??50 ?6   ?? 5       ? ColumnDefinitions 5   
   ?? 5      $	??200 ?6   ?? 5      .+?5   
   ?? -   5         ??Name$ txtLocationList??5      $
??Bold"?6-   $??0??6?   $??1??6L   $??10q?6\   $
??Left=?5      $	??Top=?6/      TitleTextBlockStyle#??[  5      	Locations5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datLocationsList??5         SelectionMode$ Single=?6-      
IsReadOnly .    AutoGenerateColumns .  $
??Left=?5   +   $??Center=?6F      MinColumnWidth$	 100Q?5      	   CanUserResizeColumns	 .  $??1??5      $??1??6    ??5      ?? 
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name??6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   ?? *Name+  5      $ Description??6%    6:   ?? *Description+  5       $ Address??6%    66   ?? *
Address1+  5!      $
 City??6%    63   ?? *City+  5"      $ State??6%    64   ?? *State+5$      5%      5&      