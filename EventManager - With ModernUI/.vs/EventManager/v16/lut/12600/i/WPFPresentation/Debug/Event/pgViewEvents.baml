   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFPresentationWPFPresentation  5      "    WPFPresentation.pgViewEvents   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WPFPresentation     5      -   5          N? Title$  pgViewEvents??5	      .??5      ?  ? RowDefinitions 5   
   ?? 5      $??60 ?6   ?? 5      ?? 5      $??50 ?6   ?? 5      $??50 ?6    ? ColumnDefinitions 5   
   ?? 5      $
??.05* ?6   ?? 5      ?? 5      $
??.05* ?6   .+?5   
   ?? -   5         ??Name$ txtBlkMyEvents??5      $
??Bold"?6,   $??0??6>   $??1??6K   $??2??6[   $??10q?6o   $
??Left=?6{   $	??Top=?6?      TitleTextBlockStyle#??[  5      Events5      ?? 5    
    ?? Orientation$ 
Horizontal=?6   $??0??6.   $??1??6;   $??2??6K   $??Right=?6_   $??Center=?6{   .+?5!      ?? -   5         ??Name$ cmbSearchEvents??5!      $??10q?60    ?? Text$ Event Filter??6=    ?? 
IsEditable .  ?? 
IsReadOnly . $??5q?6u   .@?5"      ?? -   5        	 ??Name$#	 cmbItemsChooseAllOrUserEvents??5"      .??5#      ?? $ Vertical=?6!   .+?5$      ?? $??All Events or My Events??6$   	? 5%      -   5        
 	?Name$
 radAllEvents??5%   &   .??6]   
All Events6i   	? 5&      -   5        $
 radUserEvents??5&   &    d? 	IsChecked$
 TrueY?6=   .??6n   	My Events6y   5'      5(      ?? 5)      -   5         ??Name$ sepEventFilter??5)      ?? 5*      .??5+      ?? $ Vertical=?6!   .+?5,      ?? $??Dates??6$   	? 5-      -   5        $
 radUpcomingEvents??5-   &   $
 TrueY?6A   .??6s   Upcoming Events6?   	? 5.      -	   5        $
 radPastEvents??5.   &   
 ? Tag$
 past??6=   .??6h   Past Events6u   	? 5/      -
   5        $
 radPastAndUpcomingEvents??5/   &   .??6h   Past and Upcoming Events6?   50      51      52      ?? 54      -   5         ??Name$ lblEventSearch??54      $??0??6,   $??
10,10,0,10q?6<   $??Right=?6P   $??Center=?6l   .??6?   	Search:6?   ?? 55      -   5         ??Name$ txtBoxEventSearch??55      $??0??61   $??1??6>   $??10q?6N   $??Center=?6Z   $	??150??6u   6?   56      &   System.Windows.Controls.DataGrid  5C   
   -   5          Name$ datActiveEvents??5C      $??1??6,   $??1??69      SelectionMode$ Single=?6I      
IsReadOnly .    AutoGenerateColumns .  $??Center=?5D   }   ??6   ??    Columns 5F      0  *System.Windows.Controls.DataGridTextColumn  5G      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Event Name??6&   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   ?? *	EventName+  5H         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%   $ Description??6/    6D   ?? *EventDescription+  5I      $ Event Dates??6&    6;   ?? *FormatedDate+   ElementStyle 5J      ?? 5K       ?? 
TargetType" ??6    .?5L      ??  ?? Property ? ?? ?? Value$ Center=?6B    ?? Triggers 5M      P? 5N   "    P? Property ? ?? P? Value$ No dates selected yet.??6:   . ?5O   &   ??  ? ?? ?#FFCCCB6C   ?? 5P   &    ? ??$ Center=?6J   ?? 5Q   &    ? ??$ 0,0,10,0q?6?   5R   #   5T      5V        5W      $ Location??6%    67   ?? *FormatedLocation+ 5X      ?? 5Y      " ??6    .?5Z      ??  ? ??$ Center=?6B    5[      P? 5\   "    ? ??$ No location selected yet.??6:   . ?5]   &   ??  ? ?? ?#FFCCCB6C   ?? 5^   &    ? ??$ 0,0,10,0q?6?   5_   #   5a      5c      5z      ?? 5|   
   -   5         ??Name$ btnCreateEvent??5|      $??Create Event??6)   $??2??6@   $??1??6M   6]   ?? 5}   
   -   5        $ btnCopyEvent??5}      $??
Copy Event??6'   $??2??6<   $??1??6I   $??	120 0 0 0q?6Y   $y?	Collapsed=?6?   5~      5      