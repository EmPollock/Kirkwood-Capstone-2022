   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFPresentationWPFPresentation  5      )    #WPFPresentation.pgViewAllVolunteers   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WPFPresentation     5      -   5          N? Title$  ViewAllVolunteers??5	       N? WindowTitle$ View Volunteers??6!   .??5
      ? .+?5   
   ?  ? RowDefinitions 5      ?? 5      $??50 ?6    ?? 5      $	??450 ?6    ?? 5      $??50 ?6     ? ColumnDefinitions 5      ?? 5      $	??200 ?6#   ?? 5      .+?5      ?? -   5         ??Name$ txtBlkVolunteerList??5      $
??Bold"?65   $??0??6G   $??1??6T   $??10q?6d   $
??Left=?6p   $	??Top=?6?      TitleTextBlockStyle#??[  6?   
Volunteers6?   ? 5      $??1??6   $??1??6    .+?5      &   System.Windows.Controls.DataGrid  -   5          Name$ datVolunteers??5         AutoGenerateColumns .     
IsReadOnly .    CanUserReorderColumns .  	   CanUserResizeColumns	 .  
   CanUserResizeRows
 .  $ ?? IsSynchronizedWithCurrentItem$
 TrueY?6?   $??Center=?6?      MinColumnWidth$	 100Q?6     SelectionMode$ Single=?6+  $??0q?6B   ? OverridesDefaultStyle . 
???   ???62   ??    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
First Name??6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   ?? *	GivenName+  5      $ 	Last Name??6-    6@   ?? *
FamilyName+  5      $ State??6-    6<   ?? *State+  5      $
 City??6-    6;   ?? *City+  5      $ Volunteer Type??6-    6E   ?? *VolunteerType+  5      $ Rating??6-    6=   ?? *Rating+5!      5#      ? 5%      $??1??6   $??0??6     5&      ?? 5'      $??30 ?6$   ?? 5(      ?? 5)      $??30 ?6$   ?? 5*      ?? 5+      $??30 ?6$   ?? 5,      ?? 5-      $
??30px ?6$   ?? 5.      ?? 5/      $
??30px ?6$   ?? 50      ?? 51      $
??30px ?6$   ?? 52      .+?58      ?? $??State??6   $??16(?6(   $??Center=?66   $??Bottom=?6S   ?? 59      $??1??6   $??Stretch=?6(   $??Stretch=?6F   $??Center=?6b   $
??10 5q?6?   ?? 5;      $??2??6   $
??City??6%   $??16(?64   $??Center=?6B   $??Bottom=?6_   ?? 5<      $??3??6   $??Stretch=?6'   $??Center=?6E   $
??10 5q?6g   ?? 5>      $??4??6   $??Rating??6%   $??16(?66   $??Center=?6D   $??Bottom=?6a   ?? 5?      $??5??6   $??Stretch=?6(   $??Stretch=?6F   $??Center=?6b   $
??10 5q?6?   ?? 5A      $??6??6   $??Availability??6%   $??16(?6<   $??Center=?6J   $??Bottom=?6g   ?? 5B      $??7??6   $??Stretch=?6(   $??Stretch=?6F   $??Center=?6b   $
??10 5q?6?   ?? 5D      $??8??6   $??Volunteer Type??6%   $??16(?6>   $??Center=?6L   $??Bottom=?6i   ?? 5E      $??9??6   $??Stretch=?6(   $??Stretch=?6F   $??Center=?6b   $
??10 5q?6?   ?? 5G      -   5         ??Name$ btnFilterSearch??5G      $??Search??62   $??11??6C   $??Stretch=?6Q   $??Stretch=?6o   $??3q?6?   ???	#FF4388CC
???????6?   5H      ? 5L      $??4??6   $??1??6     5M      ?? 5N      ?? 5O      5V      5X      5Y      5Z      