   M S B A M L   `   `   `  ????J  FWPFPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:WPFPresentation.EventWPFPresentation.Event  5      *    $WPFPresentation.Event.pgTaskListView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:WPFPresentation.Event     5        N? Title$  pgTaskListView??5	      .??5      ? -   5         ? RowDefinitions 5   
   ?? 5      $	??130 ?6   ?? 5      $	??150 ?6   ?? 5      $	??100??6   ?? 5       ? ColumnDefinitions 5   
   ?? 5      ?? 5      ?? 5      $	??200 ?6   .+?5   
   ?? -   5         ??Name$ lblEventName??5      $??0??6*   $??0??67   $??2??6G   $??Center=?6[   $??Center=?5         TitleTextBlockStyle#??[  6/   
Event Name6h   ? 5   
   $??1??6   $??0??6   $??2??6,   .+?5      &   System.Windows.Controls.DataGrid  -   5          Name$ datViewAllTasksForEvent??5         SelectionMode$ Single=?68      
IsReadOnly .    GridLinesVisibility$ 
Horizontal=?5         AutoGenerateColumns .  	   RowStyle	 5      ?? 5       
 ?? 
TargetType)  #System.Windows.Controls.DataGridRow"
  6    ?? Triggers 5!      ?? 5"       ?? Binding 6*   ?? *TaskPriority+ ?? Value High6K   .a?5#   "   ??  ?? Property ? ?? ?? Value
 ?  ??6M   5$      ?? 5%       6*   ?? *TaskPriority+
 Medium6K   .a?5&   "   ??  ? ??
 ? ???6P   5'      ?? 5(       6*   ?? *TaskPriority+ Low6K   .a?5)   "   ??  ? ??
 ??  ?6N   5*      5,         Columns 5.      0  *System.Windows.Controls.DataGridTextColumn  5/      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Task Name??6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   ?? *Name+  50         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6)   $ Description??63    6H   ?? *Description+  51      $ Due By??6)    69   ?? *FormatedDueDate+   ElementStyle 52      ?? 53      "
 ??6$   .?54   "   ??  ? ??$ Center=?6F    55   "   P? 56   &    P? Property ? ?? P? Value$ No due date selected??6>   . ?57   *   ??  ? ?? ?#FFCCCB6G   ?? 58   *    ? ??$ Center=?6N   5:   '   5=      5?        5@      $
 auto 6)   $ TaskPriority??66    6L   ?? *TaskPriority+5D      ?? 5E      -   5         ??Name$ lblEmptyTaskList??5E      $??20(?6.   $??Center=?6<   $??Center=?6Y   $y?Hidden=?6t   .??6?   Event has no tasks6?   5G      ?? 5I   
   $??2??6   $??0??6!   $??2??61   $??Center=?6E   ?? 5J   
   -   5        $ lblTaskListCreate??5J      $??2??6/   $??0??6<   $
??Left=?6L   $??Bottom=?6g   $??	30 0 0 65q?5K        BodyTextBlockStyle#??[ 6,   	Add Task:6c   ?? 5L   
   -   5         ??Name$ btnTaskListCreate??5L      $??2??6,   $??0??69   $
??Left=?6I   $??Bottom=?6d   $??
100 0 0 60q?5M      
???   ?
???????.??6l   +6o   ? 5O   
   $??3??6   $??0??6   $??2??6,    5P      ?? 5Q      $??50 ?6    6.   ?? 5R      6"   ?? 5S      $??20??6    $??30??6/   6@   .+?5U      ?? -   5        $ lblVolunteers??5U      $??20(?6+   $??Center=?6:   .??6W   20Click on a task to view a list of its volunteers6?     5V      -   5        $ datTaskVolunteers??5V      $??1??62    .   .    CanUserAddRows .   5W        5X      $
 Name??6)    67   ?? *Name+6R     5Y      $ Date Assigned??6)    6@   ?? *DateAssigned+ ?? StringFormat$ {0:MM/dd/yy}??6?     5Z      $
 Role??6)    67   ?? *RoleID+6T   5\      5]      5^      5_      